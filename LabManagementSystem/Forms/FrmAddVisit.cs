using LabManagementSystem.Common;
using LabManagementSystem.Enums;
using LabManagementSystem.Models;
using LabManagementSystem.Printing;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public partial class FrmAddVisit : Form
    {
        private readonly IVisitService _visitService;
        private readonly IPatientService _patientService;
        private readonly ILookupCacheService _lookupCache;

        private Visit? _visit;
        private int _lastSavedVisitId;
        private Patient? _selectedPatient;
        private Lab? _selectedLab;
        private List<Lab> _availableLabs = new();
        private bool _isUpdatingDiscount = false;

        public event Action? Saved;
        public event Action? Cancelled;

        public FrmAddVisit()
        {
            InitializeComponent();
            _visitService = ServiceLocator.VisitService;
            _patientService = ServiceLocator.PatientService;
            _lookupCache = ServiceLocator.LookupCacheService;
            lblTitle.Text = "🧪 تسجيل زيارة جديدة";
            FormStyler.StyleHeaderPanel(pnlHeader, lblTitle, lblSubtitle);
        }

        public FrmAddVisit(Visit visit)
        {
            InitializeComponent();
            _visitService = ServiceLocator.VisitService;
            _patientService = ServiceLocator.PatientService;
            _lookupCache = ServiceLocator.LookupCacheService;
            lblTitle.Text = "✏️ تعديل بيانات الزيارة";
            FormStyler.StyleHeaderPanel(pnlHeader, lblTitle, lblSubtitle);

            _visit = _visitService.GetVisitById(visit.Id);
            if (_visit == null)
            {
                MessageBox.Show("الزيارة غير موجودة في قاعدة البيانات.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            _selectedPatient = _visit.Patient;
            DateOfVisit.Value = _visit.VisitDate;
            txtNotes.Text = _visit.Notes ?? "";
            txtDoctorName.Text = _visit.DrName ?? "";

            txtPatientCode.Text = _visit.Patient.MedicalCode;
            txtPatientName.Text = _visit.Patient.Name;

            txtPayment.Text = _visit.Payments.Sum(p => p.Amount).ToString("0.00");
            txtPayment.ReadOnly = true;

            textDiscountValue.Text = _visit.DiscountValue?.ToString("0.00") ?? "0.00";
            textDiscountPercentage.Text = _visit.DiscountPercent?.ToString("0.00") ?? "0.00";
        }

        private void FrmAddVisit_Load(object sender, EventArgs e)
        {
            FormStyler.ApplyFormTheme(this, lblTitle.Text);
            FormStyler.StyleHeaderPanel(pnlHeader, lblTitle, lblSubtitle);
            FormStyler.StyleButton(btnPickPatient, ButtonStyle.Primary);
            FormStyler.StyleButton(btnAddPatient, ButtonStyle.Success);
            FormStyler.StyleButton(btnAddLab, ButtonStyle.Primary);
            FormStyler.StyleButton(btnSave, ButtonStyle.Success);
            FormStyler.StyleButton(btnPrintReceipt, ButtonStyle.Primary);
            btnSave.Height = 40;
            btnPrintReceipt.Height = 40;

            FormStyler.StyleInput(txtPatientCode);
            FormStyler.StyleInput(txtPatientName);
            FormStyler.StyleInput(txtDoctorName);
            FormStyler.StyleInput(txtNotes);
            FormStyler.StyleInput(cmbolabs);

            DataGridViewStyler.Apply(dgvVisitDetails);
            colDelete.Image = ImageHelper.LoadSafeImage("imgs/delete.png", 16, 16, Color.Crimson);

            LoadLabsCombo();

            if (_visit != null)
            {
                LoadExistingVisitLabs();
            }
        }

        private void LoadLabsCombo()
        {
            try
            {
                _availableLabs = _lookupCache.GetLabs();
                cmbolabs.DataSource = _availableLabs;
                cmbolabs.DisplayMember = "Name";
                cmbolabs.ValueMember = "Id";
                cmbolabs.DropDownStyle = ComboBoxStyle.DropDown;
                cmbolabs.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbolabs.AutoCompleteSource = AutoCompleteSource.ListItems;
                if (_availableLabs.Count > 0)
                {
                    cmbolabs.SelectedIndex = 0;
                    _selectedLab = _availableLabs[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل قائمة التحاليل:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadExistingVisitLabs()
        {
            dgvVisitDetails.Rows.Clear();

            foreach (var vl in _visit!.VisitLabs)
            {
                int rowIndex = dgvVisitDetails.Rows.Add(
                    vl.LabId,
                    vl.Lab?.Name ?? "تحليل",
                    vl.LabPrice,
                    "معمل خارجي",
                    "إدخال النتائج",
                    null,
                    vl.Id
                );
            }

            RecalculateTotals();
        }

        private void cmbolabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbolabs.SelectedItem is Lab lab)
            {
                _selectedLab = lab;
            }
        }

        private void btnAddLab_Click(object sender, EventArgs e)
        {
            var labToAdd = cmbolabs.SelectedItem as Lab;
            if (labToAdd == null && !string.IsNullOrWhiteSpace(cmbolabs.Text))
            {
                string typed = cmbolabs.Text.Trim();
                labToAdd = _availableLabs.FirstOrDefault(l => l.Name.Equals(typed, StringComparison.OrdinalIgnoreCase))
                    ?? _availableLabs.FirstOrDefault(l => l.Name.Contains(typed, StringComparison.OrdinalIgnoreCase));
                if (labToAdd != null)
                {
                    cmbolabs.SelectedItem = labToAdd;
                }
            }

            if (labToAdd == null)
            {
                MessageBox.Show("يرجى اختيار تحليل من القائمة أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbolabs.Focus();
                return;
            }

            _selectedLab = labToAdd;

            foreach (DataGridViewRow row in dgvVisitDetails.Rows)
            {
                if (row.IsNewRow) continue;
                if (Convert.ToInt32(row.Cells["colId"].Value) == _selectedLab.Id)
                {
                    MessageBox.Show("هذا التحليل تمت إضافته بالفعل إلى الزيارة.", "تحليل مكرر", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            dgvVisitDetails.Rows.Add(
                _selectedLab.Id,
                _selectedLab.Name,
                _selectedLab.Price,
                "معمل خارجي",
                "إدخال النتائج",
                null,
                null
            );

            RecalculateTotals();
        }

        private void dgvVisitDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvVisitDetails.Columns[e.ColumnIndex].Name;

            if (colName == "colDelete")
            {
                dgvVisitDetails.Rows.RemoveAt(e.RowIndex);
                RecalculateTotals();
            }
            else if (colName == "colExternal")
            {
                object? val = dgvVisitDetails.Rows[e.RowIndex].Cells["colVisitLabId"].Value;
                if (val == null || val == DBNull.Value)
                {
                    MessageBox.Show("يرجى حفظ الزيارة أولاً لتتمكن من إحالة التحليل لمعمل خارجي.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int visitLabId = Convert.ToInt32(val);
                using var frm = new FrmExternalLab(visitLabId);
                frm.ShowDialog();
            }
            else if (colName == "colResult")
            {
                object? val = dgvVisitDetails.Rows[e.RowIndex].Cells["colVisitLabId"].Value;
                if (val == null || val == DBNull.Value)
                {
                    MessageBox.Show("يرجى حفظ الزيارة أولاً لإدخال النتائج.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int visitLabId = Convert.ToInt32(val);
                using var frm = new FrmLabResult(visitLabId);
                frm.ShowDialog();
            }
        }

        private void RecalculateTotals()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvVisitDetails.Rows)
            {
                if (row.IsNewRow) continue;
                if (decimal.TryParse(row.Cells["colPrice"].Value?.ToString(), out decimal price))
                {
                    total += price;
                }
            }

            txtTotal.Text = total.ToString("0.00");
            ApplyDiscountCalculations();
        }

        private void ApplyDiscountCalculations()
        {
            if (_isUpdatingDiscount) return;
            _isUpdatingDiscount = true;
            try
            {
                decimal.TryParse(txtTotal.Text, out decimal total);
                decimal.TryParse(textDiscountPercentage.Text, out decimal percent);
                decimal.TryParse(textDiscountValue.Text, out decimal discountVal);
                decimal.TryParse(txtPayment.Text, out decimal paid);

                if (percent > 0)
                {
                    discountVal = Math.Round(total * (percent / 100m), 2);
                    textDiscountValue.Text = discountVal.ToString("0.00");
                }
                else if (discountVal > 0 && total > 0)
                {
                    percent = Math.Round((discountVal / total) * 100m, 2);
                    textDiscountPercentage.Text = percent.ToString("0.00");
                }

                decimal net = Math.Max(0, total - discountVal);
                txtAfterDiscount.Text = net.ToString("0.00");

                decimal remain = Math.Max(0, net - paid);
                txtRemaining.Text = remain.ToString("0.00");
            }
            finally
            {
                _isUpdatingDiscount = false;
            }
        }

        private void textDiscountPercentage_TextChanged(object sender, EventArgs e)
        {
            if (_isUpdatingDiscount) return;
            if (decimal.TryParse(textDiscountPercentage.Text, out decimal p) && p >= 0)
            {
                decimal.TryParse(txtTotal.Text, out decimal total);
                decimal discountVal = Math.Round(total * (p / 100m), 2);
                _isUpdatingDiscount = true;
                textDiscountValue.Text = discountVal.ToString("0.00");
                _isUpdatingDiscount = false;
            }
            ApplyDiscountCalculations();
        }

        private void textDiscountValue_TextChanged(object sender, EventArgs e)
        {
            if (_isUpdatingDiscount) return;
            if (decimal.TryParse(textDiscountValue.Text, out decimal d) && d >= 0)
            {
                decimal.TryParse(txtTotal.Text, out decimal total);
                if (total > 0)
                {
                    decimal percent = Math.Round((d / total) * 100m, 2);
                    _isUpdatingDiscount = true;
                    textDiscountPercentage.Text = percent.ToString("0.00");
                    _isUpdatingDiscount = false;
                }
            }
            ApplyDiscountCalculations();
        }

        private void txtPayment_TextChanged(object sender, EventArgs e)
        {
            ApplyDiscountCalculations();
        }

        private async void txtPatientCode_Leave(object sender, EventArgs e)
        {
            string code = txtPatientCode.Text.Trim();
            if (string.IsNullOrEmpty(code))
            {
                _selectedPatient = null;
                txtPatientName.Clear();
                return;
            }

            var patient = await _patientService.GetPatientByCodeAsync(code);
            if (patient == null)
            {
                MessageBox.Show("لم يتم العثور على مريض بهذا الكود الطبي.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPatientName.Clear();
                _selectedPatient = null;
                return;
            }

            SetPatient(patient);
        }

        private void SetPatient(Patient patient)
        {
            _selectedPatient = patient;
            txtPatientCode.Text = patient.MedicalCode;
            txtPatientName.Text = patient.Name;
        }

        private void btnPickPatient_Click(object sender, EventArgs e)
        {
            using var frm = new FrmSelectPatient();
            if (frm.ShowDialog() == DialogResult.OK && frm.SelectedPatient != null)
            {
                SetPatient(frm.SelectedPatient);
            }
        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            using var frm = new FrmAddPatient();
            if (frm.ShowDialog() == DialogResult.OK && frm.CreatedPatient != null)
            {
                SetPatient(frm.CreatedPatient);
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (_selectedPatient == null)
            {
                MessageBox.Show("يرجى اختيار مريض أولاً.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPatientCode.Focus();
                return;
            }

            var labIds = new List<int>();
            var labItemsForReceipt = new List<Tuple<string, decimal>>();
            foreach (DataGridViewRow row in dgvVisitDetails.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["colId"].Value != null)
                {
                    int id = Convert.ToInt32(row.Cells["colId"].Value);
                    labIds.Add(id);
                    string name = row.Cells["colName"].Value?.ToString() ?? "تحليل";
                    decimal price = Convert.ToDecimal(row.Cells["colPrice"].Value ?? 0);
                    labItemsForReceipt.Add(Tuple.Create(name, price));
                }
            }

            if (labIds.Count == 0)
            {
                MessageBox.Show("يرجى إضافة تحليل واحد على الأقل للزيارة.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal.TryParse(textDiscountValue.Text, out decimal discountVal);
            decimal.TryParse(textDiscountPercentage.Text, out decimal discountPercent);
            decimal.TryParse(txtPayment.Text, out decimal initialPaid);

            try
            {
                btnSave.Enabled = false;
                int savedVisitId;

                if (_visit == null)
                {
                    var newVisit = new Visit
                    {
                        PatientId = _selectedPatient.Id,
                        VisitDate = DateOfVisit.Value,
                        DrName = txtDoctorName.Text.Trim(),
                        Notes = txtNotes.Text.Trim(),
                        DiscountValue = discountVal > 0 ? discountVal : null,
                        DiscountPercent = discountPercent > 0 ? discountPercent : null
                    };

                    savedVisitId = await _visitService.CreateVisitAsync(newVisit, labIds, initialPaid, (int)PaymentMethodEnum.Cash, "دفعة أولى عند التسجيل");
                    _lastSavedVisitId = savedVisitId;
                    MessageBox.Show("تم حفظ الزيارة بنجاح.", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _visit.DrName = txtDoctorName.Text.Trim();
                    _visit.Notes = txtNotes.Text.Trim();
                    _visit.VisitDate = DateOfVisit.Value;
                    _visit.DiscountValue = discountVal > 0 ? discountVal : null;
                    _visit.DiscountPercent = discountPercent > 0 ? discountPercent : null;

                    await _visitService.UpdateVisitAsync(_visit, labIds);
                    savedVisitId = _visit.Id;
                    _lastSavedVisitId = savedVisitId;
                    MessageBox.Show("تم تعديل الزيارة بنجاح.", "تم التعديل", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Ask to print payment receipt
                var printPrompt = MessageBox.Show(
                    "هل ترغب في طباعة إيصال السداد (فاتورة الاستقبال) الآن؟",
                    "طباعة إيصال الزيارة",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (printPrompt == DialogResult.Yes)
                {
                    ShowReceiptPreview(savedVisitId, labItemsForReceipt, discountVal, initialPaid);
                }

                Saved?.Invoke();

                if (Modal)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء حفظ الزيارة:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed && btnSave != null && !btnSave.IsDisposed)
                {
                    btnSave.Enabled = true;
                }
            }
        }

        private void btnPrintReceipt_Click(object? sender, EventArgs e)
        {
            int visitId = _visit?.Id ?? _lastSavedVisitId;
            if (visitId <= 0 || _selectedPatient == null)
            {
                MessageBox.Show("يرجى حفظ الزيارة أولاً لطباعة الإيصال.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var labItemsForReceipt = new List<Tuple<string, decimal>>();
            foreach (DataGridViewRow row in dgvVisitDetails.Rows)
            {
                if (row.IsNewRow) continue;
                string name = row.Cells["colName"].Value?.ToString() ?? "تحليل";
                decimal price = Convert.ToDecimal(row.Cells["colPrice"].Value ?? 0);
                labItemsForReceipt.Add(Tuple.Create(name, price));
            }

            decimal.TryParse(textDiscountValue.Text, out decimal discountVal);
            decimal.TryParse(txtPayment.Text, out decimal paidVal);
            ShowReceiptPreview(visitId, labItemsForReceipt, discountVal, paidVal);
        }

        private void ShowReceiptPreview(int visitId, List<Tuple<string, decimal>> labItemsForReceipt, decimal discountVal, decimal paidAmount)
        {
            if (_selectedPatient == null) return;

            decimal total = labItemsForReceipt.Sum(x => x.Item2);
            decimal net = Math.Max(0, total - discountVal);
            decimal remaining = Math.Max(0, net - paidAmount);

            var receiptModel = new ThermalReceiptModel
            {
                VisitId = visitId,
                VisitDate = DateOfVisit.Value,
                PatientName = _selectedPatient.Name,
                MedicalCode = _selectedPatient.MedicalCode,
                DoctorName = txtDoctorName.Text.Trim(),
                Items = labItemsForReceipt,
                TotalAmount = total,
                DiscountAmount = discountVal,
                NetAmount = net,
                PaidAmount = paidAmount,
                RemainingBalance = remaining,
                PaymentMethodName = "نقدي",
                ReceiptDate = DateTime.Now
            };

            using var viewer = new DocumentViewerForm(new ThermalReceiptDocument(receiptModel), $"إيصال زيارة #{visitId}");
            viewer.ShowDialog();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (DialogResult != DialogResult.OK)
                Cancelled?.Invoke();
        }
    }
}
