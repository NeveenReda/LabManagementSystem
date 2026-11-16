using LabManagementSystem.Enums;
using LabManagementSystem.Models;

namespace LabManagementSystem.Services
{
    public class ParameterResultEntryDto
    {
        public int ParameterId { get; set; }
        public string ParameterName { get; set; } = "";
        public string? CategoryName { get; set; }
        public string? Unit { get; set; }
        public bool IsNumeric { get; set; }
        public decimal? NormalMin { get; set; }
        public decimal? NormalMax { get; set; }
        public string? NormalText { get; set; }
        public string FormattedNormalRange { get; set; } = "";
        public string ResultValue { get; set; } = "";
        public string? Notes { get; set; }
        public AbnormalFlagEnum Flag { get; set; } = AbnormalFlagEnum.Normal;
        public bool IsBold { get; set; }
        public string? TextColor { get; set; }

        public static (bool isBold, string? colorHex, string cleanNotes) ParseStyleFromNotes(string? notes)
        {
            if (string.IsNullOrWhiteSpace(notes)) return (false, null, "");
            bool isBold = false;
            string? colorHex = null;
            string clean = notes;

            var match = System.Text.RegularExpressions.Regex.Match(notes, @"\[STYLE:(.*?)\]");
            if (match.Success)
            {
                var tag = match.Groups[1].Value;
                clean = notes.Replace(match.Value, "").Trim();
                var parts = tag.Split(';', StringSplitOptions.RemoveEmptyEntries);
                foreach (var p in parts)
                {
                    var kv = p.Split('=', StringSplitOptions.TrimEntries);
                    if (kv.Length >= 2)
                    {
                        if (kv[0].Equals("bold", StringComparison.OrdinalIgnoreCase))
                            isBold = kv[1] == "1" || kv[1].Equals("true", StringComparison.OrdinalIgnoreCase);
                        else if (kv[0].Equals("color", StringComparison.OrdinalIgnoreCase))
                            colorHex = kv[1];
                    }
                    else if (p.Equals("bold", StringComparison.OrdinalIgnoreCase))
                    {
                        isBold = true;
                    }
                }
            }

            return (isBold, colorHex, clean);
        }

        public static string BuildNotesWithStyle(string? cleanNotes, bool isBold, string? colorHex)
        {
            cleanNotes = (cleanNotes ?? "").Trim();
            if (!isBold && string.IsNullOrWhiteSpace(colorHex))
                return cleanNotes;

            var parts = new List<string>();
            if (isBold) parts.Add("bold=1");
            if (!string.IsNullOrWhiteSpace(colorHex)) parts.Add($"color={colorHex.Trim()}");

            string tag = $"[STYLE:{string.Join(";", parts)}]";
            return string.IsNullOrEmpty(cleanNotes) ? tag : $"{tag} {cleanNotes}";
        }
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
