using LabManagementSystem.Enums;
using LabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Services
{
    public class VisitService : IVisitService
    {
        private readonly Func<LabContext> _contextFactory;

        public VisitService(Func<LabContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<VisitSummaryDto> SearchVisits(VisitSearchFilter filter)
        {
            using var db = _contextFactory();
            var query = BuildSearchVisitsQuery(db, filter);
            return ProjectVisitSummaries(query).ToList();
        }

        public async Task<List<VisitSummaryDto>> SearchVisitsAsync(VisitSearchFilter filter, int maxResults = 250, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            var query = BuildSearchVisitsQuery(db, filter);
            return await ProjectVisitSummaries(query).Take(maxResults).ToListAsync(ct);
        }

        private static IQueryable<Visit> BuildSearchVisitsQuery(LabContext db, VisitSearchFilter filter)
        {
            var query = db.Visits.AsNoTracking().AsQueryable();

            if (!filter.IncludeDeleted)
                query = query.Where(v => !v.IsDeleted);

            if (!string.IsNullOrWhiteSpace(filter.PatientCode))
            {
                string code = filter.PatientCode.Trim();
                query = query.Where(v => v.Patient.MedicalCode.Contains(code));
            }

            if (!string.IsNullOrWhiteSpace(filter.PatientName))
            {
                string name = filter.PatientName.Trim();
                query = query.Where(v => v.Patient.Name.Contains(name));
            }

            if (filter.DateFrom.HasValue)
            {
                var from = filter.DateFrom.Value.Date;
                query = query.Where(v => v.VisitDate >= from);
            }

            if (filter.DateTo.HasValue)
            {
                var toExclusive = filter.DateTo.Value.Date.AddDays(1);
                query = query.Where(v => v.VisitDate < toExclusive);
            }

            return query.OrderByDescending(v => v.VisitDate).ThenByDescending(v => v.Id);
        }

        private static IQueryable<VisitSummaryDto> ProjectVisitSummaries(IQueryable<Visit> query)
        {
            return query.Select(v => new VisitSummaryDto
            {
                Id = v.Id,
                PatientName = v.Patient.Name,
                MedicalCode = v.Patient.MedicalCode,
                VisitDate = v.VisitDate,
                LabsCount = v.VisitLabs.Count,
                TotalPrice = v.TotalPrice,
                DiscountValue = v.DiscountValue,
                DiscountPercent = v.DiscountPercent,
                NetPrice = v.NetPrice,
                PaidAmount = v.Payments.Sum(p => (decimal?)p.Amount) ?? 0m,
                RemainingAmount = v.NetPrice - (v.Payments.Sum(p => (decimal?)p.Amount) ?? 0m),
                IsDeleted = v.IsDeleted
            });
        }

        public Visit? GetVisitById(int id)
        {
            using var db = _contextFactory();
            return db.Visits
                .Include(v => v.Patient)
                    .ThenInclude(p => p.Gender)
                .Include(v => v.Payments)
                .Include(v => v.VisitLabs)
                    .ThenInclude(vl => vl.Lab)
                        .ThenInclude(l => l.LabCategory)
                .Include(v => v.VisitLabs)
                    .ThenInclude(vl => vl.VisitLabResults)
                        .ThenInclude(r => r.LabParameter)
                .Include(v => v.VisitLabs)
                    .ThenInclude(vl => vl.ExternalLabs)
                .Include(v => v.VisitLabs)
                    .ThenInclude(vl => vl.VisitLabStatus)
                .FirstOrDefault(v => v.Id == id);
        }

        public async Task<Visit?> GetVisitByIdAsync(int id, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            return await db.Visits
                .Include(v => v.Patient)
                    .ThenInclude(p => p.Gender)
                .Include(v => v.Payments)
                .Include(v => v.VisitLabs)
                    .ThenInclude(vl => vl.Lab)
                        .ThenInclude(l => l.LabCategory)
                .Include(v => v.VisitLabs)
                    .ThenInclude(vl => vl.VisitLabResults)
                        .ThenInclude(r => r.LabParameter)
                .Include(v => v.VisitLabs)
                    .ThenInclude(vl => vl.ExternalLabs)
                .Include(v => v.VisitLabs)
                    .ThenInclude(vl => vl.VisitLabStatus)
                .FirstOrDefaultAsync(v => v.Id == id, ct);
        }

        public int CreateVisit(Visit visit, List<int> labIds, decimal initialPayment, int paymentMethod, string? paymentNotes)
        {
            return CreateVisitInternal(visit, labIds, initialPayment, paymentMethod, paymentNotes);
        }

        public Task<int> CreateVisitAsync(Visit visit, List<int> labIds, decimal initialPayment, int paymentMethod, string? paymentNotes, CancellationToken ct = default)
        {
            return Task.FromResult(CreateVisitInternal(visit, labIds, initialPayment, paymentMethod, paymentNotes));
        }

        private int CreateVisitInternal(Visit visit, List<int> labIds, decimal initialPayment, int paymentMethod, string? paymentNotes)
        {
            if (visit.PatientId <= 0)
                throw new InvalidOperationException("يجب اختيار مريض مسجل أولاً.");

            if (labIds == null || labIds.Count == 0)
                throw new InvalidOperationException("يجب إضافة تحليل واحد على الأقل للزيارة.");

            using var db = _contextFactory();
            using var tx = db.Database.BeginTransaction();
            try
            {
                var labs = db.Labs.Where(l => labIds.Contains(l.Id)).ToList();
                decimal total = labs.Sum(l => l.Price);
                visit.TotalPrice = total;

                // Calculate Net Price
                decimal discountVal = visit.DiscountValue ?? 0;
                if ((visit.DiscountPercent ?? 0) > 0)
                {
                    discountVal = Math.Round(total * (visit.DiscountPercent!.Value / 100m), 2);
                    visit.DiscountValue = discountVal;
                }
                visit.NetPrice = Math.Max(0, total - discountVal);
                if (visit.VisitDate == default)
                    visit.VisitDate = DateTime.Now;
                visit.CreateDate = DateTime.Now;

                db.Visits.Add(visit);
                db.SaveChanges();

                foreach (var lab in labs)
                {
                    var vl = new VisitLab
                    {
                        VisitId = visit.Id,
                        LabId = lab.Id,
                        LabPrice = lab.Price,
                        IsExternal = lab.IsExternal,
                        VisitLabStatusId = (int)VisitLabStatusEnum.Pending
                    };
                    db.VisitLabs.Add(vl);
                }
                db.SaveChanges();

                if (initialPayment > 0)
                {
                    var payment = new Payment
                    {
                        VisitId = visit.Id,
                        Amount = initialPayment,
                        PaymentDate = visit.VisitDate,
                        PaymentMethod = paymentMethod > 0 ? paymentMethod : (int)PaymentMethodEnum.Cash,
                        Notes = string.IsNullOrWhiteSpace(paymentNotes) ? "دفعة أولى عند تسجيل الزيارة" : paymentNotes.Trim()
                    };
                    db.Payments.Add(payment);
                    db.SaveChanges();
                }

                tx.Commit();
                return visit.Id;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        public void UpdateVisit(Visit visit, List<int> currentLabIds)
        {
            UpdateVisitInternal(visit, currentLabIds);
        }

        public Task UpdateVisitAsync(Visit visit, List<int> currentLabIds, CancellationToken ct = default)
        {
            UpdateVisitInternal(visit, currentLabIds);
            return Task.CompletedTask;
        }

        private void UpdateVisitInternal(Visit visit, List<int> currentLabIds)
        {
            using var db = _contextFactory();
            using var tx = db.Database.BeginTransaction();
            try
            {
                var existing = db.Visits
                    .Include(v => v.VisitLabs)
                        .ThenInclude(vl => vl.VisitLabResults)
                    .Include(v => v.VisitLabs)
                        .ThenInclude(vl => vl.ExternalLabs)
                    .FirstOrDefault(v => v.Id == visit.Id);

                if (existing == null)
                    throw new InvalidOperationException("الزيارة غير موجودة.");

                existing.DrName = visit.DrName;
                existing.Notes = visit.Notes;
                existing.VisitDate = visit.VisitDate;

                // Load selected labs
                var allSelectedLabs = db.Labs.Where(l => currentLabIds.Contains(l.Id)).ToList();
                decimal total = allSelectedLabs.Sum(l => l.Price);
                existing.TotalPrice = total;

                decimal discountVal = visit.DiscountValue ?? 0;
                if ((visit.DiscountPercent ?? 0) > 0)
                {
                    discountVal = Math.Round(total * (visit.DiscountPercent!.Value / 100m), 2);
                    existing.DiscountValue = discountVal;
                    existing.DiscountPercent = visit.DiscountPercent;
                }
                else
                {
                    existing.DiscountValue = visit.DiscountValue;
                    existing.DiscountPercent = visit.DiscountPercent;
                }
                existing.NetPrice = Math.Max(0, total - discountVal);

                // Reconcile VisitLabs
                var existingLabs = existing.VisitLabs.ToList();
                var existingLabIds = existingLabs.Select(vl => vl.LabId).ToHashSet();
                var targetLabIds = currentLabIds.ToHashSet();

                foreach (var lab in allSelectedLabs)
                {
                    if (!existingLabIds.Contains(lab.Id))
                    {
                        db.VisitLabs.Add(new VisitLab
                        {
                            VisitId = existing.Id,
                            LabId = lab.Id,
                            LabPrice = lab.Price,
                            IsExternal = lab.IsExternal,
                            VisitLabStatusId = (int)VisitLabStatusEnum.Pending
                        });
                    }
                }

                foreach (var oldVl in existingLabs)
                {
                    if (!targetLabIds.Contains(oldVl.LabId))
                    {
                        if (oldVl.VisitLabResults.Any() || oldVl.ExternalLabs.Any())
                        {
                            throw new InvalidOperationException($"لا يمكن حذف التحليل '{oldVl.Lab?.Name}' لأنه يحتوي على نتائج مسجلة أو محال لمعمل خارجي.");
                        }
                        db.VisitLabs.Remove(oldVl);
                    }
                }

                db.SaveChanges();
                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        public void DeleteVisit(int visitId)
        {
            using var db = _contextFactory();
            var visit = db.Visits.Find(visitId);
            if (visit == null) return;

            visit.IsDeleted = true;
            db.SaveChanges();
        }

        public async Task DeleteVisitAsync(int visitId, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            var visit = await db.Visits.FindAsync(new object[] { visitId }, ct);
            if (visit == null) return;

            visit.IsDeleted = true;
            await db.SaveChangesAsync(ct);
        }

        public List<Lab> GetAllLabs()
        {
            using var db = _contextFactory();
            return db.Labs
                .AsNoTracking()
                .Include(l => l.LabCategory)
                .OrderBy(l => l.Name)
                .ToList();
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync(CancellationToken ct = default)
        {
            using var db = _contextFactory();

            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);

            // 1. Today Visits Count
            int todayVisits = await db.Visits
                .AsNoTracking()
                .CountAsync(v => !v.IsDeleted && v.VisitDate >= today && v.VisitDate < tomorrow, ct);

            // 2. Today Registered Patients
            int todayPatients = await db.Patients
                .AsNoTracking()
                .CountAsync(p => p.CreateDate >= today && p.CreateDate < tomorrow, ct);

            // 3. Pending Labs
            int pendingLabs = await db.VisitLabs
                .AsNoTracking()
                .CountAsync(vl => vl.VisitLabStatusId == (int)VisitLabStatusEnum.Pending && !vl.Visit.IsDeleted, ct);

            // 4. Completed Labs
            int completedLabs = await db.VisitLabs
                .AsNoTracking()
                .CountAsync(vl => vl.VisitLabStatusId == (int)VisitLabStatusEnum.Completed && !vl.Visit.IsDeleted, ct);

            // 5. Today's Collected Payments
            decimal todayCollected = await db.Payments
                .AsNoTracking()
                .Where(p => p.PaymentDate >= today && p.PaymentDate < tomorrow && !p.Visit.IsDeleted)
                .SumAsync(p => (decimal?)p.Amount, ct) ?? 0m;

            // 6. Outstanding Debts Total
            var allActiveVisits = await db.Visits
                .AsNoTracking()
                .Where(v => !v.IsDeleted)
                .Select(v => new
                {
                    v.NetPrice,
                    Paid = v.Payments.Sum(p => (decimal?)p.Amount) ?? 0m
                })
                .Where(x => x.NetPrice > x.Paid)
                .ToListAsync(ct);

            decimal totalDebts = allActiveVisits.Sum(x => x.NetPrice - x.Paid);

            // 7. Recent 10 Visits
            var recentVisits = await db.Visits
                .AsNoTracking()
                .Where(v => !v.IsDeleted)
                .OrderByDescending(v => v.VisitDate)
                .ThenByDescending(v => v.Id)
                .Take(10)
                .Select(v => new VisitSummaryDto
                {
                    Id = v.Id,
                    PatientName = v.Patient.Name,
                    MedicalCode = v.Patient.MedicalCode,
                    VisitDate = v.VisitDate,
                    LabsCount = v.VisitLabs.Count,
                    TotalPrice = v.TotalPrice,
                    DiscountValue = v.DiscountValue,
                    DiscountPercent = v.DiscountPercent,
                    NetPrice = v.NetPrice,
                    PaidAmount = v.Payments.Sum(p => (decimal?)p.Amount) ?? 0m,
                    RemainingAmount = v.NetPrice - (v.Payments.Sum(p => (decimal?)p.Amount) ?? 0m),
                    IsDeleted = v.IsDeleted
                })
                .ToListAsync(ct);

            return new DashboardStatsDto
            {
                TodayVisitsCount = todayVisits,
                TodayPatientsCount = todayPatients,
                PendingLabsCount = pendingLabs,
                CompletedLabsCount = completedLabs,
                TodayCollectedPayments = todayCollected,
                TotalOutstandingDebts = totalDebts,
                RecentVisits = recentVisits
            };
        }
    }
}
