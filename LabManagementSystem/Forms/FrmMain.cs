using LabManagementSystem.Common;
using LabManagementSystem.Configuration;
using LabManagementSystem.Forms;
using Timer = System.Windows.Forms.Timer;

namespace LabManagementSystem
{
    public partial class FrmMain : Form
    {
        private Button? _activeNavButton;
        private Button? _btnDashboard;
        private Form? _currentChildForm;
        private readonly Timer _clockTimer;

        public FrmMain()
        {
            InitializeComponent();

            // Setup lab header from config
            var labSettings = AppConfig.GetLabSettings();
            if (!string.IsNullOrWhiteSpace(labSettings.LabName))
                lblLabName.Text = labSettings.LabName;
            if (!string.IsNullOrWhiteSpace(labSettings.LabSubtitle))
                //lblLabSubtitle.Text = labSettings.LabSubtitle;

            // Live clock timer
            _clockTimer = new Timer { Interval = 1000 };
            _clockTimer.Tick += (s, e) => UpdateClock();
            _clockTimer.Start();
            UpdateClock();

            // Border styling on header
            pnlHeader.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeManager.Border, 1);
                e.Graphics.DrawLine(pen, 0, pnlHeader.Height - 1, pnlHeader.Width, pnlHeader.Height - 1);
            };

            // Header lab profile info
            AppConfig.LabSettingsChanged += settings =>
            {
                if (!IsDisposed)
                {
                    lblLabName.Text = settings.LabName;
                    //lblLabSubtitle.Text = settings.LabSubtitle;
                }
            };

            BuildSidebarNavigation();

