using System.Globalization;
using LabManagementSystem.Enums;
using LabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Services
{
    public class LabReportService : ILabReportService
    {
        private readonly Func<LabContext> _contextFactory;

        public LabReportService(Func<LabContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<ParameterResultEntryDto> GetParametersAndResults(int visitLabId)
        {
            using var db = _contextFactory();
            return LoadParametersAndResultsInternal(db, visitLabId);
        }

        public Task<List<ParameterResultEntryDto>> GetParametersAndResultsAsync(int visitLabId, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            return Task.FromResult(LoadParametersAndResultsInternal(db, visitLabId));
        }

        private static List<ParameterResultEntryDto> LoadParametersAndResultsInternal(LabContext db, int visitLabId)
        {
            var visitLab = db.VisitLabs
                .AsNoTracking()
                .Include(vl => vl.Lab)
                .Include(vl => vl.Visit)
                    .ThenInclude(v => v.Patient)
                .FirstOrDefault(vl => vl.Id == visitLabId);

            if (visitLab == null)
                return new List<ParameterResultEntryDto>();

            int? patientGenderId = visitLab.Visit?.Patient?.GenderId;

            // Load all parameters for this lab in one query
            var parameters = db.LabParameters
                .AsNoTracking()
                .Where(p => p.LabId == visitLab.LabId)
                .OrderBy(p => p.DisplayOrder)
                .ThenBy(p => p.Id)
                .ToList();

            // Load existing results in one query
            var existingResults = db.VisitLabResults
                .AsNoTracking()
                .Where(r => r.VisitLabId == visitLabId)
                .ToDictionary(r => r.LabParameterId);

            var list = new List<ParameterResultEntryDto>();

            foreach (var p in parameters)
            {
                if (p.GenderId.HasValue && patientGenderId.HasValue && p.GenderId.Value != patientGenderId.Value)
                {
                    if (parameters.Any(other => other.Name == p.Name && other.GenderId == patientGenderId.Value))
                        continue;
                }

                existingResults.TryGetValue(p.Id, out var res);
                string resVal = res?.ResultValue ?? "";
                string notes = res?.Notes ?? "";

                string normalRangeText = FormatNormalRange(p);
                var flag = CalculateFlag(p, resVal);

                list.Add(new ParameterResultEntryDto
                {
                    ParameterId = p.Id,
                    ParameterName = p.Name,
                    Unit = p.Unit,
                    IsNumeric = p.IsNumeric,
                    NormalMin = p.NormalRangeMin,
                    NormalMax = p.NormalRangeMax,
                    NormalText = p.NormalText,
                    FormattedNormalRange = normalRangeText,
                    ResultValue = resVal,
                    Notes = notes,
                    Flag = flag
                });
            }

            return list;
        }

        public void SaveResults(int visitLabId, List<ParameterResultEntryDto> results)
        {
            SaveResultsInternal(visitLabId, results);
        }

        public Task SaveResultsAsync(int visitLabId, List<ParameterResultEntryDto> results, CancellationToken ct = default)
        {
            SaveResultsInternal(visitLabId, results);
            return Task.CompletedTask;
        }

        private void SaveResultsInternal(int visitLabId, List<ParameterResultEntryDto> results)
        {
            using var db = _contextFactory();
            var visitLab = db.VisitLabs
                .Include(vl => vl.VisitLabResults)
                .FirstOrDefault(vl => vl.Id == visitLabId);

            if (visitLab == null)
                throw new InvalidOperationException("التحليل غير موجود.");

            var existingMap = visitLab.VisitLabResults.ToDictionary(r => r.LabParameterId);
            var paramsById = db.LabParameters
                .AsNoTracking()
                .Where(p => p.LabId == visitLab.LabId)
                .ToDictionary(p => p.Id);
            bool anyResultEntered = false;

            foreach (var item in results)
            {
                string cleanVal = item.ResultValue?.Trim() ?? "";
                if (string.IsNullOrEmpty(cleanVal))
                    continue;

                if (paramsById.TryGetValue(item.ParameterId, out var paramDef) && paramDef.IsNumeric)
                {
                    if (!TryParseNumericResult(cleanVal, out _))
                    {
                        throw new InvalidOperationException($"القيمة المدخلة للباراميتر '{paramDef.Name}' يجب أن تكون قيمة رقمية صحيحة.");
                    }
                }

                anyResultEntered = true;

                if (existingMap.TryGetValue(item.ParameterId, out var existingResult))
                {
                    existingResult.ResultValue = cleanVal;
                    existingResult.Notes = item.Notes?.Trim();
                }
                else
                {
                    db.VisitLabResults.Add(new VisitLabResult
                    {
                        VisitLabId = visitLabId,
                        LabParameterId = item.ParameterId,
                        ResultValue = cleanVal,
                        Notes = item.Notes?.Trim()
                    });
                }
            }

            if (anyResultEntered)
            {
                visitLab.VisitLabStatusId = (int)VisitLabStatusEnum.Completed;
            }

            db.SaveChanges();
        }

        public ConsolidatedVisitReportDto GetConsolidatedReport(int visitId)
        {
            using var db = _contextFactory();
            return BuildConsolidatedReport(db, visitId);
        }

        public Task<ConsolidatedVisitReportDto> GetConsolidatedReportAsync(int visitId, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            return Task.FromResult(BuildConsolidatedReport(db, visitId));
        }

        // Optimized: Single-pass batch query (eliminating the N+1 parameter queries)
        private static ConsolidatedVisitReportDto BuildConsolidatedReport(LabContext db, int visitId)
        {
            var visit = db.Visits
                .AsNoTracking()
                .Include(v => v.Patient)
                    .ThenInclude(p => p.Gender)
                .Include(v => v.VisitLabs)
                    .ThenInclude(vl => vl.Lab)
                        .ThenInclude(l => l.LabCategory)
                .Include(v => v.VisitLabs)
                    .ThenInclude(vl => vl.VisitLabStatus)
                .Include(v => v.VisitLabs)
                    .ThenInclude(vl => vl.VisitLabResults)
                .FirstOrDefault(v => v.Id == visitId);

            if (visit == null)
                throw new InvalidOperationException("الزيارة غير موجودة.");

            var report = new ConsolidatedVisitReportDto
            {
                VisitId = visit.Id,
                VisitDate = visit.VisitDate,
                DoctorName = visit.DrName ?? "",
                Notes = visit.Notes ?? "",
                PatientName = visit.Patient.Name,
                MedicalCode = visit.Patient.MedicalCode,
                Age = visit.Patient.AgeAtRecord,
                Gender = visit.Patient.Gender?.Name ?? "",
                Phone = visit.Patient.Phone
            };

            // Fix N+1: Query all parameters for all tests in ONE roundtrip
            var labIds = visit.VisitLabs.Select(vl => vl.LabId).Distinct().ToList();
            var allConfiguredParams = db.LabParameters
                .AsNoTracking()
                .Where(p => p.LabId.HasValue && labIds.Contains(p.LabId.Value))
                .OrderBy(p => p.DisplayOrder)
                .ThenBy(p => p.Id)
                .ToList();

            var paramsByLab = allConfiguredParams.ToLookup(p => p.LabId ?? 0);

            foreach (var vl in visit.VisitLabs.OrderBy(vl => vl.Lab?.LabCategory?.Name).ThenBy(vl => vl.Lab?.Name))
            {
                var testDto = new ConsolidatedTestDto
                {
                    VisitLabId = vl.Id,
                    LabId = vl.LabId,
                    TestName = vl.Lab?.Name ?? "",
                    CategoryName = vl.Lab?.LabCategory?.Name ?? "تحاليل عامة",
                    IsExternal = vl.IsExternal,
                    StatusName = vl.VisitLabStatus?.Name ?? "Pending"
                };

                var configuredParams = paramsByLab[vl.LabId];
                var resultsDict = vl.VisitLabResults.ToDictionary(r => r.LabParameterId);

                foreach (var p in configuredParams)
                {
                    resultsDict.TryGetValue(p.Id, out var res);
                    string resVal = res?.ResultValue ?? "";

                    testDto.Parameters.Add(new ParameterResultEntryDto
                    {
                        ParameterId = p.Id,
                        ParameterName = p.Name,
                        Unit = p.Unit,
                        IsNumeric = p.IsNumeric,
                        NormalMin = p.NormalRangeMin,
                        NormalMax = p.NormalRangeMax,
                        NormalText = p.NormalText,
                        FormattedNormalRange = FormatNormalRange(p),
                        ResultValue = resVal,
                        Notes = res?.Notes,
                        Flag = CalculateFlag(p, resVal)
                    });
                }

                report.Tests.Add(testDto);
            }

            return report;
        }

        private static string FormatNormalRange(LabParameter p)
        {
            if (p.IsNumeric && p.NormalRangeMin.HasValue && p.NormalRangeMax.HasValue)
            {
                return $"{p.NormalRangeMin.Value} - {p.NormalRangeMax.Value}";
            }
            if (p.IsNumeric && p.NormalRangeMin.HasValue)
            {
                return $">= {p.NormalRangeMin.Value}";
            }
            if (p.IsNumeric && p.NormalRangeMax.HasValue)
            {
                return $"<= {p.NormalRangeMax.Value}";
            }
            return p.NormalText ?? "";
        }

        public static bool TryParseNumericResult(string value, out decimal parsed)
        {
            string clean = value?.Trim() ?? "";
            return decimal.TryParse(clean, NumberStyles.Number, CultureInfo.CurrentCulture, out parsed)
                || decimal.TryParse(clean, NumberStyles.Number, CultureInfo.InvariantCulture, out parsed);
        }

        private static AbnormalFlagEnum CalculateFlag(LabParameter p, string resultValue)
        {
            if (string.IsNullOrWhiteSpace(resultValue))
                return AbnormalFlagEnum.Normal;

            if (p.IsNumeric)
            {
                if (TryParseNumericResult(resultValue, out decimal val))
                {
                    if (p.NormalRangeMin.HasValue && val < p.NormalRangeMin.Value)
                        return AbnormalFlagEnum.Low;
                    if (p.NormalRangeMax.HasValue && val > p.NormalRangeMax.Value)
                        return AbnormalFlagEnum.High;
                }
                return AbnormalFlagEnum.Normal;
            }

            string actual = resultValue.Trim().ToLowerInvariant();
            if (!string.IsNullOrWhiteSpace(p.NormalText))
            {
                string expected = p.NormalText.Trim().ToLowerInvariant();
                if (actual == expected)
                    return AbnormalFlagEnum.Normal;

                if (actual.Contains("positive") || actual.Contains("إيجابي") || actual == "reactive" || actual.Contains("abnormal") || actual.Contains("+"))
                    return AbnormalFlagEnum.Abnormal;

                if ((expected == "negative" || expected == "non-reactive" || expected == "normal" || expected == "سلبي" || expected == "طبيعي") &&
                    !actual.Contains(expected))
                    return AbnormalFlagEnum.Abnormal;
            }
            else if (actual.Contains("positive") || actual.Contains("إيجابي") || actual == "reactive" || actual.Contains("abnormal"))
            {
                return AbnormalFlagEnum.Abnormal;
            }

            return AbnormalFlagEnum.Normal;
        }
    }
}
