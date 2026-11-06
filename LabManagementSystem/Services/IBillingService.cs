using LabManagementSystem.Models;

namespace LabManagementSystem.Services
{
    public class PatientDebtDto
    {
        public int VisitId { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; } = "";
        public string MedicalCode { get; set; } = "";
        public string? Phone { get; set; }
        public DateTime VisitDate { get; set; }
        public int LabsCount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal RemainingBalance { get; set; }
    }

    public interface IBillingService
    {
        void RecordPayment(int visitId, decimal amount, DateTime paymentDate, int paymentMethod, string? notes);
        Task RecordPaymentAsync(int visitId, decimal amount, DateTime paymentDate, int paymentMethod, string? notes, CancellationToken ct = default);

        void CancelPayment(int paymentId);
        Task CancelPaymentAsync(int paymentId, CancellationToken ct = default);

        List<Payment> GetPaymentsForVisit(int visitId);
        Task<List<Payment>> GetPaymentsForVisitAsync(int visitId, CancellationToken ct = default);

        List<PatientDebtDto> GetOutstandingDebts(string? searchPatient);
        Task<List<PatientDebtDto>> GetOutstandingDebtsAsync(string? searchPatient, CancellationToken ct = default);
    }
}
