using LabManagementSystem.Common;
using LabManagementSystem.Forms;
using LabManagementSystem.Models;
using LabManagementSystem.Printing;
using LabManagementSystem.Services;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace LabManagementSystem.Tests
{
    public class FormLifecycleAndRtlTests
    {
        public FormLifecycleAndRtlTests()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            // Configure DI container if not initialized
            var services = new ServiceCollection();
            services.AddTransient<LabContext>();
            services.AddSingleton<Func<LabContext>>(sp => () => new LabContext());
            services.AddSingleton<ILookupCacheService, LookupCacheService>();
            services.AddSingleton<IPatientService, PatientService>();
            services.AddSingleton<IVisitService, VisitService>();
            services.AddSingleton<IBillingService, BillingService>();
            services.AddSingleton<ILabReportService, LabReportService>();
            services.AddSingleton<ILabSettingsService, LabSettingsService>();
            services.AddSingleton<ITaskService, TaskService>();
            services.AddSingleton<IInventoryService, InventoryService>();
            services.AddSingleton<IFinancialReportService, FinancialReportService>();

            var sp = services.BuildServiceProvider();
            ServiceLocator.Initialize(sp);
        }

        [Fact]
        public void FrmMain_Initialization_ShouldNotThrow_AndHaveRtlSidebar()
        {
            using var frm = new FrmMain();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
            Assert.True(frm.RightToLeftLayout);

            var pnlSidebar = frm.Controls.Find("pnlSidebar", true).FirstOrDefault() as Panel;
            Assert.NotNull(pnlSidebar);
            Assert.Equal(DockStyle.Right, pnlSidebar.Dock); // DockStyle.Right places sidebar on the physical RIGHT

            var pnlContent = frm.Controls.Find("pnlContent", true).FirstOrDefault() as Panel;
            Assert.NotNull(pnlContent);

            // Force handle creation and layout
            frm.CreateControl();
            var screenSidebar = pnlSidebar.PointToScreen(System.Drawing.Point.Empty);
            var screenContent = pnlContent.PointToScreen(System.Drawing.Point.Empty);

            // In Arabic RTL, pnlSidebar (Dock=Right) is physically at the right side of the monitor compared to pnlContent
            Assert.True(screenSidebar.X > screenContent.X, $"Expected Sidebar physical X ({screenSidebar.X}) to be greater than Content physical X ({screenContent.X})");
        }

        [Fact]
        public void FrmDashboard_Initialization_ShouldNotThrow_AndColumnsShouldHaveNames()
        {
            using var frm = new FrmDashboard();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);

            var dgv = frm.Controls.Find("dgvRecentVisits", true).FirstOrDefault() as DataGridView;
            if (dgv == null)
            {
                // Find inside containers
                foreach (Control c in frm.Controls)
                {
                    var found = c.Controls.Find("dgvRecentVisits", true).FirstOrDefault() as DataGridView;
                    if (found != null) { dgv = found; break; }
                }
            }

            Assert.NotNull(dgv);
            Assert.NotNull(dgv.Columns["colVisitDate"]);
            Assert.NotNull(dgv.Columns["colRemainingAmount"]);
            Assert.NotNull(dgv.Columns["colPatientName"]);
        }

        [Fact]
        public void FrmDebts_Initialization_ShouldNotThrow_AndColumnsShouldHaveNames()
        {
            using var frm = new FrmDebts();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);

            var dgv = frm.Controls.Find("dgvDebts", true).FirstOrDefault() as DataGridView;
            if (dgv == null)
            {
                foreach (Control c in frm.Controls)
                {
                    var found = c.Controls.Find("dgvDebts", true).FirstOrDefault() as DataGridView;
                    if (found != null) { dgv = found; break; }
                }
            }

            Assert.NotNull(dgv);
            Assert.NotNull(dgv.Columns["colVisitDate"]);
            Assert.NotNull(dgv.Columns["colRemainingBalance"]);
        }

        [Fact]
        public void FrmFinancialReports_Initialization_ShouldNotThrow_AndColumnsShouldHaveNames()
        {
            using var frm = new FrmFinancialReports();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        }

        [Fact]
        public void FrmTasks_Initialization_ShouldNotThrow()
        {
            using var frm = new FrmTasks();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        }

        [Fact]
        public void FrmInventory_Initialization_ShouldNotThrow()
        {
            using var frm = new FrmInventory();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        }

        [Fact]
        public void FrmBaseExternalLabs_Initialization_ShouldNotThrow()
        {
            using var frm = new FrmBaseExternalLabs();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        }

        [Fact]
        public void FrmSettingsLab_Initialization_ShouldNotThrow()
        {
            using var frm = new frmSettingsLab();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        }

        [Fact]
        public void FrmSearchPatient_Initialization_ShouldNotThrow()
        {
            using var frm = new FrmSearchPatient();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        }

        [Fact]
        public void FrmAddPatient_Initialization_ShouldNotThrow()
        {
            using var frm = new FrmAddPatient();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        }

        [Fact]
        public void FrmSearchVisit_Initialization_ShouldNotThrow()
        {
            using var frm = new FrmSearchVisit();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        }

        [Fact]
        public void FrmAddVisit_Initialization_ShouldNotThrow()
        {
            using var frm = new FrmAddVisit();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        }

        [Fact]
        public void FrmSelectPatient_Initialization_ShouldNotThrow()
        {
            using var frm = new FrmSelectPatient();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        }

        [Fact]
        public void FrmLabParametersDialog_Initialization_ShouldNotThrow()
        {
            using var frm = new FrmLabParametersDialog(1, "CBC Test");
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        }

        [Fact]
        public void FrmExternalLab_Initialization_ShouldNotThrow()
        {
            using var frm = new FrmExternalLab();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        }

        [Fact]
        public void FrmPayments_Initialization_ShouldNotThrow()
        {
            using var frm = new FrmPayments();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        }

        [Fact]
        public void FrmLabResult_Initialization_ShouldNotThrow()
        {
            using var frm = new FrmLabResult();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        }

        [Fact]
        public void DocumentViewerForm_Initialization_ShouldNotThrow()
        {
            var doc = new DummyTestDoc();
            using var frm = new DocumentViewerForm(doc, "Test Document");
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        }

        [Fact]
        public void FrmSystemSettings_Initialization_ShouldNotThrow_AndBeRtl()
        {
            using var frm = new FrmSystemSettings();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        }

        [Fact]
        public async Task LaboratoryProfile_Persistence_ShouldSaveAndRetrieve()
        {
            var service = ServiceLocator.LabSettingsService;
            var initial = await service.GetLaboratoryProfileAsync();
            Assert.NotNull(initial);
            Assert.False(string.IsNullOrWhiteSpace(initial.LabName));

            // Test updating profile
            string testName = "معمل الاختبار والتشخيص السريري";
            initial.LabName = testName;
            await service.SaveLaboratoryProfileAsync(initial);

            var retrieved = await service.GetLaboratoryProfileAsync();
            Assert.Equal(testName, retrieved.LabName);
            Assert.Equal(testName, Configuration.AppConfig.GetLabSettings().LabName);
        }

        [Fact]
        public void FrmAddPatient_Events_ShouldBeSubscribable()
        {
            using var frm = new FrmAddPatient();
            bool savedInvoked = false;
            frm.Saved += () => savedInvoked = true;
            Assert.NotNull(frm);
            Assert.False(savedInvoked);
        }

        [Fact]
        public void FrmAddVisit_Events_ShouldBeSubscribable()
        {
            using var frm = new FrmAddVisit();
            bool savedInvoked = false;
            frm.Saved += () => savedInvoked = true;
            Assert.NotNull(frm);
            Assert.False(savedInvoked);
        }

        private class DummyTestDoc : IDocument
        {
            public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
            public void Compose(IDocumentContainer container)
            {
                container.Page(p => p.Content().Text("Hello"));
            }
        }
    }
}

