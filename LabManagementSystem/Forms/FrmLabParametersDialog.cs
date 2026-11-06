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
        private TextBox txtMin = null!;
        private TextBox txtMax = null!;
        private TextBox txtNormalText = null!;
        private NumericUpDown numOrder = null!;
        private ComboBox cmbGender = null!;
        private Button btnSaveParam = null!;
        private Button btnNewParam = null!;
        private Button btnDeleteParam = null!;
        private Button btnClose = null!;
        private LabParameter? _currentEditingParam;

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
                Text = "قيمة رقمية (Numeric)",
                AutoSize = true,
                Checked = true,
                Font = ThemeManager.BodyBoldFont
            };
            chkIsNumeric.CheckedChanged += (s, e) =>
            {
                txtMin.Enabled = chkIsNumeric.Checked;
                txtMax.Enabled = chkIsNumeric.Checked;
                if (!chkIsNumeric.Checked)
                {
                    txtMin.Clear();
                    txtMax.Clear();
                }
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

            var lblMin = new Label { Text = "الحد الأدنى:", AutoSize = true, Margin = new Padding(0, 8, 4, 0), Font = ThemeManager.BodyBoldFont };
            txtMin = new TextBox { Width = 75, Font = ThemeManager.BodyFont, Margin = new Padding(4) };
            FormStyler.StyleInput(txtMin);

            var lblMax = new Label { Text = "الحد الأقصى:", AutoSize = true, Margin = new Padding(8, 8, 4, 0), Font = ThemeManager.BodyBoldFont };
            txtMax = new TextBox { Width = 75, Font = ThemeManager.BodyFont, Margin = new Padding(4) };
            FormStyler.StyleInput(txtMax);

            var lblNormalText = new Label { Text = "نص طبيعي (Text):", AutoSize = true, Margin = new Padding(8, 8, 4, 0), Font = ThemeManager.BodyBoldFont };
            txtNormalText = new TextBox { Width = 150, Font = ThemeManager.BodyFont, Margin = new Padding(4) };
            FormStyler.StyleInput(txtNormalText);

            var lblGender = new Label { Text = "النوع:", AutoSize = true, Margin = new Padding(8, 8, 4, 0), Font = ThemeManager.BodyBoldFont };
            cmbGender = new ComboBox { Width = 135, DropDownStyle = ComboBoxStyle.DropDownList, Font = ThemeManager.BodyFont, Margin = new Padding(4) };
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

        private void ResetParamInputs()
        {
            _currentEditingParam = null;
            txtParamName.Clear();
            txtUnit.Clear();
            txtMin.Clear();
            txtMax.Clear();
            txtNormalText.Clear();
            chkIsNumeric.Checked = true;
            numOrder.Value = dgvParams.Rows.Count + 1;
            cmbGender.SelectedIndex = 0;
            btnSaveParam.Text = "💾 حفظ الباراميتر";
            FormStyler.StyleButton(btnSaveParam, ButtonStyle.Success);
        }

        private void DgvParams_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvParams.CurrentRow?.DataBoundItem is LabParameter p)
            {
                _currentEditingParam = p;
                txtParamName.Text = p.Name;
                txtUnit.Text = p.Unit ?? "";
                chkIsNumeric.Checked = p.IsNumeric;
                txtMin.Text = p.NormalRangeMin?.ToString() ?? "";
                txtMax.Text = p.NormalRangeMax?.ToString() ?? "";
                txtNormalText.Text = p.NormalText ?? "";
                numOrder.Value = Math.Max(1, p.DisplayOrder);

                if (p.GenderId == 1) cmbGender.SelectedIndex = 1;
                else if (p.GenderId == 2) cmbGender.SelectedIndex = 2;
                else cmbGender.SelectedIndex = 0;

                btnSaveParam.Text = "✏️ تحديث الباراميتر";
                FormStyler.StyleButton(btnSaveParam, ButtonStyle.Primary);
            }
        }

        private void BtnSaveParam_Click(object? sender, EventArgs e)
        {
            string name = txtParamName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("يرجى إدخال اسم الباراميتر.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtParamName.Focus();
                return;
            }

            decimal? min = null;
            decimal? max = null;
            if (chkIsNumeric.Checked)
            {
                if (decimal.TryParse(txtMin.Text.Trim(), out decimal minVal)) min = minVal;
                if (decimal.TryParse(txtMax.Text.Trim(), out decimal maxVal)) max = maxVal;
            }

            int? genderId = null;
            if (cmbGender.SelectedIndex == 1) genderId = 1;
            else if (cmbGender.SelectedIndex == 2) genderId = 2;

            try
            {
                var param = _currentEditingParam ?? new LabParameter { LabId = _labId };
                param.Name = name;
                param.Unit = txtUnit.Text.Trim();
                param.IsNumeric = chkIsNumeric.Checked;
                param.NormalRangeMin = min;
                param.NormalRangeMax = max;
                param.NormalText = string.IsNullOrWhiteSpace(txtNormalText.Text) ? null : txtNormalText.Text.Trim();
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
