using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LabManagementSystem.Common;
using LabManagementSystem.Models;
using LabManagementSystem.Printing;
using LabManagementSystem.Services;
using QuestPDF.Fluent;

namespace LabManagementSystem.Forms
{
    public class FrmDoctorReports : Form
    {
        private readonly IDoctorService _doctorService;
        private LoadingOverlay _loadingOverlay = null!;
        private CancellationTokenSource? _searchCts;
        private List<DoctorVisitReportDto> _currentReport = new();

        private DateTimePicker dtpFrom = null!;
        private DateTimePicker dtpTo = null!;
        private ComboBox cmbDoctor = null!;
        private Button btnLoadReport = null!;
        private Button btnPreviewPdf = null!;
        private Button btnExportPdf = null!;
        private DataGridView dgvReport = null!;
        private Label lblTotalVisits = null!;
        private Label lblTotalDoctors = null!;
        private Label lblTopDoctor = null!;

        private readonly int? _initialDoctorId;

        public FrmDoctorReports(int? initialDoctorId = null)
        {
            _doctorService = ServiceLocator.DoctorService;
            _initialDoctorId = initialDoctorId;
            InitializeUi();
            Shown += async (s, e) =>
            {
                await LoadDoctorsDropdownAsync();
                await LoadReportAsync();
            };
        }

        private void InitializeUi()
        {
            FormStyler.ApplyFormTheme(this, "تقرير الزيارات حسب الطبيب");
            Size = new Size(980, 640);
            MinimumSize = new Size(840, 520);
            StartPosition = FormStartPosition.CenterParent;
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;

            // 1. Header
            var pnlHeader = FormStyler.CreateHeaderPanel(
                "تقرير الزيارات حسب الطبيب المحول",
                "إحصائية شاملة لعدد الزيارات المحولة لكل طبيب ومعدلات التحويل مع إمكانية التصدير والطباعة",
                "📊"
            );

            // 2. Filter Card
            var pnlFilterCard = FormStyler.CreateCardPanel(DockStyle.Top, new Padding(12, 10, 12, 10));
            pnlFilterCard.Height = 68;

            var flpFilter = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoScroll = false,
                Padding = new Padding(0)
            };

            // From Date
            var lblFrom = new Label
            {
                Text = "من:",
                AutoSize = true,
                Margin = new Padding(0, 8, 4, 0),
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText
            };
            dtpFrom = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                Width = 110,
                Font = ThemeManager.BodyFont,
                Margin = new Padding(4)
            };

