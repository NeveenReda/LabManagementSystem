using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabManagementSystem.Common;
using LabManagementSystem.Models;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public class FrmDoctors : Form
    {
        private readonly IDoctorService _doctorService;
        private readonly DebounceHelper _debouncer;
        private LoadingOverlay _loadingOverlay = null!;
        private CancellationTokenSource? _searchCts;

        private TextBox txtSearch = null!;
        private ComboBox cmbStatusFilter = null!;
        private Button btnNewDoctor = null!;
        private Button btnEditDoctor = null!;
        private Button btnToggleStatus = null!;
        private Button btnDoctorReport = null!;
        private Button btnRefresh = null!;
        private DataGridView dgvDoctors = null!;
        private Label lblCount = null!;
        private Label lblTotalVisits = null!;

        public event Action? DoctorReportRequested;

        public FrmDoctors()
        {
            _doctorService = ServiceLocator.DoctorService;
            _debouncer = new DebounceHelper(280);
            InitializeUi();
            Shown += async (s, e) => await LoadDoctorsAsync();
        }

        private void InitializeUi()
        {
            FormStyler.ApplyFormTheme(this, "سجل وإدارة الأطباء المحولين");
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;

            // 1. Header
            var pnlHeader = FormStyler.CreateHeaderPanel(
                "سجل وإدارة الأطباء المحولين",
                "إدارة بيانات الأطباء والتخصصات ومعلومات التواصل ومتابعة إحصائيات وزيارات كل طبيب",
                "👨‍⚕️"
            );

            // 2. Action / Filter Bar
            var pnlFilterCard = FormStyler.CreateCardPanel(DockStyle.Top, new Padding(12, 10, 12, 10));
            pnlFilterCard.Height = 62;

            var flpFilter = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0)
            };

            var lblSearch = new Label
            {
                Text = "🔍 بحث:",
                AutoSize = true,
                Margin = new Padding(0, 8, 4, 0),
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText
            };

            txtSearch = new TextBox
            {
                Width = 220,
                Font = ThemeManager.BodyFont,
                PlaceholderText = "اسم الطبيب، التخصص، أو الهاتف...",
                Margin = new Padding(4)
            };
            FormStyler.StyleInput(txtSearch);
            txtSearch.TextChanged += (s, e) =>
            {
                _debouncer.Debounce(async () => await LoadDoctorsAsync());
            };

            var lblFilter = new Label
            {
                Text = "الحالة:",
                AutoSize = true,
                Margin = new Padding(10, 8, 4, 0),
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText
            };

            cmbStatusFilter = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 120,
                Font = ThemeManager.BodyFont,
                Margin = new Padding(4)
            };
            cmbStatusFilter.Items.AddRange(new object[] { "الكل", "النشطون فقط", "غير النشطين" });
            cmbStatusFilter.SelectedIndex = 0;
            cmbStatusFilter.SelectedIndexChanged += async (s, e) => await LoadDoctorsAsync();

            btnNewDoctor = new Button
            {
                Text = "➕ طبيب جديد",
                Width = 130,
                Height = 34,
                Margin = new Padding(10, 2, 4, 2)
            };
            FormStyler.StyleButton(btnNewDoctor, ButtonStyle.Success);
            btnNewDoctor.Click += BtnNewDoctor_Click;

            btnEditDoctor = new Button
            {
                Text = "✏️ تعديل",
                Width = 90,
                Height = 34,
                Margin = new Padding(4, 2, 4, 2)
            };
            FormStyler.StyleButton(btnEditDoctor, ButtonStyle.Primary);
            btnEditDoctor.Click += BtnEditDoctor_Click;

            btnToggleStatus = new Button
            {
                Text = "🔄 تفعيل / تعطيل",
                Width = 130,
                Height = 34,
                Margin = new Padding(4, 2, 4, 2)
            };
            FormStyler.StyleButton(btnToggleStatus, ButtonStyle.Secondary);
            btnToggleStatus.Click += async (s, e) => await BtnToggleStatus_Click();

            btnDoctorReport = new Button
            {
                Text = "📊 تقرير الزيارات",
                Width = 140,
                Height = 34,
                Margin = new Padding(4, 2, 4, 2)
            };
            FormStyler.StyleButton(btnDoctorReport, ButtonStyle.Primary);
            btnDoctorReport.Click += BtnDoctorReport_Click;

            btnRefresh = new Button
            {
                Text = "🔄 تحديث",
                Width = 90,
                Height = 34,
                Margin = new Padding(4, 2, 4, 2)
            };
            FormStyler.StyleButton(btnRefresh, ButtonStyle.Secondary);
            btnRefresh.Click += async (s, e) => await LoadDoctorsAsync();

            flpFilter.Controls.Add(lblSearch);
            flpFilter.Controls.Add(txtSearch);
            flpFilter.Controls.Add(lblFilter);
            flpFilter.Controls.Add(cmbStatusFilter);
            flpFilter.Controls.Add(btnNewDoctor);
            flpFilter.Controls.Add(btnEditDoctor);
            flpFilter.Controls.Add(btnToggleStatus);
            flpFilter.Controls.Add(btnDoctorReport);
            flpFilter.Controls.Add(btnRefresh);

            pnlFilterCard.Controls.Add(flpFilter);

            // 3. Grid Card
            var pnlGridCard = FormStyler.CreateCardPanel(DockStyle.Fill, new Padding(12));

            dgvDoctors = new DataGridView
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes
            };
            DataGridViewStyler.Apply(dgvDoctors);
            ConfigureGridColumns();
            dgvDoctors.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex >= 0) BtnEditDoctor_Click(s, e);
            };

            pnlGridCard.Controls.Add(dgvDoctors);

            // 4. Footer Summary Bar
            var pnlSummary = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 44,
                BackColor = ThemeManager.CardBackground,
                Padding = new Padding(16, 8, 16, 8)
            };

            var tlpSummary = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                RightToLeft = RightToLeft.Yes
            };
            tlpSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            lblCount = new Label
            {
                Text = "إجمالي الأطباء: 0",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText
            };

            lblTotalVisits = new Label
            {
                Text = "إجمالي زيارات الأطباء: 0",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.PrimaryNavy
            };

            tlpSummary.Controls.Add(lblCount, 0, 0);
            tlpSummary.Controls.Add(lblTotalVisits, 1, 0);
            pnlSummary.Controls.Add(tlpSummary);

            Controls.Add(pnlGridCard);
            Controls.Add(pnlSummary);
            Controls.Add(pnlFilterCard);
            Controls.Add(pnlHeader);

            _loadingOverlay = LoadingOverlay.AttachTo(dgvDoctors);
        }

        private void ConfigureGridColumns()
        {
            dgvDoctors.Columns.Clear();
            dgvDoctors.AutoGenerateColumns = false;
            dgvDoctors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDoctors.MultiSelect = false;

            dgvDoctors.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colId",
                HeaderText = "#",
                DataPropertyName = "Id",
                Width = 60,
                ReadOnly = true
            });

            dgvDoctors.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colName",
                HeaderText = "اسم الطبيب",
                DataPropertyName = "Name",
                Width = 220,
                ReadOnly = true
            });

            dgvDoctors.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSpecialty",
                HeaderText = "التخصص",
                DataPropertyName = "Specialty",
                Width = 160,
                ReadOnly = true
            });

            dgvDoctors.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPhone",
                HeaderText = "رقم الهاتف",
                DataPropertyName = "PhoneNumber",
                Width = 140,
                ReadOnly = true
            });

            dgvDoctors.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTotalVisits",
                HeaderText = "عدد الزيارات",
                DataPropertyName = "TotalVisits",
                Width = 110,
                ReadOnly = true
            });

            dgvDoctors.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "الحالة",
                Width = 100,
                ReadOnly = true
            });

            dgvDoctors.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colAddress",
                HeaderText = "العنوان / العيادة",
                DataPropertyName = "Address",
                Width = 200,
                ReadOnly = true
            });

            dgvDoctors.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNotes",
                HeaderText = "ملاحظات",
                DataPropertyName = "Notes",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            });

            dgvDoctors.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvDoctors.Rows.Count) return;

                if (dgvDoctors.Columns[e.ColumnIndex].Name == "colStatus")
                {
                    if (dgvDoctors.Rows[e.RowIndex].DataBoundItem is DoctorListItemDto item)
                    {
                        if (item.IsActive)
                        {
                            e.Value = "نشط ✓";
                            if (e.CellStyle != null)
                            {
                                e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52); // Dark Green
                                e.CellStyle.Font = ThemeManager.BodyBoldFont;
                            }
                        }
                        else
                        {
                            e.Value = "معطل ✕";
                            if (e.CellStyle != null)
                            {
                                e.CellStyle.ForeColor = Color.FromArgb(185, 28, 28); // Dark Red
                                e.CellStyle.Font = ThemeManager.BodyBoldFont;
                            }
                        }
                        e.FormattingApplied = true;
                    }
                }
            };
        }

        private async Task LoadDoctorsAsync()
        {
            _searchCts?.Cancel();
            _searchCts = new CancellationTokenSource();
            var ct = _searchCts.Token;

            _loadingOverlay.ShowOverlay("جاري تحميل بيانات الأطباء...");

            try
            {
                string term = txtSearch.Text.Trim();
                bool? onlyActive = cmbStatusFilter.SelectedIndex switch
                {
                    1 => true,
                    2 => false,
                    _ => null
                };

                var docs = await _doctorService.GetDoctorsOverviewAsync(term, onlyActive, ct);

                if (!ct.IsCancellationRequested)
                {
                    dgvDoctors.DataSource = docs;
                    lblCount.Text = $"إجمالي الأطباء: {docs.Count} (نشط: {docs.Count(d => d.IsActive)})";
                    lblTotalVisits.Text = $"إجمالي الزيارات المسجلة للأطباء: {docs.Sum(d => d.TotalVisits)}";
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تحميل بيانات الأطباء: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _loadingOverlay.HideOverlay();
            }
        }

        private void BtnNewDoctor_Click(object? sender, EventArgs e)
        {
            using var dlg = new FrmDoctorDialog();
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                _ = LoadDoctorsAsync();
            }
        }

        private async void BtnEditDoctor_Click(object? sender, EventArgs e)
        {
            if (dgvDoctors.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى اختيار طبيب من القائمة للتعديل.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvDoctors.SelectedRows[0].DataBoundItem is DoctorListItemDto item)
            {
                var doc = await _doctorService.GetDoctorByIdAsync(item.Id);
                if (doc == null)
                {
                    MessageBox.Show("لم يتم العثور على بيانات الطبيب.", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using var dlg = new FrmDoctorDialog(doc);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    await LoadDoctorsAsync();
                }
            }
        }

        private async Task BtnToggleStatus_Click()
        {
            if (dgvDoctors.SelectedRows.Count == 0)
            {
                MessageBox.Show("يرجى اختيار طبيب من القائمة لتغيير حالته.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (dgvDoctors.SelectedRows[0].DataBoundItem is DoctorListItemDto item)
            {
                string actionName = item.IsActive ? "تعطيل" : "تفعيل";
                var confirm = MessageBox.Show(
                    $"هل أنت متأكد من رغبتك في {actionName} الطبيب '{item.Name}'؟",
                    "تأكيد",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    bool newStatus = await _doctorService.ToggleDoctorActiveStatusAsync(item.Id);
                    await LoadDoctorsAsync();
                }
            }
        }

        private void BtnDoctorReport_Click(object? sender, EventArgs e)
        {
            int? selectedDoctorId = null;
            if (dgvDoctors.SelectedRows.Count > 0 && dgvDoctors.SelectedRows[0].DataBoundItem is DoctorListItemDto item)
            {
                selectedDoctorId = item.Id;
            }

            if (DoctorReportRequested != null)
            {
                DoctorReportRequested.Invoke();
            }
            else
            {
                var frmReport = new FrmDoctorReports(selectedDoctorId);
                frmReport.ShowDialog(this);
            }
        }
    }
}
