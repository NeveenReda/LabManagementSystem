using LabManagementSystem.Common;
using LabManagementSystem.Models;
using LabManagementSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Forms
{
    public partial class FrmExternalLab : Form
    {
        private readonly int _visitLabId;
        private VisitLab? _visitLab;
        private readonly LabContext _db = new();

        public FrmExternalLab()
        {
            InitializeComponent();
            FormStyler.ApplyFormTheme(this);
            FormStyler.StyleButton(btnSave, ButtonStyle.Primary);
            FormStyler.StyleButton(btnCancel, ButtonStyle.Secondary);
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };
        }

        public FrmExternalLab(int visitLabId) : this()
        {
            _visitLabId = visitLabId;

            _visitLab = _db.VisitLabs
                .Include(vl => vl.Visit)
                .ThenInclude(v => v.Patient)
                .Include(vl => vl.Lab)
                .FirstOrDefault(vl => vl.Id == _visitLabId);

            if (_visitLab == null)
            {
                MessageBox.Show("التحليل غير موجود.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblPatientInfo.Text =
                $"المريض: {_visitLab.Visit.Patient.Name} (كود: {_visitLab.Visit.Patient.MedicalCode}) | الزيارة: {_visitLab.Visit.VisitDate:yyyy/MM/dd} | التحليل: {_visitLab.Lab.Name}";

            txtExpextedDays.Text = _visitLab.Lab.Days.ToString();
            dtpDateReceive.Value = dtpDateSend.Value.AddDays(_visitLab.Lab.Days);
        }

        private void FrmExternalLab_Load(object sender, EventArgs e)
        {
            LoadExternalLabs();
            dtpDateReceive.Enabled = false;
            txtExpextedDays.ReadOnly = true;

            dtpDateSend.ValueChanged += (s, ev) =>
            {
                if (_visitLab != null)
                {
                    dtpDateReceive.Value = dtpDateSend.Value.AddDays(_visitLab.Lab.Days);
                }
            };
        }

        private void LoadExternalLabs()
        {
            try
            {
                var labs = ServiceLocator.LookupCacheService.GetExternalLabs();
                cmbExternalLab.DataSource = labs;
                cmbExternalLab.DisplayMember = "Name";
                cmbExternalLab.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل المعامل الخارجية: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbExternalLab.SelectedValue == null)
            {
                MessageBox.Show("من فضلك اختر المعمل الخارجي.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtContactPerson.Text))
            {
                MessageBox.Show("من فضلك أدخل اسم المسؤول في المعمل الخارجي.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContactPerson.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("من فضلك أدخل رقم هاتف المعمل الخارجي.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return;
            }

            try
            {
                var externalLab = new ExternalLab
                {
                    VistLabId = _visitLabId,
                    BaseExternalLabId = Convert.ToInt32(cmbExternalLab.SelectedValue),
                    ContactPerson = txtContactPerson.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    DateTimeSend = dtpDateSend.Value,
                    DateTimeReceive = dtpDateReceive.Value
                };

                _db.ExternalLabs.Add(externalLab);

                if (_visitLab != null)
                {
                    _visitLab.IsExternal = true;
                }

                _db.SaveChanges();

                MessageBox.Show("تم تحويل التحليل للمعمل الخارجي بنجاح.", "تم التحويل", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تحويل التحليل: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _db.Dispose();
        }
    }
}