            // To Date
            var lblTo = new Label
            {
                Text = "إلى:",
                AutoSize = true,
                Margin = new Padding(8, 8, 4, 0),
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText
            };
            dtpTo = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now,
                Width = 110,
                Font = ThemeManager.BodyFont,
                Margin = new Padding(4)
            };

            // Doctor filter
            var lblDoc = new Label
            {
                Text = "الطبيب:",
                AutoSize = true,
                Margin = new Padding(8, 8, 4, 0),
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText
            };
            cmbDoctor = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 170,
                Font = ThemeManager.BodyFont,
                Margin = new Padding(4)
            };

            btnLoadReport = new Button
            {
                Text = "🔍 عرض التقرير",
                Width = 115,
                Height = 34,
                Margin = new Padding(8, 2, 4, 2)
            };
            FormStyler.StyleButton(btnLoadReport, ButtonStyle.Primary);
            btnLoadReport.Click += async (s, e) => await LoadReportAsync();

            btnPreviewPdf = new Button
            {
                Text = "🖨️ معاينة وطباعة",
                Width = 135,
                Height = 34,
                Margin = new Padding(4, 2, 4, 2)
            };
            FormStyler.StyleButton(btnPreviewPdf, ButtonStyle.Secondary);
            btnPreviewPdf.Click += BtnPreviewPdf_Click;

            btnExportPdf = new Button
            {
                Text = "💾 تصدير PDF",
                Width = 115,
                Height = 34,
                Margin = new Padding(4, 2, 4, 2)
            };
            FormStyler.StyleButton(btnExportPdf, ButtonStyle.Success);
            btnExportPdf.Click += BtnExportPdf_Click;

            flpFilter.Controls.Add(lblFrom);
            flpFilter.Controls.Add(dtpFrom);
            flpFilter.Controls.Add(lblTo);
            flpFilter.Controls.Add(dtpTo);
            flpFilter.Controls.Add(lblDoc);
            flpFilter.Controls.Add(cmbDoctor);
            flpFilter.Controls.Add(btnLoadReport);
            flpFilter.Controls.Add(btnPreviewPdf);
            flpFilter.Controls.Add(btnExportPdf);

            pnlFilterCard.Controls.Add(flpFilter);

            // 3. Grid Card
            var pnlGridCard = FormStyler.CreateCardPanel(DockStyle.Fill, new Padding(12));

            dgvReport = new DataGridView
            {
                Dock = DockStyle.Fill,
                RightToLeft = RightToLeft.Yes
            };
            DataGridViewStyler.Apply(dgvReport);
            ConfigureGridColumns();

            pnlGridCard.Controls.Add(dgvReport);

            // 4. Summary Cards Bar (Bottom)
            var pnlSummary = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 52,
                BackColor = ThemeManager.CardBackground,
                Padding = new Padding(16, 8, 16, 8)
            };

            var tlpSummary = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                RightToLeft = RightToLeft.Yes
            };
            tlpSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tlpSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));
            tlpSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33F));

            lblTotalVisits = new Label
            {
                Text = "إجمالي الزيارات المحولة: 0",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleRight,
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.PrimaryNavy
            };

            lblTotalDoctors = new Label
            {
                Text = "عدد الأطباء في التقرير: 0",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText
            };

            lblTopDoctor = new Label
            {
                Text = "الأعلى تحويلاً: -",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.Success
            };

            tlpSummary.Controls.Add(lblTotalVisits, 0, 0);
            tlpSummary.Controls.Add(lblTotalDoctors, 1, 0);
            tlpSummary.Controls.Add(lblTopDoctor, 2, 0);

            pnlSummary.Controls.Add(tlpSummary);

            Controls.Add(pnlGridCard);
            Controls.Add(pnlSummary);
            Controls.Add(pnlFilterCard);
            Controls.Add(pnlHeader);

            _loadingOverlay = LoadingOverlay.AttachTo(dgvReport);
        }

        private void ConfigureGridColumns()
        {
            dgvReport.Columns.Clear();
            dgvReport.AutoGenerateColumns = false;
            dgvReport.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReport.MultiSelect = false;

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDoctorName",
                HeaderText = "اسم الطبيب",
                DataPropertyName = "DoctorName",
                Width = 240,
                ReadOnly = true
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colSpecialty",
                HeaderText = "التخصص",
                DataPropertyName = "Specialty",
                Width = 160,
                ReadOnly = true
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPhone",
                HeaderText = "رقم الهاتف",
                DataPropertyName = "PhoneNumber",
                Width = 140,
                ReadOnly = true
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTotalVisits",
                HeaderText = "إجمالي الزيارات",
                DataPropertyName = "TotalVisits",
                Width = 130,
                ReadOnly = true
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colPercentage",
                HeaderText = "النسبة من الإجمالي",
                Width = 130,
                ReadOnly = true
            });

            dgvReport.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colLastVisit",
                HeaderText = "تاريخ آخر زيارة",
                Width = 140,
                ReadOnly = true
            });

            dgvReport.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvReport.Rows.Count) return;
                var item = dgvReport.Rows[e.RowIndex].DataBoundItem as DoctorVisitReportDto;
                if (item == null) return;

                int grandTotal = _currentReport.Sum(x => x.TotalVisits);

                if (dgvReport.Columns[e.ColumnIndex].Name == "colPercentage")
                {
                    double pct = grandTotal > 0 ? (item.TotalVisits * 100.0 / grandTotal) : 0;
                    e.Value = $"{pct:0.0}%";
                    e.FormattingApplied = true;
                }
                else if (dgvReport.Columns[e.ColumnIndex].Name == "colLastVisit")
                {
                    e.Value = item.LastVisitDate.HasValue ? item.LastVisitDate.Value.ToString("yyyy/MM/dd") : "-";
                    e.FormattingApplied = true;
                }
                else if (dgvReport.Columns[e.ColumnIndex].Name == "colTotalVisits")
                {
                    if (e.CellStyle != null)
                    {
                        e.CellStyle.Font = ThemeManager.BodyBoldFont;
                        e.CellStyle.ForeColor = ThemeManager.PrimaryNavy;
                    }
                }
            };
        }

        private async Task LoadDoctorsDropdownAsync()
        {
            try
            {
                var docs = await _doctorService.GetAllDoctorsAsync(false);
                var items = new List<DoctorDropdownItem>
                {
                    new DoctorDropdownItem { Id = 0, Name = "--- كل الأطباء ---" }
                };

                foreach (var d in docs)
                {
                    string label = string.IsNullOrWhiteSpace(d.Specialty) ? d.Name : $"{d.Name} ({d.Specialty})";
                    items.Add(new DoctorDropdownItem { Id = d.Id, Name = label });
                }

                cmbDoctor.DataSource = items;
                cmbDoctor.DisplayMember = "Name";
                cmbDoctor.ValueMember = "Id";

                if (_initialDoctorId.HasValue && _initialDoctorId.Value > 0)
                {
                    cmbDoctor.SelectedValue = _initialDoctorId.Value;
                }
                else
                {
                    cmbDoctor.SelectedIndex = 0;
                }
            }
            catch { }
        }

        private async Task LoadReportAsync()
        {
            _searchCts?.Cancel();
            _searchCts = new CancellationTokenSource();
            var ct = _searchCts.Token;

            _loadingOverlay.ShowOverlay("جاري استخراج تقرير زيارات الأطباء...");

            try
            {
                var from = dtpFrom.Value.Date;
                var to = dtpTo.Value.Date;
                if (from > to)
                {
                    MessageBox.Show("تاريخ البداية يجب أن يكون قبل أو يساوي تاريخ النهاية.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int? docId = null;
                if (cmbDoctor.SelectedValue is int selId && selId > 0)
                {
                    docId = selId;
                }

                _currentReport = await _doctorService.GetDoctorVisitReportAsync(from, to, docId, ct);

                if (!ct.IsCancellationRequested)
                {
                    dgvReport.DataSource = null;
                    dgvReport.DataSource = _currentReport;

                    int totalVisits = _currentReport.Sum(x => x.TotalVisits);
                    int totalDocs = _currentReport.Count;
                    var top = _currentReport.FirstOrDefault(x => x.TotalVisits > 0);

                    lblTotalVisits.Text = $"إجمالي الزيارات المحولة: {totalVisits}";
                    lblTotalDoctors.Text = $"عدد الأطباء في التقرير: {totalDocs}";
                    lblTopDoctor.Text = top != null ? $"الأعلى تحويلاً: {top.DoctorName} ({top.TotalVisits})" : "الأعلى تحويلاً: -";
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تحميل التقرير: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _loadingOverlay.HideOverlay();
            }
        }

        private void BtnPreviewPdf_Click(object? sender, EventArgs e)
        {
            if (_currentReport.Count == 0)
            {
                MessageBox.Show("لا توجد بيانات في التقرير للمعاينة أو الطباعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string? docName = null;
            if (cmbDoctor.SelectedValue is int selId && selId > 0 && cmbDoctor.SelectedItem is DoctorDropdownItem item)
            {
                docName = item.Name;
            }

            var doc = new DoctorVisitReportDocument(_currentReport, dtpFrom.Value.Date, dtpTo.Value.Date, docName);
            using var viewer = new DocumentViewerForm(doc, $"تقرير زيارات الأطباء ({dtpFrom.Value:yyyy-MM-dd} إلى {dtpTo.Value:yyyy-MM-dd})");
            viewer.ShowDialog(this);
        }

        private void BtnExportPdf_Click(object? sender, EventArgs e)
        {
            if (_currentReport.Count == 0)
            {
                MessageBox.Show("لا توجد بيانات في التقرير للتصدير.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "PDF Document (*.pdf)|*.pdf",
                FileName = $"Doctor_Visits_Report_{dtpFrom.Value:yyyyMMdd}_{dtpTo.Value:yyyyMMdd}.pdf"
            };

            if (sfd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    string? docName = null;
                    if (cmbDoctor.SelectedValue is int selId && selId > 0 && cmbDoctor.SelectedItem is DoctorDropdownItem item)
                    {
                        docName = item.Name;
                    }

                    var doc = new DoctorVisitReportDocument(_currentReport, dtpFrom.Value.Date, dtpTo.Value.Date, docName);
                    doc.GeneratePdf(sfd.FileName);
                    MessageBox.Show("تم تصدير التقرير بصيغة PDF بنجاح.", "تم التصدير", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"حدث خطأ أثناء تصدير الملف: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private class DoctorDropdownItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
        }
    }
}
