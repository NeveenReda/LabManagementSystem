using System.Text;
using LabManagementSystem.Enums;
using LabManagementSystem.Models;
using LabManagementSystem.Printing;
using LabManagementSystem.Services;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace LabManagementSystem.Tests
{
    public class FinalDeliveryWorkflowTests
    {
        private readonly Func<LabContext> _contextFactory = () => new LabContext();
        private readonly IPatientService _patientService;
        private readonly IVisitService _visitService;
        private readonly IBillingService _billingService;
        private readonly ILabSettingsService _settingsService;
        private readonly ILabReportService _reportService;
        private readonly IInventoryService _inventoryService;

        public FinalDeliveryWorkflowTests()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            _patientService = new PatientService(_contextFactory);
            _visitService = new VisitService(_contextFactory);
            _billingService = new BillingService(_contextFactory);
            _settingsService = new LabSettingsService(_contextFactory);
            _reportService = new LabReportService(_contextFactory);
            _inventoryService = new InventoryService(_contextFactory);
        }

        [Fact]
        public async Task PartialPaymentWorkflow_5000_2000_1000_2000_ShouldPersistAccuratelyAcrossFreshContexts()
        {
            Patient? patient = null;
            Lab? tempLab = null;
            int visitId = 0;

            try
            {
                using (var db = _contextFactory())
                {
                    var cat = db.LabCategorys.First();
                    var genderId = db.Genders.Select(g => g.Id).First();

                    tempLab = new Lab
                    {
                        Name = $"__TEST_PAY_LAB_{Guid.NewGuid():N}"[..28],
                        Price = 5000m,
                        Days = 1,
                        LabCategoryId = cat.Id,
                        IsExternal = false
                    };
                    db.Labs.Add(tempLab);

                    patient = new Patient
                    {
                        Name = $"مريض سداد جزئي {Guid.NewGuid():N}"[..24],
                        MedicalCode = $"T{DateTime.Now:yyyyMMddHHmmssff}"[..16],
                        Phone = "01099998888",
                        GenderId = genderId,
                        DateOfBirth = DateTime.Today.AddYears(-30),
                        CreateDate = DateTime.Now,
                        AgeAtRecord = 30
                    };
                    db.Patients.Add(patient);
                    await db.SaveChangesAsync();
                }

                // Create Visit with 5,000 total debt and 0 initial payment
                var newVisit = new Visit
                {
                    PatientId = patient.Id,
                    VisitDate = DateTime.Now,
                    DrName = "د. أحمد",
                    Notes = "اختبار السداد الجزئي"
                };
                visitId = await _visitService.CreateVisitAsync(newVisit, new List<int> { tempLab.Id }, 0m, (int)PaymentMethodEnum.Cash, null);

                // Reject invalid zero/negative and overpayment amounts
                await Assert.ThrowsAsync<InvalidOperationException>(() =>
                    _billingService.RecordPaymentAsync(visitId, 0m, DateTime.Now, (int)PaymentMethodEnum.Cash, "Zero"));
                await Assert.ThrowsAsync<InvalidOperationException>(() =>
                    _billingService.RecordPaymentAsync(visitId, -50m, DateTime.Now, (int)PaymentMethodEnum.Cash, "Negative"));
                await Assert.ThrowsAsync<InvalidOperationException>(() =>
                    _billingService.RecordPaymentAsync(visitId, 5001m, DateTime.Now, (int)PaymentMethodEnum.Cash, "Overpay"));

                // Verify initial outstanding debt = 5,000
                var debts0 = await _billingService.GetOutstandingDebtsAsync(patient.MedicalCode);
                var debt0 = Assert.Single(debts0, d => d.VisitId == visitId);
                Assert.Equal(5000m, debt0.NetAmount);
                Assert.Equal(0m, debt0.TotalPaid);
                Assert.Equal(5000m, debt0.RemainingBalance);

                // 1st partial payment: 2,000 -> Remaining 3,000
                await _billingService.RecordPaymentAsync(visitId, 2000m, DateTime.Now, (int)PaymentMethodEnum.Cash, "دفعة 1");
                var visitAfter1 = await _visitService.GetVisitByIdAsync(visitId);
                Assert.NotNull(visitAfter1);
                Assert.Equal(2000m, visitAfter1.Payments.Sum(p => p.Amount));
                Assert.Equal(3000m, visitAfter1.NetPrice - visitAfter1.Payments.Sum(p => p.Amount));

                // 2nd partial payment: 1,000 -> Remaining 2,000
                await _billingService.RecordPaymentAsync(visitId, 1000m, DateTime.Now, (int)PaymentMethodEnum.Visa, "دفعة 2");
                var visitAfter2 = await _visitService.GetVisitByIdAsync(visitId);
                Assert.NotNull(visitAfter2);
                Assert.Equal(3000m, visitAfter2.Payments.Sum(p => p.Amount));
                Assert.Equal(2000m, visitAfter2.NetPrice - visitAfter2.Payments.Sum(p => p.Amount));

                // Overpay attempt > 2,000 must fail and not mutate balance
                await Assert.ThrowsAsync<InvalidOperationException>(() =>
                    _billingService.RecordPaymentAsync(visitId, 2500m, DateTime.Now, (int)PaymentMethodEnum.Cash, "Overpay2"));
                var visitAfterRejected = await _visitService.GetVisitByIdAsync(visitId);
                Assert.NotNull(visitAfterRejected);
                Assert.Equal(3000m, visitAfterRejected.Payments.Sum(p => p.Amount));

                // 3rd final payment: 2,000 -> Remaining 0
                await _billingService.RecordPaymentAsync(visitId, 2000m, DateTime.Now, (int)PaymentMethodEnum.BankTransfer, "دفعة نهائية");

                // Re-query from fresh DbContext to confirm persistence
                using (var verifyDb = _contextFactory())
                {
                    var payments = verifyDb.Payments.Where(p => p.VisitId == visitId).OrderBy(p => p.Id).ToList();
                    Assert.Equal(3, payments.Count);
                    Assert.Equal(2000m, payments[0].Amount);
                    Assert.Equal(1000m, payments[1].Amount);
                    Assert.Equal(2000m, payments[2].Amount);
                    Assert.Equal(5000m, payments.Sum(p => p.Amount));
                }

                // Visit should no longer be listed in outstanding debts
                var debtsFinal = await _billingService.GetOutstandingDebtsAsync(patient.MedicalCode);
                Assert.DoesNotContain(debtsFinal, d => d.VisitId == visitId);
            }
            finally
            {
                CleanupTestEntities(visitId, tempLab?.Id, patient?.Id);
            }
        }

        [Fact]
        public async Task EndToEnd_Analysis_NumericAndTextParams_Results_And_ArabicPdfReport_ShouldWork()
        {
            Patient? patient = null;
            Lab? tempLab = null;
            int visitId = 0;

            try
            {
                int catId;
                int genderId;
                using (var db = _contextFactory())
                {
                    catId = db.LabCategorys.First().Id;
                    genderId = db.Genders.First().Id;
                }

                // 1. Create Analysis via LabSettingsService
                tempLab = new Lab
                {
                    Name = $"__TEST_PANEL_{Guid.NewGuid():N}"[..24],
                    Price = 350m,
                    Days = 1,
                    LabCategoryId = catId,
                    IsExternal = false
                };
                _settingsService.SaveLab(tempLab);
                Assert.True(tempLab.Id > 0);

                // 2. Add Numeric Parameter & Text Parameter
                var numParam = new LabParameter
                {
                    LabId = tempLab.Id,
                    Name = "Glucose Fasting",
                    Unit = "mg/dL",
                    IsNumeric = true,
                    NormalRangeMin = 70m,
                    NormalRangeMax = 100m,
                    DisplayOrder = 1
                };
                var textParam = new LabParameter
                {
                    LabId = tempLab.Id,
                    Name = "CRP Screening",
                    Unit = "",
                    IsNumeric = false,
                    NormalText = "Negative",
                    DisplayOrder = 2
                };
                _settingsService.SaveParameter(numParam);
                _settingsService.SaveParameter(textParam);
                Assert.True(numParam.Id > 0);
                Assert.True(textParam.Id > 0);

                // 3. Create Patient & Visit
                patient = new Patient
                {
                    Name = "مريض فحص شامل",
                    MedicalCode = $"R{DateTime.Now:yyyyMMddHHmmssff}"[..16],
                    Phone = "01011223344",
                    GenderId = genderId,
                    DateOfBirth = DateTime.Today.AddYears(-28),
                    CreateDate = DateTime.Now,
                    AgeAtRecord = 28
                };
                using (var db = _contextFactory())
                {
                    db.Patients.Add(patient);
                    await db.SaveChangesAsync();
                }

                visitId = await _visitService.CreateVisitAsync(new Visit
                {
                    PatientId = patient.Id,
                    VisitDate = DateTime.Now,
                    DrName = "محمد سامي"
                }, new List<int> { tempLab.Id }, 350m, (int)PaymentMethodEnum.Cash, null);

                int visitLabId;
                using (var db = _contextFactory())
                {
                    visitLabId = db.VisitLabs.Where(vl => vl.VisitId == visitId).Select(vl => vl.Id).First();
                }

                // 4. Reject non-numeric result on Numeric parameter
                await Assert.ThrowsAsync<InvalidOperationException>(() => _reportService.SaveResultsAsync(visitLabId, new List<ParameterResultEntryDto>
                {
                    new() { ParameterId = numParam.Id, ResultValue = "InvalidText" },
                    new() { ParameterId = textParam.Id, ResultValue = "Positive" }
                }));

                // 5. Save valid numeric result + text result (without numeric constraint on text parameter)
                await _reportService.SaveResultsAsync(visitLabId, new List<ParameterResultEntryDto>
                {
                    new() { ParameterId = numParam.Id, ResultValue = "125" },
                    new() { ParameterId = textParam.Id, ResultValue = "Positive (Reactive)" }
                });

                var report = await _reportService.GetConsolidatedReportAsync(visitId);
                Assert.Single(report.Tests);
                var testParams = report.Tests[0].Parameters;
                Assert.Equal(2, testParams.Count);
                Assert.Equal(AbnormalFlagEnum.High, testParams[0].Flag);
                Assert.Equal(AbnormalFlagEnum.Abnormal, testParams[1].Flag);

                // 6. Generate Consolidated PDF Report
                byte[] pdfBytes = new ConsolidatedLabReportDocument(report).GeneratePdf();
                Assert.NotNull(pdfBytes);
                Assert.True(pdfBytes.Length > 1500);
                Assert.StartsWith("%PDF-", Encoding.ASCII.GetString(pdfBytes, 0, 5));
            }
            finally
            {
                CleanupTestEntities(visitId, tempLab?.Id, patient?.Id);
            }
        }

        [Fact]
        public void PurchaseInvoiceWorkflow_SingleMultiAndMultiPage_ShouldRenderReadOnlyWithoutMutatingStock()
        {
            var createdOrderIds = new List<int>();
            var createdProductIds = new List<int>();

            try
            {
                int catId;
                int unitId;
                using (var db = _contextFactory())
                {
                    catId = db.ProductCategorys.Select(c => c.Id).First();
                    unitId = db.Units.Select(u => u.Id).First();

                    var p1 = new Product
                    {
                        Name = "كيت تحليل سكر صائم (اختبار)",
                        ProductCategoryId = catId,
                        BaseUnitId = unitId,
                        InitialAmount = 10,
                        MinAmount = 2,
                        CreateDate = DateTime.Now
                    };
                    var p2 = new Product
                    {
                        Name = "أنابيب سحب دم EDTA معقمة — عبوة 100 قطعة",
                        ProductCategoryId = catId,
                        BaseUnitId = unitId,
                        InitialAmount = 25,
                        MinAmount = 5,
                        CreateDate = DateTime.Now
                    };
                    db.Products.AddRange(p1, p2);
                    db.SaveChanges();
                    createdProductIds.Add(p1.Id);
                    createdProductIds.Add(p2.Id);
                }

                // 1. Single-item purchase invoice
                var singleOrder = new Order
                {
                    OrderDate = DateTime.Now,
                    ExpectedDeliveryDate = DateTime.Today.AddDays(2),
                    Notes = "طلبية عاجلة لقسم الكيمياء"
                };
                var singleDetails = new List<OrderDetail>
                {
                    new() { ProductId = createdProductIds[0], UnitId = unitId, Quantity = 4, Price = 250m }
                };
                _inventoryService.SaveOrder(singleOrder, singleDetails);
                createdOrderIds.Add(singleOrder.Id);
                Assert.True(singleOrder.Id > 0);
                Assert.Equal(1000m, singleOrder.TotalPrice);

                // 2. Multi-item purchase invoice
                var multiOrder = new Order
                {
                    OrderDate = DateTime.Now,
                    Notes = "توريد مستلزمات المعمل الشهرية - مراجعة الاستلام قبل الإضافة"
                };
                var multiDetails = new List<OrderDetail>
                {
                    new() { ProductId = createdProductIds[0], UnitId = unitId, Quantity = 3, Price = 250m },
                    new() { ProductId = createdProductIds[1], UnitId = unitId, Quantity = 5, Price = 180.50m },
                    new() { ProductId = createdProductIds[0], UnitId = unitId, Quantity = 2, Price = 300m }
                };
                _inventoryService.SaveOrder(multiOrder, multiDetails);
                createdOrderIds.Add(multiOrder.Id);
                Assert.Equal(2252.50m, multiOrder.TotalPrice);

                // 3. Large multi-page purchase invoice (36 line items -> guarantees 2+ A4 pages)
                var largeOrder = new Order
                {
                    OrderDate = DateTime.Now,
                    ExpectedDeliveryDate = DateTime.Today.AddDays(5),
                    Notes = "فاتورة مشتريات مجمعة كبيرة لاختبار تعدد الصفحات وتكرار رأس الجدول وعدم انقسام الإجماليات"
                };
                var largeDetails = new List<OrderDetail>();
                for (int i = 1; i <= 36; i++)
                {
                    largeDetails.Add(new OrderDetail
                    {
                        ProductId = createdProductIds[i % 2],
                        UnitId = unitId,
                        Quantity = (i % 5) + 1,
                        Price = 100m + i * 2.5m
                    });
                }
                _inventoryService.SaveOrder(largeOrder, largeDetails);
                createdOrderIds.Add(largeOrder.Id);

                // Snapshot DB state BEFORE read-only invoice viewing/printing/PDF generation
                Dictionary<int, int> stockBefore;
                int orderCountBefore;
                using (var db = _contextFactory())
                {
                    stockBefore = db.Products
                        .Where(p => createdProductIds.Contains(p.Id))
                        .ToDictionary(p => p.Id, p => p.InitialAmount);
                    orderCountBefore = db.Orders.Count();
                }

                // Verify GetOrderById uses read-only reload with complete graph & generates valid PDFs/images
                var loadedSingle = _inventoryService.GetOrderById(singleOrder.Id);
                Assert.NotNull(loadedSingle);
                Assert.Single(loadedSingle!.OrderDetails);
                Assert.Equal("كيت تحليل سكر صائم (اختبار)", loadedSingle.OrderDetails.First().Product?.Name);

                var singleDoc = new PurchaseInvoiceDocument(loadedSingle);
                byte[] singlePdf = singleDoc.GeneratePdfBytes();
                Assert.StartsWith("%PDF-", Encoding.ASCII.GetString(singlePdf, 0, 5));
                Assert.Single(singleDoc.GenerateImages());

                var loadedMulti = _inventoryService.GetOrderById(multiOrder.Id);
                Assert.NotNull(loadedMulti);
                Assert.Equal(3, loadedMulti!.OrderDetails.Count);
                Assert.Equal(2252.50m, loadedMulti.TotalPrice);
                byte[] multiPdf = new PurchaseInvoiceDocument(loadedMulti).GeneratePdfBytes();
                Assert.StartsWith("%PDF-", Encoding.ASCII.GetString(multiPdf, 0, 5));

                var loadedLarge = _inventoryService.GetOrderById(largeOrder.Id);
                Assert.NotNull(loadedLarge);
                Assert.Equal(36, loadedLarge!.OrderDetails.Count);
                var largeDoc = new PurchaseInvoiceDocument(loadedLarge);
                byte[] largePdf = largeDoc.GeneratePdfBytes();
                Assert.StartsWith("%PDF-", Encoding.ASCII.GetString(largePdf, 0, 5));
                var largePages = largeDoc.GenerateImages().ToList();
                Assert.True(largePages.Count >= 2, $"Expected >= 2 pages for 36-item purchase invoice, got {largePages.Count}");

                // Assert zero stock or order mutations occurred during read/preview/PDF generation
                using (var db = _contextFactory())
                {
                    var stockAfter = db.Products
                        .Where(p => createdProductIds.Contains(p.Id))
                        .ToDictionary(p => p.Id, p => p.InitialAmount);
                    int orderCountAfter = db.Orders.Count();

                    Assert.Equal(orderCountBefore, orderCountAfter);
                    Assert.Equal(stockBefore[createdProductIds[0]], stockAfter[createdProductIds[0]]);
                    Assert.Equal(stockBefore[createdProductIds[1]], stockAfter[createdProductIds[1]]);
                }
            }
            finally
            {
                try
                {
                    using var db = _contextFactory();
                    if (createdOrderIds.Count > 0)
                    {
                        var details = db.OrderDetails.Where(d => createdOrderIds.Contains(d.OrderId)).ToList();
                        db.OrderDetails.RemoveRange(details);
                        var orders = db.Orders.Where(o => createdOrderIds.Contains(o.Id)).ToList();
                        db.Orders.RemoveRange(orders);
                        db.SaveChanges();
                    }

                    if (createdProductIds.Count > 0)
                    {
                        var products = db.Products.Where(p => createdProductIds.Contains(p.Id)).ToList();
                        db.Products.RemoveRange(products);
                        db.SaveChanges();
                    }
                }
                catch
                {
                    // Ignore cleanup failure in test teardown
                }
            }
        }

        private void CleanupTestEntities(int visitId, int? labId, int? patientId)
        {
            try
            {
                using var db = _contextFactory();
                if (visitId > 0)
                {
                    var visitLabIds = db.VisitLabs.Where(vl => vl.VisitId == visitId).Select(vl => vl.Id).ToList();
                    if (visitLabIds.Count > 0)
                    {
                        var results = db.VisitLabResults.Where(r => visitLabIds.Contains(r.VisitLabId)).ToList();
                        db.VisitLabResults.RemoveRange(results);
                        var ext = db.ExternalLabs.Where(e => visitLabIds.Contains(e.VistLabId)).ToList();
                        db.ExternalLabs.RemoveRange(ext);
                        var vls = db.VisitLabs.Where(vl => vl.VisitId == visitId).ToList();
                        db.VisitLabs.RemoveRange(vls);
                    }
                    var payments = db.Payments.Where(p => p.VisitId == visitId).ToList();
                    db.Payments.RemoveRange(payments);
                    var visit = db.Visits.Find(visitId);
                    if (visit != null) db.Visits.Remove(visit);
                    db.SaveChanges();
                }

                if (labId.HasValue && labId.Value > 0)
                {
                    var parameters = db.LabParameters.Where(p => p.LabId == labId.Value).ToList();
                    db.LabParameters.RemoveRange(parameters);
                    var cats = db.LabParameterCategories.Where(c => c.LabId == labId.Value).ToList();
                    db.LabParameterCategories.RemoveRange(cats);
                    var lab = db.Labs.Find(labId.Value);
                    if (lab != null) db.Labs.Remove(lab);
                    db.SaveChanges();
                }

                if (patientId.HasValue && patientId.Value > 0)
                {
                    var p = db.Patients.Find(patientId.Value);
                    if (p != null) db.Patients.Remove(p);
                    db.SaveChanges();
                }
            }
            catch
            {
                // Ignore cleanup failure in test teardown
            }
        }
    }
}
