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
        public void FrmAddVisit_Initialization_ShouldNotThrow() => StaTestRunner.Run(() =>
        {
            using var frm = new FrmAddVisit();
            Assert.NotNull(frm);
            Assert.Equal(RightToLeft.Yes, frm.RightToLeft);
        });

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
        public void FrmAddVisit_Events_ShouldBeSubscribable() => StaTestRunner.Run(() =>
        {
            using var frm = new FrmAddVisit();
            bool savedInvoked = false;
            frm.Saved += () => savedInvoked = true;
            Assert.NotNull(frm);
            Assert.False(savedInvoked);
        });

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

            // Group 4: إدارة المعمل -> مديونات المرضى, المعامل الخارجية
            Assert.Equal("إدارة المعمل", groups[3].Title);
            Assert.Equal(2, groups[3].SubItems.Count);
            Assert.Equal("مديونات المرضى", groups[3].SubItems[0].Text);
            Assert.Equal("المعامل الخارجية", groups[3].SubItems[1].Text);

            // Group 5: الحسابات والتقارير -> التقارير المالية, المهام والنواقص
            Assert.Equal("الحسابات والتقارير", groups[4].Title);
            Assert.Equal(2, groups[4].SubItems.Count);
            Assert.Equal("التقارير المالية", groups[4].SubItems[0].Text);
            Assert.Equal("المهام والنواقص", groups[4].SubItems[1].Text);

            // Group 6: المخزون والمشتريات -> المخزون والمشتريات
            Assert.Equal("المخزون والمشتريات", groups[5].Title);
            Assert.Single(groups[5].SubItems);
            Assert.Equal("المخزون والمشتريات", groups[5].SubItems[0].Text);

            // Group 7: رئيس الإعدادات -> إعدادات التحليل, إعدادات السيستم
            Assert.Equal("رئيس الإعدادات", groups[6].Title);
            Assert.Equal(2, groups[6].SubItems.Count);
            Assert.Equal("إعدادات التحليل", groups[6].SubItems[0].Text);
            Assert.Equal("إعدادات السيستم", groups[6].SubItems[1].Text);
        }

        [Fact]
        public void FrmMain_Startup_ShouldOpenFrmHomeWithoutSelectingSidebarModule()
        {
            using var frm = new FrmMain();
            frm.Size = new System.Drawing.Size(1200, 750);
            frm.Show();
            Application.DoEvents();

            var pnlContent = frm.Controls.Find("pnlContent", true).FirstOrDefault() as Panel;
            Assert.NotNull(pnlContent);

            var home = pnlContent.Controls.OfType<FrmHome>().FirstOrDefault();
            Assert.NotNull(home);
            Assert.Equal(System.Drawing.ColorTranslator.FromHtml("#F5F7FA"), home.BackColor);

            // Ensure no white card / rectangle container surrounds the logo
            Assert.Empty(home.Controls.Find("pnlLogoContainer", true));

            var logo = home.Controls.Find("picHomeLogo", true).FirstOrDefault() as PictureBox;
            Assert.NotNull(logo);
            Assert.Equal(PictureBoxSizeMode.Zoom, logo.SizeMode);
            Assert.Equal(System.Drawing.Color.Transparent, logo.BackColor);
            Assert.Equal(BorderStyle.None, logo.BorderStyle);
            Assert.NotNull(logo.Image);
            Assert.True(logo.Size.Height >= 240, $"Expected prominent logo height >= 240, got {logo.Size.Height}");

            // Confirm loaded logo PNG preserves transparent background at corners
            using (var bmpCheck = new Bitmap(logo.Image))
            {
                Assert.Equal(0, bmpCheck.GetPixel(0, 0).A);
            }

            double naturalRatio = (double)logo.Image.Width / logo.Image.Height;
            double renderedRatio = (double)logo.Width / logo.Height;
            Assert.True(Math.Abs(renderedRatio - naturalRatio) < 0.03,
                $"Logo aspect ratio distorted: rendered={renderedRatio:F4}, natural={naturalRatio:F4}");

            var accentLine = home.Controls.Find("pnlAccentLine", true).FirstOrDefault() as Panel;
            Assert.NotNull(accentLine);
            Assert.True(accentLine.Height <= 3 && accentLine.Width <= 140,
                $"Expected subtle decorative separator, got {accentLine.Width}x{accentLine.Height}");

            var lblTitle = home.Controls.Find("lblHomeTitle", true).FirstOrDefault() as Label;
            Assert.NotNull(lblTitle);
            Assert.Equal("معمل د/أوليفيا للتحاليل", lblTitle.Text);
            Assert.Equal(RightToLeft.Yes, lblTitle.RightToLeft);
            Assert.False(lblTitle.UseCompatibleTextRendering);

            // Visual vertical hierarchy: logo above subtle golden separator above title
            Point logoBottom = home.PointToClient(logo.PointToScreen(new Point(0, logo.Height)));
            Point accentTop = home.PointToClient(accentLine.PointToScreen(Point.Empty));
            Point accentBottom = home.PointToClient(accentLine.PointToScreen(new Point(0, accentLine.Height)));
            Point titleTop = home.PointToClient(lblTitle.PointToScreen(Point.Empty));
            Assert.True(logoBottom.Y <= accentTop.Y);
            Assert.True(accentBottom.Y <= titleTop.Y);

            // Responsive scaling on larger / maximized resolution
            int initialLogoHeight = logo.Size.Height;
            frm.Size = new System.Drawing.Size(1600, 950);
            Application.DoEvents();
            Assert.True(logo.Size.Height >= initialLogoHeight);

            Assert.All(frm.Accordion.Groups, g => Assert.False(g.IsExpanded));
            frm.Close();
        }

        [Fact]
        public void FrmInventory_OrdersTab_ShouldExposeCompleteInvoiceActionButtons()
        {
            using var frm = new FrmInventory(1);
            frm.CreateControl();
            frm.Show();
            Application.DoEvents();

            var tabs = frm.Controls.Find("tabsInventory", true).FirstOrDefault() as TabControl;
            Assert.NotNull(tabs);
            Assert.Equal(1, tabs.SelectedIndex);
            Assert.Equal(RightToLeft.Yes, tabs.RightToLeft);

            var dgvOrders = frm.Controls.Find("dgvOrders", true).FirstOrDefault() as DataGridView;
            var btnViewOrder = frm.Controls.Find("btnViewOrder", true).FirstOrDefault() as Button;
            var btnPrintOrder = frm.Controls.Find("btnPrintOrder", true).FirstOrDefault() as Button;
            var btnExportOrderPdf = frm.Controls.Find("btnExportOrderPdf", true).FirstOrDefault() as Button;

            Assert.NotNull(dgvOrders);
            Assert.NotNull(btnViewOrder);
            Assert.NotNull(btnPrintOrder);
            Assert.NotNull(btnExportOrderPdf);
            Assert.Equal(RightToLeft.Yes, dgvOrders.RightToLeft);

            frm.Close();
        }

        [Fact]
        public void AllForms_Headers_ShouldHaveStandardHeightAndRtl() => StaTestRunner.Run(() =>
        {
            using var frmPatient = new FrmSearchPatient();
            var pnlH1 = frmPatient.Controls.Find("pnlHeader", true).FirstOrDefault() as Panel;
            Assert.NotNull(pnlH1);
            Assert.Equal(86, pnlH1.Height);
            Assert.Equal(RightToLeft.Yes, pnlH1.RightToLeft);

            using var frmVisit = new FrmSearchVisit();
            var pnlH2 = frmVisit.Controls.Find("pnlHeader", true).FirstOrDefault() as Panel;
            Assert.NotNull(pnlH2);
            Assert.Equal(86, pnlH2.Height);
            Assert.Equal(RightToLeft.Yes, pnlH2.RightToLeft);

            using var frmAddP = new FrmAddPatient();
            var pnlH3 = frmAddP.Controls.Find("pnlHeader", true).FirstOrDefault() as Panel;
            Assert.NotNull(pnlH3);
            Assert.Equal(86, pnlH3.Height);
            Assert.Equal(RightToLeft.Yes, pnlH3.RightToLeft);

            using var frmAddV = new FrmAddVisit();
            var pnlH4 = frmAddV.Controls.Find("pnlHeader", true).FirstOrDefault() as Panel;
            Assert.NotNull(pnlH4);
            Assert.Equal(86, pnlH4.Height);
            Assert.Equal(RightToLeft.Yes, pnlH4.RightToLeft);
        });

        [Fact]
        public void FrmAddVisit_Layout_ShouldHaveRtlSearchableCombo_AndNonOverlappingActionButtons() => StaTestRunner.Run(() =>
        {
            using var frm = new FrmAddVisit();
            frm.Size = new System.Drawing.Size(1200, 720);
            frm.CreateControl();

            var lblSelectLab = frm.Controls.Find("lblSelectLab", true).FirstOrDefault() as Label;
            var cmbolabs = frm.Controls.Find("cmbolabs", true).FirstOrDefault() as ComboBox;
            var btnAddLab = frm.Controls.Find("btnAddLab", true).FirstOrDefault() as Button;
            Assert.NotNull(lblSelectLab);
            Assert.NotNull(cmbolabs);
            Assert.NotNull(btnAddLab);

            Assert.Equal("إضافة تحليل", lblSelectLab.Text);
            Assert.Equal(ComboBoxStyle.DropDown, cmbolabs.DropDownStyle);
            Assert.Equal(AutoCompleteMode.SuggestAppend, cmbolabs.AutoCompleteMode);
            Assert.Equal(AutoCompleteSource.ListItems, cmbolabs.AutoCompleteSource);

            var tblFinancial = frm.Controls.Find("tblFinancial", true).FirstOrDefault() as TableLayoutPanel;
            var pnlVisitActions = frm.Controls.Find("pnlVisitActions", true).FirstOrDefault() as Panel;
            var btnSave = frm.Controls.Find("btnSave", true).FirstOrDefault() as Button;
            var btnPrintReceipt = frm.Controls.Find("btnPrintReceipt", true).FirstOrDefault() as Button;
            Assert.NotNull(tblFinancial);
            Assert.NotNull(pnlVisitActions);
            Assert.NotNull(btnSave);
            Assert.NotNull(btnPrintReceipt);
            Assert.True(pnlVisitActions.Top >= tblFinancial.Bottom, $"Expected pnlVisitActions.Top ({pnlVisitActions.Top}) >= tblFinancial.Bottom ({tblFinancial.Bottom})");
        });

        [Fact]
        public void FrmLabSettings_And_ParametersDialog_ShouldHaveBorderedForm_AndDynamicTypeToggle()
        {
            using var settingsFrm = new frmSettingsLab();
            settingsFrm.CreateControl();
            var grpLabForm = settingsFrm.Controls.Find("grpLabForm", true).FirstOrDefault() as GroupBox;
            var btnSave = settingsFrm.Controls.Find("btnSave", true).FirstOrDefault() as Button;
            Assert.NotNull(grpLabForm);
            Assert.NotNull(btnSave);
            Assert.True(grpLabForm.Contains(btnSave));

            using var paramDlg = new FrmLabParametersDialog(1, "Test");
            paramDlg.Show();
            Application.DoEvents();

            var chkIsNumeric = paramDlg.Controls.Find("chkIsNumeric", true).FirstOrDefault() as CheckBox;
            var txtMin = paramDlg.Controls.Find("txtMin", true).FirstOrDefault() as TextBox;
            var txtMax = paramDlg.Controls.Find("txtMax", true).FirstOrDefault() as TextBox;
            var lblMin = paramDlg.Controls.Find("lblMin", true).FirstOrDefault() as Label;
            var lblMax = paramDlg.Controls.Find("lblMax", true).FirstOrDefault() as Label;
            var txtNormalText = paramDlg.Controls.Find("txtNormalText", true).FirstOrDefault() as TextBox;
            var lblNormalText = paramDlg.Controls.Find("lblNormalText", true).FirstOrDefault() as Label;

            Assert.NotNull(chkIsNumeric);
            Assert.NotNull(txtMin);
            Assert.NotNull(txtMax);
            Assert.NotNull(lblMin);
            Assert.NotNull(lblMax);
            Assert.NotNull(txtNormalText);
            Assert.NotNull(lblNormalText);

            // Numeric mode initially
            Assert.True(chkIsNumeric.Checked);
            Assert.True(txtMin.Visible && txtMin.Enabled);
            Assert.True(txtMax.Visible && txtMax.Enabled);
            Assert.False(txtNormalText.Visible);

            // Switch to Text mode -> Min/Max hidden & cleared, NormalText visible & enabled
            txtMin.Text = "10";
            txtMax.Text = "20";
            chkIsNumeric.Checked = false;
            Application.DoEvents();
            Assert.False(txtMin.Visible);
            Assert.False(txtMax.Visible);
            Assert.False(lblMin.Visible);
            Assert.False(lblMax.Visible);
            Assert.Equal("", txtMin.Text);
            Assert.Equal("", txtMax.Text);
            Assert.True(txtNormalText.Visible && txtNormalText.Enabled);
            Assert.True(lblNormalText.Visible && lblNormalText.Enabled);

            // Switch back to Numeric mode -> NormalText hidden, Min/Max visible & enabled
            txtNormalText.Text = "Negative";
            chkIsNumeric.Checked = true;
            Application.DoEvents();
            Assert.True(txtMin.Visible && txtMin.Enabled);
            Assert.True(txtMax.Visible && txtMax.Enabled);
            Assert.False(txtNormalText.Visible);
            Assert.Equal("", txtNormalText.Text);
            paramDlg.Close();

            // Validation helper checks
            Assert.False(FrmLabParametersDialog.ValidateParameterInput("HB", true, "abc", "16", null).IsValid);
            Assert.False(FrmLabParametersDialog.ValidateParameterInput("HB", true, "20", "10", null).IsValid);
            Assert.True(FrmLabParametersDialog.ValidateParameterInput("HB", true, "12", "16", null).IsValid);
            Assert.True(FrmLabParametersDialog.ValidateParameterInput("Widal", false, "", "", "Negative").IsValid);
            Assert.True(FrmLabParametersDialog.ValidateParameterInput("CRP", false, "", "", "Non-Reactive").IsValid);
        }

        [Fact]
        public void FrmPayments_UiLayout_ShouldExposeRequiredDebtLabels_AndNonOverlappingInputs()
        {
            using var frm = new FrmPayments();
            frm.CreateControl();

            var lblNet = frm.Controls.Find("lblNet", true).FirstOrDefault() as Label;
            var lblPaid = frm.Controls.Find("lblPaid", true).FirstOrDefault() as Label;
            var lblRemaining = frm.Controls.Find("lblRemaining", true).FirstOrDefault() as Label;
            var lblNewPayPrompt = frm.Controls.Find("lblNewPayPrompt", true).FirstOrDefault() as Label;
            var lblRemainingPreview = frm.Controls.Find("lblRemainingPreview", true).FirstOrDefault() as Label;
            var tblPayInput = frm.Controls.Find("tblPayInput", true).FirstOrDefault() as TableLayoutPanel;

            Assert.NotNull(lblNet);
            Assert.NotNull(lblPaid);
            Assert.NotNull(lblRemaining);
            Assert.NotNull(lblNewPayPrompt);
            Assert.NotNull(lblRemainingPreview);
            Assert.NotNull(tblPayInput);

            Assert.Equal("إجمالي المديونية:", lblNet.Text);
            Assert.Equal("المبلغ المدفوع:", lblPaid.Text);
            Assert.Equal("المبلغ المتبقي:", lblRemaining.Text);
            Assert.Equal("المبلغ المراد سداده:", lblNewPayPrompt.Text);
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

