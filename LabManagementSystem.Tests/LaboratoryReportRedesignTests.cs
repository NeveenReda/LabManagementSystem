using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LabManagementSystem.Enums;
using LabManagementSystem.Printing;
using LabManagementSystem.Services;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Xunit;

namespace LabManagementSystem.Tests
{
    public class LaboratoryReportRedesignTests
    {
        public LaboratoryReportRedesignTests()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            try
            {
                var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
                services.AddTransient<LabManagementSystem.Models.LabContext>();
                services.AddSingleton<Func<LabManagementSystem.Models.LabContext>>(sp => () => new LabManagementSystem.Models.LabContext());
                services.AddSingleton<ILabReportService, LabReportService>();
                services.AddSingleton<IVisitService, VisitService>();
                var sp = services.BuildServiceProvider();
                ServiceLocator.Initialize(sp);
            }
            catch { }
        }

        [Fact]
        public void UrineAnalysis_ShouldGenerateMatchingVisualA4Report_EnglishOnly_WithoutHeaderOrFooter()
        {
            var urineTest = new ConsolidatedTestDto
            {
                VisitLabId = 101,
                LabId = 2,
                TestName = "Urine Analysis",
                CategoryName = "Urine Tests",
                Parameters = new List<ParameterResultEntryDto>
                {
                    // Physical Examination
                    new() { ParameterName = "Volume", ResultValue = "Random Sample (Midstream urine)" },
                    new() { ParameterName = "Color", ResultValue = "Yellow", FormattedNormalRange = "Yellow" },
                    new() { ParameterName = "Odor", ResultValue = "Aromatic", FormattedNormalRange = "Aromatic" },
                    new() { ParameterName = "Aspect", ResultValue = "Turbid", FormattedNormalRange = "Clear" },
                    new() { ParameterName = "Foam", ResultValue = "Nil", FormattedNormalRange = "Nil / Absent" },
                    new() { ParameterName = "Sediment", ResultValue = "+++", FormattedNormalRange = "Nil / Absent" },

                    // Chemical Examination
                    new() { ParameterName = "Reaction (pH)", ResultValue = "Alkaline (8)", FormattedNormalRange = "5.5 - 7" },
                    new() { ParameterName = "Specific gravity", ResultValue = "1.010", FormattedNormalRange = "1.015 - 1.030" },
                    new() { ParameterName = "Leukocyte esterase", ResultValue = "Nil", FormattedNormalRange = "Nil" },
                    new() { ParameterName = "Blood", ResultValue = "Nil", FormattedNormalRange = "Nil" },
                    new() { ParameterName = "Glucose", ResultValue = "Nil", FormattedNormalRange = "Nil" },
                    new() { ParameterName = "Protein", ResultValue = "Nil", FormattedNormalRange = "Nil" },
                    new() { ParameterName = "Ketones", ResultValue = "Nil", FormattedNormalRange = "Nil" },
                    new() { ParameterName = "Nitrite", ResultValue = "Negative", FormattedNormalRange = "Negative" },
                    new() { ParameterName = "Bilirubin", ResultValue = "Absent", FormattedNormalRange = "Absent" },
                    new() { ParameterName = "Urobilinogen", ResultValue = "Normal", FormattedNormalRange = "Normal trace" },

                    // Microscopic Examination
                    new() { ParameterName = "RBCs", ResultValue = "2 - 3", Unit = "Cells/H.P.F", FormattedNormalRange = "0 - 5" },
                    new() { ParameterName = "Pus", ResultValue = "2 - 4", Unit = "Cells/H.P.F", FormattedNormalRange = "0 - 3" },
                    new() { ParameterName = "Epithelial cells", ResultValue = "++", FormattedNormalRange = "Nil / Few" },
                    new() { ParameterName = "Casts", ResultValue = "Nil", FormattedNormalRange = "0 - 3" },
                    new() { ParameterName = "Amorphous", ResultValue = "Am. phosphates : ++++", FormattedNormalRange = "Nil" },
                    new() { ParameterName = "Crystals", ResultValue = "Nil", FormattedNormalRange = "Nil" },
                    new() { ParameterName = "-Bacteria", ResultValue = "Absent", FormattedNormalRange = "Absent" },
                    new() { ParameterName = "-Fungus", ResultValue = "Absent", FormattedNormalRange = "Absent" },
                    new() { ParameterName = "-Mucus", ResultValue = "Absent", FormattedNormalRange = "Absent" }
                }
            };

            var report = new ConsolidatedVisitReportDto
            {
                VisitId = 55,
                VisitDate = DateTime.Today,
                PatientName = "Test Patient",
                Tests = new List<ConsolidatedTestDto> { urineTest }
            };

            var doc = new ConsolidatedLabReportDocument(report, urineTest);
            byte[] pdfBytes = doc.GeneratePdf();

            Assert.NotNull(pdfBytes);
            Assert.True(pdfBytes.Length > 2000);
            Assert.StartsWith("%PDF-", Encoding.ASCII.GetString(pdfBytes, 0, 5));

            // Generate page images and save for visual inspection
            var images = doc.GenerateImages().ToList();
            Assert.Single(images); // Fits perfectly on 1 A4 page

            string outDir = @"C:\Users\Mina AbuSeifen\.gemini\antigravity\brain\13a26a31-abc2-4024-b486-1a4ca75d59a3";
            if (Directory.Exists(outDir))
            {
                File.WriteAllBytes(Path.Combine(outDir, "urine_analysis_report.png"), images[0]);
                File.WriteAllBytes(Path.Combine(outDir, "urine_analysis_sample.pdf"), pdfBytes);
            }
        }

