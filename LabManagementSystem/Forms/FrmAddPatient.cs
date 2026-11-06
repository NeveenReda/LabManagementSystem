using LabManagementSystem.Common;
using LabManagementSystem.Models;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public partial class FrmAddPatient : Form
    {
        private readonly IPatientService _patientService;
        private readonly ILookupCacheService _lookupCache;
        private Patient? _patient;
        public Patient? CreatedPatient { get; private set; }

        public event Action? Saved;
        public event Action? Cancelled;

        public FrmAddPatient()
        {
            InitializeComponent();
            _patientService = ServiceLocator.PatientService;
            _lookupCache = ServiceLocator.LookupCacheService;
            lblTitle.Text = "👤 تسجيل مريض جديد";
            FormStyler.StyleHeaderPanel(pnlHeader, lblTitle, lblSubtitle);
        }

        public FrmAddPatient(Patient patient)
        {
            InitializeComponent();
            _patientService = ServiceLocator.PatientService;
            _lookupCache = ServiceLocator.LookupCacheService;
            lblTitle.Text = "✏️ تعديل بيانات المريض";
            FormStyler.StyleHeaderPanel(pnlHeader, lblTitle, lblSubtitle);
            _patient = patient;

            LoadGenders();
            txtPatientCode.Text = _patient.MedicalCode;
            txtPatientName.Text = _patient.Name;
            txtPatienPhone.Text = _patient.Phone ?? "";
            txtPatientAgeOnRegister.Text = _patient.AgeAtRecord.ToString();
            DOB.Value = _patient.DateOfBirth ?? DateTime.Today;
            address.Text = _patient.Address ?? "";
            cmboGender.SelectedValue = _patient.GenderId;
            DateOFRegister.Value = _patient.CreateDate == default ? DateTime.Today : _patient.CreateDate;
            txtPatientCode.ReadOnly = true;
        }

        private async void FrmAddPatient_Load(object sender, EventArgs e)
        {
            FormStyler.ApplyFormTheme(this, lblTitle.Text);
            FormStyler.StyleHeaderPanel(pnlHeader, lblTitle, lblSubtitle);
            FormStyler.StyleButton(btnSave, ButtonStyle.Success);
            FormStyler.StyleButton(btnCancel, ButtonStyle.Secondary);

            LoadGenders();

            if (_patient != null)
            {
                cmboGender.SelectedValue = _patient.GenderId;
                txtPatientCode.Text = _patient.MedicalCode;
                txtPatientCode.ReadOnly = true;
                return;
            }

            DateOFRegister.Value = DateTime.Today;
            txtPatientCode.Text = await _patientService.GenerateNextMedicalCodeAsync();
            txtPatientCode.ReadOnly = true;
        }

        private void LoadGenders()
        {
            try
            {
                var genders = _lookupCache.GetGenders();
                cmboGender.DataSource = genders;
                cmboGender.DisplayMember = "Name";
                cmboGender.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل قائمة الأنواع:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPatientAgeOnRegister_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtPatientAgeOnRegister.Text.Trim(), out int age) && age >= 0 && age <= 130)
            {
                int birthYear = Math.Max(1900, DateTime.Today.Year - age);
                DOB.Value = new DateTime(birthYear, 1, 1);
            }
        }

        private void DOB_ValueChanged(object sender, EventArgs e)
        {
            int age = DateTime.Today.Year - DOB.Value.Year;
            if (DOB.Value.Date > DateTime.Today.AddYears(-age)) age--;
            if (age >= 0 && txtPatientAgeOnRegister.Text != age.ToString())
            {
                txtPatientAgeOnRegister.Text = age.ToString();
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtPatientName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("يرجى إدخال اسم المريض.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPatientName.Focus();
                return;
            }

            if (!int.TryParse(txtPatientAgeOnRegister.Text.Trim(), out int age) || age < 0 || age > 130)
            {
                MessageBox.Show("يرجى إدخال سن صحيح للمريض (بين 0 و 130).", "بيانات غير صحيحة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPatientAgeOnRegister.Focus();
                return;
            }

            if (cmboGender.SelectedValue == null)
            {
                MessageBox.Show("يرجى اختيار النوع (ذكر / أنثى).", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnSave.Enabled = false;

                if (_patient == null)
                {
                    var newPatient = new Patient
                    {
                        MedicalCode = txtPatientCode.Text.Trim(),
                        Name = name,
                        Phone = string.IsNullOrWhiteSpace(txtPatienPhone.Text) ? null : txtPatienPhone.Text.Trim(),
                        AgeAtRecord = age,
                        DateOfBirth = DOB.Value.Date,
                        Address = string.IsNullOrWhiteSpace(address.Text) ? null : address.Text.Trim(),
                        GenderId = (int)cmboGender.SelectedValue
                    };

                    await _patientService.AddPatientAsync(newPatient);
                    CreatedPatient = newPatient;
                    MessageBox.Show("تم تسجيل المريض بنجاح في قاعدة البيانات.", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _patient.Name = name;
                    _patient.Phone = string.IsNullOrWhiteSpace(txtPatienPhone.Text) ? null : txtPatienPhone.Text.Trim();
                    _patient.AgeAtRecord = age;
                    _patient.DateOfBirth = DOB.Value.Date;
                    _patient.Address = string.IsNullOrWhiteSpace(address.Text) ? null : address.Text.Trim();
                    _patient.GenderId = (int)cmboGender.SelectedValue;

                    await _patientService.UpdatePatientAsync(_patient);
                    CreatedPatient = _patient;
                    MessageBox.Show("تم تعديل بيانات المريض بنجاح.", "تم التعديل", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show($"حدث خطأ أثناء حفظ بيانات المريض:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed && btnSave != null && !btnSave.IsDisposed)
                {
                    btnSave.Enabled = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancelled?.Invoke();
            if (Modal)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}
