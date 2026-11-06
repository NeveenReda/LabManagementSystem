namespace LabManagementSystem.Services
{
    public class FinancialSummaryDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int TotalVisits { get; set; }
        public int TotalTests { get; set; }
        public decimal TotalGrossRevenue { get; set; }
        public decimal TotalDiscounts { get; set; }
        public decimal TotalNetRevenue { get; set; }
        public decimal TotalCollectedPayments { get; set; }
        public decimal TotalOutstandingBalances { get; set; }
        public List<TestRevenueItemDto> TestsBreakdown { get; set; } = new();
        public List<CategoryRevenueItemDto> CategoriesBreakdown { get; set; } = new();
        public List<PaymentMethodSummaryDto> PaymentMethodsBreakdown { get; set; } = new();
    }

    public class TestRevenueItemDto
    {
        public string TestName { get; set; } = "";
        public string CategoryName { get; set; } = "";
        public int Count { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class CategoryRevenueItemDto
    {
        public string CategoryName { get; set; } = "";
        public int Count { get; set; }
        public decimal TotalRevenue { get; set; }
    }

    public class PaymentMethodSummaryDto
    {
        public string MethodName { get; set; } = "";
        public int TransactionsCount { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public interface IFinancialReportService
    {
        FinancialSummaryDto GetFinancialReport(DateTime fromDate, DateTime toDate);
        Task<FinancialSummaryDto> GetFinancialReportAsync(DateTime fromDate, DateTime toDate, CancellationToken ct = default);
    }
}
