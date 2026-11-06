using LabManagementSystem.Common;
using LabManagementSystem.Models;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public class FrmBaseExternalLabs : Form
    {
        private readonly ILabSettingsService _settingsService;
        private readonly ILookupCacheService _lookupCache;

        private DataGridView dgvLabs = null!;
        private TextBox txtName = null!;
        private TextBox txtPhone = null!;
        private TextBox txtAddress = null!;
        private Button btnSave = null!;
        private Button btnNew = null!;
        private Button btnDelete = null!;
        private BaseExternalLab? _selectedLab;

        public FrmBaseExternalLabs()
        {
            _settingsService = ServiceLocator.LabSettingsService;
            _lookupCache = ServiceLocator.LookupCacheService;
            InitializeUi();
            LoadExternalLabs();
        }

        private void InitializeUi()
        {
            FormStyler.ApplyFormTheme(this, "دليل المعامل الخارجية");
            Size = new Size(920, 620);
            Padding = new Padding(16);

            // 1. Header Banner
            var pnlHeader = FormStyler.CreateHeaderPanel(
                "إدارة المعامل الخارجية ومراكز التحاليل المرجعية",
                "سجل المعامل الخارجية المتعاقد معها، بيانات التواصل والعناوين لتحويل الفحوصات المتخصصة",
                "🏥"
            );

            // 2. Inputs Card (Responsive TableLayoutPanel)
            var pnlInputs = FormStyler.CreateCardPanel(DockStyle.Top, new Padding(14));
            pnlInputs.Height = 135;

            var tlpInputs = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 6,
                RowCount = 2,
                RightToLeft = RightToLeft.Yes
            };
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));  // Name Label
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));  // Name Box
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));  // Phone Label
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));  // Phone Box
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));  // Address Label
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));  // Address Box

            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));

            var lblName = new Label { Text = "اسم المعمل:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Font = ThemeManager.BodyBoldFont };
            txtName = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtName);

            var lblPhone = new Label { Text = "الهاتف:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Font = ThemeManager.BodyBoldFont };
            txtPhone = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtPhone);

            var lblAddress = new Label { Text = "العنوان:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Font = ThemeManager.BodyBoldFont };
            txtAddress = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtAddress);

            tlpInputs.Controls.Add(lblName, 0, 0);
            tlpInputs.Controls.Add(txtName, 1, 0);
            tlpInputs.Controls.Add(lblPhone, 2, 0);
            tlpInputs.Controls.Add(txtPhone, 3, 0);
            tlpInputs.Controls.Add(lblAddress, 4, 0);
            tlpInputs.Controls.Add(txtAddress, 5, 0);

            // Row 1: Action Buttons
            var flpButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0)
            };

            btnSave = new Button { Text = "💾 حفظ المعمل", Width = 140, Height = 36, Margin = new Padding(0, 4, 6, 0) };
            FormStyler.StyleButton(btnSave, ButtonStyle.Success);
            btnSave.Click += BtnSave_Click;

            btnNew = new Button { Text = "➕ جديد", Width = 95, Height = 36, Margin = new Padding(0, 4, 6, 0) };
            FormStyler.StyleButton(btnNew, ButtonStyle.Secondary);
            btnNew.Click += (s, e) => ResetInputs();

            btnDelete = new Button { Text = "🗑️ حذف المعمل", Width = 130, Height = 36, Margin = new Padding(0, 4, 6, 0) };
            FormStyler.StyleButton(btnDelete, ButtonStyle.Danger);
            btnDelete.Click += BtnDelete_Click;

            flpButtons.Controls.Add(btnSave);
            flpButtons.Controls.Add(btnNew);
            flpButtons.Controls.Add(btnDelete);

            tlpInputs.SetColumnSpan(flpButtons, 6);
            tlpInputs.Controls.Add(flpButtons, 0, 1);

            pnlInputs.Controls.Add(tlpInputs);

            // 3. Grid Card
            var pnlGridCard = FormStyler.CreateCardPanel(DockStyle.Fill, new Padding(12));

            dgvLabs = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = false };
            DataGridViewStyler.Apply(dgvLabs);

            dgvLabs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colId", DataPropertyName = "Id", HeaderText = "#", FillWeight = 10 });
            dgvLabs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colName", DataPropertyName = "Name", HeaderText = "اسم المعمل الخارجي", FillWeight = 40 });
            dgvLabs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colPhone", DataPropertyName = "Phone", HeaderText = "رقم الهاتف", FillWeight = 25 });
            dgvLabs.Columns.Add(new DataGridViewTextBoxColumn { Name = "colAddress", DataPropertyName = "Address", HeaderText = "العنوان", FillWeight = 35 });

            dgvLabs.SelectionChanged += DgvLabs_SelectionChanged;

            pnlGridCard.Controls.Add(dgvLabs);

            Controls.Add(pnlGridCard);
            Controls.Add(pnlInputs);
            Controls.Add(pnlHeader);
        }

        private void LoadExternalLabs()
        {
            try
            {
                var list = _settingsService.GetAllExternalLabs();
                dgvLabs.DataSource = list;
                ResetInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في استرجاع المعامل الخارجية: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetInputs()
        {
            _selectedLab = null;
            txtName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            btnSave.Text = "💾 حفظ المعمل";
            FormStyler.StyleButton(btnSave, ButtonStyle.Success);
        }

        private void DgvLabs_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvLabs.CurrentRow?.DataBoundItem is BaseExternalLab lab)
            {
                _selectedLab = lab;
                txtName.Text = lab.Name;
                txtPhone.Text = lab.Phone ?? "";
                txtAddress.Text = lab.Address ?? "";
                btnSave.Text = "✏️ تحديث المعمل";
                FormStyler.StyleButton(btnSave, ButtonStyle.Primary);
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("يرجى إدخال اسم المعمل.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            try
            {
                var lab = _selectedLab ?? new BaseExternalLab();
                lab.Name = name;
                lab.Phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? null : txtPhone.Text.Trim();
                lab.Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim();

                _settingsService.SaveExternalLab(lab);
                _lookupCache.InvalidateExternalLabs();
                MessageBox.Show("تم حفظ بيانات المعمل الخارجي بنجاح.", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadExternalLabs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء حفظ المعمل:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (_selectedLab == null)
            {
                MessageBox.Show("يرجى تحديد معمل خارجي لحذفه أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"هل أنت متأكد من حذف المعمل الخارجي '{_selectedLab.Name}'؟",
                "تأكيد الحذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _settingsService.DeleteExternalLab(_selectedLab.Id);
                    _lookupCache.InvalidateExternalLabs();
                    MessageBox.Show("تم حذف المعمل بنجاح.", "تم الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadExternalLabs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
