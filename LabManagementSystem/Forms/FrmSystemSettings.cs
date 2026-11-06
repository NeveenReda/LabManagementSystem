using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabManagementSystem.Common;
using LabManagementSystem.Models;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public class FrmSystemSettings : Form
    {
        private readonly ILabSettingsService _settingsService;
        private LaboratoryProfile? _currentProfile;
        private LoadingOverlay _loadingOverlay = null!;

        // Input controls
        private TextBox txtLabNameAr = null!;
        private TextBox txtLabNameEn = null!;
        private TextBox txtSubtitle = null!;
        private TextBox txtAddress = null!;
        private TextBox txtPhone = null!;
        private TextBox txtPhone2 = null!;
        private TextBox txtDirector = null!;
        private TextBox txtEmail = null!;
        private TextBox txtFooterNote = null!;

        private Button btnSave = null!;
        private Button btnReset = null!;

        public FrmSystemSettings()
        {
            _settingsService = ServiceLocator.LabSettingsService;
            InitializeUi();
            Shown += async (s, e) => await LoadSettingsAsync();
        }

        private void InitializeUi()
        {
            FormStyler.ApplyFormTheme(this, "بيانات وإعدادات المعمل");
            Padding = new Padding(16);

            // 1. Header
            var pnlHeader = FormStyler.CreateHeaderPanel(
                "بيانات وإعدادات المعمل الرسمية (Laboratory Settings)",
                "إدارة وتحديث اسم المعمل وعناوينه وأرقام التواصل المعتمدة في كافة الفواتير والتقارير الطبية",
                "⚙️");

            // 2. Main Content Card
            var pnlCard = FormStyler.CreateCardPanel(DockStyle.Fill, new Padding(24));
            pnlCard.AutoScroll = true;

            var lblSectionTitle = new Label
            {
                Text = "🏥 البيانات الرسمية وهوية المعمل",
                Font = ThemeManager.SubtitleFont,
                ForeColor = ThemeManager.PrimaryNavy,
                Dock = DockStyle.Top,
                Height = 36,
                TextAlign = ContentAlignment.MiddleRight
            };

            var tlp = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                ColumnCount = 4,
                RightToLeft = RightToLeft.Yes,
                Padding = new Padding(0, 8, 0, 16)
            };

            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F)); // Label 1
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));   // Input 1
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 190F)); // Label 2
            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));   // Input 2

            for (int i = 0; i < 6; i++)
            {
                tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            }

            // Row 0: Arabic Name (Required) & English Name
            var lbl1 = CreateFieldLabel("اسم المعمل (بالعربية) *:");
            txtLabNameAr = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyBoldFont };
            FormStyler.StyleInput(txtLabNameAr);

            var lbl2 = CreateFieldLabel("اسم المعمل (بالإنجليزية):");
            txtLabNameEn = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtLabNameEn);

            tlp.Controls.Add(lbl1, 0, 0);
            tlp.Controls.Add(txtLabNameAr, 1, 0);
            tlp.Controls.Add(lbl2, 2, 0);
            tlp.Controls.Add(txtLabNameEn, 3, 0);

            // Row 1: Subtitle & Director Name
            var lbl3 = CreateFieldLabel("الوصف / الشعار النصي:");
            txtSubtitle = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtSubtitle);

            var lbl4 = CreateFieldLabel("المدير الفني / الطبيب المسؤول:");
            txtDirector = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtDirector);

            tlp.Controls.Add(lbl3, 0, 1);
            tlp.Controls.Add(txtSubtitle, 1, 1);
            tlp.Controls.Add(lbl4, 2, 1);
            tlp.Controls.Add(txtDirector, 3, 1);

            // Row 2: Phone 1 & Phone 2
            var lbl5 = CreateFieldLabel("رقم الهاتف الأساسي:");
            txtPhone = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtPhone);

            var lbl6 = CreateFieldLabel("رقم هاتف إضافي / طوارئ:");
            txtPhone2 = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtPhone2);

            tlp.Controls.Add(lbl5, 0, 2);
            tlp.Controls.Add(txtPhone, 1, 2);
            tlp.Controls.Add(lbl6, 2, 2);
            tlp.Controls.Add(txtPhone2, 3, 2);

            // Row 3: Address & Email
            var lbl7 = CreateFieldLabel("عنوان المقر / الفرع:");
            txtAddress = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtAddress);

            var lbl8 = CreateFieldLabel("البريد الإلكتروني:");
            txtEmail = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtEmail);

            tlp.Controls.Add(lbl7, 0, 3);
            tlp.Controls.Add(txtAddress, 1, 3);
            tlp.Controls.Add(lbl8, 2, 3);
            tlp.Controls.Add(txtEmail, 3, 3);

            // Row 4: Report Footer Note (Spans across)
            var lbl9 = CreateFieldLabel("ملاحظة تذييل التقارير:");
            txtFooterNote = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtFooterNote);

            tlp.Controls.Add(lbl9, 0, 4);
            tlp.SetColumnSpan(txtFooterNote, 3);
            tlp.Controls.Add(txtFooterNote, 1, 4);

            // Row 5: Action buttons
            var flpButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                AutoScroll = false,
                Margin = new Padding(0, 10, 0, 0)
            };

            btnSave = new Button { Text = "💾 حفظ الإعدادات الرسمية", Width = 210, Height = 40, Margin = new Padding(0, 4, 12, 0) };
            FormStyler.StyleButton(btnSave, ButtonStyle.Success);
            btnSave.Click += async (s, e) => await SaveSettingsAsync();

            btnReset = new Button { Text = "🔄 استعادة القيم", Width = 140, Height = 40, Margin = new Padding(0, 4, 12, 0) };
            FormStyler.StyleButton(btnReset, ButtonStyle.Secondary);
            btnReset.Click += async (s, e) => await LoadSettingsAsync();

            flpButtons.Controls.Add(btnSave);
            flpButtons.Controls.Add(btnReset);

            tlp.SetColumnSpan(flpButtons, 4);
            tlp.Controls.Add(flpButtons, 0, 5);

            // Notice Banner
            var pnlNotice = new Panel
            {
                Dock = DockStyle.Top,
                Height = 75,
                BackColor = ColorTranslator.FromHtml("#EFF6FF"),
                Padding = new Padding(16, 12, 16, 12),
                Margin = new Padding(0, 16, 0, 0)
            };

            var lblNotice = new Label
            {
                Text = "ℹ️ ملحوظة هامة: تنعكس هذه البيانات فورياً في شريط البرنامج الرئيسي، وكافة إيصالات السداد الحرارية (80mm)، وفواتير الاستقبال الرسمية (A5)، والتقارير الطبية المجمعة (A4)، والتقارير المالية المطبوعة.",
                Dock = DockStyle.Fill,
                Font = ThemeManager.SmallFont,
                ForeColor = ThemeManager.PrimaryBlue,
                TextAlign = ContentAlignment.MiddleRight
            };
            pnlNotice.Controls.Add(lblNotice);

            pnlCard.Controls.Add(pnlNotice);
            pnlCard.Controls.Add(tlp);
            pnlCard.Controls.Add(lblSectionTitle);

            Controls.Add(pnlCard);
            Controls.Add(pnlHeader);

            _loadingOverlay = LoadingOverlay.AttachTo(pnlCard);
        }

        private Label CreateFieldLabel(string text)
        {
            return new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText,
                TextAlign = ContentAlignment.MiddleRight
            };
        }

        private async Task LoadSettingsAsync()
        {
            try
            {
                _loadingOverlay.ShowOverlay("جاري تحميل بيانات المعمل...");
                _currentProfile = await _settingsService.GetLaboratoryProfileAsync();

                txtLabNameAr.Text = _currentProfile.LabName;
                txtLabNameEn.Text = _currentProfile.LabNameEn ?? "";
                txtSubtitle.Text = _currentProfile.Subtitle ?? "";
                txtAddress.Text = _currentProfile.Address ?? "";
                txtPhone.Text = _currentProfile.Phone ?? "";
                txtPhone2.Text = _currentProfile.AdditionalPhone ?? "";
                txtDirector.Text = _currentProfile.DirectorName ?? "";
                txtEmail.Text = _currentProfile.Email ?? "";
                txtFooterNote.Text = _currentProfile.ReportFooterNote ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ أثناء تحميل بيانات المعمل:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _loadingOverlay.HideOverlay();
            }
        }

        private async Task SaveSettingsAsync()
        {
            string labName = txtLabNameAr.Text.Trim();
            if (string.IsNullOrWhiteSpace(labName))
            {
                MessageBox.Show("يرجى إدخال اسم المعمل بالعربية (حقل إلزامي).", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLabNameAr.Focus();
                return;
            }

            try
            {
                btnSave.Enabled = false;
                _loadingOverlay.ShowOverlay("جاري حفظ بيانات وإعدادات المعمل في قاعدة البيانات...");

                var profile = _currentProfile ?? new LaboratoryProfile();
                profile.LabName = labName;
                profile.LabNameEn = string.IsNullOrWhiteSpace(txtLabNameEn.Text) ? null : txtLabNameEn.Text.Trim();
                profile.Subtitle = string.IsNullOrWhiteSpace(txtSubtitle.Text) ? null : txtSubtitle.Text.Trim();
                profile.Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim();
                profile.Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim();
                profile.AdditionalPhone = string.IsNullOrWhiteSpace(txtPhone2.Text) ? null : txtPhone2.Text.Trim();
                profile.DirectorName = string.IsNullOrWhiteSpace(txtDirector.Text) ? null : txtDirector.Text.Trim();
                profile.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
                profile.ReportFooterNote = string.IsNullOrWhiteSpace(txtFooterNote.Text) ? null : txtFooterNote.Text.Trim();

                await _settingsService.SaveLaboratoryProfileAsync(profile);
                _currentProfile = profile;

                MessageBox.Show("تم حفظ بيانات المعمل بنجاح وتحديث كافة التقارير والفواتير تلقائياً.", "تم الحفظ بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء حفظ الإعدادات:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed && !btnSave.IsDisposed)
                    btnSave.Enabled = true;
                _loadingOverlay.HideOverlay();
            }
        }
    }
}

