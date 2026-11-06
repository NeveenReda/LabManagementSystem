using LabManagementSystem.Enums;
using LabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Services
{
    public class BillingService : IBillingService
    {
        private readonly Func<LabContext> _contextFactory;

        public BillingService(Func<LabContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void RecordPayment(int visitId, decimal amount, DateTime paymentDate, int paymentMethod, string? notes)
        {
            RecordPaymentInternal(visitId, amount, paymentDate, paymentMethod, notes);
        }

        public Task RecordPaymentAsync(int visitId, decimal amount, DateTime paymentDate, int paymentMethod, string? notes, CancellationToken ct = default)
        {
            RecordPaymentInternal(visitId, amount, paymentDate, paymentMethod, notes);
            return Task.CompletedTask;
        }

        private void RecordPaymentInternal(int visitId, decimal amount, DateTime paymentDate, int paymentMethod, string? notes)
        {
            if (amount <= 0)
                throw new InvalidOperationException("قيمة الدفعة يجب أن تكون أكبر من الصفر.");

            using var db = _contextFactory();
            var visit = db.Visits
                .Include(v => v.Payments)
                .FirstOrDefault(v => v.Id == visitId && !v.IsDeleted);

            if (visit == null)
                throw new InvalidOperationException("الزيارة غير موجودة.");

            decimal currentPaid = visit.Payments.Sum(p => p.Amount);
            decimal remaining = visit.NetPrice - currentPaid;

            if (amount > remaining)
                throw new InvalidOperationException($"المبلغ المدفوع ({amount:0.00}) يتجاوز المبلغ المتبقي على الزيارة ({remaining:0.00}).");

            var payment = new Payment
            {
                VisitId = visitId,
                Amount = amount,
                PaymentDate = paymentDate == default ? DateTime.Now : paymentDate,
                PaymentMethod = paymentMethod > 0 ? paymentMethod : (int)PaymentMethodEnum.Cash,
                Notes = notes?.Trim()
            };

            db.Payments.Add(payment);
            db.SaveChanges();
        }

        public void CancelPayment(int paymentId)
        {
            using var db = _contextFactory();
            var payment = db.Payments.Find(paymentId);
            if (payment == null) return;

            db.Payments.Remove(payment);
            db.SaveChanges();
        }

        public async Task CancelPaymentAsync(int paymentId, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            var payment = await db.Payments.FindAsync(new object[] { paymentId }, ct);
            if (payment == null) return;

            db.Payments.Remove(payment);
            await db.SaveChangesAsync(ct);
        }

        public List<Payment> GetPaymentsForVisit(int visitId)
        {
            using var db = _contextFactory();
            return db.Payments
                .AsNoTracking()
                .Where(p => p.VisitId == visitId)
                .OrderBy(p => p.PaymentDate)
                .ThenBy(p => p.Id)
                .ToList();
        }

        public async Task<List<Payment>> GetPaymentsForVisitAsync(int visitId, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            return await db.Payments
                .AsNoTracking()
                .Where(p => p.VisitId == visitId)
                .OrderBy(p => p.PaymentDate)
                .ThenBy(p => p.Id)
                .ToListAsync(ct);
        }

        public List<PatientDebtDto> GetOutstandingDebts(string? searchPatient)
        {
            using var db = _contextFactory();
            return QueryDebts(db, searchPatient).ToList();
        }

        public async Task<List<PatientDebtDto>> GetOutstandingDebtsAsync(string? searchPatient, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            return await QueryDebts(db, searchPatient).ToListAsync(ct);
        }

        private static IQueryable<PatientDebtDto> QueryDebts(LabContext db, string? searchPatient)
        {
            var query = db.Visits
                .AsNoTracking()
                .Where(v => !v.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchPatient))
            {
                string term = searchPatient.Trim();
                query = query.Where(v => v.Patient.Name.Contains(term) || v.Patient.MedicalCode.Contains(term) || (v.Patient.Phone != null && v.Patient.Phone.Contains(term)));
            }

            return query
                .Select(v => new
                {
                    VisitId = v.Id,
                    PatientId = v.Patient.Id,
                    PatientName = v.Patient.Name,
                    MedicalCode = v.Patient.MedicalCode,
                    Phone = v.Patient.Phone,
                    VisitDate = v.VisitDate,
                    LabsCount = v.VisitLabs.Count,
                    TotalAmount = v.TotalPrice,
                    DiscountAmount = v.DiscountValue ?? 0m,
                    NetAmount = v.NetPrice,
                    TotalPaid = v.Payments.Sum(p => (decimal?)p.Amount) ?? 0m
                })
                .Where(x => x.NetAmount - x.TotalPaid > 0.01m)
                .OrderByDescending(x => x.VisitDate)
                .Select(x => new PatientDebtDto
                {
                    VisitId = x.VisitId,
                    PatientId = x.PatientId,
                    PatientName = x.PatientName,
                    MedicalCode = x.MedicalCode,
                    Phone = x.Phone,
                    VisitDate = x.VisitDate,
                    LabsCount = x.LabsCount,
                    TotalAmount = x.TotalAmount,
                    DiscountAmount = x.DiscountAmount,
                    NetAmount = x.NetAmount,
                    TotalPaid = x.TotalPaid,
                    RemainingBalance = x.NetAmount - x.TotalPaid
                });
        }
    }
}
