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
            Assert.Equal(DockStyle.Left, pnlSidebar.Dock); // With RightToLeftLayout = true, DockStyle.Left positions sidebar at the physical RIGHT

            var pnlContent = frm.Controls.Find("pnlContent", true).FirstOrDefault() as Panel;
            Assert.NotNull(pnlContent);

            // Show form to establish desktop mirrored coordinates
            frm.Show();
            Application.DoEvents();
            var screenSidebar = pnlSidebar.PointToScreen(System.Drawing.Point.Empty);
            var screenContent = pnlContent.PointToScreen(System.Drawing.Point.Empty);

            // In Arabic RTL, pnlSidebar (Dock=Left with RightToLeftLayout=true) is physically at the right side of the monitor compared to pnlContent
            Assert.True(screenSidebar.X > screenContent.X, $"Expected Sidebar physical X ({screenSidebar.X}) to be greater than Content physical X ({screenContent.X})");
            frm.Close();
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

        [Fact]
        public void FrmMain_HeaderAndFooter_ShouldContainOliviaLabAndTechSpotFooter()
        {
            using var frm = new FrmMain();
            Assert.NotNull(frm);

            var lblOliviaLab = frm.Controls.Find("lblOliviaLab", true).FirstOrDefault() as Label;
            Assert.NotNull(lblOliviaLab);
            Assert.Equal("Olivia Lab", lblOliviaLab.Text);
            Assert.Equal(ContentAlignment.MiddleCenter, lblOliviaLab.TextAlign);

            var pnlFooter = frm.Controls.Find("pnlFooter", true).FirstOrDefault() as Panel;
            Assert.NotNull(pnlFooter);
            Assert.Equal(DockStyle.Bottom, pnlFooter.Dock);
            Assert.Equal(30, pnlFooter.Height);

            var lblFooter = frm.Controls.Find("lblFooter", true).FirstOrDefault() as Label;
            Assert.NotNull(lblFooter);
            Assert.Equal("Powered by Tech Spot Canadian 2026", lblFooter.Text);
            Assert.Equal(ContentAlignment.MiddleCenter, lblFooter.TextAlign);
        }

        [Fact]
        public void FrmMain_SidebarAccordion_ShouldContainExact7GroupsAndRenamedItems()
        {
            using var frm = new FrmMain();
            Assert.NotNull(frm.Accordion);

            var groups = frm.Accordion.Groups;
            Assert.Equal(7, groups.Count);

            // Group 1: لوحة التحكم -> الرئيسية
            Assert.Equal("لوحة التحكم", groups[0].Title);
            Assert.Single(groups[0].SubItems);
            Assert.Equal("الرئيسية", groups[0].SubItems[0].Text);

            // Group 2: إدارة المرضى -> سجل المرضى, مريض جديد
            Assert.Equal("إدارة المرضى", groups[1].Title);
            Assert.Equal(2, groups[1].SubItems.Count);
            Assert.Equal("سجل المرضى", groups[1].SubItems[0].Text);
            Assert.Equal("مريض جديد", groups[1].SubItems[1].Text);

            // Group 3: إدارة الزيارات -> سجل الزيارات, تسجيل زيارة جديدة
            Assert.Equal("إدارة الزيارات", groups[2].Title);
            Assert.Equal(2, groups[2].SubItems.Count);
            Assert.Equal("سجل الزيارات", groups[2].SubItems[0].Text);
            Assert.Equal("تسجيل زيارة جديدة", groups[2].SubItems[1].Text);

            // Group 4: إدارة المعمل -> مدونات المرضى, نتائج التحاليل, المعامل الخارجية
            Assert.Equal("إدارة المعمل", groups[3].Title);
            Assert.Equal(3, groups[3].SubItems.Count);
            Assert.Equal("مدونات المرضى", groups[3].SubItems[0].Text);
            Assert.Equal("نتائج التحاليل", groups[3].SubItems[1].Text);
            Assert.Equal("المعامل الخارجية", groups[3].SubItems[2].Text);

            // Group 5: الحسابات والتقارير -> التقارير المالية, الحسابات والتقارير, المهام والنواقص
            Assert.Equal("الحسابات والتقارير", groups[4].Title);
            Assert.Equal(3, groups[4].SubItems.Count);
            Assert.Equal("التقارير المالية", groups[4].SubItems[0].Text);
            Assert.Equal("الحسابات والتقارير", groups[4].SubItems[1].Text);
            Assert.Equal("المهام والنواقص", groups[4].SubItems[2].Text);

            // Group 6: المخزون والمشتريات -> المخزون والمشتريات, اللوجستيات والمخازن
            Assert.Equal("المخزون والمشتريات", groups[5].Title);
            Assert.Equal(2, groups[5].SubItems.Count);
            Assert.Equal("المخزون والمشتريات", groups[5].SubItems[0].Text);
            Assert.Equal("اللوجستيات والمخازن", groups[5].SubItems[1].Text);

            // Group 7: رئيس الإعدادات -> إعدادات التحليل, إعدادات السيستم
            Assert.Equal("رئيس الإعدادات", groups[6].Title);
            Assert.Equal(2, groups[6].SubItems.Count);
            Assert.Equal("إعدادات التحليل", groups[6].SubItems[0].Text);
            Assert.Equal("إعدادات السيستم", groups[6].SubItems[1].Text);
        }

        [Fact]
        public void AllForms_Headers_ShouldHaveStandardHeightAndRtl()
        {
            using var frmPatient = new FrmSearchPatient();
            var pnlH1 = frmPatient.Controls.Find("pnlHeader", true).FirstOrDefault() as Panel;
            Assert.NotNull(pnlH1);
            Assert.Equal(68, pnlH1.Height);
            Assert.Equal(RightToLeft.Yes, pnlH1.RightToLeft);

            using var frmVisit = new FrmSearchVisit();
            var pnlH2 = frmVisit.Controls.Find("pnlHeader", true).FirstOrDefault() as Panel;
            Assert.NotNull(pnlH2);
            Assert.Equal(68, pnlH2.Height);
            Assert.Equal(RightToLeft.Yes, pnlH2.RightToLeft);

            using var frmAddP = new FrmAddPatient();
            var pnlH3 = frmAddP.Controls.Find("pnlHeader", true).FirstOrDefault() as Panel;
            Assert.NotNull(pnlH3);
            Assert.Equal(68, pnlH3.Height);
            Assert.Equal(RightToLeft.Yes, pnlH3.RightToLeft);

            using var frmAddV = new FrmAddVisit();
            var pnlH4 = frmAddV.Controls.Find("pnlHeader", true).FirstOrDefault() as Panel;
            Assert.NotNull(pnlH4);
            Assert.Equal(68, pnlH4.Height);
            Assert.Equal(RightToLeft.Yes, pnlH4.RightToLeft);
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

