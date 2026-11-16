using System.Globalization;
using LabManagementSystem.Enums;
using LabManagementSystem.Models;
using LabManagementSystem.Printing;
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

            // Ensure standard clinical parameters are present for CBC / Urine Analysis
            EnsureStandardParametersInDb(db, visitLab.Lab);

            int? patientGenderId = visitLab.Visit?.Patient?.GenderId;

            // Load all parameters for this lab in one query
            var parameters = db.LabParameters
                .AsNoTracking()
                .Include(p => p.LabParametersCategory)
                .Where(p => p.LabId == visitLab.LabId)
                .OrderBy(p => p.DisplayOrder)
                .ThenBy(p => p.Id)
                .ToList();

            // Load existing results in one query safely handling any duplicate rows
            var existingResults = new Dictionary<int, VisitLabResult>();
            foreach (var r in db.VisitLabResults.AsNoTracking().Where(r => r.VisitLabId == visitLabId))
            {
                existingResults[r.LabParameterId] = r;
            }

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
                string rawNotes = res?.Notes ?? "";
                var (isBold, colorHex, cleanNotes) = ParameterResultEntryDto.ParseStyleFromNotes(rawNotes);

                string normalRangeText = FormatNormalRange(p);
                var flag = CalculateFlag(p, resVal);

                list.Add(new ParameterResultEntryDto
                {
                    ParameterId = p.Id,
                    ParameterName = p.Name,
                    CategoryName = p.LabParametersCategory?.Name,
                    Unit = p.Unit,
                    IsNumeric = p.IsNumeric,
                    NormalMin = p.NormalRangeMin,
                    NormalMax = p.NormalRangeMax,
                    NormalText = p.NormalText,
                    FormattedNormalRange = normalRangeText,
                    ResultValue = resVal,
                    Notes = cleanNotes,
                    IsBold = isBold,
                    TextColor = colorHex,
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

            var existingMap = new Dictionary<int, VisitLabResult>();
            foreach (var r in visitLab.VisitLabResults)
            {
                existingMap[r.LabParameterId] = r;
            }
            var paramsById = new Dictionary<int, LabParameter>();
            foreach (var p in db.LabParameters.AsNoTracking().Where(p => p.LabId == visitLab.LabId))
            {
                paramsById[p.Id] = p;
            }
            bool anyResultEntered = false;

            foreach (var item in results)
            {
                string cleanVal = item.ResultValue?.Trim() ?? "";
                if (string.IsNullOrEmpty(cleanVal))
                    continue;

                anyResultEntered = true;
                string formattedNotes = ParameterResultEntryDto.BuildNotesWithStyle(item.Notes, item.IsBold, item.TextColor);

                if (existingMap.TryGetValue(item.ParameterId, out var existingResult))
                {
                    existingResult.ResultValue = cleanVal;
                    existingResult.Notes = formattedNotes;
                }
                else
                {
                    db.VisitLabResults.Add(new VisitLabResult
                    {
                        VisitLabId = visitLabId,
                        LabParameterId = item.ParameterId,
                        ResultValue = cleanVal,
                        Notes = formattedNotes
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

            foreach (var vl in visit.VisitLabs)
            {
                EnsureStandardParametersInDb(db, vl.Lab);
            }

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
                .Include(p => p.LabParametersCategory)
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
                var resultsDict = new Dictionary<int, VisitLabResult>();
                foreach (var r in vl.VisitLabResults)
                {
                    resultsDict[r.LabParameterId] = r;
                }

                foreach (var p in configuredParams)
                {
                    resultsDict.TryGetValue(p.Id, out var res);
                    string resVal = res?.ResultValue ?? "";
                    string rawNotes = res?.Notes ?? "";
                    var (isBold, colorHex, cleanNotes) = ParameterResultEntryDto.ParseStyleFromNotes(rawNotes);

                    testDto.Parameters.Add(new ParameterResultEntryDto
                    {
                        ParameterId = p.Id,
                        ParameterName = p.Name,
                        CategoryName = p.LabParametersCategory?.Name,
                        Unit = p.Unit,
                        IsNumeric = p.IsNumeric,
                        NormalMin = p.NormalRangeMin,
                        NormalMax = p.NormalRangeMax,
                        NormalText = p.NormalText,
                        FormattedNormalRange = FormatNormalRange(p),
                        ResultValue = resVal,
                        Notes = cleanNotes,
                        IsBold = isBold,
                        TextColor = colorHex,
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

        public static void EnsureStandardParametersInDb(LabContext db, Lab? lab)
        {
            if (lab == null) return;
            string labName = (lab.Name ?? "").Trim();
            var dummyDto = new ConsolidatedTestDto { TestName = labName, CategoryName = lab.LabCategory?.Name ?? "" };
            bool isCbc = LabReportEnglishHelper.IsCbc(dummyDto);
            bool isUrine = LabReportEnglishHelper.IsUrineAnalysis(dummyDto);

            if (!isCbc && !isUrine) return;

            var existingParams = db.LabParameters
                .Where(p => p.LabId == lab.Id)
                .ToList();

            // Find or create category for this lab
            var cat = db.LabParameterCategories.FirstOrDefault(c => c.LabId == lab.Id);
            if (cat == null)
            {
                cat = new LabParameterCategory
                {
                    Name = isCbc ? "Blood Count" : "Urine Details",
                    LabId = lab.Id
                };
                db.LabParameterCategories.Add(cat);
                try { db.SaveChanges(); } catch { }
            }

            int catId = cat.Id;
            bool addedAny = false;

            if (isCbc)
            {
                var standardCbc = new (string Name, string Unit, decimal? Min, decimal? Max, int Order)[]
                {
                    ("Hemoglobin", "g/dL", 13.5m, 17.5m, 1),
                    ("RBC-Erythrocytes", "10^12/L", 4.50m, 5.90m, 2),
                    ("Hct-Hematocrit", "%", 36.0m, 53.0m, 3),
                    ("MCV", "fl", 80.0m, 100.0m, 4),
                    ("MCH", "pg", 27.5m, 32.4m, 5),
                    ("MCHC", "g/dL", 31.7m, 34.2m, 6),
                    ("RDWs", "fl", 36.2m, 49.7m, 7),
                    ("RDWc", "%", 11.1m, 14.0m, 8),
                    ("Platelet", "10^9/L", 150m, 450m, 9),
                    ("PCT-Plateletcrit", "%", 0.16m, 0.36m, 10),
                    ("PDWs", "fl", 11.1m, 19.7m, 11),
                    ("PDWc", "%", 37.8m, 43.6m, 12),
                    ("MPV", "fl", 8.3m, 12.1m, 13),
                    ("P-LCC", "10^9/L", 55m, 139m, 14),
                    ("P-LCR", "%", 25.3m, 53.8m, 15),
                    ("WBC-Leukocytes", "10^9/L", 4.50m, 11.50m, 16),
                    ("LYM %", "%", 19.1m, 48.5m, 17),
                    ("MID %", "%", 4.5m, 12.1m, 18),
                    ("GRA %", "%", 43.6m, 73.4m, 19),
                    ("LYM #", "10^9/l", 1.30m, 4.00m, 20),
                    ("MID #", "10^9/l", 0.30m, 1.00m, 21),
                    ("GRA #", "10^9/l", 1.00m, 7.60m, 22)
                };

                foreach (var item in standardCbc)
                {
                    bool exists = existingParams.Any(p =>
                        p.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase) ||
                        LabReportEnglishHelper.NormalizeParameterName(p.Name).Equals(item.Name, StringComparison.OrdinalIgnoreCase));

                    if (!exists)
                    {
                        var newParam = new LabParameter
                        {
                            LabId = lab.Id,
                            Name = item.Name,
                            Unit = item.Unit,
                            IsNumeric = false, // Relaxed so users can enter text/numbers freely
                            NormalRangeMin = item.Min,
                            NormalRangeMax = item.Max,
                            NormalText = $"{item.Min:F2} - {item.Max:F2}",
                            DisplayOrder = item.Order,
                            LabParametersCategoryId = catId
                        };
                        db.LabParameters.Add(newParam);
                        addedAny = true;
                    }
                }
            }
            else if (isUrine)
            {
                var standardUrine = new (string Name, string Unit, string? Text, int Order)[]
                {
                    ("Volume", "", "Random Sample (Midstream urine)", 1),
                    ("Color", "", "Yellow", 2),
                    ("Odor", "", "Aromatic", 3),
                    ("Aspect", "", "Clear", 4),
                    ("Foam", "", "Nil / Absent", 5),
                    ("Sediment", "", "Nil / Absent", 6),
                    ("Reaction (pH)", "", "5.5 - 7", 7),
                    ("Specific gravity", "", "1.015 - 1.030", 8),
                    ("Leukocyte esterase", "", "Nil", 9),
                    ("Blood", "", "Nil", 10),
                    ("Glucose", "", "Nil", 11),
                    ("Protein", "", "Nil", 12),
                    ("Ketones", "", "Nil", 13),
                    ("Nitrite", "", "Negative", 14),
                    ("Bilirubin", "", "Absent", 15),
                    ("Urobilinogen", "", "Normal trace", 16),
                    ("RBCs", "Cells/H.P.F", "0 - 5", 17),
                    ("Pus", "Cells/H.P.F", "0 - 3", 18),
                    ("Epithelial cells", "", "Nil / Few", 19),
                    ("Casts", "", "0 - 3", 20),
                    ("Amorphous", "", "Nil", 21),
                    ("Crystals", "", "Nil", 22),
                    ("Bacteria", "", "Absent", 23),
                    ("Fungus", "", "Absent", 24),
                    ("Mucus", "", "Absent", 25)
                };

                foreach (var item in standardUrine)
                {
                    bool exists = existingParams.Any(p =>
                        p.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase) ||
                        LabReportEnglishHelper.NormalizeParameterName(p.Name).Equals(item.Name, StringComparison.OrdinalIgnoreCase));

                    if (!exists)
                    {
                        var newParam = new LabParameter
                        {
                            LabId = lab.Id,
                            Name = item.Name,
                            Unit = item.Unit,
                            IsNumeric = false,
                            NormalText = item.Text,
                            DisplayOrder = item.Order,
                            LabParametersCategoryId = catId
                        };
                        db.LabParameters.Add(newParam);
                        addedAny = true;
                    }
                }
            }

            if (addedAny)
            {
                try
                {
                    db.SaveChanges();
                }
                catch { }
            }
        }
    }
}
