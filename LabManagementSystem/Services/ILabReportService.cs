using LabManagementSystem.Enums;
using LabManagementSystem.Models;

namespace LabManagementSystem.Services
{
    public class ParameterResultEntryDto
    {
        public int ParameterId { get; set; }
        public string ParameterName { get; set; } = "";
        public string? Unit { get; set; }
        public bool IsNumeric { get; set; }
        public decimal? NormalMin { get; set; }
        public decimal? NormalMax { get; set; }
        public string? NormalText { get; set; }
        public string FormattedNormalRange { get; set; } = "";
        public string ResultValue { get; set; } = "";
        public string? Notes { get; set; }
        public AbnormalFlagEnum Flag { get; set; } = AbnormalFlagEnum.Normal;
    }

    public class ConsolidatedTestDto
    {
        public int VisitLabId { get; set; }
        public int LabId { get; set; }
        public string TestName { get; set; } = "";
        public string CategoryName { get; set; } = "";
        public bool IsExternal { get; set; }
        public string StatusName { get; set; } = "";
        public List<ParameterResultEntryDto> Parameters { get; set; } = new();
    }

    public class ConsolidatedVisitReportDto
    {
        public int VisitId { get; set; }
        public DateTime VisitDate { get; set; }
        public string DoctorName { get; set; } = "";
        public string Notes { get; set; } = "";
        public string PatientName { get; set; } = "";
        public string MedicalCode { get; set; } = "";
        public int Age { get; set; }
        public string Gender { get; set; } = "";
        public string? Phone { get; set; }
        public List<ConsolidatedTestDto> Tests { get; set; } = new();
    }

    public interface ILabReportService
    {
        List<ParameterResultEntryDto> GetParametersAndResults(int visitLabId);
        Task<List<ParameterResultEntryDto>> GetParametersAndResultsAsync(int visitLabId, CancellationToken ct = default);

        void SaveResults(int visitLabId, List<ParameterResultEntryDto> results);
        Task SaveResultsAsync(int visitLabId, List<ParameterResultEntryDto> results, CancellationToken ct = default);

        ConsolidatedVisitReportDto GetConsolidatedReport(int visitId);
        Task<ConsolidatedVisitReportDto> GetConsolidatedReportAsync(int visitId, CancellationToken ct = default);
    }
}