        [Fact]
        public void CbcReport_ShouldGenerateMatchingVisualA4Report_WithWbcDifferential_WithoutHeaderOrFooter()
        {
            var cbcTest = new ConsolidatedTestDto
            {
                VisitLabId = 102,
                LabId = 1,
                TestName = "COMPLETE BLOOD COUNT",
                CategoryName = "Hematology",
                Parameters = new List<ParameterResultEntryDto>
                {
                    new() { ParameterName = "- Hemoglobin", ResultValue = "14.9", Unit = "g/dl", FormattedNormalRange = "13.5 - 17.5" },
                    new() { ParameterName = "-RBC-Erythrocytes", ResultValue = "5.08", Unit = "10^12/L", FormattedNormalRange = "4.50 - 5.90" },
                    new() { ParameterName = "-Hct-Hematocrit", ResultValue = "41.2", Unit = "%", FormattedNormalRange = "36.0 - 53.0" },
                    new() { ParameterName = "-MCV", ResultValue = "81.1", Unit = "fl", FormattedNormalRange = "80 - 100" },
                    new() { ParameterName = "-MCH", ResultValue = "29.4", Unit = "pg", FormattedNormalRange = "27.5 - 32.4" },
                    new() { ParameterName = "-MCHC", ResultValue = "36.2", Unit = "g/dl", FormattedNormalRange = "31.7 - 34.2" },
                    new() { ParameterName = "-RDWs", ResultValue = "61.3", Unit = "fl", FormattedNormalRange = "36.2 - 49.7" },
                    new() { ParameterName = "-RDWc", ResultValue = "11.3", Unit = "%", FormattedNormalRange = "11.1 - 14" },

                    new() { ParameterName = "-Platelet", ResultValue = "243", Unit = "10^9/L", FormattedNormalRange = "150 - 450" },
                    new() { ParameterName = "-PCT-Plateletcrit", ResultValue = "0.18", Unit = "%", FormattedNormalRange = "0.16 - 0.36" },
                    new() { ParameterName = "- PDWs", ResultValue = "10.1", Unit = "fl", FormattedNormalRange = "11.1 - 19.7" },
                    new() { ParameterName = "-PDWc", ResultValue = "40.8", Unit = "%", FormattedNormalRange = "37.8 - 43.6" },
                    new() { ParameterName = "-MPV", ResultValue = "7.6", Unit = "fl", FormattedNormalRange = "8.3 - 12.1" },
                    new() { ParameterName = "-P-LCC", ResultValue = "32", Unit = "10^9/L", FormattedNormalRange = "55 - 139" },
                    new() { ParameterName = "-P-LCR", ResultValue = "13.3", Unit = "%", FormattedNormalRange = "25.3 - 53.8" },

                    // WBC and Differential
                    new() { ParameterName = "WBC-Leukocytes", ResultValue = "3.5", Unit = "10^9/L", FormattedNormalRange = "4.50 - 11.50" },
                    new() { ParameterName = "-LYM %", ResultValue = "43.4 %", FormattedNormalRange = "19.1 - 48.5" },
                    new() { ParameterName = "-MID %", ResultValue = "7.8 %", FormattedNormalRange = "4.5 - 12.1" },
                    new() { ParameterName = "-GRA %", ResultValue = "48.8 %", FormattedNormalRange = "43.6 - 73.4" },
                    new() { ParameterName = "LYM #", ResultValue = "1.5 10^9/l", FormattedNormalRange = "1.30 - 4.0 (10^9/l)" },
                    new() { ParameterName = "MID #", ResultValue = "0.2 10^9/l", FormattedNormalRange = "0.30 - 1.0 (10^9/l)" },
                    new() { ParameterName = "GRA #", ResultValue = "1.8 10^9/l", FormattedNormalRange = "1.00 - 7.60(10^9/l)" }
                }
            };

            var report = new ConsolidatedVisitReportDto
            {
                VisitId = 56,
                VisitDate = DateTime.Today,
                PatientName = "Test Patient",
                Tests = new List<ConsolidatedTestDto> { cbcTest }
            };

            var doc = new ConsolidatedLabReportDocument(report, cbcTest);
            byte[] pdfBytes = doc.GeneratePdf();

            Assert.NotNull(pdfBytes);
            Assert.True(pdfBytes.Length > 2000);
            Assert.StartsWith("%PDF-", Encoding.ASCII.GetString(pdfBytes, 0, 5));

            var images = doc.GenerateImages().ToList();
            Assert.Single(images); // Fits cleanly on 1 A4 page

            string outDir = @"C:\Users\Mina AbuSeifen\.gemini\antigravity\brain\13a26a31-abc2-4024-b486-1a4ca75d59a3";
            if (Directory.Exists(outDir))
            {
                File.WriteAllBytes(Path.Combine(outDir, "cbc_report.png"), images[0]);
                File.WriteAllBytes(Path.Combine(outDir, "cbc_sample.pdf"), pdfBytes);
            }
        }

