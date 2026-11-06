using LabManagementSystem.Models;

namespace LabManagementSystem.Services
{
    public class VisitSearchFilter
    {
        public string? PatientCode { get; set; }
        public string? PatientName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public bool IncludeDeleted { get; set; } = false;
    }

    public class VisitSummaryDto
    {
        public int Id { get; set; }
        public string PatientName { get; set; } = "";
        public string MedicalCode { get; set; } = "";
        public DateTime VisitDate { get; set; }
        public int LabsCount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? DiscountPercent { get; set; }
        public decimal NetPrice { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class DashboardStatsDto
    {
        public int TodayVisitsCount { get; set; }
        public int TodayPatientsCount { get; set; }
        public int PendingLabsCount { get; set; }
        public int CompletedLabsCount { get; set; }
        public decimal TodayCollectedPayments { get; set; }
        public decimal TotalOutstandingDebts { get; set; }
        public List<VisitSummaryDto> RecentVisits { get; set; } = new();
    }

    public interface IVisitService
    {
        List<VisitSummaryDto> SearchVisits(VisitSearchFilter filter);
        Task<List<VisitSummaryDto>> SearchVisitsAsync(VisitSearchFilter filter, int maxResults = 250, CancellationToken ct = default);

        Visit? GetVisitById(int id);
        Task<Visit?> GetVisitByIdAsync(int id, CancellationToken ct = default);

        int CreateVisit(Visit visit, List<int> labIds, decimal initialPayment, int paymentMethod, string? paymentNotes);
        Task<int> CreateVisitAsync(Visit visit, List<int> labIds, decimal initialPayment, int paymentMethod, string? paymentNotes, CancellationToken ct = default);

        void UpdateVisit(Visit visit, List<int> currentLabIds);
        Task UpdateVisitAsync(Visit visit, List<int> currentLabIds, CancellationToken ct = default);

        void DeleteVisit(int visitId);
        Task DeleteVisitAsync(int visitId, CancellationToken ct = default);

        List<Lab> GetAllLabs();
        Task<DashboardStatsDto> GetDashboardStatsAsync(CancellationToken ct = default);
    }
}
