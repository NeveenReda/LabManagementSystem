using LabManagementSystem.Common;
using LabManagementSystem.Models;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public partial class frmSettingsLab : Form
    {
        private readonly ILabSettingsService _settingsService;
        private readonly ILookupCacheService _lookupCache;
        private bool _editingMode = false;
        private Lab? _selectedLab;

        public frmSettingsLab()
        {
            InitializeComponent();
            _settingsService = ServiceLocator.LabSettingsService;
            _lookupCache = ServiceLocator.LookupCacheService;
            FormStyler.StyleHeaderPanel(pnlHeader, lblTitle, lblSubtitle);
        }

        private void frmSettingsLab_Load(object sender, EventArgs e)
        {
            FormStyler.ApplyFormTheme(this, "إعدادات الفحوصات والتحاليل");
            FormStyler.StyleHeaderPanel(pnlHeader, lblTitle, lblSubtitle);
            FormStyler.StyleButton(btnSave, ButtonStyle.Success);
            FormStyler.StyleButton(btnNew, ButtonStyle.Secondary);
            FormStyler.StyleButton(btnOpenParams, ButtonStyle.Primary);

            FormStyler.StyleInput(txtName);
            FormStyler.StyleInput(txtPrice);
            FormStyler.StyleInput(txtDays);
            FormStyler.StyleInput(cmbLabCat);

            DataGridViewStyler.Apply(dgvLabs);
            colEdit.Image = ImageHelper.LoadSafeImage("imgs/edit.png", 16, 16, Color.SteelBlue);

            dgvLabs.CellDoubleClick += (s, ev) =>
            {
                if (ev.RowIndex >= 0) OpenSelectedLabParams();
            };

            LoadCategories();
            LoadLabs();
        }

        private void LoadCategories()
        {
            try
            {
                var categories = _lookupCache.GetLabCategories();
                cmbLabCat.DataSource = categories;
                cmbLabCat.DisplayMember = "Name";
                cmbLabCat.ValueMember = "Id";
                cmbLabCat.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل أقسام التحاليل: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLabs()
        {
            try
            {
                var labs = _settingsService.GetAllLabs();
                var displayList = labs.Select(l => new
                {
                    l.Id,
                    l.Name,
                    CategoryName = l.LabCategory?.Name ?? "غير محدد",
                    Price = l.Price.ToString("0.00"),
                    Days = l.Days.ToString(),
                    IsExternalText = l.IsExternal ? "معمل خارجي" : "معمل داخلي",
                    Source = l
                }).ToList();

                dgvLabs.DataSource = displayList;
                ResetInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في استرجاع التحاليل: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetInputs()
        {
            _editingMode = false;
            _selectedLab = null;
            txtName.Clear();
            txtPrice.Clear();
            txtDays.Text = "1";
            chkExternal.Checked = false;
            if (cmbLabCat.Items.Count > 0) cmbLabCat.SelectedIndex = 0;
            btnSave.Text = "💾 حفظ التحليل";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ResetInputs();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("يرجى إدخال اسم التحليل.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || price < 0)
            {
                MessageBox.Show("يرجى إدخال سعر صحيح للتحليل.", "بيانات غير صحيحة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return;
            }

            if (!int.TryParse(txtDays.Text.Trim(), out int days) || days < 0)
            {
                days = 1;
            }

            if (cmbLabCat.SelectedValue == null)
            {
                MessageBox.Show("يرجى اختيار قسم التحليل.", "بيانات ناقصة", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!_editingMode)
                {
                    var newLab = new Lab
                    {
                        Name = name,
                        Price = price,
                        Days = days,
                        LabCategoryId = (int)cmbLabCat.SelectedValue,
                        IsExternal = chkExternal.Checked
                    };

                    _settingsService.SaveLab(newLab);
                    MessageBox.Show("تم حفظ التحليل بنجاح.", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (_selectedLab != null)
                    {
                        _selectedLab.Name = name;
                        _selectedLab.Price = price;
                        _selectedLab.Days = days;
                        _selectedLab.LabCategoryId = (int)cmbLabCat.SelectedValue;
                        _selectedLab.IsExternal = chkExternal.Checked;

                        _settingsService.SaveLab(_selectedLab);
                        MessageBox.Show("تم تعديل التحليل بنجاح.", "تم التعديل", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                _lookupCache.InvalidateLabs();
                LoadLabs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء حفظ التحليل:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvLabs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string colName = dgvLabs.Columns[e.ColumnIndex].Name;
            if (colName == "colEdit")
            {
                var rowItem = dgvLabs.Rows[e.RowIndex].DataBoundItem;
                if (rowItem != null)
                {
                    dynamic dyn = rowItem;
                    _selectedLab = dyn.Source;
                    if (_selectedLab != null)
                    {
                        _editingMode = true;
                        txtName.Text = _selectedLab.Name;
                        txtPrice.Text = _selectedLab.Price.ToString("0.00");
                        txtDays.Text = _selectedLab.Days.ToString();
                        chkExternal.Checked = _selectedLab.IsExternal;
                        cmbLabCat.SelectedValue = _selectedLab.LabCategoryId;
                        btnSave.Text = "✏️ تعديل التحليل";
                    }
                }
            }
        }

        private void btnOpenParams_Click(object? sender, EventArgs e)
        {
            OpenSelectedLabParams();
        }

        private void OpenSelectedLabParams()
        {
            if (dgvLabs.CurrentRow?.DataBoundItem != null)
            {
                dynamic dyn = dgvLabs.CurrentRow.DataBoundItem;
                int labId = dyn.Id;
                string labName = dyn.Name;

                using var frm = new FrmLabParametersDialog(labId, labName);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("يرجى تحديد تحليل من القائمة أولاً لضبط الباراميترات.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
