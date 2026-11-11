using System.Globalization;
using LabManagementSystem.Common;
using LabManagementSystem.Enums;
using LabManagementSystem.Models;
using LabManagementSystem.Printing;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public partial class FrmPayments : Form
    {
        private readonly IBillingService _billingService;
        private readonly IVisitService _visitService;

        private int _visitId;
        private Visit? _visit;
        private decimal _currentRemaining;
        private bool _isSubmitting;

        private sealed class PaymentMethodOption
        {
            public string Text { get; init; } = "";
            public int Value { get; init; }
            public override string ToString() => Text;
        }

        public FrmPayments()
        {
            InitializeComponent();
            _billingService = ServiceLocator.BillingService;
            _visitService = ServiceLocator.VisitService;
            FormStyler.StyleHeaderPanel(pnlHeader, lblTitle, lblSubtitle);
        }

        public FrmPayments(int visitId)
        {
            InitializeComponent();
            _billingService = ServiceLocator.BillingService;
            _visitService = ServiceLocator.VisitService;
            _visitId = visitId;
            FormStyler.StyleHeaderPanel(pnlHeader, lblTitle, lblSubtitle);
        }

        private async void FrmPayments_Load(object sender, EventArgs e)
        {
            FormStyler.ApplyFormTheme(this, "خزينة وسداد دفعات الزيارة");
            FormStyler.StyleHeaderPanel(pnlHeader, lblTitle, lblSubtitle);
            FormStyler.StyleButton(btnAddPayment, ButtonStyle.Success);
            FormStyler.StyleButton(btnPayFull, ButtonStyle.Outline);
            FormStyler.StyleButton(btnPrintReceipt, ButtonStyle.Primary);
            FormStyler.StyleButton(btnPrintInvoice, ButtonStyle.Secondary);
            FormStyler.StyleButton(btnClose, ButtonStyle.Secondary);

            FormStyler.StyleInput(txtNewPayment);
            FormStyler.StyleInput(cmbPaymentMethod);

            DataGridViewStyler.Apply(dgvPayments);

            // Populate Payment Methods combo
            cmbPaymentMethod.Items.Clear();
            cmbPaymentMethod.Items.Add(new PaymentMethodOption { Text = "نقدي (Cash)", Value = (int)PaymentMethodEnum.Cash });
            cmbPaymentMethod.Items.Add(new PaymentMethodOption { Text = "فيزا (Card)", Value = (int)PaymentMethodEnum.Visa });
            cmbPaymentMethod.Items.Add(new PaymentMethodOption { Text = "تحويل (Transfer)", Value = (int)PaymentMethodEnum.BankTransfer });
            cmbPaymentMethod.DisplayMember = "Text";
            cmbPaymentMethod.ValueMember = "Value";
            cmbPaymentMethod.SelectedIndex = 0;

            dtPaymentDate.Value = DateTime.Now;

            if (_visitId > 0)
            {
                await LoadVisitAsync();
            }
            else
            {
                _currentRemaining = 0;
                txtNewPayment.Text = "0.00";
                UpdateRemainingPreview();
            }
        }

        private async Task LoadVisitAsync()
        {
            _visit = await _visitService.GetVisitByIdAsync(_visitId);

            if (_visit == null)
            {
                MessageBox.Show("الزيارة غير موجودة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            txtVisitCode.Text = _visit.Id.ToString();
            txtPatientName.Text = _visit.Patient.Name;
            txtLabsCount.Text = _visit.VisitLabs.Count.ToString();
            txtTotal.Text = _visit.TotalPrice.ToString("0.00");
            txtDiscount.Text = _visit.DiscountValue?.ToString("0.00") ?? "0.00";
            textAfterDiscount.Text = _visit.NetPrice.ToString("0.00");

            decimal totalPaid = _visit.Payments.Sum(p => p.Amount);
            txtPaid.Text = totalPaid.ToString("0.00");

            decimal remaining = Math.Max(0m, Math.Round(_visit.NetPrice - totalPaid, 2));
            _currentRemaining = remaining;
            txtRemaining.Text = remaining.ToString("0.00");

            if (remaining > 0)
            {
                txtRemaining.ForeColor = ThemeManager.Danger;
                txtNewPayment.Enabled = true;
                btnAddPayment.Enabled = true;
                btnPayFull.Enabled = true;
                txtNewPayment.Text = remaining.ToString("0.00");
            }
            else
            {
                txtRemaining.ForeColor = ThemeManager.Success;
                txtNewPayment.Text = "0.00";
                txtNewPayment.Enabled = false;
                btnAddPayment.Enabled = false;
                btnPayFull.Enabled = false;
            }

            UpdateRemainingPreview();
            await LoadPaymentsGridAsync();
        }

        private void txtNewPayment_TextChanged(object? sender, EventArgs e)
        {
            UpdateRemainingPreview();
        }

        private void btnPayFull_Click(object? sender, EventArgs e)
        {
            if (_currentRemaining > 0)
            {
                txtNewPayment.Text = _currentRemaining.ToString("0.00");
                txtNewPayment.Focus();
            }
        }

        private void UpdateRemainingPreview()
        {
            if (_currentRemaining <= 0)
            {
                lblRemainingPreview.ForeColor = ThemeManager.Success;
                lblRemainingPreview.Text = "✅ الزيارة مسددة بالكامل (المتبقي بعد السداد: 0.00 ج.م)";
                return;
            }

            if (!TryParseAmount(txtNewPayment.Text, out decimal amount) || amount <= 0)
            {
                lblRemainingPreview.ForeColor = ThemeManager.Warning;
                lblRemainingPreview.Text = $"⚠️ أدخل مبلغاً أكبر من صفر (المتبقي الحالي: {_currentRemaining:0.00} ج.م)";
                return;
            }

            if (amount > _currentRemaining)
            {
                lblRemainingPreview.ForeColor = ThemeManager.Danger;
                lblRemainingPreview.Text = $"❌ المبلغ المدخل ({amount:0.00}) يتجاوز المبلغ المتبقي ({_currentRemaining:0.00} ج.م)";
                return;
            }

            decimal afterPay = Math.Max(0m, Math.Round(_currentRemaining - amount, 2));
            if (afterPay == 0m)
            {
                lblRemainingPreview.ForeColor = ThemeManager.Success;
                lblRemainingPreview.Text = "✅ المتبقي بعد السداد: 0.00 ج.م (سداد كامل للمديونية)";
            }
            else
            {
                lblRemainingPreview.ForeColor = ThemeManager.PrimaryBlue;
                lblRemainingPreview.Text = $"📊 المتبقي بعد السداد: {afterPay:0.00} ج.م";
            }
        }

        private static bool TryParseAmount(string raw, out decimal amount)
        {
            string clean = raw?.Trim() ?? "";
            return decimal.TryParse(clean, NumberStyles.Number, CultureInfo.CurrentCulture, out amount)
                || decimal.TryParse(clean, NumberStyles.Number, CultureInfo.InvariantCulture, out amount);
        }

        private async Task LoadPaymentsGridAsync()
        {
            dgvPayments.Rows.Clear();
            var payments = await _billingService.GetPaymentsForVisitAsync(_visitId);

            foreach (var p in payments)
            {
                string methodText = ((PaymentMethodEnum)p.PaymentMethod).ToArabic();
                dgvPayments.Rows.Add(
                    p.PaymentDate.ToString("yyyy/MM/dd  HH:mm"),
                    p.Amount.ToString("0.00"),
                    string.IsNullOrWhiteSpace(p.Notes) ? methodText : $"{methodText} - {p.Notes}"
                );
            }
        }

        private async void btnAddPayment_Click(object sender, EventArgs e)
        {
            if (_isSubmitting) return;
            if (_visit == null)
            {
                MessageBox.Show("يرجى اختيار زيارة صالحة أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryParseAmount(txtNewPayment.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("يرجى إدخال مبلغ دفع صحيح أكبر من صفر.", "بيانات غير صحيحة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPayment.Focus();
                return;
            }

            amount = Math.Round(amount, 2);
            if (amount > _currentRemaining)
            {
                MessageBox.Show($"المبلغ المدخل ({amount:0.00}) يتجاوز المتبقي على الزيارة ({_currentRemaining:0.00}).", "خطأ في المبلغ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPayment.Focus();
                return;
            }

            try
            {
                _isSubmitting = true;
                btnAddPayment.Enabled = false;

                int method = (cmbPaymentMethod.SelectedItem as PaymentMethodOption)?.Value ?? (int)PaymentMethodEnum.Cash;
                string notes = amount == _currentRemaining ? "سداد المتبقي بالكامل" : "سداد دفعة جزئية";

                await _billingService.RecordPaymentAsync(_visitId, amount, dtPaymentDate.Value, method, notes);

                MessageBox.Show("تم تسجيل الدفعة بنجاح في قاعدة البيانات.", "تم التسجيل", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadVisitAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء حفظ الدفعة:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isSubmitting = false;
                if (!IsDisposed && btnAddPayment != null && !btnAddPayment.IsDisposed)
                    btnAddPayment.Enabled = _currentRemaining > 0;
            }
        }

        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            if (_visit == null) return;

            var items = _visit.VisitLabs
                .Select(vl => Tuple.Create(vl.Lab?.Name ?? "تحليل", vl.LabPrice))
                .ToList();

            decimal totalPaid = _visit.Payments.Sum(p => p.Amount);
            decimal remaining = Math.Max(0, _visit.NetPrice - totalPaid);

            var model = new ThermalReceiptModel
            {
                VisitId = _visit.Id,
                VisitDate = _visit.VisitDate,
                PatientName = _visit.Patient.Name,
                MedicalCode = _visit.Patient.MedicalCode,
                DoctorName = _visit.DrName ?? "",
                Items = items,
                TotalAmount = _visit.TotalPrice,
                DiscountAmount = _visit.DiscountValue ?? 0,
                NetAmount = _visit.NetPrice,
                PaidAmount = totalPaid,
                RemainingBalance = remaining,
                PaymentMethodName = "نقدي",
                ReceiptDate = DateTime.Now
            };

            using var viewer = new DocumentViewerForm(new ThermalReceiptDocument(model), $"إيصال حراري - زيارة #{_visit.Id}");
            viewer.ShowDialog();
        }

        private void btnPrintInvoice_Click(object sender, EventArgs e)
        {
            if (_visit == null) return;

            var items = _visit.VisitLabs
                .Select(vl => Tuple.Create(vl.Lab?.Name ?? "تحليل", vl.LabPrice))
                .ToList();

            decimal totalPaid = _visit.Payments.Sum(p => p.Amount);
            decimal remaining = Math.Max(0, _visit.NetPrice - totalPaid);

            var model = new ThermalReceiptModel
            {
                VisitId = _visit.Id,
                VisitDate = _visit.VisitDate,
                PatientName = _visit.Patient.Name,
                MedicalCode = _visit.Patient.MedicalCode,
                DoctorName = _visit.DrName ?? "",
                Items = items,
                TotalAmount = _visit.TotalPrice,
                DiscountAmount = _visit.DiscountValue ?? 0,
                NetAmount = _visit.NetPrice,
                PaidAmount = totalPaid,
                RemainingBalance = remaining,
                PaymentMethodName = "نقدي",
                ReceiptDate = DateTime.Now
            };

            using var viewer = new DocumentViewerForm(new InvoiceDocument(model), $"فاتورة رسمية A5 - زيارة #{_visit.Id}");
            viewer.ShowDialog();
        }
    }
}
