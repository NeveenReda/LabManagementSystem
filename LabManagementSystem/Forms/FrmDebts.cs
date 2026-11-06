using LabManagementSystem.Common;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public class FrmDebts : Form
    {
        private readonly IBillingService _billingService;
        private readonly DebounceHelper _debouncer;
        private LoadingOverlay _loadingOverlay = null!;
        private CancellationTokenSource? _searchCts;

        private TextBox txtSearch = null!;
        private Button btnRefresh = null!;
        private Button btnPay = null!;
        private Label lblTotalDebt = null!;
        private Label lblCount = null!;
        private DataGridView dgvDebts = null!;
        private Panel pnlFilterCard = null!;
        private Panel pnlGridCard = null!;

        public FrmDebts()
        {
            _billingService = ServiceLocator.BillingService;
            _debouncer = new DebounceHelper(280);
            InitializeUi();
            Shown += async (s, e) => await LoadDebtsAsync();
        }

        private void InitializeUi()
        {
            FormStyler.ApplyFormTheme(this, "إدارة مديونيات ومستحقات المرضى");

            // 1. Header
            var pnlHeader = FormStyler.CreateHeaderPanel(
                "سجل مديونيات ومستحقات المرضى",
                "متابعة وتحصيل المبالغ المتبقية على الزيارات والفحوصات الطبية غير المسددة بالكامل",
                "💳"
            );

            // 2. Filter Bar (Responsive TableLayoutPanel)
            pnlFilterCard = FormStyler.CreateCardPanel(DockStyle.Top, new Padding(12, 10, 12, 10));
            pnlFilterCard.Height = 62;

            var tlpFilter = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                RightToLeft = RightToLeft.Yes
            };
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F)); // Right: Search & Actions
            tlpFilter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F)); // Left: Total Debt Summary

            var flpRight = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0)
            };

            var lblSearch = new Label
            {
                Text = "🔍 بحث:",
                AutoSize = true,
                Margin = new Padding(0, 8, 4, 0),
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText
            };

            txtSearch = new TextBox
            {
                Width = 220,
                Font = ThemeManager.BodyFont,
                PlaceholderText = "اسم المريض أو الكود أو الهاتف...",
                Margin = new Padding(4)
            };
            FormStyler.StyleInput(txtSearch);
            txtSearch.TextChanged += (s, e) =>
            {
                _debouncer.Debounce(async () => await LoadDebtsAsync());
            };

            btnRefresh = new Button
            {
                Text = "🔄 تحديث",
                Width = 100,
                Height = 34,
                Margin = new Padding(6, 2, 4, 2)
            };
            FormStyler.StyleButton(btnRefresh, ButtonStyle.Secondary);
            btnRefresh.Click += async (s, e) => await LoadDebtsAsync();

            btnPay = new Button
            {
                Text = "💵 سداد دفعة للمتبقي",
                Width = 160,
                Height = 34,
                Margin = new Padding(6, 2, 4, 2)
            };
            FormStyler.StyleButton(btnPay, ButtonStyle.Success);
            btnPay.Click += BtnPay_Click;

            flpRight.Controls.Add(lblSearch);
            flpRight.Controls.Add(txtSearch);
            flpRight.Controls.Add(btnRefresh);
            flpRight.Controls.Add(btnPay);

            var flpLeft = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0)
            };

            lblTotalDebt = new Label
            {
                Text = "إجمالي المديونيات: 0.00 ج.م",
                AutoSize = true,
                Font = ThemeManager.SubtitleFont,
                ForeColor = ThemeManager.Danger,
                Margin = new Padding(4, 6, 8, 0)
            };

            lblCount = new Label
            {
                Text = "الحالات: 0",
                AutoSize = true,
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.SecondaryText,
                Margin = new Padding(4, 8, 8, 0)
            };

            flpLeft.Controls.Add(lblTotalDebt);
            flpLeft.Controls.Add(lblCount);

            tlpFilter.Controls.Add(flpRight, 0, 0);
            tlpFilter.Controls.Add(flpLeft, 1, 0);
            pnlFilterCard.Controls.Add(tlpFilter);

            // 3. Grid Card
            pnlGridCard = FormStyler.CreateCardPanel(DockStyle.Fill, new Padding(12));

            dgvDebts = new DataGridView { Name = "dgvDebts", Dock = DockStyle.Fill, AutoGenerateColumns = false };
            DataGridViewStyler.Apply(dgvDebts);

            dgvDebts.Columns.Add(new DataGridViewTextBoxColumn { Name = "colVisitId", DataPropertyName = "VisitId", HeaderText = "# الزيارة", FillWeight = 10 });
            dgvDebts.Columns.Add(new DataGridViewTextBoxColumn { Name = "colMedicalCode", DataPropertyName = "MedicalCode", HeaderText = "الكود الطبي", FillWeight = 18 });
            dgvDebts.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPatientName", DataPropertyName = "PatientName", HeaderText = "اسم المريض", FillWeight = 30 });
            dgvDebts.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPhone", DataPropertyName = "Phone", HeaderText = "رقم الهاتف", FillWeight = 18 });
            dgvDebts.Columns.Add(new DataGridViewTextBoxColumn { Name = "colVisitDate", DataPropertyName = "VisitDate", HeaderText = "تاريخ الزيارة", FillWeight = 16 });
            dgvDebts.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNetAmount", DataPropertyName = "NetAmount", HeaderText = "المطلوب", FillWeight = 14 });
            dgvDebts.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTotalPaid", DataPropertyName = "TotalPaid", HeaderText = "المدفوع", FillWeight = 14 });
            dgvDebts.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRemainingBalance", DataPropertyName = "RemainingBalance", HeaderText = "المتبقي (دين)", FillWeight = 16 });

            if (dgvDebts.Columns["colVisitDate"] != null)
                dgvDebts.Columns["colVisitDate"].DefaultCellStyle.Format = "yyyy/MM/dd";
            if (dgvDebts.Columns["colRemainingBalance"] != null)
            {
                dgvDebts.Columns["colRemainingBalance"].DefaultCellStyle.ForeColor = ThemeManager.Danger;
                dgvDebts.Columns["colRemainingBalance"].DefaultCellStyle.Font = ThemeManager.BodyBoldFont;
            }

            dgvDebts.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0) BtnPay_Click(this, EventArgs.Empty);
            };

            pnlGridCard.Controls.Add(dgvDebts);

            Controls.Add(pnlGridCard);
            Controls.Add(pnlFilterCard);
            Controls.Add(pnlHeader);

            _loadingOverlay = LoadingOverlay.AttachTo(pnlGridCard);
        }

        private async Task LoadDebtsAsync()
        {
            if (IsDisposed) return;

            _searchCts?.Cancel();
            _searchCts?.Dispose();
            _searchCts = new CancellationTokenSource();
            var token = _searchCts.Token;

            _loadingOverlay.ShowOverlay("جاري تحميل مديونيات المرضى...");
            try
            {
                string term = txtSearch.Text.Trim();
                var debts = await _billingService.GetOutstandingDebtsAsync(term, token);

                if (token.IsCancellationRequested || IsDisposed) return;

                dgvDebts.DataSource = debts;
                decimal total = debts.Sum(d => d.RemainingBalance);
                lblTotalDebt.Text = $"إجمالي المديونيات: {total:0.00} ج.م";
                lblCount.Text = $"الحالات: {debts.Count}";
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                if (!IsDisposed)
                    MessageBox.Show($"خطأ أثناء تحميل المديونيات:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                    _loadingOverlay.HideOverlay();
            }
        }

        private async void BtnPay_Click(object? sender, EventArgs e)
        {
            if (dgvDebts.CurrentRow?.DataBoundItem is PatientDebtDto item)
            {
                using var frmPay = new FrmPayments(item.VisitId);
                frmPay.ShowDialog(this);
                await LoadDebtsAsync();
            }
            else
            {
                MessageBox.Show("يرجى تحديد زيارة من القائمة لتسجيل الدفعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _debouncer.Dispose();
            _searchCts?.Dispose();
        }
    }
}
