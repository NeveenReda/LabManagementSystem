using System.Globalization;
using LabManagementSystem.Common;
using LabManagementSystem.Models;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public class FrmLabParametersDialog : Form
    {
        private readonly ILabSettingsService _settingsService;
        private readonly int _labId;
        private readonly string _labName;

        private DataGridView dgvParams = null!;
        private TextBox txtParamName = null!;
        private TextBox txtUnit = null!;
        private CheckBox chkIsNumeric = null!;
        private Label lblMin = null!;
        private TextBox txtMin = null!;
        private Label lblMax = null!;
        private TextBox txtMax = null!;
        private Label lblNormalText = null!;
        private TextBox txtNormalText = null!;
        private NumericUpDown numOrder = null!;
        private ComboBox cmbGender = null!;
        private Button btnSaveParam = null!;
        private Button btnNewParam = null!;
        private Button btnDeleteParam = null!;
        private Button btnClose = null!;
        private LabParameter? _currentEditingParam;
        private bool _isLoadingParam;

        public FrmLabParametersDialog(int labId, string labName)
        {
            _settingsService = ServiceLocator.LabSettingsService;
            _labId = labId;
            _labName = labName;
            InitializeUi();
            LoadParameters();
        }

        private void InitializeUi()
        {
            FormStyler.ApplyFormTheme(this, $"إعداد باراميترات التحليل: [{_labName}]");
            Size = new Size(1000, 700);
            MinimumSize = new Size(880, 580);
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;

            var pnlHeader = FormStyler.CreateHeaderPanel(
                $"إعداد باراميترات ومعدلات التحليل: {_labName}",
                "تحديد العناصر الفرعية، الحدود الطبيعية (الرقمية أو النصية)، ووحدات القياس والترتيب");

            // Card Panel for inputs (Responsive TableLayoutPanel)
            var pnlInputsCard = FormStyler.CreateCardPanel();
            pnlInputsCard.Dock = DockStyle.Top;
            pnlInputsCard.Height = 175;
            pnlInputsCard.Padding = new Padding(14);

            var tlpInputs = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 8,
                RowCount = 3,
                RightToLeft = RightToLeft.Yes
            };
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));  // Label 1
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));  // Param Name
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));  // Label 2
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18F));  // Unit
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F)); // IsNumeric Check
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 65F));  // Order Label
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 75F));  // Order Num
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));  // Remaining / Spacer

            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));

            // Row 0: Name, Unit, IsNumeric, Order
            var lblName = new Label { Text = "اسم الباراميتر:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Font = ThemeManager.BodyBoldFont };
            txtParamName = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtParamName);

            var lblUnit = new Label { Text = "الوحدة:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Font = ThemeManager.BodyBoldFont };
            txtUnit = new TextBox { Dock = DockStyle.Fill, Font = ThemeManager.BodyFont };
            FormStyler.StyleInput(txtUnit);

            chkIsNumeric = new CheckBox
            {
                Name = "chkIsNumeric",
                Text = "قيمة رقمية (Numeric)",
                AutoSize = true,
                Checked = true,
                Font = ThemeManager.BodyBoldFont
            };
            chkIsNumeric.CheckedChanged += (s, e) =>
            {
                if (!_isLoadingParam)
                    UpdateParameterTypeMode(clearInactive: true);
            };

            var lblOrder = new Label { Text = "الترتيب:", AutoSize = true, Anchor = AnchorStyles.Left | AnchorStyles.Right, Font = ThemeManager.BodyBoldFont };
            numOrder = new NumericUpDown
            {
                Dock = DockStyle.Fill,
                Minimum = 1,
                Maximum = 999,
                Value = 1,
                Font = ThemeManager.BodyFont
            };

            tlpInputs.Controls.Add(lblName, 0, 0);
            tlpInputs.Controls.Add(txtParamName, 1, 0);
            tlpInputs.Controls.Add(lblUnit, 2, 0);
            tlpInputs.Controls.Add(txtUnit, 3, 0);
            tlpInputs.Controls.Add(chkIsNumeric, 4, 0);
            tlpInputs.Controls.Add(lblOrder, 5, 0);
            tlpInputs.Controls.Add(numOrder, 6, 0);

            // Row 1: Min, Max, NormalText, Gender
            var flpRow1 = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0)
            };

            lblMin = new Label { Name = "lblMin", Text = "الحد الأدنى:", AutoSize = true, Margin = new Padding(0, 8, 4, 0), Font = ThemeManager.BodyBoldFont };
            txtMin = new TextBox { Name = "txtMin", Width = 85, Font = ThemeManager.BodyFont, Margin = new Padding(4), PlaceholderText = "مثال: 0" };
            FormStyler.StyleInput(txtMin);

            lblMax = new Label { Name = "lblMax", Text = "الحد الأقصى:", AutoSize = true, Margin = new Padding(8, 8, 4, 0), Font = ThemeManager.BodyBoldFont };
            txtMax = new TextBox { Name = "txtMax", Width = 85, Font = ThemeManager.BodyFont, Margin = new Padding(4), PlaceholderText = "مثال: 100" };
            FormStyler.StyleInput(txtMax);

            lblNormalText = new Label { Name = "lblNormalText", Text = "القيمة الطبيعية النصية:", AutoSize = true, Margin = new Padding(8, 8, 4, 0), Font = ThemeManager.BodyBoldFont };
            txtNormalText = new TextBox { Name = "txtNormalText", Width = 190, Font = ThemeManager.BodyFont, Margin = new Padding(4), PlaceholderText = "Negative / Normal / Positive..." };
            FormStyler.StyleInput(txtNormalText);

            var lblGender = new Label { Name = "lblGender", Text = "النوع:", AutoSize = true, Margin = new Padding(8, 8, 4, 0), Font = ThemeManager.BodyBoldFont };
            cmbGender = new ComboBox { Name = "cmbGender", Width = 135, DropDownStyle = ComboBoxStyle.DropDownList, Font = ThemeManager.BodyFont, Margin = new Padding(4) };
            cmbGender.Items.Add(new { Text = "كلا النوعين (الكل)", Value = (int?)null });
            cmbGender.Items.Add(new { Text = "ذكور فقط (Male)", Value = (int?)1 });
            cmbGender.Items.Add(new { Text = "إناث فقط (Female)", Value = (int?)2 });
            cmbGender.DisplayMember = "Text";
            cmbGender.ValueMember = "Value";
            cmbGender.SelectedIndex = 0;

            flpRow1.Controls.Add(lblMin);
            flpRow1.Controls.Add(txtMin);
            flpRow1.Controls.Add(lblMax);
            flpRow1.Controls.Add(txtMax);
            flpRow1.Controls.Add(lblNormalText);
            flpRow1.Controls.Add(txtNormalText);
            flpRow1.Controls.Add(lblGender);
            flpRow1.Controls.Add(cmbGender);

            UpdateParameterTypeMode(clearInactive: false);

            tlpInputs.SetColumnSpan(flpRow1, 8);
            tlpInputs.Controls.Add(flpRow1, 0, 1);

            // Row 2: Action Buttons
            var flpButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0)
            };

            btnSaveParam = new Button { Text = "💾 حفظ الباراميتر", Width = 150, Height = 36, Margin = new Padding(0, 4, 6, 0) };
            FormStyler.StyleButton(btnSaveParam, ButtonStyle.Success);
            btnSaveParam.Click += BtnSaveParam_Click;

            btnNewParam = new Button { Text = "➕ باراميتر جديد", Width = 130, Height = 36, Margin = new Padding(0, 4, 6, 0) };
            FormStyler.StyleButton(btnNewParam, ButtonStyle.Outline);
            btnNewParam.Click += (s, e) => ResetParamInputs();

            btnDeleteParam = new Button { Text = "🗑️ حذف الباراميتر", Width = 135, Height = 36, Margin = new Padding(0, 4, 6, 0) };
            FormStyler.StyleButton(btnDeleteParam, ButtonStyle.Danger);
            btnDeleteParam.Click += BtnDeleteParam_Click;

            flpButtons.Controls.Add(btnSaveParam);
            flpButtons.Controls.Add(btnNewParam);
            flpButtons.Controls.Add(btnDeleteParam);

            tlpInputs.SetColumnSpan(flpButtons, 8);
            tlpInputs.Controls.Add(flpButtons, 0, 2);

            pnlInputsCard.Controls.Add(tlpInputs);

            // Bottom Panel
            var pnlBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 55,
                Padding = new Padding(16, 8, 16, 8),
                BackColor = ThemeManager.CardBackground
            };

            btnClose = new Button
            {
                Text = "إغلاق",
                Dock = DockStyle.Left,
                Width = 100,
                Cursor = Cursors.Hand
            };
            FormStyler.StyleButton(btnClose, ButtonStyle.Secondary);
            btnClose.Click += (s, e) => Close();
            pnlBottom.Controls.Add(btnClose);

            // Grid Container Card
            var pnlGridContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(16, 8, 16, 8),
                BackColor = ThemeManager.Background
            };

            dgvParams = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false
            };
            DataGridViewStyler.Apply(dgvParams);

            dgvParams.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDisplayOrder",
                DataPropertyName = "DisplayOrder",
                HeaderText = "الترتيب",
                FillWeight = 10,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            dgvParams.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colName",
                DataPropertyName = "Name",
                HeaderText = "اسم الباراميتر",
                FillWeight = 30
            });
            dgvParams.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colUnit",
                DataPropertyName = "Unit",
                HeaderText = "الوحدة",
                FillWeight = 15,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            dgvParams.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMin",
                DataPropertyName = "NormalRangeMin",
                HeaderText = "الحد الأدنى",
                FillWeight = 12,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            dgvParams.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMax",
                DataPropertyName = "NormalRangeMax",
                HeaderText = "الحد الأقصى",
                FillWeight = 12,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            dgvParams.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNormalText",
                DataPropertyName = "NormalText",
                HeaderText = "المعدل الطبيعي (نصي)",
                FillWeight = 21
            });

            dgvParams.SelectionChanged += DgvParams_SelectionChanged;

            pnlGridContainer.Controls.Add(dgvParams);

            Controls.Add(pnlGridContainer);
            Controls.Add(pnlBottom);
            Controls.Add(pnlInputsCard);
            Controls.Add(pnlHeader);
        }

        private void LoadParameters()
        {
            var list = _settingsService.GetParametersForLab(_labId);
            dgvParams.DataSource = list;
            ResetParamInputs();
        }

        private void UpdateParameterTypeMode(bool clearInactive = true)
        {
            bool isNumeric = chkIsNumeric.Checked;

            lblMin.Visible = isNumeric;
            lblMin.Enabled = isNumeric;
            txtMin.Visible = isNumeric;
            txtMin.Enabled = isNumeric;

            lblMax.Visible = isNumeric;
            lblMax.Enabled = isNumeric;
            txtMax.Visible = isNumeric;
            txtMax.Enabled = isNumeric;

            lblNormalText.Visible = !isNumeric;
            lblNormalText.Enabled = !isNumeric;
            txtNormalText.Visible = !isNumeric;
            txtNormalText.Enabled = !isNumeric;

            if (clearInactive)
            {
                if (isNumeric)
                {
                    txtNormalText.Clear();
                }
                else
                {
                    txtMin.Clear();
                    txtMax.Clear();
                }
            }
        }

        private void ResetParamInputs()
        {
            _isLoadingParam = true;
            try
            {
                _currentEditingParam = null;
                txtParamName.Clear();
                txtUnit.Clear();
                txtMin.Clear();
                txtMax.Clear();
                txtNormalText.Clear();
                chkIsNumeric.Checked = true;
                UpdateParameterTypeMode(clearInactive: false);
                numOrder.Value = dgvParams.Rows.Count + 1;
                cmbGender.SelectedIndex = 0;
                btnSaveParam.Text = "💾 حفظ الباراميتر";
                FormStyler.StyleButton(btnSaveParam, ButtonStyle.Success);
            }
            finally
            {
                _isLoadingParam = false;
            }
        }

        private void DgvParams_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvParams.CurrentRow?.DataBoundItem is LabParameter p)
            {
                _isLoadingParam = true;
                try
                {
                    _currentEditingParam = p;
                    txtParamName.Text = p.Name;
                    txtUnit.Text = p.Unit ?? "";
                    chkIsNumeric.Checked = p.IsNumeric;
                    txtMin.Text = p.NormalRangeMin?.ToString() ?? "";
                    txtMax.Text = p.NormalRangeMax?.ToString() ?? "";
                    txtNormalText.Text = p.NormalText ?? "";
                    UpdateParameterTypeMode(clearInactive: false);
                    numOrder.Value = Math.Max(1, p.DisplayOrder);

                    if (p.GenderId == 1) cmbGender.SelectedIndex = 1;
                    else if (p.GenderId == 2) cmbGender.SelectedIndex = 2;
                    else cmbGender.SelectedIndex = 0;

                    btnSaveParam.Text = "✏️ تحديث الباراميتر";
                    FormStyler.StyleButton(btnSaveParam, ButtonStyle.Primary);
                }
                finally
                {
                    _isLoadingParam = false;
                }
            }
        }

        public static (bool IsValid, string? ErrorMessage, decimal? Min, decimal? Max, string? NormalText) ValidateParameterInput(
            string name,
            bool isNumeric,
            string? minText,
            string? maxText,
            string? normalText)
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, "يرجى إدخال اسم الباراميتر.", null, null, null);

            if (!isNumeric)
            {
                string? cleanText = string.IsNullOrWhiteSpace(normalText) ? null : normalText.Trim();
                return (true, null, null, null, cleanText);
            }

            decimal? min = null;
            decimal? max = null;
            string minRaw = minText?.Trim() ?? "";
            string maxRaw = maxText?.Trim() ?? "";

            if (!string.IsNullOrEmpty(minRaw))
            {
                if (!TryParseDecimal(minRaw, out decimal minVal))
                    return (false, "يرجى إدخال قيمة رقمية صحيحة في حقل الحد الأدنى.", null, null, null);
                min = minVal;
            }

            if (!string.IsNullOrEmpty(maxRaw))
            {
                if (!TryParseDecimal(maxRaw, out decimal maxVal))
                    return (false, "يرجى إدخال قيمة رقمية صحيحة في حقل الحد الأقصى.", null, null, null);
                max = maxVal;
            }

            if (!min.HasValue && !max.HasValue)
                return (false, "يرجى إدخال الحد الأدنى أو الحد الأقصى للباراميتر الرقمي.", null, null, null);

            if (min.HasValue && max.HasValue && min.Value > max.Value)
                return (false, "الحد الأدنى يجب أن يكون أقل من أو يساوي الحد الأقصى.", null, null, null);

            return (true, null, min, max, null);
        }

        private static bool TryParseDecimal(string input, out decimal value)
        {
            return decimal.TryParse(input, NumberStyles.Number, CultureInfo.CurrentCulture, out value)
                || decimal.TryParse(input, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
        }

        private void BtnSaveParam_Click(object? sender, EventArgs e)
        {
            var validation = ValidateParameterInput(
                txtParamName.Text.Trim(),
                chkIsNumeric.Checked,
                txtMin.Text,
                txtMax.Text,
                txtNormalText.Text);

            if (!validation.IsValid)
            {
                MessageBox.Show(validation.ErrorMessage, "بيانات ناقصة أو غير صحيحة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (string.IsNullOrWhiteSpace(txtParamName.Text))
                    txtParamName.Focus();
                else if (chkIsNumeric.Checked)
                    txtMin.Focus();
                return;
            }

            int? genderId = null;
            if (cmbGender.SelectedIndex == 1) genderId = 1;
            else if (cmbGender.SelectedIndex == 2) genderId = 2;

            try
            {
                var param = _currentEditingParam ?? new LabParameter { LabId = _labId };
                param.Name = txtParamName.Text.Trim();
                param.Unit = txtUnit.Text.Trim();
                param.IsNumeric = chkIsNumeric.Checked;
                param.NormalRangeMin = validation.Min;
                param.NormalRangeMax = validation.Max;
                param.NormalText = validation.NormalText;
                param.DisplayOrder = (int)numOrder.Value;
                param.GenderId = genderId;

                _settingsService.SaveParameter(param);
                ServiceLocator.LookupCacheService.InvalidateLabs();
                MessageBox.Show("تم حفظ الباراميتر بنجاح.", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadParameters();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء حفظ الباراميتر:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeleteParam_Click(object? sender, EventArgs e)
        {
            if (_currentEditingParam == null)
            {
                MessageBox.Show("يرجى تحديد باراميتر لحذفه أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirm = MessageBox.Show(
                $"هل أنت متأكد من حذف الباراميتر '{_currentEditingParam.Name}'؟",
                "تأكيد الحذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _settingsService.DeleteParameter(_currentEditingParam.Id);
                    ServiceLocator.LookupCacheService.InvalidateLabs();
                    MessageBox.Show("تم حذف الباراميتر بنجاح.", "تم الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadParameters();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
