using LabManagementSystem.Common;
using LabManagementSystem.Models;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public class FrmSelectPatient : Form
    {
        private readonly IPatientService _patientService;
        private readonly DebounceHelper _debouncer;
        private LoadingOverlay _loadingOverlay = null!;
        private CancellationTokenSource? _searchCts;

        public Patient? SelectedPatient { get; private set; }

        private TextBox txtSearch = null!;
        private Button btnSelect = null!;
        private Button btnNewPatient = null!;
        private Button btnCancel = null!;
        private DataGridView dgv = null!;

        public FrmSelectPatient()
        {
            _patientService = ServiceLocator.PatientService;
            _debouncer = new DebounceHelper(280);
            InitializeUi();
            Shown += async (s, e) => await SearchAsync();
        }

        private void InitializeUi()
        {
            FormStyler.ApplyFormTheme(this, "اختيار مريض");
            Size = new Size(860, 560);
            MinimumSize = new Size(720, 480);
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;

            var pnlHeader = FormStyler.CreateHeaderPanel("اختيار مريض من السجل", "ابحث بالاسم أو الكود الطبي أو رقم الهاتف للاختيار السريع");

            // Search Card (Responsive FlowLayoutPanel)
            var pnlSearch = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = ThemeManager.CardBackground,
                Padding = new Padding(12, 10, 12, 10)
            };

            var flpSearch = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0)
            };

            var lblSearch = new Label
            {
                Text = "🔍 بحث سريع:",
                AutoSize = true,
                Margin = new Padding(0, 8, 4, 0),
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText
            };

            txtSearch = new TextBox
            {
                Width = 340,
                Font = ThemeManager.BodyFont,
                PlaceholderText = "اكتب اسم المريض أو الكود الطبي أو الهاتف...",
                Margin = new Padding(4)
            };
            FormStyler.StyleInput(txtSearch);

            btnNewPatient = new Button
            {
                Text = "➕ تسجيل مريض جديد",
                Width = 160,
                Height = 34,
                Cursor = Cursors.Hand,
                Margin = new Padding(8, 2, 4, 2)
            };
            FormStyler.StyleButton(btnNewPatient, ButtonStyle.Success);
            btnNewPatient.Click += BtnNewPatient_Click;

            flpSearch.Controls.Add(lblSearch);
            flpSearch.Controls.Add(txtSearch);
            flpSearch.Controls.Add(btnNewPatient);

            pnlSearch.Controls.Add(flpSearch);

            txtSearch.TextChanged += (s, e) =>
            {
                _debouncer.Debounce(async () =>
                {
                    await SearchAsync();
                });
            };

            txtSearch.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    await SearchAsync();
                }
            };

            // Bottom Actions Panel
            var pnlBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = ThemeManager.CardBackground,
                Padding = new Padding(16, 10, 16, 10)
            };

            btnSelect = new Button
            {
                Text = "✔ اختيار المريض المحدد",
                Dock = DockStyle.Right,
                Width = 175,
                Cursor = Cursors.Hand
            };
            FormStyler.StyleButton(btnSelect, ButtonStyle.Primary);
            btnSelect.Click += async (s, e) => await SelectCurrentPatientAsync();

            btnCancel = new Button
            {
                Text = "إلغاء",
                Dock = DockStyle.Left,
                Width = 90,
                Cursor = Cursors.Hand
            };
            FormStyler.StyleButton(btnCancel, ButtonStyle.Secondary);
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            pnlBottom.Controls.Add(btnSelect);
            pnlBottom.Controls.Add(btnCancel);

            // Grid Container Card
            var pnlGridContainer = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(16, 8, 16, 8),
                BackColor = ThemeManager.Background
            };

            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false
            };
            DataGridViewStyler.Apply(dgv);

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMedicalCode",
                DataPropertyName = "MedicalCode",
                HeaderText = "الكود الطبي",
                FillWeight = 22,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = ThemeManager.BodyBoldFont }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colName",
                DataPropertyName = "Name",
                HeaderText = "اسم المريض",
                FillWeight = 42
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPhone",
                DataPropertyName = "Phone",
                HeaderText = "رقم الهاتف",
                FillWeight = 22,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });
            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAge",
                DataPropertyName = "AgeAtRecord",
                HeaderText = "السن",
                FillWeight = 14,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter }
            });

            dgv.CellDoubleClick += async (s, e) =>
            {
                if (e.RowIndex >= 0)
                    await SelectCurrentPatientAsync();
            };

            pnlGridContainer.Controls.Add(dgv);

            Controls.Add(pnlGridContainer);
            Controls.Add(pnlBottom);
            Controls.Add(pnlSearch);
            Controls.Add(pnlHeader);

            _loadingOverlay = LoadingOverlay.AttachTo(pnlGridContainer);
        }

        private async Task SearchAsync()
        {
            if (IsDisposed) return;

            _searchCts?.Cancel();
            _searchCts?.Dispose();
            _searchCts = new CancellationTokenSource();
            var token = _searchCts.Token;

            _loadingOverlay.ShowOverlay("جاري البحث في سجل المرضى...");
            try
            {
                string term = txtSearch.Text.Trim();
                var list = await _patientService.SearchPatientsAsync(term, 100, token);
                if (token.IsCancellationRequested || IsDisposed) return;

                dgv.DataSource = list;
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                if (!IsDisposed)
                    MessageBox.Show($"خطأ أثناء البحث: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed)
                    _loadingOverlay.HideOverlay();
            }
        }

        private async Task SelectCurrentPatientAsync()
        {
            if (dgv.CurrentRow?.DataBoundItem is PatientSearchResultDto dto)
            {
                try
                {
                    _loadingOverlay.ShowOverlay("جاري تحميل بيانات المريض...");
                    var patient = await _patientService.GetPatientByIdAsync(dto.Id);
                    if (patient != null)
                    {
                        SelectedPatient = patient;
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("تعذر العثور على سجل المريض في قاعدة البيانات.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"حدث خطأ أثناء تحميل المريض:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _loadingOverlay.HideOverlay();
                }
            }
            else
            {
                MessageBox.Show("يرجى تحديد مريض من الجدول أولاً.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void BtnNewPatient_Click(object? sender, EventArgs e)
        {
            using var frm = new FrmAddPatient();
            if (frm.ShowDialog() == DialogResult.OK && frm.CreatedPatient != null)
            {
                SelectedPatient = frm.CreatedPatient;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                await SearchAsync();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            _debouncer.Dispose();
            _searchCts?.Dispose();
        }
    }
}
