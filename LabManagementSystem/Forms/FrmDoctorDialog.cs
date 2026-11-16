using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabManagementSystem.Common;
using LabManagementSystem.Models;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public class FrmDoctorDialog : Form
    {
        private readonly IDoctorService _doctorService;
        private readonly Doctor? _existingDoctor;

        public Doctor? SavedDoctor { get; private set; }

        private TextBox txtName = null!;
        private TextBox txtSpecialty = null!;
        private TextBox txtPhone = null!;
        private TextBox txtAddress = null!;
        private TextBox txtNotes = null!;
        private CheckBox chkIsActive = null!;
        private Button btnSave = null!;
        private Button btnCancel = null!;

        public FrmDoctorDialog(Doctor? doctor = null)
        {
            _doctorService = ServiceLocator.DoctorService;
            _existingDoctor = doctor;
            InitializeUi();
            LoadData();
        }

        private void InitializeUi()
        {
            bool isEdit = _existingDoctor != null && _existingDoctor.Id > 0;
            string title = isEdit ? "تعديل بيانات طبيب" : "إضافة طبيب جديد";

            FormStyler.ApplyFormTheme(this, title);
            Size = new Size(580, 520);
            MinimumSize = new Size(520, 480);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;

            var pnlHeader = FormStyler.CreateHeaderPanel(
                isEdit ? "تعديل بيانات الطبيب" : "تسجيل طبيب معالج جديد",
                "أدخل بيانات الطبيب والتخصص وأرقام التواصل لربطها بالزيارات والتقارير",
                "👨‍⚕️"
            );

            var pnlBody = FormStyler.CreateCardPanel(DockStyle.Fill, new Padding(24, 16, 24, 16));

            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6,
                RightToLeft = RightToLeft.Yes
            };
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 115F));
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

            // 1. Name
            var lblName = new Label
            {
                Text = "اسم الطبيب: *",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText
            };
            txtName = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = ThemeManager.BodyFont,
                PlaceholderText = "مثال: د. مجدي يعقوب"
            };
            FormStyler.StyleInput(txtName);

            // 2. Specialty
            var lblSpecialty = new Label
            {
                Text = "التخصص:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText
            };
            txtSpecialty = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = ThemeManager.BodyFont,
                PlaceholderText = "مثال: باطنة، أطفال، عظام، نساء وتوليد..."
            };
            FormStyler.StyleInput(txtSpecialty);

            // 3. Phone
            var lblPhone = new Label
            {
                Text = "رقم الهاتف:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText
            };
            txtPhone = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = ThemeManager.BodyFont,
                PlaceholderText = "رقم الهاتف أو الموبايل..."
            };
            FormStyler.StyleInput(txtPhone);

            // 4. Address
            var lblAddress = new Label
            {
                Text = "العنوان / العيادة:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText
            };
            txtAddress = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = ThemeManager.BodyFont,
                PlaceholderText = "عنوان العيادة أو المستشفى..."
            };
            FormStyler.StyleInput(txtAddress);

            // 5. Notes
            var lblNotes = new Label
            {
                Text = "ملاحظات:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.TopRight,
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText,
                Padding = new Padding(0, 6, 0, 0)
            };
            txtNotes = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = ThemeManager.BodyFont,
                PlaceholderText = "أي ملاحظات إضافية عن الطبيب أو طريقة التواصل..."
            };
            FormStyler.StyleInput(txtNotes);

            // 6. Active Checkbox
            var lblActive = new Label
            {
                Text = "حالة النشاط:",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText
            };
            chkIsActive = new CheckBox
            {
                Text = "طبيب نشط (يظهر في قوائم تسجيل الزيارات)",
                Checked = true,
                Dock = DockStyle.Fill,
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.Success
            };

            tlp.Controls.Add(lblName, 0, 0);
            tlp.Controls.Add(txtName, 1, 0);
            tlp.Controls.Add(lblSpecialty, 0, 1);
            tlp.Controls.Add(txtSpecialty, 1, 1);
            tlp.Controls.Add(lblPhone, 0, 2);
            tlp.Controls.Add(txtPhone, 1, 2);
            tlp.Controls.Add(lblAddress, 0, 3);
            tlp.Controls.Add(txtAddress, 1, 3);
            tlp.Controls.Add(lblNotes, 0, 4);
            tlp.Controls.Add(txtNotes, 1, 4);
            tlp.Controls.Add(lblActive, 0, 5);
            tlp.Controls.Add(chkIsActive, 1, 5);

            pnlBody.Controls.Add(tlp);

            // Footer / Action Bar
            var pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = ThemeManager.CardBackground,
                Padding = new Padding(16, 12, 16, 12)
            };

            var flpButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };

            btnSave = new Button
            {
                Text = "💾 حفظ البيانات",
                Width = 140,
                Height = 36,
                Margin = new Padding(0, 0, 8, 0)
            };
            FormStyler.StyleButton(btnSave, ButtonStyle.Success);
            btnSave.Click += async (s, e) => await SaveAsync();

            btnCancel = new Button
            {
                Text = "❌ إلغاء",
                Width = 100,
                Height = 36,
                Margin = new Padding(0, 0, 8, 0)
            };
            FormStyler.StyleButton(btnCancel, ButtonStyle.Secondary);
            btnCancel.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            flpButtons.Controls.Add(btnSave);
            flpButtons.Controls.Add(btnCancel);
            pnlFooter.Controls.Add(flpButtons);

            Controls.Add(pnlBody);
            Controls.Add(pnlFooter);
            Controls.Add(pnlHeader);
        }

        private void LoadData()
        {
            if (_existingDoctor != null)
            {
                txtName.Text = _existingDoctor.Name;
                txtSpecialty.Text = _existingDoctor.Specialty ?? "";
                txtPhone.Text = _existingDoctor.PhoneNumber ?? "";
                txtAddress.Text = _existingDoctor.Address ?? "";
                txtNotes.Text = _existingDoctor.Notes ?? "";
                chkIsActive.Checked = _existingDoctor.IsActive;
            }
        }

        private async Task SaveAsync()
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("يرجى إدخال اسم الطبيب أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            btnSave.Enabled = false;
            btnCancel.Enabled = false;
            try
            {
                var doc = _existingDoctor ?? new Doctor();
                doc.Name = name;
                doc.Specialty = string.IsNullOrWhiteSpace(txtSpecialty.Text) ? null : txtSpecialty.Text.Trim();
                doc.PhoneNumber = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim();
                doc.Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim();
                doc.Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim();
                doc.IsActive = chkIsActive.Checked;

                int id = await _doctorService.SaveDoctorAsync(doc);
                doc.Id = id;
                SavedDoctor = doc;

                MessageBox.Show("تم حفظ بيانات الطبيب بنجاح.", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطأ في الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }
        }
    }
}
