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

        public FrmPayments()
        {
            InitializeComponent();
            _billingService = ServiceLocator.BillingService;
            _visitService = ServiceLocator.VisitService;
        }

        public FrmPayments(int visitId)
        {
            InitializeComponent();
            _billingService = ServiceLocator.BillingService;
            _visitService = ServiceLocator.VisitService;
            _visitId = visitId;
        }

        private async void FrmPayments_Load(object sender, EventArgs e)
        {
            FormStyler.ApplyFormTheme(this, "خزينة وسداد الزيارة");
            FormStyler.StyleButton(btnAddPayment, ButtonStyle.Primary);
            FormStyler.StyleButton(btnPrintReceipt, ButtonStyle.Primary);
            FormStyler.StyleButton(btnPrintInvoice, ButtonStyle.Secondary);
            FormStyler.StyleButton(btnClose, ButtonStyle.Secondary);

            FormStyler.StyleInput(txtNewPayment);
            FormStyler.StyleInput(cmbPaymentMethod);

            DataGridViewStyler.Apply(dgvPayments);

            // Populate Payment Methods combo
            cmbPaymentMethod.Items.Clear();
            cmbPaymentMethod.Items.Add(new { Text = "نقدي (Cash)", Value = (int)PaymentMethodEnum.Cash });
            cmbPaymentMethod.Items.Add(new { Text = "فيزا (Card)", Value = (int)PaymentMethodEnum.Visa });
            cmbPaymentMethod.Items.Add(new { Text = "تحويل (Transfer)", Value = (int)PaymentMethodEnum.BankTransfer });
            cmbPaymentMethod.DisplayMember = "Text";
            cmbPaymentMethod.ValueMember = "Value";
            cmbPaymentMethod.SelectedIndex = 0;

            dtPaymentDate.Value = DateTime.Now;

            await LoadVisitAsync();
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

            decimal remaining = Math.Max(0, _visit.NetPrice - totalPaid);
            txtRemaining.Text = remaining.ToString("0.00");

            if (remaining > 0)
            {
                txtRemaining.ForeColor = ThemeManager.Danger;
                txtNewPayment.Text = remaining.ToString("0.00");
            }
            else
            {
                txtRemaining.ForeColor = ThemeManager.Success;
                txtNewPayment.Text = "0.00";
            }

            await LoadPaymentsGridAsync();
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
            if (!decimal.TryParse(txtNewPayment.Text.Trim(), out decimal amount) || amount <= 0)
            {
                MessageBox.Show("يرجى إدخال مبلغ دفع صحيح أكبر من صفر.", "بيانات غير صحيحة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNewPayment.Focus();
                return;
            }

            decimal remaining = decimal.Parse(txtRemaining.Text);
            if (amount > remaining)
            {
                MessageBox.Show($"المبلغ المدخل ({amount:0.00}) يتجاوز المتبقي على الزيارة ({remaining:0.00}).", "خطأ في المبلغ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnAddPayment.Enabled = false;

                dynamic sel = cmbPaymentMethod.SelectedItem!;
                int method = sel.Value;

                await _billingService.RecordPaymentAsync(_visitId, amount, dtPaymentDate.Value, method, "سداد دفعة");

                MessageBox.Show("تم تسجيل الدفعة بنجاح في قاعدة البيانات.", "تم التسجيل", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await LoadVisitAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء حفظ الدفعة:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed && btnAddPayment != null && !btnAddPayment.IsDisposed)
                    btnAddPayment.Enabled = true;
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
