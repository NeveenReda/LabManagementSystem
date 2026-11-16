using System;
using System.Collections.Generic;
using System.Linq;
using LabManagementSystem.Enums;
using LabManagementSystem.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LabManagementSystem.Printing
{
    /// <summary>
    /// Professional Laboratory Medical Report Document (A4, English Only).
    /// Tailored for preprinted clinic stationery:
    /// - Zero application header, logo, lab name, or branding.
    /// - Zero application footer or page numbers.
    /// - Contains ONLY the laboratory report content, centered on the printable A4 page.
    /// - Provides specialized layouts for Urine Analysis and Complete Blood Count (CBC)
    ///   matching doctor reference samples, plus a clean medical layout for all general lab tests.
    /// </summary>
    public class ConsolidatedLabReportDocument : IDocument
    {
        private readonly ConsolidatedVisitReportDto _report;
        private readonly ConsolidatedTestDto? _singleTest;

        public ConsolidatedLabReportDocument(ConsolidatedVisitReportDto report, ConsolidatedTestDto? singleTest = null)
        {
            _report = report ?? throw new ArgumentNullException(nameof(report));
            _singleTest = singleTest;
        }

        public DocumentMetadata GetMetadata() => new DocumentMetadata
        {
            Title = _singleTest != null
                ? LabReportEnglishHelper.NormalizeTestName(_singleTest.TestName)
                : "Laboratory Medical Report",
            Author = "Clinical Laboratory",
            Subject = "Medical Laboratory Test Results",
            Creator = "Laboratory Information System"
        };

        public void Compose(IDocumentContainer container)
        {
            var tests = _singleTest != null
                ? new List<ConsolidatedTestDto> { _singleTest }
                : (_report.Tests ?? new List<ConsolidatedTestDto>());

            if (tests.Count == 0)
            {
                container.Page(page =>
                {
                    ConfigureA4Page(page);
                    page.Content().AlignCenter().AlignMiddle().Text("No laboratory tests recorded for this visit.")
                        .FontFamily("Arial").FontSize(11).Italic().FontColor(Colors.Grey.Darken2);
                });
                return;
            }

            foreach (var test in tests)
            {
                container.Page(page =>
                {
                    ConfigureA4Page(page);
                    page.Content().Element(c => ComposeTestPage(c, test));
                });
            }
        }

        private static void ConfigureA4Page(PageDescriptor page)
        {
            page.Size(PageSizes.A4);
            // Safe margins for clinic preprinted letterhead paper:
            // ~3.2cm top margin clears the preprinted header/logo.
            // ~2.8cm bottom margin clears the preprinted footer/signature block.
            // ~1.8cm side margins provide balanced horizontal centering.
            page.MarginTop(3.2f, Unit.Centimetre);
            page.MarginBottom(2.8f, Unit.Centimetre);
            page.MarginHorizontal(1.8f, Unit.Centimetre);

            // English LTR layout
            page.ContentFromLeftToRight();
            page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(9f).FontColor("#1A1A1A"));

            // IMPORTANT: Preprinted stationery requirement:
            // DO NOT print any application header, logo, lab name, or footer!
        }

        private void ComposeTestPage(IContainer container, ConsolidatedTestDto test)
        {
            if (LabReportEnglishHelper.IsUrineAnalysis(test))
            {
                ComposeUrineAnalysis(container, test);
            }
            else if (LabReportEnglishHelper.IsCbc(test))
            {
                ComposeCbcReport(container, test);
            }
            else
            {
                ComposeGeneralLabTest(container, test);
            }
        }

        #region 1. URINE ANALYSIS REPORT (Reference Image 2)

        private void ComposeUrineAnalysis(IContainer container, ConsolidatedTestDto test)
        {
            container.Column(col =>
            {
                // Top Title Box
                col.Item().AlignCenter().Background("#1E1E1E").PaddingVertical(3.5f).PaddingHorizontal(28)
                    .Text("Urine Analysis").FontFamily("Arial").Bold().FontSize(12.5f).FontColor(Colors.White);

                col.Item().PaddingTop(10);

                // Categorize test parameters into the 3 clinical sections
                var physicalParams = new List<ParameterResultEntryDto>();
                var chemicalParams = new List<ParameterResultEntryDto>();
                var microCells = new List<ParameterResultEntryDto>();
                var microOther = new List<ParameterResultEntryDto>();
                var microOrganisms = new List<ParameterResultEntryDto>();

                foreach (var p in test.Parameters)
                {
                    var section = LabReportEnglishHelper.ClassifyUrineParameter(p.ParameterName, p.CategoryName);
                    switch (section)
                    {
                        case UrineSection.Physical:
                            physicalParams.Add(p);
                            break;
                        case UrineSection.Chemical:
                            chemicalParams.Add(p);
                            break;
                        case UrineSection.MicroscopicCells:
                            microCells.Add(p);
                            break;
                        case UrineSection.Microorganisms:
                            microOrganisms.Add(p);
                            break;
                        default:
                            microOther.Add(p);
                            break;
                    }
                }

                // If no pre-categorization was possible, provide standard clinical defaults while preserving real results
                EnsureStandardUrineParameters(physicalParams, chemicalParams, microCells, microOther, microOrganisms, test.Parameters);

                // SECTION A: Physical Examination
                col.Item().Element(c => ComposeUrinePhysicalSection(c, physicalParams));

                col.Item().PaddingTop(8);

                // SECTION B: Chemical Examination
                col.Item().Element(c => ComposeUrineChemicalSection(c, chemicalParams));

                col.Item().PaddingTop(8);

                // SECTION C: Microscopic Examination
                col.Item().Element(c => ComposeUrineMicroscopicSection(c, microCells, microOther, microOrganisms));
            });
        }

        private static void ComposeUrinePhysicalSection(IContainer container, List<ParameterResultEntryDto> parameters)
        {
            container.Column(col =>
            {
                // Section Header Bar
                col.Item().Row(row =>
                {
                    row.RelativeItem().AlignLeft().Background("#5A5D62").PaddingVertical(2.5f).PaddingHorizontal(8)
                        .Text("-Physical Examination").FontFamily("Arial").Bold().FontSize(8.5f).FontColor(Colors.White);

                    row.ConstantItem(120).AlignRight().Background("#1E1E1E").PaddingVertical(2.5f).AlignCenter()
                        .Text("Normal").FontFamily("Arial").Bold().FontSize(8.5f).FontColor(Colors.White);
                });

                // Physical Parameters Table
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(2.2f); // Parameter Name
                        columns.RelativeColumn(4.4f); // Patient Result
                        columns.RelativeColumn(2.2f); // Normal Range
                    });

                    foreach (var p in parameters)
                    {
                        string name = LabReportEnglishHelper.NormalizeParameterName(p.ParameterName);
                        string result = LabReportEnglishHelper.NormalizeResultValue(p.ResultValue);
                        string normal = !string.IsNullOrWhiteSpace(p.FormattedNormalRange) && p.FormattedNormalRange != "-"
                            ? LabReportEnglishHelper.NormalizeNormalRange(p.FormattedNormalRange, p.NormalText)
                            : GetDefaultUrinePhysicalNormal(name);

                        // Parameter cell
                        table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#E2E4E8").PaddingVertical(2.5f).PaddingHorizontal(6)
                            .AlignCenter().Text(name).FontFamily("Arial").Bold().FontSize(8.5f).FontColor("#1A1A1A");

                        // Result cell
                        table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#F0F1F4").PaddingVertical(2.5f).PaddingHorizontal(6)
                            .AlignCenter().Text(result).FontFamily("Arial").FontSize(8.5f).FontColor("#111111");

                        // Normal cell
                        table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#E2E4E8").PaddingVertical(2.5f).PaddingHorizontal(6)
                            .AlignCenter().Text(normal).FontFamily("Arial").FontSize(8.5f).FontColor("#333333");
                    }
                });
            });
        }

        private static void ComposeUrineChemicalSection(IContainer container, List<ParameterResultEntryDto> parameters)
        {
            container.Column(col =>
            {
                // Section Header Bar
                col.Item().Row(row =>
                {
                    row.RelativeItem().AlignLeft().Background("#5A5D62").PaddingVertical(2.5f).PaddingHorizontal(8)
                        .Text("-Chemical Examination").FontFamily("Arial").Bold().FontSize(8.5f).FontColor(Colors.White);
                });

                // Standard clinical order: 5 left, 5 right (matching Reference Image 2)
                string[] leftOrder = { "Reaction (pH)", "Specific gravity", "Leukocyte esterase", "Blood", "Glucose" };
                string[] rightOrder = { "Protein", "Ketones", "Nitrite", "Bilirubin", "Urobilinogen" };

                var leftGroup = new List<ParameterResultEntryDto>();
                var rightGroup = new List<ParameterResultEntryDto>();
                var remaining = new List<ParameterResultEntryDto>();

                foreach (var name in leftOrder)
                {
                    var found = parameters.FirstOrDefault(p => LabReportEnglishHelper.NormalizeParameterName(p.ParameterName).Equals(name, StringComparison.OrdinalIgnoreCase));
                    if (found != null) leftGroup.Add(found);
                }

                foreach (var name in rightOrder)
                {
                    var found = parameters.FirstOrDefault(p => LabReportEnglishHelper.NormalizeParameterName(p.ParameterName).Equals(name, StringComparison.OrdinalIgnoreCase));
                    if (found != null) rightGroup.Add(found);
                }

                foreach (var p in parameters)
                {
                    if (!leftGroup.Contains(p) && !rightGroup.Contains(p))
                        remaining.Add(p);
                }

                // Balance any additional custom chemical tests between left and right
                for (int i = 0; i < remaining.Count; i++)
                {
                    if (i % 2 == 0) leftGroup.Add(remaining[i]);
                    else rightGroup.Add(remaining[i]);
                }

                col.Item().Row(row =>
                {
                    row.RelativeItem().Element(c => ComposeChemicalSubTable(c, leftGroup));
                    row.ConstantItem(6); // Space between sub-tables
                    row.RelativeItem().Element(c => ComposeChemicalSubTable(c, rightGroup));
                });
            });
        }

        private static void ComposeChemicalSubTable(IContainer container, List<ParameterResultEntryDto> items)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(3.2f); // Parameter Name
                    columns.RelativeColumn(2.6f); // Patient Result
                    columns.RelativeColumn(2.6f); // Normal Value
                });

                table.Header(header =>
                {
                    header.Cell().Border(0.5f).BorderColor(Colors.White).Background("#E8EAEF").PaddingVertical(1.5f);
                    header.Cell().Border(0.5f).BorderColor(Colors.White).Background("#E8EAEF").PaddingVertical(1.5f);
                    header.Cell().Border(0.5f).BorderColor(Colors.White).Background("#1E1E1E").PaddingVertical(1.5f).AlignCenter()
                        .Text("Normal").FontFamily("Arial").Bold().FontSize(8f).FontColor(Colors.White);
                });

                foreach (var p in items)
                {
                    string name = LabReportEnglishHelper.NormalizeParameterName(p.ParameterName);
                    string result = LabReportEnglishHelper.NormalizeResultValue(p.ResultValue);
                    string normal = !string.IsNullOrWhiteSpace(p.FormattedNormalRange) && p.FormattedNormalRange != "-"
                        ? LabReportEnglishHelper.NormalizeNormalRange(p.FormattedNormalRange, p.NormalText)
                        : GetDefaultUrineChemicalNormal(name);

                    table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#E2E4E8").PaddingVertical(2f).PaddingHorizontal(5)
                        .AlignCenter().Text(name).FontFamily("Arial").Bold().FontSize(8f).FontColor("#1A1A1A");

                    table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#F0F1F4").PaddingVertical(2f).PaddingHorizontal(5)
                        .AlignCenter().Text(result).FontFamily("Arial").FontSize(8f).FontColor("#111111");

                    table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#E2E4E8").PaddingVertical(2f).PaddingHorizontal(5)
                        .AlignCenter().Text(normal).FontFamily("Arial").FontSize(8f).FontColor("#333333");
                }
            });
        }

        private static void ComposeUrineMicroscopicSection(
            IContainer container,
            List<ParameterResultEntryDto> microCells,
            List<ParameterResultEntryDto> microOther,
            List<ParameterResultEntryDto> microOrganisms)
        {
            container.Column(col =>
            {
                // Section Header Bar
                col.Item().Row(row =>
                {
                    row.RelativeItem().AlignLeft().Background("#5A5D62").PaddingVertical(2.5f).PaddingHorizontal(8)
                        .Text("-Microscopic Examination").FontFamily("Arial").Bold().FontSize(8.5f).FontColor(Colors.White);

                    row.ConstantItem(120).AlignRight().Background("#1E1E1E").PaddingVertical(2.5f).AlignCenter()
                        .Text("Normal").FontFamily("Arial").Bold().FontSize(8.5f).FontColor(Colors.White);
                });

                // 1. Cells Sub-Block
                if (microCells.Count > 0)
                {
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1.8f); // "Cells" header
                            columns.RelativeColumn(2.0f); // Cell Type (RBCs, Pus, Epithelial)
                            columns.RelativeColumn(2.2f); // Result
                            columns.RelativeColumn(2.0f); // Unit (Cells/H.P.F)
                            columns.RelativeColumn(2.2f); // Normal Range
                        });

                        int rIdx = 0;
                        foreach (var p in microCells)
                        {
                            string name = LabReportEnglishHelper.NormalizeParameterName(p.ParameterName);
                            string result = LabReportEnglishHelper.NormalizeResultValue(p.ResultValue);
                            string unit = !string.IsNullOrWhiteSpace(p.Unit) ? LabReportEnglishHelper.NormalizeUnit(p.Unit) : (name.Contains("Epithelial") ? "" : "Cells/H.P.F");
                            string normal = !string.IsNullOrWhiteSpace(p.FormattedNormalRange) && p.FormattedNormalRange != "-"
                                ? LabReportEnglishHelper.NormalizeNormalRange(p.FormattedNormalRange, p.NormalText)
                                : GetDefaultUrineMicroNormal(name);

                            if (rIdx == 0)
                            {
                                table.Cell().RowSpan((uint)microCells.Count).Border(0.5f).BorderColor(Colors.White).Background("#7A7E85")
                                    .AlignCenter().AlignMiddle().Text("Cells").FontFamily("Arial").Bold().FontSize(8.5f).FontColor(Colors.White);
                            }

                            table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#E2E4E8").PaddingVertical(2.5f).PaddingHorizontal(6)
                                .AlignCenter().Text(name).FontFamily("Arial").Bold().FontSize(8.5f).FontColor("#1A1A1A");

                            table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#F0F1F4").PaddingVertical(2.5f).PaddingHorizontal(6)
                                .AlignCenter().Text(result).FontFamily("Arial").FontSize(8.5f).FontColor("#111111");

                            table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#F0F1F4").PaddingVertical(2.5f).PaddingHorizontal(6)
                                .AlignCenter().Text(unit).FontFamily("Arial").FontSize(8f).FontColor("#555555");

                            table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#E2E4E8").PaddingVertical(2.5f).PaddingHorizontal(6)
                                .AlignCenter().Text(normal).FontFamily("Arial").FontSize(8.5f).FontColor("#333333");

                            rIdx++;
                        }
                    });
                }

                // 2. Other Findings Table (Casts, Amorphous, Crystals)
                if (microOther.Count > 0)
                {
                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2.2f); // Parameter Name
                            columns.RelativeColumn(4.4f); // Result
                            columns.RelativeColumn(2.2f); // Normal
                        });

                        foreach (var p in microOther)
                        {
                            string name = LabReportEnglishHelper.NormalizeParameterName(p.ParameterName);
                            string result = LabReportEnglishHelper.NormalizeResultValue(p.ResultValue);
                            string normal = !string.IsNullOrWhiteSpace(p.FormattedNormalRange) && p.FormattedNormalRange != "-"
                                ? LabReportEnglishHelper.NormalizeNormalRange(p.FormattedNormalRange, p.NormalText)
                                : GetDefaultUrineMicroNormal(name);

                            table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#E2E4E8").PaddingVertical(2.5f).PaddingHorizontal(6)
                                .AlignCenter().Text(name).FontFamily("Arial").Bold().FontSize(8.5f).FontColor("#1A1A1A");

                            table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#F0F1F4").PaddingVertical(2.5f).PaddingHorizontal(6)
                                .AlignCenter().Text(result).FontFamily("Arial").FontSize(8.5f).FontColor("#111111");

                            table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#E2E4E8").PaddingVertical(2.5f).PaddingHorizontal(6)
                                .AlignCenter().Text(normal).FontFamily("Arial").FontSize(8.5f).FontColor("#333333");
                        }
                    });
                }

                // 3. Microorganisms & Others Sub-Block (Matching Reference Image 2)
                if (microOrganisms.Count > 0)
                {
                    var bacteriaAndFungus = microOrganisms.Where(p =>
                    {
                        var norm = LabReportEnglishHelper.NormalizeParameterName(p.ParameterName).ToLowerInvariant();
                        return norm.Contains("bacteria") || norm.Contains("fungus") || norm.Contains("yeast");
                    }).ToList();

                    var othersList = microOrganisms.Except(bacteriaAndFungus).ToList();

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2.2f); // Group Label ("Microorganisms:" / "Others")
                            columns.RelativeColumn(2.2f); // Organism Name (-Bacteria)
                            columns.RelativeColumn(2.2f); // Result (Absent)
                            columns.RelativeColumn(2.2f); // Normal (Absent)
                        });

                        // Render Microorganisms group
                        if (bacteriaAndFungus.Count > 0)
                        {
                            int orgIdx = 0;
                            foreach (var p in bacteriaAndFungus)
                            {
                                string name = LabReportEnglishHelper.NormalizeParameterName(p.ParameterName);
                                if (!name.StartsWith("-")) name = "-" + name;
                                string result = LabReportEnglishHelper.NormalizeResultValue(p.ResultValue);
                                string normal = !string.IsNullOrWhiteSpace(p.FormattedNormalRange) && p.FormattedNormalRange != "-"
                                    ? LabReportEnglishHelper.NormalizeNormalRange(p.FormattedNormalRange, p.NormalText)
                                    : "Absent";

                                if (orgIdx == 0)
                                {
                                    table.Cell().RowSpan((uint)bacteriaAndFungus.Count).Border(0.5f).BorderColor(Colors.White).Background("#E2E4E8")
                                        .AlignCenter().AlignMiddle().Text("Microorganisms:").FontFamily("Arial").Bold().FontSize(8.5f).FontColor("#1A1A1A");
                                }

                                table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#E2E4E8").PaddingVertical(2.5f).PaddingHorizontal(6)
                                    .AlignLeft().Text(name).FontFamily("Arial").Bold().FontSize(8.5f).FontColor("#1A1A1A");

                                table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#F0F1F4").PaddingVertical(2.5f).PaddingHorizontal(6)
                                    .AlignCenter().Text(result).FontFamily("Arial").FontSize(8.5f).FontColor("#111111");

                                table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#E2E4E8").PaddingVertical(2.5f).PaddingHorizontal(6)
                                    .AlignCenter().Text(normal).FontFamily("Arial").FontSize(8.5f).FontColor("#333333");

                                orgIdx++;
                            }
                        }

                        // Render Others group
                        if (othersList.Count > 0)
                        {
                            int otherIdx = 0;
                            foreach (var p in othersList)
                            {
                                string name = LabReportEnglishHelper.NormalizeParameterName(p.ParameterName);
                                if (!name.StartsWith("-")) name = "-" + name;
                                string result = LabReportEnglishHelper.NormalizeResultValue(p.ResultValue);
                                string normal = !string.IsNullOrWhiteSpace(p.FormattedNormalRange) && p.FormattedNormalRange != "-"
                                    ? LabReportEnglishHelper.NormalizeNormalRange(p.FormattedNormalRange, p.NormalText)
                                    : "Absent";

                                if (otherIdx == 0)
                                {
                                    table.Cell().RowSpan((uint)othersList.Count).Border(0.5f).BorderColor(Colors.White).Background("#E2E4E8")
                                        .AlignCenter().AlignMiddle().Text("Others").FontFamily("Arial").Bold().FontSize(8.5f).FontColor("#1A1A1A");
                                }

                                table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#E2E4E8").PaddingVertical(2.5f).PaddingHorizontal(6)
                                    .AlignLeft().Text(name).FontFamily("Arial").Bold().FontSize(8.5f).FontColor("#1A1A1A");

                                table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#F0F1F4").PaddingVertical(2.5f).PaddingHorizontal(6)
                                    .AlignCenter().Text(result).FontFamily("Arial").FontSize(8.5f).FontColor("#111111");

                                table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#E2E4E8").PaddingVertical(2.5f).PaddingHorizontal(6)
                                    .AlignCenter().Text(normal).FontFamily("Arial").FontSize(8.5f).FontColor("#333333");

                                otherIdx++;
                            }
                        }
                    });
                }
            });
        }

        private static void EnsureStandardUrineParameters(
            List<ParameterResultEntryDto> physical,
            List<ParameterResultEntryDto> chemical,
            List<ParameterResultEntryDto> microCells,
            List<ParameterResultEntryDto> microOther,
            List<ParameterResultEntryDto> microOrg,
            List<ParameterResultEntryDto> allParams)
        {
            // Map any existing parameters from allParams by name matching (safely handling duplicate keys)
            var map = new Dictionary<string, ParameterResultEntryDto>(StringComparer.OrdinalIgnoreCase);
            foreach (var p in allParams)
            {
                var key = LabReportEnglishHelper.NormalizeParameterName(p.ParameterName).ToLowerInvariant();
                if (!map.TryGetValue(key, out var existing) || (string.IsNullOrWhiteSpace(existing.ResultValue) && !string.IsNullOrWhiteSpace(p.ResultValue)))
                {
                    map[key] = p;
                }
            }

            // Standard Physical definition
            string[] stdPhysical = { "Volume", "Color", "Odor", "Aspect", "Foam", "Sediment" };
            if (physical.Count == 0 || physical.Count < stdPhysical.Length)
            {
                foreach (var name in stdPhysical)
                {
                    if (!physical.Any(p => LabReportEnglishHelper.NormalizeParameterName(p.ParameterName).Equals(name, StringComparison.OrdinalIgnoreCase)))
                    {
                        if (map.TryGetValue(name.ToLowerInvariant(), out var found))
                            physical.Add(found);
                        else
                            physical.Add(new ParameterResultEntryDto { ParameterName = name, ResultValue = name == "Volume" ? "Random Sample (Midstream urine)" : "Nil" });
                    }
                }
            }

            // Standard Chemical definition (10 parameters)
            string[] stdChemical = { "Reaction (pH)", "Specific gravity", "Leukocyte esterase", "Blood", "Glucose", "Protein", "Ketones", "Nitrite", "Bilirubin", "Urobilinogen" };
            if (chemical.Count == 0 || chemical.Count < stdChemical.Length)
            {
                foreach (var name in stdChemical)
                {
                    if (!chemical.Any(p => LabReportEnglishHelper.NormalizeParameterName(p.ParameterName).Equals(name, StringComparison.OrdinalIgnoreCase)))
                    {
                        if (map.TryGetValue(name.ToLowerInvariant(), out var found))
                            chemical.Add(found);
                        else
                            chemical.Add(new ParameterResultEntryDto { ParameterName = name, ResultValue = name == "Nitrite" ? "Negative" : (name == "Bilirubin" ? "Absent" : (name == "Urobilinogen" ? "Normal" : "Nil")) });
                    }
                }
            }

            // Standard Microscopic Cells (RBCs, Pus, Epithelial cells)
            string[] stdCells = { "RBCs", "Pus", "Epithelial cells" };
            if (microCells.Count == 0 || microCells.Count < stdCells.Length)
            {
                foreach (var name in stdCells)
                {
                    if (!microCells.Any(p => LabReportEnglishHelper.NormalizeParameterName(p.ParameterName).Equals(name, StringComparison.OrdinalIgnoreCase)))
                    {
                        if (map.TryGetValue(name.ToLowerInvariant(), out var found))
                            microCells.Add(found);
                        else
                            microCells.Add(new ParameterResultEntryDto { ParameterName = name, ResultValue = "Nil", Unit = name == "Epithelial cells" ? "" : "Cells/H.P.F" });
                    }
                }
            }

            // Standard Other (Casts, Amorphous, Crystals)
            string[] stdOther = { "Casts", "Amorphous", "Crystals" };
            if (microOther.Count == 0 || microOther.Count < stdOther.Length)
            {
                foreach (var name in stdOther)
                {
                    if (!microOther.Any(p => LabReportEnglishHelper.NormalizeParameterName(p.ParameterName).Equals(name, StringComparison.OrdinalIgnoreCase)))
                    {
                        if (map.TryGetValue(name.ToLowerInvariant(), out var found))
                            microOther.Add(found);
                        else
                            microOther.Add(new ParameterResultEntryDto { ParameterName = name, ResultValue = "Nil" });
                    }
                }
            }

            // Standard Microorganisms (Bacteria, Fungus, Mucus)
            string[] stdOrg = { "Bacteria", "Fungus", "Mucus" };
            if (microOrg.Count == 0 || microOrg.Count < stdOrg.Length)
            {
                foreach (var name in stdOrg)
                {
                    if (!microOrg.Any(p => LabReportEnglishHelper.NormalizeParameterName(p.ParameterName).TrimStart('-').Equals(name, StringComparison.OrdinalIgnoreCase)))
                    {
                        if (map.TryGetValue(name.ToLowerInvariant(), out var found))
                            microOrg.Add(found);
                        else
                            microOrg.Add(new ParameterResultEntryDto { ParameterName = "-" + name, ResultValue = "Absent" });
                    }
                }
            }

            // Move any misplaced microscopic items out of chemical
            for (int i = chemical.Count - 1; i >= 0; i--)
            {
                var sec = LabReportEnglishHelper.ClassifyUrineParameter(chemical[i].ParameterName, chemical[i].CategoryName);
                if (sec != UrineSection.Chemical)
                {
                    if (sec == UrineSection.MicroscopicOther && !microOther.Contains(chemical[i]))
                        microOther.Add(chemical[i]);
                    else if (sec == UrineSection.MicroscopicCells && !microCells.Contains(chemical[i]))
                        microCells.Add(chemical[i]);
                    else if (sec == UrineSection.Microorganisms && !microOrg.Contains(chemical[i]))
                        microOrg.Add(chemical[i]);
                    chemical.RemoveAt(i);
                }
            }

            // Order microCells: RBCs, Pus, Epithelial cells
            string[] cellOrder = { "RBCs", "Pus", "Epithelial" };
            var sortedCells = microCells
                .OrderBy(p =>
                {
                    string norm = LabReportEnglishHelper.NormalizeParameterName(p.ParameterName);
                    int idx = Array.FindIndex(cellOrder, x => norm.StartsWith(x, StringComparison.OrdinalIgnoreCase));
                    return idx >= 0 ? idx : 999;
                })
                .ToList();
            microCells.Clear();
            microCells.AddRange(sortedCells);

            // Order microOther: Casts, Amorphous, Crystals
            string[] otherOrder = { "Casts", "Amorphous", "Crystals" };
            var sortedOther = microOther
                .OrderBy(p =>
                {
                    string norm = LabReportEnglishHelper.NormalizeParameterName(p.ParameterName);
                    int idx = Array.FindIndex(otherOrder, x => norm.StartsWith(x, StringComparison.OrdinalIgnoreCase));
                    return idx >= 0 ? idx : 999;
                })
                .ToList();
            microOther.Clear();
            microOther.AddRange(sortedOther);
        }

        private static string GetDefaultUrinePhysicalNormal(string name) => name switch
        {
            "Color" => "Yellow",
            "Odor" => "Aromatic",
            "Aspect" => "Clear",
            "Foam" => "Nil / Absent",
            "Sediment" => "Nil / Absent",
            _ => "-"
        };

        private static string GetDefaultUrineChemicalNormal(string name) => name switch
        {
            "Reaction (pH)" => "5.5 - 7",
            "Specific gravity" => "1.015 - 1.030",
            "Leukocyte esterase" => "Nil",
            "Blood" => "Nil",
            "Glucose" => "Nil",
            "Protein" => "Nil",
            "Ketones" => "Nil",
            "Nitrite" => "Negative",
            "Bilirubin" => "Absent",
            "Urobilinogen" => "Normal trace",
            _ => "Nil"
        };

        private static string GetDefaultUrineMicroNormal(string name) => name switch
        {
            "RBCs" => "0 - 5",
            "Pus" => "0 - 3",
            "Epithelial cells" => "Nil / Few",
            "Casts" => "0 - 3",
            "Amorphous" => "Nil",
            "Crystals" => "Nil",
            "Bacteria" or "-Bacteria" => "Absent",
            "Fungus" or "-Fungus" => "Absent",
            "Mucus" or "-Mucus" => "Absent",
            _ => "Absent"
        };

        #endregion

        #region 2. COMPLETE BLOOD COUNT REPORT (Reference Image 1)

        private void ComposeCbcReport(IContainer container, ConsolidatedTestDto test)
        {
            container.Column(col =>
            {
                // Top Title Box
                col.Item().AlignCenter().Background("#1E1E1E").PaddingVertical(3.5f).PaddingHorizontal(26)
                    .Text("COMPLETE BLOOD COUNT").FontFamily("Arial").Bold().FontSize(12.5f).FontColor(Colors.White);

                col.Item().PaddingTop(10);

                // Split into: Main CBC parameters, WBC Total, and WBC Differential
                var mainParams = new List<ParameterResultEntryDto>();
                ParameterResultEntryDto? wbcTotal = null;
                var diffParams = new List<ParameterResultEntryDto>();

                foreach (var p in test.Parameters)
                {
                    var section = LabReportEnglishHelper.ClassifyCbcParameter(p.ParameterName);
                    switch (section)
                    {
                        case CbcSection.WbcTotal:
                            wbcTotal = p;
                            break;
                        case CbcSection.WbcDifferential:
                            diffParams.Add(p);
                            break;
                        default:
                            mainParams.Add(p);
                            break;
                    }
                }

                // If default CBC test is loaded from DB with standard items, ensure required order matching Reference Image 1
                EnsureStandardCbcParameters(mainParams, ref wbcTotal, diffParams, test.Parameters);

                // A. Main CBC Table
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(3.4f); // Test details
                        columns.RelativeColumn(1.4f); // Result
                        columns.RelativeColumn(1.4f); // Units
                        columns.RelativeColumn(2.2f); // Normal Ranges
                    });

                    // Header
                    table.Header(header =>
                    {
                        header.Cell().Border(0.5f).BorderColor(Colors.White).Background("#282A2D").PaddingVertical(3.5f).PaddingHorizontal(8)
                            .AlignLeft().Text("Test details").FontFamily("Arial").Bold().FontSize(9f).FontColor(Colors.White);

                        header.Cell().Border(0.5f).BorderColor(Colors.White).Background("#282A2D").PaddingVertical(3.5f).AlignCenter()
                            .Text("Result").FontFamily("Arial").Bold().FontSize(9f).FontColor(Colors.White);

                        header.Cell().Border(0.5f).BorderColor(Colors.White).Background("#282A2D").PaddingVertical(3.5f).AlignCenter()
                            .Text("Units").FontFamily("Arial").Bold().FontSize(9f).FontColor(Colors.White);

                        header.Cell().Border(0.5f).BorderColor(Colors.White).Background("#282A2D").PaddingVertical(3.5f).AlignCenter()
                            .Text("Normal Ranges").FontFamily("Arial").Bold().FontSize(9f).FontColor(Colors.White);
                    });

                    int rIdx = 0;
                    foreach (var p in mainParams)
                    {
                        string name = LabReportEnglishHelper.NormalizeParameterName(p.ParameterName);
                        if (!name.StartsWith("-")) name = "- " + name;

                        string result = !string.IsNullOrWhiteSpace(p.ResultValue) ? p.ResultValue.Trim() : "-";
                        string unit = LabReportEnglishHelper.NormalizeUnit(p.Unit);
                        string normal = !string.IsNullOrWhiteSpace(p.FormattedNormalRange) && p.FormattedNormalRange != "-"
                            ? LabReportEnglishHelper.NormalizeNormalRange(p.FormattedNormalRange, p.NormalText)
                            : GetDefaultCbcNormal(name);

                        // If switching between RBC parameters and Platelet parameters, add subtle divider space
                        bool isPlateletStart = name.Contains("Platelet", StringComparison.OrdinalIgnoreCase) && rIdx > 0 &&
                            !mainParams[rIdx - 1].ParameterName.Contains("Platelet", StringComparison.OrdinalIgnoreCase);

                        if (isPlateletStart)
                        {
                            table.Cell().ColumnSpan(4).Background(Colors.White).PaddingVertical(3);
                        }

                        // Col 1: Test details
                        table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#EAECEF").PaddingVertical(2.5f).PaddingHorizontal(8)
                            .AlignLeft().Text(name).FontFamily("Arial").Bold().FontSize(8.5f).FontColor("#1A1A1A");

                        // Col 2: Result
                        bool isCustomBold = p.IsBold || (p.Notes != null && p.Notes.Contains("bold=1"));
                        string? customColor = !string.IsNullOrWhiteSpace(p.TextColor) ? p.TextColor : null;
                        string fontColor = !string.IsNullOrEmpty(customColor) ? customColor : "#111111";

                        var resText = table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#EAECEF").PaddingVertical(2.5f).AlignCenter()
                            .Text(result).FontFamily("Arial").FontSize(9f).FontColor(fontColor);
                        if (isCustomBold) resText.Bold();

                        // Col 3: Units
                        table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#EAECEF").PaddingVertical(2.5f).AlignCenter()
                            .Text(unit).FontFamily("Arial").FontSize(8.5f).FontColor("#333333");

                        // Col 4: Normal Ranges
                        table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#EAECEF").PaddingVertical(2.5f).AlignCenter()
                            .Text(normal).FontFamily("Arial").FontSize(8.5f).FontColor("#333333");

                        rIdx++;
                    }
                });

                col.Item().PaddingTop(12);

                // B. WBC Differential Section
                col.Item().Element(c => ComposeWbcDifferentialSection(c, wbcTotal, diffParams));
            });
        }

        private static void ComposeWbcDifferentialSection(
            IContainer container,
            ParameterResultEntryDto? wbcTotal,
            List<ParameterResultEntryDto> diffParams)
        {
            container.Column(col =>
            {
                // Top Row: WBC-Leukocytes
                string wbcName = wbcTotal != null ? LabReportEnglishHelper.NormalizeParameterName(wbcTotal.ParameterName) : "WBC-Leukocytes";
                string wbcResult = wbcTotal != null && !string.IsNullOrWhiteSpace(wbcTotal.ResultValue) ? wbcTotal.ResultValue.Trim() : "-";
                string wbcUnit = wbcTotal != null && !string.IsNullOrWhiteSpace(wbcTotal.Unit) ? LabReportEnglishHelper.NormalizeUnit(wbcTotal.Unit) : "10^9/L";
                string wbcNormal = wbcTotal != null && !string.IsNullOrWhiteSpace(wbcTotal.FormattedNormalRange) && wbcTotal.FormattedNormalRange != "-"
                    ? LabReportEnglishHelper.NormalizeNormalRange(wbcTotal.FormattedNormalRange, wbcTotal.NormalText)
                    : "4.50 - 11.50";

                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(2.6f); // WBC-Leukocytes
                        columns.RelativeColumn(1.6f); // Result
                        columns.RelativeColumn(1.6f); // Unit
                        columns.RelativeColumn(2.6f); // Normal
                    });

                    // Mini headers
                    table.Header(header =>
                    {
                        header.Cell().Border(0.5f).BorderColor(Colors.White).Background("#F8F9FA").PaddingVertical(1.5f);
                        header.Cell().Border(0.5f).BorderColor(Colors.White).Background("#5A5D62").PaddingVertical(1.5f).AlignCenter()
                            .Text("Result").FontFamily("Arial").Bold().FontSize(8f).FontColor(Colors.White);
                        header.Cell().Border(0.5f).BorderColor(Colors.White).Background("#5A5D62").PaddingVertical(1.5f).AlignCenter()
                            .Text("Unit").FontFamily("Arial").Bold().FontSize(8f).FontColor(Colors.White);
                        header.Cell().Border(0.5f).BorderColor(Colors.White).Background("#5A5D62").PaddingVertical(1.5f).AlignCenter()
                            .Text("Normal").FontFamily("Arial").Bold().FontSize(8f).FontColor(Colors.White);
                    });

                    // Data row
                    table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#484B50").PaddingVertical(3f).PaddingHorizontal(8)
                        .AlignCenter().Text(wbcName).FontFamily("Arial").Bold().FontSize(8.5f).FontColor(Colors.White);

                    table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#EAECEF").PaddingVertical(3f).AlignCenter()
                        .Text(wbcResult).FontFamily("Arial").Bold().FontSize(9f).FontColor("#111111");

                    table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#EAECEF").PaddingVertical(3f).AlignCenter()
                        .Text(wbcUnit).FontFamily("Arial").FontSize(8.5f).FontColor("#333333");

                    table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#EAECEF").PaddingVertical(3f).AlignCenter()
                        .Text(wbcNormal).FontFamily("Arial").FontSize(8.5f).FontColor("#333333");
                });

                col.Item().PaddingTop(4);

                // Grouped Table: -WBC differential
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(2.6f); // -WBC differential
                        columns.RelativeColumn(1.3f); // Relative Result
                        columns.RelativeColumn(1.5f); // Relative Normal
                        columns.RelativeColumn(1.3f); // Absolute Result
                        columns.RelativeColumn(1.7f); // Absolute Normal
                    });

                    // Level 1 Header
                    table.Cell().RowSpan(2).Border(0.5f).BorderColor(Colors.White).Background("#484B50").PaddingVertical(3f).PaddingHorizontal(6)
                        .AlignLeft().AlignMiddle().Text("-WBC differential").FontFamily("Arial").Bold().FontSize(8.5f).FontColor(Colors.White);

                    table.Cell().ColumnSpan(2).Border(0.5f).BorderColor(Colors.White).Background("#5A5D62").PaddingVertical(2f).AlignCenter()
                        .Text("Relative count").FontFamily("Arial").Bold().FontSize(8f).FontColor(Colors.White);

                    table.Cell().ColumnSpan(2).Border(0.5f).BorderColor(Colors.White).Background("#5A5D62").PaddingVertical(2f).AlignCenter()
                        .Text("Absolute count").FontFamily("Arial").Bold().FontSize(8f).FontColor(Colors.White);

                    // Level 2 Sub-Headers
                    table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#6C7077").PaddingVertical(1.5f).AlignCenter()
                        .Text("Result").FontFamily("Arial").Bold().FontSize(7.5f).FontColor(Colors.White);
                    table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#6C7077").PaddingVertical(1.5f).AlignCenter()
                        .Text("Normal").FontFamily("Arial").Bold().FontSize(7.5f).FontColor(Colors.White);
                    table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#6C7077").PaddingVertical(1.5f).AlignCenter()
                        .Text("Result").FontFamily("Arial").Bold().FontSize(7.5f).FontColor(Colors.White);
                    table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#6C7077").PaddingVertical(1.5f).AlignCenter()
                        .Text("Normal").FontFamily("Arial").Bold().FontSize(7.5f).FontColor(Colors.White);

                    // Map Differential rows: LYM %, MID %, GRA %
                    var diffRows = BuildWbcDifferentialRows(wbcResult, diffParams);

                    foreach (var rowItem in diffRows)
                    {
                        table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#EAECEF").PaddingVertical(2.5f).PaddingHorizontal(6)
                            .AlignLeft().Text(rowItem.Name).FontFamily("Arial").Bold().FontSize(8.5f).FontColor("#1A1A1A");

                        table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#EAECEF").PaddingVertical(2.5f).AlignCenter()
                            .Text(rowItem.RelResult).FontFamily("Arial").Bold().FontSize(8.5f).FontColor("#111111");

                        table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#EAECEF").PaddingVertical(2.5f).AlignCenter()
                            .Text(rowItem.RelNormal).FontFamily("Arial").FontSize(8f).FontColor("#333333");

                        table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#EAECEF").PaddingVertical(2.5f).AlignCenter()
                            .Text(rowItem.AbsResult).FontFamily("Arial").Bold().FontSize(8.5f).FontColor("#111111");

                        table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#EAECEF").PaddingVertical(2.5f).AlignCenter()
                            .Text(rowItem.AbsNormal).FontFamily("Arial").FontSize(8f).FontColor("#333333");
                    }
                });
            });
        }

        private class WbcDiffRowModel
        {
            public string Name { get; set; } = "";
            public string RelResult { get; set; } = "-";
            public string RelNormal { get; set; } = "-";
            public string AbsResult { get; set; } = "-";
            public string AbsNormal { get; set; } = "-";
        }

        private static List<WbcDiffRowModel> BuildWbcDifferentialRows(string wbcResultStr, List<ParameterResultEntryDto> diffParams)
        {
            var list = new List<WbcDiffRowModel>();
            double.TryParse(wbcResultStr, out double wbcTotalNum);

            // Target standard differential rows: LYM %, MID %, GRA %
            string[] stdKeys = { "LYM", "MID", "GRA" };

            foreach (var key in stdKeys)
            {
                var relP = diffParams.FirstOrDefault(p =>
                {
                    string n = p.ParameterName.ToUpperInvariant();
                    return n.Contains(key) && (n.Contains("%") || (!n.Contains("#") && !n.Contains("ABS")));
                });

                var absP = diffParams.FirstOrDefault(p =>
                {
                    string n = p.ParameterName.ToUpperInvariant();
                    return n.Contains(key) && (n.Contains("#") || n.Contains("ABS"));
                });

                string rowName = key switch
                {
                    "LYM" => "-LYM %",
                    "MID" => "-MID %",
                    "GRA" => "-GRA %",
                    _ => $"-{key} %"
                };

                string relRes = "-";
                string relNorm = key switch
                {
                    "LYM" => "19.1 - 48.5",
                    "MID" => "4.5 - 12.1",
                    "GRA" => "43.6 - 73.4",
                    _ => "-"
                };

                if (relP != null && !string.IsNullOrWhiteSpace(relP.ResultValue))
                {
                    relRes = relP.ResultValue.Trim();
                    if (!relRes.EndsWith("%") && double.TryParse(relRes, out _))
                        relRes += " %";

                    if (!string.IsNullOrWhiteSpace(relP.FormattedNormalRange) && relP.FormattedNormalRange != "-")
                        relNorm = relP.FormattedNormalRange;
                }

                string absRes = "-";
                string absNorm = key switch
                {
                    "LYM" => "1.30 - 4.0 (10^9/l)",
                    "MID" => "0.30 - 1.0 (10^9/l)",
                    "GRA" => "1.00 - 7.60(10^9/l)",
                    _ => "-"
                };

                if (absP != null && !string.IsNullOrWhiteSpace(absP.ResultValue))
                {
                    absRes = absP.ResultValue.Trim();
                    if (!absRes.Contains("10^9") && double.TryParse(absRes, out _))
                        absRes += " 10^9/l";

                    if (!string.IsNullOrWhiteSpace(absP.FormattedNormalRange) && absP.FormattedNormalRange != "-")
                        absNorm = absP.FormattedNormalRange;
                }
                else if (wbcTotalNum > 0 && relP != null && double.TryParse(relP.ResultValue?.Replace("%", "").Trim(), out double relNum))
                {
                    // Calculate absolute count dynamically from (WBC * %)/100
                    double calculatedAbs = Math.Round((wbcTotalNum * relNum) / 100.0, 2);
                    absRes = $"{calculatedAbs:F1} 10^9/l";
                }

                list.Add(new WbcDiffRowModel
                {
                    Name = rowName,
                    RelResult = relRes,
                    RelNormal = relNorm,
                    AbsResult = absRes,
                    AbsNormal = absNorm
                });
            }

            // Include any additional differential parameters present in the test (e.g. 5-part differential)
            foreach (var extra in diffParams)
            {
                string en = extra.ParameterName.ToUpperInvariant();
                if (en.Contains("LYM") || en.Contains("MID") || en.Contains("GRA"))
                    continue;

                list.Add(new WbcDiffRowModel
                {
                    Name = "-" + LabReportEnglishHelper.NormalizeParameterName(extra.ParameterName),
                    RelResult = extra.ResultValue,
                    RelNormal = extra.FormattedNormalRange,
                    AbsResult = "-",
                    AbsNormal = "-"
                });
            }

            return list;
        }

        private static void EnsureStandardCbcParameters(
            List<ParameterResultEntryDto> mainParams,
            ref ParameterResultEntryDto? wbcTotal,
            List<ParameterResultEntryDto> diffParams,
            List<ParameterResultEntryDto> allParams)
        {
            var map = new Dictionary<string, ParameterResultEntryDto>(StringComparer.OrdinalIgnoreCase);
            foreach (var p in allParams)
            {
                var key = LabReportEnglishHelper.NormalizeParameterName(p.ParameterName).ToLowerInvariant();
                if (!map.TryGetValue(key, out var existing) || (string.IsNullOrWhiteSpace(existing.ResultValue) && !string.IsNullOrWhiteSpace(p.ResultValue)))
                {
                    map[key] = p;
                }
            }

            string[] stdMainList = {
                "Hemoglobin", "RBC-Erythrocytes", "Hct-Hematocrit", "MCV", "MCH", "MCHC", "RDWs", "RDWc",
                "Platelet", "PCT-Plateletcrit", "PDWs", "PDWc", "MPV", "P-LCC", "P-LCR"
            };

            // Order and deduplicate main parameters matching Reference Image 1
            var newMain = new List<ParameterResultEntryDto>();
            var seenKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var name in stdMainList)
            {
                string key = name.ToLowerInvariant();
                seenKeys.Add(key);
                if (map.TryGetValue(key, out var found))
                {
                    newMain.Add(found);
                }
                else
                {
                    newMain.Add(new ParameterResultEntryDto
                    {
                        ParameterName = name,
                        Unit = GetDefaultCbcUnit(name),
                        FormattedNormalRange = GetDefaultCbcNormal(name),
                        ResultValue = ""
                    });
                }
            }

            // Append any additional custom CBC parameters from mainParams that aren't part of standard list
            foreach (var p in mainParams)
            {
                string key = LabReportEnglishHelper.NormalizeParameterName(p.ParameterName).ToLowerInvariant();
                if (seenKeys.Add(key))
                {
                    newMain.Add(p);
                }
            }

            mainParams.Clear();
            mainParams.AddRange(newMain);

            if (wbcTotal == null)
            {
                if (map.TryGetValue("wbc-leukocytes", out var w1)) wbcTotal = w1;
                else if (map.TryGetValue("wbc", out var w2)) wbcTotal = w2;
                else
                {
                    wbcTotal = new ParameterResultEntryDto
                    {
                        ParameterName = "WBC-Leukocytes",
                        Unit = "10^9/L",
                        FormattedNormalRange = "4.50 - 11.50",
                        ResultValue = ""
                    };
                }
            }
        }

        private static string GetDefaultCbcUnit(string name)
        {
            string clean = name.TrimStart('-', ' ').ToUpperInvariant();
            if (clean.Contains("HEMOGLOBIN") || clean.Contains("MCHC")) return "g/dl";
            if (clean.Contains("RBC")) return "10^12/L";
            if (clean.Contains("HCT") || clean.Contains("RDWC") || clean.Contains("PCT") || clean.Contains("PDWC") || clean.Contains("P-LCR") || clean.Contains("%")) return "%";
            if (clean.Contains("MCV") || clean.Contains("RDWS") || clean.Contains("PDWS") || clean.Contains("MPV")) return "fl";
            if (clean.Contains("MCH")) return "pg";
            if (clean.Contains("PLATELET") || clean.Contains("P-LCC") || clean.Contains("WBC")) return "10^9/L";
            return "";
        }

        private static string GetDefaultCbcNormal(string name)
        {
            string clean = name.TrimStart('-', ' ').ToUpperInvariant();
            if (clean.Contains("HEMOGLOBIN")) return "13.5 - 17.5";
            if (clean.Contains("RBC")) return "4.50 - 5.90";
            if (clean.Contains("HCT")) return "36.0 - 53.0";
            if (clean.Contains("MCV")) return "80 - 100";
            if (clean.Contains("MCH")) return "27.5 - 32.4";
            if (clean.Contains("MCHC")) return "31.7 - 34.2";
            if (clean.Contains("RDWS")) return "36.2 - 49.7";
            if (clean.Contains("RDWC")) return "11.1 - 14";
            if (clean.Contains("PLATELET")) return "150 - 450";
            if (clean.Contains("PCT")) return "0.16 - 0.36";
            if (clean.Contains("PDWS")) return "11.1 - 19.7";
            if (clean.Contains("PDWC")) return "37.8 - 43.6";
            if (clean.Contains("MPV")) return "8.3 - 12.1";
            if (clean.Contains("P-LCC")) return "55 - 139";
            if (clean.Contains("P-LCR")) return "25.3 - 53.8";
            if (clean.Contains("WBC")) return "4.50 - 11.50";
            return "-";
        }

        #endregion

        #region 3. GENERAL LABORATORY REPORT

        private void ComposeGeneralLabTest(IContainer container, ConsolidatedTestDto test)
        {
            string title = LabReportEnglishHelper.NormalizeTestName(test.TestName);

            container.Column(col =>
            {
                // Top Title Box
                col.Item().AlignCenter().Background("#1E1E1E").PaddingVertical(3.5f).PaddingHorizontal(24)
                    .Text(title).FontFamily("Arial").Bold().FontSize(12.5f).FontColor(Colors.White);

                col.Item().PaddingTop(12);

                // Group by category if present
                var groupedParams = test.Parameters
                    .GroupBy(p => !string.IsNullOrWhiteSpace(p.CategoryName) ? p.CategoryName.Trim() : "")
                    .ToList();

                foreach (var group in groupedParams)
                {
                    if (!string.IsNullOrWhiteSpace(group.Key))
                    {
                        string catTitle = LabReportEnglishHelper.NormalizeParameterName(group.Key);
                        col.Item().PaddingTop(6).Background("#5A5D62").PaddingVertical(2.5f).PaddingHorizontal(8)
                            .Text(catTitle).FontFamily("Arial").Bold().FontSize(8.5f).FontColor(Colors.White);
                    }

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3.4f); // Test details
                            columns.RelativeColumn(1.6f); // Result
                            columns.RelativeColumn(1.4f); // Units
                            columns.RelativeColumn(2.4f); // Normal Ranges
                        });

                        // Header
                        table.Header(header =>
                        {
                            header.Cell().Border(0.5f).BorderColor(Colors.White).Background("#282A2D").PaddingVertical(3.5f).PaddingHorizontal(8)
                                .AlignLeft().Text("Test details").FontFamily("Arial").Bold().FontSize(9f).FontColor(Colors.White);

                            header.Cell().Border(0.5f).BorderColor(Colors.White).Background("#282A2D").PaddingVertical(3.5f).AlignCenter()
                                .Text("Result").FontFamily("Arial").Bold().FontSize(9f).FontColor(Colors.White);

                            header.Cell().Border(0.5f).BorderColor(Colors.White).Background("#282A2D").PaddingVertical(3.5f).AlignCenter()
                                .Text("Units").FontFamily("Arial").Bold().FontSize(9f).FontColor(Colors.White);

                            header.Cell().Border(0.5f).BorderColor(Colors.White).Background("#282A2D").PaddingVertical(3.5f).AlignCenter()
                                .Text("Normal Ranges").FontFamily("Arial").Bold().FontSize(9f).FontColor(Colors.White);
                        });

                        foreach (var p in group)
                        {
                            string name = LabReportEnglishHelper.NormalizeParameterName(p.ParameterName);
                            if (!name.StartsWith("-")) name = "- " + name;

                            string result = LabReportEnglishHelper.NormalizeResultValue(p.ResultValue);
                            string unit = LabReportEnglishHelper.NormalizeUnit(p.Unit);
                            string normal = LabReportEnglishHelper.NormalizeNormalRange(p.FormattedNormalRange, p.NormalText);

                            bool isAbnormal = p.Flag == AbnormalFlagEnum.High || p.Flag == AbnormalFlagEnum.Low || p.Flag == AbnormalFlagEnum.Abnormal;
                            bool isCustomBold = p.IsBold || (p.Notes != null && p.Notes.Contains("bold=1"));
                            string? customColor = !string.IsNullOrWhiteSpace(p.TextColor) ? p.TextColor : null;
                            string fontColor = !string.IsNullOrEmpty(customColor) ? customColor : (isAbnormal ? Colors.Red.Darken2 : "#111111");

                            // Col 1: Test details
                            table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#EAECEF").PaddingVertical(2.5f).PaddingHorizontal(8)
                                .AlignLeft().Text(name).FontFamily("Arial").Bold().FontSize(8.5f).FontColor("#1A1A1A");

                            // Col 2: Result
                            var resCell = table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#EAECEF").PaddingVertical(2.5f).AlignCenter();
                            var resText = resCell.Text(result).FontFamily("Arial").FontSize(9f).FontColor(fontColor);
                            if (isAbnormal || isCustomBold) resText.Bold();

                            // Col 3: Units
                            table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#EAECEF").PaddingVertical(2.5f).AlignCenter()
                                .Text(unit).FontFamily("Arial").FontSize(8.5f).FontColor("#333333");

                            // Col 4: Normal Ranges
                            table.Cell().Border(0.5f).BorderColor(Colors.White).Background("#EAECEF").PaddingVertical(2.5f).AlignCenter()
                                .Text(normal).FontFamily("Arial").FontSize(8.5f).FontColor("#333333");
                        }
                    });
                }
            });
        }

        #endregion
    }
}