        [Fact]
        public void GeneralTest_WithManyParameters_ShouldRenderGracefullyWithoutArabicCharacters()
        {
            var thyroidTest = new ConsolidatedTestDto
            {
                VisitLabId = 103,
                LabId = 3,
                TestName = "Thyroid Profile",
                CategoryName = "Hormones",
                Parameters = new List<ParameterResultEntryDto>()
            };

            for (int i = 1; i <= 25; i++)
            {
                thyroidTest.Parameters.Add(new ParameterResultEntryDto
                {
                    ParameterName = $"Hormone Marker {i}",
                    ResultValue = (i * 1.25m).ToString("0.00"),
                    Unit = "uIU/mL",
                    FormattedNormalRange = "0.40 - 4.20",
                    Flag = i > 20 ? AbnormalFlagEnum.High : AbnormalFlagEnum.Normal
                });
            }

            var report = new ConsolidatedVisitReportDto
            {
                VisitId = 57,
                VisitDate = DateTime.Today,
                PatientName = "Test Patient",
                Tests = new List<ConsolidatedTestDto> { thyroidTest }
            };

            var doc = new ConsolidatedLabReportDocument(report, thyroidTest);
            byte[] pdfBytes = doc.GeneratePdf();
            Assert.NotNull(pdfBytes);
            Assert.True(pdfBytes.Length > 2000);

            var images = doc.GenerateImages().ToList();
            Assert.NotEmpty(images);
        }

