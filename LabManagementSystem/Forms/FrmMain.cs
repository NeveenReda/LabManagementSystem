using System;
using System.Drawing;
using System.Windows.Forms;
using LabManagementSystem.Common;
using LabManagementSystem.Forms;
using Timer = System.Windows.Forms.Timer;

namespace LabManagementSystem
{
    public partial class FrmMain : Form
    {
        private NavigationAccordion _accordion = null!;
        public NavigationAccordion Accordion => _accordion;
        private Form? _currentChildForm;

        public FrmMain()
        {
            InitializeComponent();
            pnlFooter.Height = 30;

            // Border styling on header and footer
            pnlHeader.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeManager.Border, 1);
                e.Graphics.DrawLine(pen, 0, pnlHeader.Height - 1, pnlHeader.Width, pnlHeader.Height - 1);
            };

            pnlFooter.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeManager.Border, 1);
                e.Graphics.DrawLine(pen, 0, 0, pnlFooter.Width, 0);
            };

            BuildSidebarNavigation();

            Load += (s, e) =>
            {
                OpenHome();
            };
        }

        private void BuildSidebarNavigation()
        {
            _accordion = new NavigationAccordion(pnlSidebar);

            // Group 1: لوحة التحكم
            var grpDashboard = _accordion.AddGroup("grp_dash", "لوحة التحكم", "📊");
            grpDashboard.AddSubItem("dash_home", "الرئيسية", () => OpenDashboard());

            // Group 2: إدارة المرضى
            var grpPatients = _accordion.AddGroup("grp_patients", "إدارة المرضى", "👥");
            grpPatients.AddSubItem("search_patient", "سجل المرضى", () => OpenSearchPatients());
            grpPatients.AddSubItem("add_patient", "مريض جديد", () => OpenAddPatient());

            // Group 3: إدارة الزيارات
            var grpVisits = _accordion.AddGroup("grp_visits", "إدارة الزيارات", "🧪");
            grpVisits.AddSubItem("search_visit", "سجل الزيارات", () => OpenSearchVisits());
            grpVisits.AddSubItem("add_visit", "تسجيل زيارة جديدة", () => OpenAddVisit());

            // Group 4: إدارة المعمل
            var grpLab = _accordion.AddGroup("grp_lab", "إدارة المعمل", "🔬");
            grpLab.AddSubItem("debts", "مديونات المرضى", () => OpenDebts());
            grpLab.AddSubItem("ext_labs", "المعامل الخارجية", () => OpenExternalLabs());

            // Group 5: الحسابات والتقارير
            var grpFinance = _accordion.AddGroup("grp_finance", "الحسابات والتقارير", "💰");
            grpFinance.AddSubItem("fin_reports", "التقارير المالية", () => OpenFinancialReports());
            grpFinance.AddSubItem("tasks", "المهام والنواقص", () => OpenTasks());

            // Group 6: المخزون والمشتريات
            var grpInventory = _accordion.AddGroup("grp_inventory", "المخزون والمشتريات", "📦");
            grpInventory.AddSubItem("inventory_prods", "المخزون والمشتريات", () => OpenInventoryProducts());

            // Group 7: رئيس الإعدادات
            var grpSettings = _accordion.AddGroup("grp_settings", "رئيس الإعدادات", "⚙️");
            grpSettings.AddSubItem("lab_settings", "إعدادات التحليل", () => OpenLabSettings());
            grpSettings.AddSubItem("system_settings", "إعدادات السيستم", () => OpenSystemSettings());

            _accordion.Render();
        }

        public void OpenView(Form childForm, string sectionTitle)
        {
            pnlContent.SuspendLayout();

            // Safely remove and dispose old child form without triggering re-entrant event loops
            if (_currentChildForm != null)
            {
                var old = _currentChildForm;
                _currentChildForm = null;
                pnlContent.Controls.Remove(old);
                old.Dispose();
            }

            _currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(childForm);
            childForm.Show();

            pnlContent.ResumeLayout();
        }

        public void OpenHome()
        {
            _accordion.ClearSelection();
            OpenView(new FrmHome(), "معمل د/أوليفيا للتحاليل");
        }

        public void OpenDashboard()
        {
            _accordion.SelectItemByKey("dash_home");
            var dash = new FrmDashboard();
            dash.QuickAddVisitRequested += () => OpenAddVisit();
            dash.QuickAddPatientRequested += () => OpenAddPatient();
            dash.QuickPaymentsRequested += () => OpenDebts();
            dash.QuickReportsRequested += () => OpenFinancialReports();

            OpenView(dash, "لوحة التحكم والعمليات اليومية");
        }

        public void OpenSearchPatients()
        {
            _accordion.SelectItemByKey("search_patient");
            var frm = new FrmSearchPatient();
            frm.AddPatientClicked += () => OpenAddPatient();
            OpenView(frm, "سجل وبيانات المرضى");
        }

        public void OpenAddPatient()
        {
            _accordion.SelectItemByKey("add_patient");
            var frm = new FrmAddPatient();
            frm.Saved += () => OpenSearchPatients();
            frm.Cancelled += () => OpenSearchPatients();
            OpenView(frm, "تسجيل مريض جديد");
        }

        public void OpenSearchVisits()
        {
            _accordion.SelectItemByKey("search_visit");
            var frm = new FrmSearchVisit();
            frm.AddvisitClicked += () => OpenAddVisit();
            OpenView(frm, "سجل وبحث الزيارات الطبية");
        }

        public void OpenAddVisit()
        {
            _accordion.SelectItemByKey("add_visit");
            var frm = new FrmAddVisit();
            frm.Saved += () => OpenSearchVisits();
            frm.Cancelled += () => OpenSearchVisits();
            OpenView(frm, "تسجيل زيارة وفحوصات جديدة");
        }

        public void OpenDebts()
        {
            _accordion.SelectItemByKey("debts");
            OpenView(new FrmDebts(), "مديونيات ومستحقات المرضى");
        }

        public void OpenExternalLabs()
        {
            _accordion.SelectItemByKey("ext_labs");
            OpenView(new FrmBaseExternalLabs(), "المعامل والمراكز الخارجية");
        }

        public void OpenFinancialReports()
        {
            _accordion.SelectItemByKey("fin_reports");
            OpenView(new FrmFinancialReports(), "التقارير المالية والإحصائية");
        }

        public void OpenTasks()
        {
            _accordion.SelectItemByKey("tasks");
            OpenView(new FrmTasks(), "سجل المهام والنواقص والتنبيهات");
        }

        public void OpenInventoryProducts()
        {
            _accordion.SelectItemByKey("inventory_prods");
            OpenView(new FrmInventory(0), "إدارة الأصناف والمخزون");
        }

        public void OpenLabSettings()
        {
            _accordion.SelectItemByKey("lab_settings");
            OpenView(new frmSettingsLab(), "إعدادات الفحوصات الطبية");
        }

        public void OpenSystemSettings()
        {
            _accordion.SelectItemByKey("system_settings");
            OpenView(new FrmSystemSettings(), "بيانات وإعدادات المعمل الرسمية");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_currentChildForm != null)
            {
                _currentChildForm.Close();
                _currentChildForm.Dispose();
            }

            base.OnFormClosed(e);
        }
    }
}
