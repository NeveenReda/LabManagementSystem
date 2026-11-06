using LabManagementSystem.Configuration;
using LabManagementSystem.Enums;
using LabManagementSystem.Printing;
using LabManagementSystem.Services;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace LabManagementSystem.Tests
{
    public class LaboratorySystemTests
    {
        public LaboratorySystemTests()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        [Fact]
        public void ThermalReceipt_ShouldGenerateValidPdf()
        {
            var model = new ThermalReceiptModel
            {
                VisitId = 101,
                VisitDate = DateTime.Now,
                PatientName = "أحمد محمد محمود",
                MedicalCode = "20261104001",
                DoctorName = "خالد سليم",
                TotalAmount = 450m,
                DiscountAmount = 50m,
                NetAmount = 400m,
                PaidAmount = 400m,
                RemainingBalance = 0m,
                PaymentMethodName = "نقدي (Cash)",
                Items = new List<Tuple<string, decimal>>
                {
                    Tuple.Create("صورة دم كاملة (CBC)", 150m),
                    Tuple.Create("وظائف كبد (Liver Function)", 150m),
                    Tuple.Create("سكر صائم (FBS)", 150m)
                }
            };

            var doc = new ThermalReceiptDocument(model);
            byte[] pdf = doc.GeneratePdf();

            Assert.NotNull(pdf);
            Assert.True(pdf.Length > 100);

            var images = doc.GenerateImages().ToList();
            Assert.NotEmpty(images);
            Assert.True(images[0].Length > 100);
        }

        [Fact]
        public void StandardInvoice_ShouldGenerateValidPdf()
        {
            var model = new ThermalReceiptModel
            {
                VisitId = 202,
                VisitDate = DateTime.Now,
                PatientName = "سارة حسن إبراهيم",
                MedicalCode = "20261104005",
                DoctorName = "محمد نبيل",
                TotalAmount = 600m,
                DiscountAmount = 100m,
                NetAmount = 500m,
                PaidAmount = 300m,
                RemainingBalance = 200m,
                PaymentMethodName = "فيزا",
                Items = new List<Tuple<string, decimal>>
                {
                    Tuple.Create("Lipid Profile (دهون كاملة)", 300m),
                    Tuple.Create("Kidney Function (وظائف كلى)", 300m)
                }
            };

            var doc = new InvoiceDocument(model);
            byte[] pdf = doc.GeneratePdf();

            Assert.NotNull(pdf);
            Assert.True(pdf.Length > 100);
        }

        [Fact]
        public void ConsolidatedLabReport_MultiTest_ShouldGenerateValidA4Pdf()
        {
            var report = new ConsolidatedVisitReportDto
            {
                VisitId = 303,
                VisitDate = DateTime.Today,
                PatientName = "محمود عبد الرحمن",
                MedicalCode = "20261104010",
                Age = 35,
                Gender = "ذكر",
                DoctorName = "طارق فهمي",
                Tests = new List<ConsolidatedTestDto>
                {
                    new ConsolidatedTestDto
                    {
                        VisitLabId = 1,
                        LabId = 10,
                        TestName = "صورة دم كاملة (CBC)",
                        CategoryName = "أمراض الدم (Hematology)",
                        Parameters = new List<ParameterResultEntryDto>
                        {
                            new ParameterResultEntryDto { ParameterName = "Hemoglobin (Hb)", ResultValue = "14.5", Unit = "g/dL", FormattedNormalRange = "13.0 - 17.5", Flag = AbnormalFlagEnum.Normal },
                            new ParameterResultEntryDto { ParameterName = "WBCs", ResultValue = "12.8", Unit = "x10^3/uL", FormattedNormalRange = "4.0 - 11.0", Flag = AbnormalFlagEnum.High },
                            new ParameterResultEntryDto { ParameterName = "Platelets", ResultValue = "250", Unit = "x10^3/uL", FormattedNormalRange = "150 - 450", Flag = AbnormalFlagEnum.Normal }
                        }
                    },
                    new ConsolidatedTestDto
                    {
                        VisitLabId = 2,
                        LabId = 15,
                        TestName = "تحليل سكر تراكمي (HbA1c)",
                        CategoryName = "كيمياء حيوية (Biochemistry)",
                        Parameters = new List<ParameterResultEntryDto>
                        {
                            new ParameterResultEntryDto { ParameterName = "HbA1c", ResultValue = "7.8", Unit = "%", FormattedNormalRange = "4.5 - 6.0", Flag = AbnormalFlagEnum.High }
                        }
                    }
                }
            };

            var doc = new ConsolidatedLabReportDocument(report);
            byte[] pdf = doc.GeneratePdf();

            Assert.NotNull(pdf);
            Assert.True(pdf.Length > 200);
        }

        [Fact]
        public void FinancialSummary_ShouldGenerateValidPdf()
        {
            var summary = new FinancialSummaryDto
            {
                FromDate = DateTime.Today.AddDays(-30),
                ToDate = DateTime.Today,
                TotalVisits = 45,
                TotalTests = 120,
                TotalGrossRevenue = 15000m,
                TotalDiscounts = 1500m,
                TotalNetRevenue = 13500m,
                TotalCollectedPayments = 12000m,
                TotalOutstandingBalances = 1500m,
                TestsBreakdown = new List<TestRevenueItemDto>
                {
                    new TestRevenueItemDto { TestName = "CBC", CategoryName = "Hematology", Count = 40, TotalRevenue = 6000m },
                    new TestRevenueItemDto { TestName = "Lipid Profile", CategoryName = "Biochemistry", Count = 25, TotalRevenue = 5000m }
                },
                CategoriesBreakdown = new List<CategoryRevenueItemDto>
                {
                    new CategoryRevenueItemDto { CategoryName = "Hematology", Count = 50, TotalRevenue = 7500m },
                    new CategoryRevenueItemDto { CategoryName = "Biochemistry", Count = 70, TotalRevenue = 7500m }
                },
                PaymentMethodsBreakdown = new List<PaymentMethodSummaryDto>
                {
                    new PaymentMethodSummaryDto { MethodName = "نقدي (Cash)", TransactionsCount = 35, TotalAmount = 9000m },
                    new PaymentMethodSummaryDto { MethodName = "فيزا (Card)", TransactionsCount = 10, TotalAmount = 3000m }
                }
            };

            var doc = new FinancialReportDocument(summary);
            byte[] pdf = doc.GeneratePdf();

            Assert.NotNull(pdf);
            Assert.True(pdf.Length > 200);
        }

        [Theory]
        [InlineData(PaymentMethodEnum.Cash, "نقدي (كاش)")]
        [InlineData(PaymentMethodEnum.Visa, "فيزا / بطاقة إلكترونية")]
        [InlineData(PaymentMethodEnum.BankTransfer, "تحويل بنكي / محفظة")]
        public void PaymentMethod_ToArabic_ShouldReturnExpectedString(PaymentMethodEnum method, string expected)
        {
            Assert.Equal(expected, method.ToArabic());
        }

        [Theory]
        [InlineData(TaskPriorityEnum.Low, "منخفضة")]
        [InlineData(TaskPriorityEnum.High, "هامة")]
        [InlineData(TaskPriorityEnum.Urgent, "عاجلة جداً")]
        public void TaskPriority_ToArabic_ShouldReturnExpectedString(TaskPriorityEnum priority, string expected)
        {
            Assert.Equal(expected, priority.ToArabic());
        }

        [Fact]
        public void AppConfig_ShouldLoadFallbackOrConfiguredString()
        {
            string conn = AppConfig.GetConnectionString();
            Assert.False(string.IsNullOrWhiteSpace(conn));
            Assert.Contains("db43945", conn);

            var settings = AppConfig.GetLabSettings();
            Assert.NotNull(settings);
            Assert.False(string.IsNullOrWhiteSpace(settings.LabName));
        }
    }
}
