using LabManagementSystem.Common;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public class FrmDashboard : Form
    {
        private readonly IVisitService _visitService;
        private LoadingOverlay _loadingOverlay = null!;

        private Panel pnlKpiVisits = null!;
        private Panel pnlKpiPatients = null!;
        private Panel pnlKpiPending = null!;
        private Panel pnlKpiCompleted = null!;
        private Panel pnlKpiCollected = null!;
        private Panel pnlKpiDebts = null!;

        private DataGridView dgvRecentVisits = null!;
        private CancellationTokenSource? _cts;

        public event Action? QuickAddVisitRequested;
        public event Action? QuickAddPatientRequested;
        public event Action? QuickPaymentsRequested;
        public event Action? QuickReportsRequested;

        public FrmDashboard()
        {
            _visitService = ServiceLocator.VisitService;
            InitializeUi();
        }

        private void InitializeUi()
        {
            FormStyler.ApplyFormTheme(this, "لوحة التحكم والعمليات");

            var pnlMainContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(16),
                BackColor = ThemeManager.Background
            };

            // 1. Header Banner
            var pnlHeader = FormStyler.CreateHeaderPanel(
                "لوحة التحكم والمؤشرات التشغيلية للمعمل",
                "متابعة حية وفورية لحركة الزيارات، الفحوصات الطبية، والتحصيلات المالية اليومية",
                "📈"
            );

            // 2. Quick Action Toolbar
            var pnlActions = FormStyler.CreateCardPanel(DockStyle.Top, new Padding(8, 8, 8, 8));
            pnlActions.Height = 58;

            var flpActions = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(4)
            };

            var btnAddVisit = new Button { Text = "➕ تسجيل زيارة جديدة", Width = 160, Margin = new Padding(4) };
            FormStyler.StyleButton(btnAddVisit, ButtonStyle.Primary);
            btnAddVisit.Click += (s, e) => QuickAddVisitRequested?.Invoke();

            var btnAddPatient = new Button { Text = "👤 إضافة مريض جديد", Width = 150, Margin = new Padding(4) };
            FormStyler.StyleButton(btnAddPatient, ButtonStyle.Secondary);
            btnAddPatient.Click += (s, e) => QuickAddPatientRequested?.Invoke();

            var btnDebts = new Button { Text = "💳 سداد مديونية / دفعة", Width = 160, Margin = new Padding(4) };
            FormStyler.StyleButton(btnDebts, ButtonStyle.Warning);
            btnDebts.Click += (s, e) => QuickPaymentsRequested?.Invoke();

            var btnReports = new Button { Text = "📊 التقرير المالي اليومي", Width = 160, Margin = new Padding(4) };
            FormStyler.StyleButton(btnReports, ButtonStyle.Outline);
            btnReports.Click += (s, e) => QuickReportsRequested?.Invoke();

            var btnRefresh = new Button { Text = "🔄 تحديث المؤشرات", Width = 140, Margin = new Padding(4) };
            FormStyler.StyleButton(btnRefresh, ButtonStyle.Secondary);
            btnRefresh.Click += async (s, e) => await RefreshDashboardAsync();

            flpActions.Controls.Add(btnAddVisit);
            flpActions.Controls.Add(btnAddPatient);
            flpActions.Controls.Add(btnDebts);
            flpActions.Controls.Add(btnReports);
            flpActions.Controls.Add(btnRefresh);

            pnlActions.Controls.Add(flpActions);

            // 3. KPI Cards Grid
            var tblKpis = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 110,
                ColumnCount = 6,
                RowCount = 1,
                Padding = new Padding(0, 10, 0, 10),
                BackColor = Color.Transparent
            };
            for (int i = 0; i < 6; i++)
                tblKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66f));

            pnlKpiVisits = FormStyler.CreateKpiCard("زيارات اليوم", "0", ThemeManager.PrimaryBlue, "إجمالي الحالات المسجلة");
            pnlKpiPatients = FormStyler.CreateKpiCard("مرضى جدد اليوم", "0", ThemeManager.PrimaryNavy, "ملفات جديدة");
            pnlKpiPending = FormStyler.CreateKpiCard("تحاليل قيد الفحص", "0", ThemeManager.Warning, "لم تسجل نتائجها بعد");
            pnlKpiCompleted = FormStyler.CreateKpiCard("تحاليل مكتملة", "0", ThemeManager.Success, "تم اعتماد نتائجها");
            pnlKpiCollected = FormStyler.CreateKpiCard("تحصيلات اليوم", "0.00 ج.م", ThemeManager.Success, "خزينة اليوم");
            pnlKpiDebts = FormStyler.CreateKpiCard("إجمالي المديونيات", "0.00 ج.م", ThemeManager.Danger, "مستحقات معلقة");

            pnlKpiVisits.Dock = DockStyle.Fill;
            pnlKpiPatients.Dock = DockStyle.Fill;
            pnlKpiPending.Dock = DockStyle.Fill;
            pnlKpiCompleted.Dock = DockStyle.Fill;
            pnlKpiCollected.Dock = DockStyle.Fill;
            pnlKpiDebts.Dock = DockStyle.Fill;

            tblKpis.Controls.Add(pnlKpiVisits, 0, 0);
            tblKpis.Controls.Add(pnlKpiPatients, 1, 0);
            tblKpis.Controls.Add(pnlKpiPending, 2, 0);
            tblKpis.Controls.Add(pnlKpiCompleted, 3, 0);
            tblKpis.Controls.Add(pnlKpiCollected, 4, 0);
            tblKpis.Controls.Add(pnlKpiDebts, 5, 0);

            // 4. Recent Visits Card & Table
            var pnlVisitsCard = FormStyler.CreateCardPanel(DockStyle.Fill, new Padding(14));

            var lblVisitsTitle = new Label
            {
                Text = "⏱️ آخر الزيارات المسجلة بالنظام اليوم",
                Font = ThemeManager.SubtitleFont,
                ForeColor = ThemeManager.PrimaryNavy,
                Dock = DockStyle.Top,
                Height = 32,
                TextAlign = ContentAlignment.MiddleRight
            };

            dgvRecentVisits = new DataGridView { Name = "dgvRecentVisits", Dock = DockStyle.Fill, AutoGenerateColumns = false };
            DataGridViewStyler.Apply(dgvRecentVisits);

            dgvRecentVisits.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", DataPropertyName = "Id", HeaderText = "رقم الزيارة", FillWeight = 12 });
            dgvRecentVisits.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMedicalCode", DataPropertyName = "MedicalCode", HeaderText = "الكود الطبي", FillWeight = 18 });
            dgvRecentVisits.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPatientName", DataPropertyName = "PatientName", HeaderText = "اسم المريض", FillWeight = 28 });
            dgvRecentVisits.Columns.Add(new DataGridViewTextBoxColumn { Name = "colVisitDate", DataPropertyName = "VisitDate", HeaderText = "وقت الزيارة", FillWeight = 16 });
            dgvRecentVisits.Columns.Add(new DataGridViewTextBoxColumn { Name = "colLabsCount", DataPropertyName = "LabsCount", HeaderText = "عدد الفحوصات", FillWeight = 12 });
            dgvRecentVisits.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNetPrice", DataPropertyName = "NetPrice", HeaderText = "المطلوب", FillWeight = 14 });
            dgvRecentVisits.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPaidAmount", DataPropertyName = "PaidAmount", HeaderText = "المدفوع", FillWeight = 14 });
            dgvRecentVisits.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRemainingAmount", DataPropertyName = "RemainingAmount", HeaderText = "المتبقي", FillWeight = 14 });

            if (dgvRecentVisits.Columns["colVisitDate"] != null)
                dgvRecentVisits.Columns["colVisitDate"].DefaultCellStyle.Format = "HH:mm  yyyy/MM/dd";
            if (dgvRecentVisits.Columns["colRemainingAmount"] != null)
                dgvRecentVisits.Columns["colRemainingAmount"].DefaultCellStyle.Font = ThemeManager.BodyBoldFont;

            pnlVisitsCard.Controls.Add(dgvRecentVisits);
            pnlVisitsCard.Controls.Add(lblVisitsTitle);

            pnlMainContainer.Controls.Add(pnlVisitsCard);
            pnlMainContainer.Controls.Add(tblKpis);
            pnlMainContainer.Controls.Add(pnlActions);
            pnlMainContainer.Controls.Add(pnlHeader);

            Controls.Add(pnlMainContainer);

            _loadingOverlay = LoadingOverlay.AttachTo(pnlMainContainer);

            Load += async (s, e) => await RefreshDashboardAsync();
        }

        public async Task RefreshDashboardAsync()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            _loadingOverlay.ShowOverlay("جاري تحديث بيانات ومؤشرات المعمل...");
            try
            {
                var stats = await _visitService.GetDashboardStatsAsync(token);

                if (token.IsCancellationRequested) return;

                UpdateKpiValue(pnlKpiVisits, stats.TodayVisitsCount.ToString());
                UpdateKpiValue(pnlKpiPatients, stats.TodayPatientsCount.ToString());
                UpdateKpiValue(pnlKpiPending, stats.PendingLabsCount.ToString());
                UpdateKpiValue(pnlKpiCompleted, stats.CompletedLabsCount.ToString());
                UpdateKpiValue(pnlKpiCollected, $"{stats.TodayCollectedPayments:0.00} ج.م");
                UpdateKpiValue(pnlKpiDebts, $"{stats.TotalOutstandingDebts:0.00} ج.م");

                dgvRecentVisits.DataSource = stats.RecentVisits;

                // Colorize remaining column in recent visits
                foreach (DataGridViewRow row in dgvRecentVisits.Rows)
                {
                    if (row.DataBoundItem is VisitSummaryDto item)
                    {
                        var cell = row.Cells["colRemainingAmount"];
                        if (cell != null)
                        {
                            cell.Style.ForeColor = item.RemainingAmount > 0 ? ThemeManager.Danger : ThemeManager.Success;
                        }
                    }
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تحديث المؤشرات:\n{ex.Message}", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                _loadingOverlay.HideOverlay();
            }
        }

        private static void UpdateKpiValue(Panel card, string val)
        {
            foreach (Control c in card.Controls)
            {
                if (c is Label lbl && lbl.Font.Size >= 15f)
                {
                    lbl.Text = val;
                    break;
                }
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _cts?.Cancel();
            _cts?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
