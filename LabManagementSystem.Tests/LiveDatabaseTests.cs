using LabManagementSystem.Configuration;
using LabManagementSystem.Enums;
using LabManagementSystem.Models;
using LabManagementSystem.Printing;
using LabManagementSystem.Services;
using QuestPDF.Fluent;

namespace LabManagementSystem.Tests
{
    public class LiveDatabaseTests
    {
        private readonly IPatientService _patientService;
        private readonly IVisitService _visitService;
        private readonly IBillingService _billingService;
        private readonly ILabReportService _reportService;
        private readonly ILabSettingsService _settingsService;
        private readonly ITaskService _taskService;

        public LiveDatabaseTests()
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            Func<LabContext> contextFactory = () => new LabContext();
            _patientService = new PatientService(contextFactory);
            _visitService = new VisitService(contextFactory);
            _billingService = new BillingService(contextFactory);
            _reportService = new LabReportService(contextFactory);
            _settingsService = new LabSettingsService(contextFactory);
            _taskService = new TaskService(contextFactory);
        }

        [Fact]
        public void GenerateMedicalCode_ShouldReturnStandardFormattedCode()
        {
            string code = _patientService.GenerateNextMedicalCode();
            Assert.NotNull(code);
            Assert.StartsWith(DateTime.Today.ToString("yyyyMMdd"), code);
            Assert.True(code.Length >= 11);
        }

        [Fact]
        public void SearchPatients_ShouldReturnResultsWithoutCrashing()
        {
            var patients = _patientService.SearchPatients(null);
            Assert.NotNull(patients);
        }

        [Fact]
        public void GetAllLabs_ShouldReturnConfiguredTests()
        {
            var labs = _visitService.GetAllLabs();
            Assert.NotNull(labs);
            Assert.NotEmpty(labs);
        }

        [Fact]
        public void GetTasks_ShouldReturnActiveTasks()
        {
            var tasks = _taskService.GetTasks(new TaskFilterDto());
            Assert.NotNull(tasks);
        }

        [Fact]
        public void GetOutstandingDebts_ShouldExecuteCleanly()
        {
            var debts = _billingService.GetOutstandingDebts(null);
            Assert.NotNull(debts);
        }

        [Fact]
        public void LabSettings_ShouldReturnExternalLabsAndCategories()
        {
            var cats = _settingsService.GetLabCategories();
            Assert.NotNull(cats);

            var externalLabs = _settingsService.GetAllExternalLabs();
            Assert.NotNull(externalLabs);
        }

        [Fact]
        public void Benchmark_MeasureOperations()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            // 1. Patient search
            sw.Restart();
            var patients = _patientService.SearchPatients(null);
            long patientSearchMs = sw.ElapsedMilliseconds;

            // 2. Visits search
            sw.Restart();
            var visits = _visitService.SearchVisits(new VisitSearchFilter { IncludeDeleted = false });
            long visitSearchMs = sw.ElapsedMilliseconds;

            // 3. Outstanding debts
            sw.Restart();
            var debts = _billingService.GetOutstandingDebts(null);
            long debtsMs = sw.ElapsedMilliseconds;

            // 4. Lab settings / tests
            sw.Restart();
            var labs = _settingsService.GetAllLabs();
            long labsMs = sw.ElapsedMilliseconds;

            // 5. PDF generation benchmark
            sw.Restart();
            var receiptModel = new ThermalReceiptModel
            {
                VisitId = 999,
                VisitDate = DateTime.Now,
                PatientName = "مريض تجريبي",
                MedicalCode = "20261106001",
                DoctorName = "د. عيادة",
                TotalAmount = 250m,
                DiscountAmount = 0m,
                NetAmount = 250m,
                PaidAmount = 250m,
                RemainingBalance = 0m,
                PaymentMethodName = "نقدي",
                Items = new List<Tuple<string, decimal>> { Tuple.Create("تحليل CBC", 250m) }
            };
            var pdf = new ThermalReceiptDocument(receiptModel).GeneratePdf();
            long pdfMs = sw.ElapsedMilliseconds;

            long memoryBytes = GC.GetTotalMemory(true);

            string report = $"[BENCHMARK] PatientSearch: {patientSearchMs}ms, VisitSearch: {visitSearchMs}ms, Debts: {debtsMs}ms, Labs: {labsMs}ms, PdfGen: {pdfMs}ms, Memory: {memoryBytes / 1024 / 1024}MB";
            System.Diagnostics.Trace.WriteLine(report);
            Console.WriteLine(report);
        }

        [Fact]
        public async Task Benchmark_MeasureOptimizedAsyncOperations()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();

            // 1. Optimized Async Patient Search (Projected DTO + AsNoTracking + Take 100)
            sw.Restart();
            var patients = await _patientService.SearchPatientsAsync(null, 100);
            long patientSearchMs = sw.ElapsedMilliseconds;

            // 2. Optimized Async Visit Search (Projected DTO + AsNoTracking)
            sw.Restart();
            var visits = await _visitService.SearchVisitsAsync(new VisitSearchFilter { IncludeDeleted = false });
            long visitSearchMs = sw.ElapsedMilliseconds;

            // 3. Optimized Async Outstanding Debts (Direct SQL projection)
            sw.Restart();
            var debts = await _billingService.GetOutstandingDebtsAsync(null);
            long debtsMs = sw.ElapsedMilliseconds;

            // 4. Cached Lookup Service
            var cache = new LookupCacheService(() => new LabContext());
            _ = cache.GetLabs(); // Warm-up load
            sw.Restart();
            var cachedLabs = cache.GetLabs();
            var cachedGenders = cache.GetGenders();
            long cacheRetrievalMs = sw.ElapsedMilliseconds;

            // 5. Consolidated Report Single-Query Lab Parameters Fetch
            sw.Restart();
            var report = await _reportService.GetConsolidatedReportAsync(1); // Visit 1
            long reportMs = sw.ElapsedMilliseconds;

            long memoryBytes = GC.GetTotalMemory(true);

            string reportStr = $"[OPTIMIZED BENCHMARK] AsyncPatientSearch: {patientSearchMs}ms, AsyncVisitSearch: {visitSearchMs}ms, AsyncDebts: {debtsMs}ms, CachedLookups: {cacheRetrievalMs}ms, BatchLabReport: {reportMs}ms, Memory: {memoryBytes / 1024 / 1024}MB";
            Console.WriteLine(reportStr);
            Assert.NotNull(patients);
            Assert.NotNull(visits);
            Assert.NotNull(debts);
            Assert.NotEmpty(cachedLabs);
        }
    }
}