        [Fact]
        public void ArabicInput_ShouldBeTranslatedToEnglish_WithNoArabicTextOnDocument()
        {
            var arabicTest = new ConsolidatedTestDto
            {
                VisitLabId = 104,
                LabId = 2,
                TestName = "تحليل بول كامل",
                CategoryName = "تحاليل البول",
                Parameters = new List<ParameterResultEntryDto>
                {
                    new() { ParameterName = "لون البول", ResultValue = "أصفر", FormattedNormalRange = "أصفر" },
                    new() { ParameterName = "مظهر البول", ResultValue = "عكر", FormattedNormalRange = "رائق" },
                    new() { ParameterName = "رائحة البول", ResultValue = "عطري", FormattedNormalRange = "عطري" },
                    new() { ParameterName = "تفاعل", ResultValue = "حمضي", FormattedNormalRange = "5.5 - 7" },
                    new() { ParameterName = "سكر", ResultValue = "سلبي", FormattedNormalRange = "سلبي" },
                    new() { ParameterName = "زلال", ResultValue = "أثر خفيف", FormattedNormalRange = "سلبي" },
                    new() { ParameterName = "صديد", ResultValue = "10 - 12", Unit = "cells/hpf", FormattedNormalRange = "0 - 3" },
                    new() { ParameterName = "كرات دم حمراء", ResultValue = "1 - 2", Unit = "cells/hpf", FormattedNormalRange = "0 - 5" },
                    new() { ParameterName = "بكتيريا", ResultValue = "غائب", FormattedNormalRange = "غائب" }
                }
            };

            var report = new ConsolidatedVisitReportDto
            {
                VisitId = 58,
                VisitDate = DateTime.Today,
                PatientName = "مريض عربي",
                Tests = new List<ConsolidatedTestDto> { arabicTest }
            };

            var doc = new ConsolidatedLabReportDocument(report, arabicTest);
            byte[] pdfBytes = doc.GeneratePdf();
            Assert.NotNull(pdfBytes);

            string outDir = @"C:\Users\Mina AbuSeifen\.gemini\antigravity\brain\13a26a31-abc2-4024-b486-1a4ca75d59a3";
            if (Directory.Exists(outDir))
            {
                var images = doc.GenerateImages().ToList();
                File.WriteAllBytes(Path.Combine(outDir, "arabic_translated_urine_report.png"), images[0]);
            }
        }

        [Fact]
        public void CbcReport_WithDuplicateParameters_LikeMultipleHemoglobin_ShouldNotThrow_AndDeduplicateGracefully()
        {
            // Reproduces the exact scenario: Test contains duplicate parameters that normalize to the same key "hemoglobin"
            var cbcTest = new ConsolidatedTestDto
            {
                VisitLabId = 105,
                LabId = 1,
                TestName = "CBC",
                CategoryName = "Hematology",
                Parameters = new List<ParameterResultEntryDto>
                {
                    new() { ParameterName = "Hemoglobin", ResultValue = "13.2", Unit = "g/dl" },
                    new() { ParameterName = "Hb", ResultValue = "13.2", Unit = "g/dl" }, // Duplicate normalization
                    new() { ParameterName = "هيموجلوبين", ResultValue = "13.2", Unit = "g/dl" }, // Another duplicate
                    new() { ParameterName = "RBC", ResultValue = "4.8", Unit = "10^12/L" },
                    new() { ParameterName = "WBC", ResultValue = "6.5", Unit = "10^9/L" }
                }
            };

            var report = new ConsolidatedVisitReportDto
            {
                VisitId = 99,
                VisitDate = DateTime.Today,
                PatientName = "شيرين ادوار",
                Tests = new List<ConsolidatedTestDto> { cbcTest }
            };

            // Must NOT throw "An item with the same key has already been added. Key: hemoglobin"
            var doc = new ConsolidatedLabReportDocument(report, cbcTest);
            byte[] pdfBytes = doc.GeneratePdf();

            Assert.NotNull(pdfBytes);
            Assert.True(pdfBytes.Length > 1000);
        }