            Load += (s, e) => OpenDashboard();
        }

        private void UpdateClock()
        {
            lblClock.Text = $"{DateTime.Now:dddd، dd MMMM yyyy  -  HH:mm:ss}";
        }

        private void BuildSidebarNavigation()
        {
            pnlSidebar.Controls.Clear();
            pnlSidebar.SuspendLayout();

            // Add navigation buttons in reverse dock order (Bottom-up dock)
            AddNavGroupHeader("⚙️ النظام والإعدادات");
            var btnSystemSettings = CreateNavButton("⚙️ بيانات وإعدادات المعمل", () => OpenView(new FrmSystemSettings(), "بيانات وإعدادات المعمل الرسمية"));
            var btnExternalLabs = CreateNavButton("🏥 المعامل الخارجية", () => OpenView(new FrmBaseExternalLabs(), "المعامل والمراكز الخارجية"));
            var btnLabSettings = CreateNavButton("📋 الفحوصات والأسعار", () => OpenView(new frmSettingsLab(), "إعدادات الفحوصات الطبية"));

            AddNavGroupHeader("📦 اللوجستيات والمخازن");
            var btnInventory = CreateNavButton("📦 المخزون والمشتريات", () => OpenView(new FrmInventory(), "إدارة المخزون والمستلزمات"));
            var btnTasks = CreateNavButton("📝 المهام والنواقص", () => OpenView(new FrmTasks(), "سجل المهام والنواقص والتنبيهات"));

            AddNavGroupHeader("💰 الحسابات والتقارير");
            var btnReports = CreateNavButton("📊 التقارير المالية", () => OpenView(new FrmFinancialReports(), "التقارير المالية والإحصائية"));
            var btnDebts = CreateNavButton("💳 مديونيات المرضى", () => OpenView(new FrmDebts(), "إدارة مديونيات ومستحقات المرضى"));

            AddNavGroupHeader("🧪 الزيارات والفحوصات");
            var btnAddVisit = CreateNavButton("➕ تسجيل زيارة جديدة", () => OpenAddVisit());
            var btnSearchVisit = CreateNavButton("🔍 سجل الزيارات", () => OpenSearchVisits());

            AddNavGroupHeader("👥 إدارة المرضى");
            var btnAddPatient = CreateNavButton("➕ مريض جديد", () => OpenAddPatient());
            var btnSearchPatient = CreateNavButton("🔍 سجل المرضى", () => OpenSearchPatients());

            AddNavGroupHeader("🏠 الرئيسية");
            _btnDashboard = CreateNavButton("📈 لوحة التحكم والعمليات", () => OpenDashboard());

            pnlSidebar.ResumeLayout();
        }

        private void AddNavGroupHeader(string title)
        {
            var lbl = new Label
            {
                Text = title,
                Dock = DockStyle.Top,
                Height = 32,
                Font = new Font(ThemeManager.PreferredFontFamily, 8.5f, FontStyle.Bold),
                ForeColor = ColorTranslator.FromHtml("#64748B"),
                TextAlign = ContentAlignment.BottomRight,
                Padding = new Padding(14, 0, 14, 4),
                BackColor = Color.Transparent
            };
            pnlSidebar.Controls.Add(lbl);
            lbl.SendToBack();
        }

        private Button CreateNavButton(string text, Action onClick)
        {
            var btn = new Button
            {
                Text = $"  {text}",
                Dock = DockStyle.Top,
                Height = 44,
                FlatStyle = FlatStyle.Flat,
                Font = new Font(ThemeManager.PreferredFontFamily, 10f, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#E2E8F0"),
                BackColor = ThemeManager.SidebarBackground,
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(12, 0, 12, 0),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = ThemeManager.SidebarHover;

            btn.Click += (s, e) =>
            {
                SetActiveNavButton(btn);
                onClick();
            };

            pnlSidebar.Controls.Add(btn);
            btn.SendToBack();

            return btn;
        }

        private void SetActiveNavButton(Button btn)
        {
            if (_activeNavButton != null && _activeNavButton != btn)
            {
                _activeNavButton.BackColor = ThemeManager.SidebarBackground;
                _activeNavButton.ForeColor = ColorTranslator.FromHtml("#E2E8F0");
                _activeNavButton.Font = new Font(ThemeManager.PreferredFontFamily, 10f, FontStyle.Regular);
            }

            _activeNavButton = btn;
            _activeNavButton.BackColor = ThemeManager.SidebarActive;
            _activeNavButton.ForeColor = Color.White;
            _activeNavButton.Font = new Font(ThemeManager.PreferredFontFamily, 10f, FontStyle.Bold);
        }

        public void OpenView(Form childForm, string sectionTitle)
        {
            lblCurrentSection.Text = sectionTitle;

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

        public void OpenDashboard()
        {
            if (_btnDashboard != null) SetActiveNavButton(_btnDashboard);
            var dash = new FrmDashboard();
            dash.QuickAddVisitRequested += () => OpenAddVisit();
            dash.QuickAddPatientRequested += () => OpenAddPatient();
            dash.QuickPaymentsRequested += () => OpenView(new FrmDebts(), "مديونيات ومستحقات المرضى");
            dash.QuickReportsRequested += () => OpenView(new FrmFinancialReports(), "التقارير المالية والإحصائية");

            OpenView(dash, "لوحة التحكم والعمليات اليومية");
        }

        private void OpenSearchPatients()
        {
            var frm = new FrmSearchPatient();
            frm.AddPatientClicked += () => OpenAddPatient();
            OpenView(frm, "سجل وبيانات المرضى");
        }

        private void OpenAddPatient()
        {
            var frm = new FrmAddPatient();
            frm.Saved += () => OpenSearchPatients();
            frm.Cancelled += () => OpenSearchPatients();
            OpenView(frm, "تسجيل مريض جديد");
        }

        private void OpenSearchVisits()
        {
            var frm = new FrmSearchVisit();
            frm.AddvisitClicked += () => OpenAddVisit();
            OpenView(frm, "سجل وبحث الزيارات الطبية");
        }

        private void OpenAddVisit()
        {
            var frm = new FrmAddVisit();
            frm.Saved += () => OpenSearchVisits();
            frm.Cancelled += () => OpenSearchVisits();
            OpenView(frm, "تسجيل زيارة وفحوصات جديدة");
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _clockTimer.Stop();
            _clockTimer.Dispose();

            if (_currentChildForm != null)
            {
                _currentChildForm.Close();
                _currentChildForm.Dispose();
            }

            base.OnFormClosed(e);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void pnlSidebar_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
