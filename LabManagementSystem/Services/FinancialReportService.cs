using LabManagementSystem.Enums;
using LabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Services
{
    public class FinancialReportService : IFinancialReportService
    {
        private readonly Func<LabContext> _contextFactory;

        public FinancialReportService(Func<LabContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public FinancialSummaryDto GetFinancialReport(DateTime fromDate, DateTime toDate)
        {
            using var db = _contextFactory();
            return BuildFinancialReport(db, fromDate, toDate);
        }

        public Task<FinancialSummaryDto> GetFinancialReportAsync(DateTime fromDate, DateTime toDate, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            return Task.FromResult(BuildFinancialReport(db, fromDate, toDate));
        }

        private static FinancialSummaryDto BuildFinancialReport(LabContext db, DateTime fromDate, DateTime toDate)
        {
            var from = fromDate.Date;
            var toExclusive = toDate.Date.AddDays(1);

            // Visits in range (non-deleted) with AsNoTracking
            var visits = db.Visits
                .AsNoTracking()
                .Where(v => !v.IsDeleted && v.VisitDate >= from && v.VisitDate < toExclusive)
                .Include(v => v.VisitLabs)
                    .ThenInclude(vl => vl.Lab)
                        .ThenInclude(l => l.LabCategory)
                .Include(v => v.Payments)
                .ToList();

            // Payments collected in range
            var payments = db.Payments
                .AsNoTracking()
                .Where(p => p.PaymentDate >= from && p.PaymentDate < toExclusive && !p.Visit.IsDeleted)
                .ToList();

            var summary = new FinancialSummaryDto
            {
                FromDate = fromDate,
                ToDate = toDate,
                TotalVisits = visits.Count,
                TotalTests = visits.Sum(v => v.VisitLabs.Count),
                TotalGrossRevenue = visits.Sum(v => v.TotalPrice),
                TotalDiscounts = visits.Sum(v => v.DiscountValue ?? 0m),
                TotalNetRevenue = visits.Sum(v => v.NetPrice),
                TotalCollectedPayments = payments.Sum(p => p.Amount),
                TotalOutstandingBalances = visits.Sum(v => Math.Max(0, v.NetPrice - v.Payments.Sum(p => p.Amount)))
            };

            // Breakdown by test
            var allVisitLabs = visits.SelectMany(v => v.VisitLabs).Where(vl => vl.Lab != null).ToList();

            summary.TestsBreakdown = allVisitLabs
                .GroupBy(vl => new { vl.LabId, vl.Lab.Name, Category = vl.Lab.LabCategory?.Name ?? "عام" })
                .Select(g => new TestRevenueItemDto
                {
                    TestName = g.Key.Name,
                    CategoryName = g.Key.Category,
                    Count = g.Count(),
                    TotalRevenue = g.Sum(vl => vl.LabPrice)
                })
                .OrderByDescending(t => t.Count)
                .ToList();

            // Breakdown by category
            summary.CategoriesBreakdown = allVisitLabs
                .GroupBy(vl => vl.Lab.LabCategory?.Name ?? "تحاليل عامة")
                .Select(g => new CategoryRevenueItemDto
                {
                    CategoryName = g.Key,
                    Count = g.Count(),
                    TotalRevenue = g.Sum(vl => vl.LabPrice)
                })
                .OrderByDescending(c => c.Count)
                .ToList();

            // Breakdown by payment method
            summary.PaymentMethodsBreakdown = payments
                .GroupBy(p => p.PaymentMethod)
                .Select(g => new PaymentMethodSummaryDto
                {
                    MethodName = ((PaymentMethodEnum)g.Key).ToArabic(),
                    TransactionsCount = g.Count(),
                    TotalAmount = g.Sum(p => p.Amount)
                })
                .OrderByDescending(m => m.TotalAmount)
                .ToList();

            return summary;
        }
    }
}