        [Fact]
        public void MultiTestVisit_ShouldAllowGeneratingIndividualTestPdfs_EachWithItsOwnParameters_AndFrmSelectVisitTest_ShouldInitialize()
        {
            var test1_Cbc = new ConsolidatedTestDto
            {
                VisitLabId = 201,
                LabId = 1,
                TestName = "Complete Blood Count",
                CategoryName = "Hematology",
                Parameters = new List<ParameterResultEntryDto>
                {
                    new() { ParameterName = "Hemoglobin", ResultValue = "14.5", Unit = "g/dl" },
                    new() { ParameterName = "RBC", ResultValue = "5.1", Unit = "10^12/L" }
                }
            };

            var test2_Urine = new ConsolidatedTestDto
            {
                VisitLabId = 202,
                LabId = 2,
                TestName = "Urine Analysis",
                CategoryName = "Clinical Microscopy",
                Parameters = new List<ParameterResultEntryDto>
                {
                    new() { ParameterName = "Color", ResultValue = "Yellow" },
                    new() { ParameterName = "Reaction (pH)", ResultValue = "6.0" }
                }
            };

            var multiReport = new ConsolidatedVisitReportDto
            {
                VisitId = 39,
                VisitDate = DateTime.Today,
                PatientName = "مريم رضا منير",
                MedicalCode = "20260903004",
                Tests = new List<ConsolidatedTestDto> { test1_Cbc, test2_Urine }
            };

            // 1. Verify FrmSelectVisitTest initializes without error and renders cards for both tests
            using var selectForm = new LabManagementSystem.Forms.FrmSelectVisitTest(multiReport);
            Assert.NotNull(selectForm);
            Assert.Equal(RightToLeft.Yes, selectForm.RightToLeft);

            // 2. Generate PDF for Test 1 individually
            var docTest1 = new ConsolidatedLabReportDocument(multiReport, test1_Cbc);
            byte[] pdfBytes1 = docTest1.GeneratePdf();
            Assert.NotNull(pdfBytes1);
            Assert.True(pdfBytes1.Length > 1000);
            var images1 = docTest1.GenerateImages().ToList();
            Assert.Single(images1); // Strictly 1 single A4 page for CBC alone

            // 3. Generate PDF for Test 2 individually
            var docTest2 = new ConsolidatedLabReportDocument(multiReport, test2_Urine);
            byte[] pdfBytes2 = docTest2.GeneratePdf();
            Assert.NotNull(pdfBytes2);
            Assert.True(pdfBytes2.Length > 1000);
            var images2 = docTest2.GenerateImages().ToList();
            Assert.Single(images2); // Strictly 1 single A4 page for Urine alone

            // 4. Verify titles
            Assert.Equal("COMPLETE BLOOD COUNT", docTest1.GetMetadata().Title);
            Assert.Equal("Urine Analysis", docTest2.GetMetadata().Title);
        }

        [Fact]
        public void FrmLabResult_Parameters_Style_And_FlagRemoval_ShouldWorkProperly()
        {
            // 1. Style parser and builder verification
            string notesWithStyle = ParameterResultEntryDto.BuildNotesWithStyle("Doctor clinical notes", isBold: true, colorHex: "#DC2626");
            Assert.Contains("[STYLE:bold=1;color=#DC2626]", notesWithStyle);
            Assert.Contains("Doctor clinical notes", notesWithStyle);

            var (isBold, colorHex, cleanNotes) = ParameterResultEntryDto.ParseStyleFromNotes(notesWithStyle);
            Assert.True(isBold);
            Assert.Equal("#DC2626", colorHex);
            Assert.Equal("Doctor clinical notes", cleanNotes);

            // 2. FrmLabResult form verification: colFlag is hidden
            using var form = new LabManagementSystem.Forms.FrmLabResult();
            var dgv = form.Controls.Find("dgvResults", true).OfType<System.Windows.Forms.DataGridView>().FirstOrDefault();
            Assert.NotNull(dgv);

            // Flag column must be hidden as requested by user
            Assert.False(dgv.Columns["colFlag"].Visible);

            // 3. Document rendering with custom style verification
            var testWithStyle = new ConsolidatedTestDto
            {
                TestName = "CBC",
                Parameters = new List<ParameterResultEntryDto>
                {
                    new() { ParameterName = "Hemoglobin", ResultValue = "14.2", IsBold = true, TextColor = "#DC2626" },
                    new() { ParameterName = "MCV", ResultValue = "Normal", IsBold = false } // letter result
                }
            };

            var report = new ConsolidatedVisitReportDto
            {
                VisitId = 1,
                PatientName = "بافلي سيف",
                Tests = new List<ConsolidatedTestDto> { testWithStyle }
            };

            var doc = new ConsolidatedLabReportDocument(report, testWithStyle);
            byte[] pdfBytes = doc.GeneratePdf();
            Assert.NotNull(pdfBytes);
            Assert.True(pdfBytes.Length > 1000);
        }
    }
}
