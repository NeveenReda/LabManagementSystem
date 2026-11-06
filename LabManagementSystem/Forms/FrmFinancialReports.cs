using LabManagementSystem.Common;
using LabManagementSystem.Printing;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public class FrmFinancialReports : Form
    {
        private readonly IFinancialReportService _reportService;
        private LoadingOverlay _loadingOverlay = null!;
        private CancellationTokenSource? _cts;

        private DateTimePicker dtFrom = null!;
        private DateTimePicker dtTo = null!;
        private Button btnFilter = null!;
        private Button btnToday = null!;
        private Button btnThisMonth = null!;
        private Button btnPrint = null!;

        private Panel pnlKpiVisits = null!;
        private Panel pnlKpiTests = null!;
        private Panel pnlKpiGross = null!;
        private Panel pnlKpiDiscounts = null!;
        private Panel pnlKpiNet = null!;
        private Panel pnlKpiCollected = null!;
        private Panel pnlKpiDebts = null!;

        private DataGridView dgvTests = null!;
        private DataGridView dgvCategories = null!;
        private DataGridView dgvPaymentMethods = null!;

        private FinancialSummaryDto? _currentReport;

        public FrmFinancialReports()
        {
            _reportService = ServiceLocator.FinancialReportService;
            InitializeUi();
            Shown += async (s, e) => await LoadReportAsync();
        }

        private void InitializeUi()
        {
            FormStyler.ApplyFormTheme(this, "التقارير المالية والإحصائية");
            Padding = new Padding(16);

            // 1. Header Banner
            var pnlHeader = FormStyler.CreateHeaderPanel(
                "التقارير المالية والإيرادات والتحليلات الإحصائية للمعمل",
                "تحليل الإيرادات، الخصومات، المتحصلات النقدية، مبيعات كل تحليل وقسم، وتصدير التقارير الرسمية",
                "📊"
            );

            // 2. Filter Toolbar Card (Responsive TableLayoutPanel)
            var pnlFilter = FormStyler.CreateCardPanel(DockStyle.Top, new Padding(10, 8, 10, 8));
            pnlFilter.Height = 58;

            var tlpFilter = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                RightToLeft = RightToLeft.Yes
            };
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72F)); // Date selectors & actions
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F)); // Print PDF button

            var flpDateControls = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0)
            };

            var lblF = new Label { Text = "من:", AutoSize = true, Margin = new Padding(0, 8, 4, 0), Font = ThemeManager.BodyBoldFont };
            dtFrom = new DateTimePicker { Width = 115, Format = DateTimePickerFormat.Short, Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1), Font = ThemeManager.BodyFont, Margin = new Padding(4) };

            var lblT = new Label { Text = "إلى:", AutoSize = true, Margin = new Padding(6, 8, 4, 0), Font = ThemeManager.BodyBoldFont };
            dtTo = new DateTimePicker { Width = 115, Format = DateTimePickerFormat.Short, Value = DateTime.Today, Font = ThemeManager.BodyFont, Margin = new Padding(4) };

            btnFilter = new Button { Text = "🔍 عرض التقرير", Width = 120, Height = 34, Margin = new Padding(8, 2, 4, 2) };
            FormStyler.StyleButton(btnFilter, ButtonStyle.Primary);
            btnFilter.Click += async (s, e) => await LoadReportAsync();

            btnToday = new Button { Text = "اليوم", Width = 80, Height = 34, Margin = new Padding(4, 2, 4, 2) };
            FormStyler.StyleButton(btnToday, ButtonStyle.Secondary);
            btnToday.Click += async (s, e) =>
            {
                dtFrom.Value = DateTime.Today;
                dtTo.Value = DateTime.Today;
                await LoadReportAsync();
            };

            btnThisMonth = new Button { Text = "هذا الشهر", Width = 90, Height = 34, Margin = new Padding(4, 2, 4, 2) };
            FormStyler.StyleButton(btnThisMonth, ButtonStyle.Secondary);
            btnThisMonth.Click += async (s, e) =>
            {
                dtFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                dtTo.Value = DateTime.Today;
                await LoadReportAsync();
            };

            flpDateControls.Controls.Add(lblF);
            flpDateControls.Controls.Add(dtFrom);
            flpDateControls.Controls.Add(lblT);
            flpDateControls.Controls.Add(dtTo);
            flpDateControls.Controls.Add(btnFilter);
            flpDateControls.Controls.Add(btnToday);
            flpDateControls.Controls.Add(btnThisMonth);

            var flpPrint = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0)
            };

            btnPrint = new Button { Text = "🖨️ طباعة وحفظ التقرير (PDF)", Width = 210, Height = 34, Margin = new Padding(4, 2, 4, 2) };
            FormStyler.StyleButton(btnPrint, ButtonStyle.Success);
            btnPrint.Click += BtnPrint_Click;
            flpPrint.Controls.Add(btnPrint);

            tlpFilter.Controls.Add(flpDateControls, 0, 0);
            tlpFilter.Controls.Add(flpPrint, 1, 0);
            pnlFilter.Controls.Add(tlpFilter);

            // 3. KPI Cards Strip
            var tblKpis = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 95,
                ColumnCount = 7,
                RowCount = 1,
                Padding = new Padding(0, 8, 0, 8),
                BackColor = Color.Transparent
            };
            for (int i = 0; i < 7; i++)
                tblKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 14.28f));

            pnlKpiVisits = FormStyler.CreateKpiCard("عدد الزيارات", "0", ThemeManager.PrimaryBlue);
            pnlKpiTests = FormStyler.CreateKpiCard("عدد التحاليل", "0", ThemeManager.PrimaryNavy);
            pnlKpiGross = FormStyler.CreateKpiCard("إجمالي الأسعار", "0.00", ThemeManager.SecondaryText);
            pnlKpiDiscounts = FormStyler.CreateKpiCard("الخصومات", "0.00", ThemeManager.Warning);
            pnlKpiNet = FormStyler.CreateKpiCard("صافي الإيراد", "0.00", ThemeManager.PrimaryBlue);
            pnlKpiCollected = FormStyler.CreateKpiCard("المحصل الفعلي", "0.00", ThemeManager.Success);
            pnlKpiDebts = FormStyler.CreateKpiCard("المتبقي (ديون)", "0.00", ThemeManager.Danger);

            pnlKpiVisits.Dock = DockStyle.Fill;
            pnlKpiTests.Dock = DockStyle.Fill;
            pnlKpiGross.Dock = DockStyle.Fill;
            pnlKpiDiscounts.Dock = DockStyle.Fill;
            pnlKpiNet.Dock = DockStyle.Fill;
            pnlKpiCollected.Dock = DockStyle.Fill;
            pnlKpiDebts.Dock = DockStyle.Fill;

            tblKpis.Controls.Add(pnlKpiVisits, 0, 0);
            tblKpis.Controls.Add(pnlKpiTests, 1, 0);
            tblKpis.Controls.Add(pnlKpiGross, 2, 0);
            tblKpis.Controls.Add(pnlKpiDiscounts, 3, 0);
            tblKpis.Controls.Add(pnlKpiNet, 4, 0);
            tblKpis.Controls.Add(pnlKpiCollected, 5, 0);
            tblKpis.Controls.Add(pnlKpiDebts, 6, 0);

            // 4. Tabbed Breakdowns
            var tabsCard = FormStyler.CreateCardPanel(DockStyle.Fill, new Padding(8));
            var tabs = new TabControl { Dock = DockStyle.Fill, Font = ThemeManager.BodyBoldFont, RightToLeft = RightToLeft.Yes, RightToLeftLayout = true };

            // Tab 1: Tests
            var tabTests = new TabPage { Text = "📊 إيرادات الفحوصات والتحاليل" };
            dgvTests = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = false };
            DataGridViewStyler.Apply(dgvTests);
            dgvTests.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTestName", DataPropertyName = "TestName", HeaderText = "اسم التحليل", FillWeight = 40 });
            dgvTests.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCategoryName", DataPropertyName = "CategoryName", HeaderText = "القسم / التصنيف", FillWeight = 25 });
            dgvTests.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCount", DataPropertyName = "Count", HeaderText = "عدد الفحوصات", FillWeight = 15 });
            dgvTests.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTotalRevenue", DataPropertyName = "TotalRevenue", HeaderText = "إجمالي الإيراد (ج.م)", FillWeight = 20 });
            if (dgvTests.Columns["colTotalRevenue"] != null)
                dgvTests.Columns["colTotalRevenue"].DefaultCellStyle.Format = "0.00";
            tabTests.Controls.Add(dgvTests);

            // Tab 2: Categories
            var tabCategories = new TabPage { Text = "📁 إيرادات أقسام التحاليل" };
            dgvCategories = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = false };
            DataGridViewStyler.Apply(dgvCategories);
            dgvCategories.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCategoryName", DataPropertyName = "CategoryName", HeaderText = "اسم القسم", FillWeight = 50 });
            dgvCategories.Columns.Add(new DataGridViewTextBoxColumn { Name = "colCount", DataPropertyName = "Count", HeaderText = "عدد الفحوصات", FillWeight = 25 });
            dgvCategories.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTotalRevenue", DataPropertyName = "TotalRevenue", HeaderText = "إجمالي الإيراد (ج.م)", FillWeight = 25 });
            if (dgvCategories.Columns["colTotalRevenue"] != null)
                dgvCategories.Columns["colTotalRevenue"].DefaultCellStyle.Format = "0.00";
            tabCategories.Controls.Add(dgvCategories);

            // Tab 3: Payment Methods
            var tabMethods = new TabPage { Text = "💳 طرق الدفع والتحصيل" };
            dgvPaymentMethods = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = false };
            DataGridViewStyler.Apply(dgvPaymentMethods);
            dgvPaymentMethods.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMethodName", DataPropertyName = "MethodName", HeaderText = "طريقة الدفع", FillWeight = 40 });
            dgvPaymentMethods.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTransactionsCount", DataPropertyName = "TransactionsCount", HeaderText = "عدد العمليات", FillWeight = 30 });
            dgvPaymentMethods.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTotalAmount", DataPropertyName = "TotalAmount", HeaderText = "إجمالي المتحصل (ج.م)", FillWeight = 30 });
            if (dgvPaymentMethods.Columns["colTotalAmount"] != null)
                dgvPaymentMethods.Columns["colTotalAmount"].DefaultCellStyle.Format = "0.00";
            tabMethods.Controls.Add(dgvPaymentMethods);

            tabs.TabPages.AddRange(new[] { tabTests, tabCategories, tabMethods });
            tabsCard.Controls.Add(tabs);

            Controls.Add(tabsCard);
            Controls.Add(tblKpis);
            Controls.Add(pnlFilter);
            Controls.Add(pnlHeader);

            _loadingOverlay = LoadingOverlay.AttachTo(tabsCard);
        }

        private async Task LoadReportAsync()
        {
            if (IsDisposed) return;

            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            _loadingOverlay.ShowOverlay("جاري استخراج وتحليل البيانات المالية...");
            try
            {
                var from = dtFrom.Value.Date;
                var to = dtTo.Value.Date.AddDays(1).AddTicks(-1);

                _currentReport = await _reportService.GetFinancialReportAsync(from, to, token);

                if (token.IsCancellationRequested || IsDisposed) return;

                UpdateKpiValue(pnlKpiVisits, _currentReport.TotalVisits.ToString());
                UpdateKpiValue(pnlKpiTests, _currentReport.TotalTests.ToString());
                UpdateKpiValue(pnlKpiGross, $"{_currentReport.TotalGrossRevenue:0.00}");
                UpdateKpiValue(pnlKpiDiscounts, $"{_currentReport.TotalDiscounts:0.00}");
                UpdateKpiValue(pnlKpiNet, $"{_currentReport.TotalNetRevenue:0.00}");
                UpdateKpiValue(pnlKpiCollected, $"{_currentReport.TotalCollectedPayments:0.00}");
                UpdateKpiValue(pnlKpiDebts, $"{_currentReport.TotalOutstandingBalances:0.00}");

                dgvTests.DataSource = _currentReport.TestsBreakdown;
                dgvCategories.DataSource = _currentReport.CategoriesBreakdown;
                dgvPaymentMethods.DataSource = _currentReport.PaymentMethodsBreakdown;
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                if (!IsDisposed)
                    MessageBox.Show($"خطأ أثناء استخراج التقرير:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
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

        private void BtnPrint_Click(object? sender, EventArgs e)
        {
            if (_currentReport == null)
            {
                MessageBox.Show("يرجى عرض التقرير أولاً قبل الطباعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var doc = new FinancialReportDocument(_currentReport);
                using var viewer = new DocumentViewerForm(doc, $"التقرير المالي ({_currentReport.FromDate:yyyy/MM/dd} إلى {_currentReport.ToDate:yyyy/MM/dd})");
                viewer.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء فتح المعاينة:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _cts?.Cancel();
            _cts?.Dispose();
        }
    }
}
