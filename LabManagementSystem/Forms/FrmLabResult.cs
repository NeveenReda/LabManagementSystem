using LabManagementSystem.Common;
using LabManagementSystem.Enums;
using LabManagementSystem.Models;
using LabManagementSystem.Printing;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public partial class FrmLabResult : Form
    {
        private readonly ILabReportService _labReportService;
        private readonly IVisitService _visitService;

        private int _visitLabId;
        private Visit? _visit;
        private VisitLab? _visitLab;

        public FrmLabResult()
        {
            InitializeComponent();
            _labReportService = ServiceLocator.LabReportService;
            _visitService = ServiceLocator.VisitService;
            FormStyler.StyleHeaderPanel(pnlHeader, lblTitle, lblSubtitle);
        }

        public FrmLabResult(int visitLabId)
        {
            InitializeComponent();
            _labReportService = ServiceLocator.LabReportService;
            _visitService = ServiceLocator.VisitService;
            _visitLabId = visitLabId;
            FormStyler.StyleHeaderPanel(pnlHeader, lblTitle, lblSubtitle);
        }

        private async void FrmLabResult_Load(object sender, EventArgs e)
        {
            FormStyler.ApplyFormTheme(this, "نتائج الفحص الطبي");
            FormStyler.StyleHeaderPanel(pnlHeader, lblTitle, lblSubtitle);
            FormStyler.StyleButton(btnSave, ButtonStyle.Success);
            FormStyler.StyleButton(btnPrintReport, ButtonStyle.Primary);
            FormStyler.StyleButton(btnClose, ButtonStyle.Secondary);

            DataGridViewStyler.Apply(dgvResults);
            dgvResults.CellEndEdit += DgvResults_CellEndEdit;

            // Hide the Flag/Status column as requested
            colFlag.Visible = false;

            // Add the font styling and quick insertion toolbar
            InitializeFormattingBar();

            await LoadVisitLabInfoAsync();
            await LoadParametersAndExistingResultsAsync();
        }

        private void InitializeFormattingBar()
        {
            var pnlToolbar = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 44,
                BackColor = Color.FromArgb(241, 245, 249),
                Padding = new Padding(8, 6, 8, 4),
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                RightToLeft = RightToLeft.Yes
            };

            var lblFormat = new Label
            {
                Text = "🎨 تنسيق النتيجة:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = ThemeManager.PrimaryNavy,
                AutoSize = true,
                Margin = new Padding(4, 6, 8, 4)
            };

            var btnBold = new Button
            {
                Text = "B عريض",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Width = 72,
                Height = 30,
                Cursor = Cursors.Hand,
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(3, 2, 8, 2)
            };
            btnBold.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
            btnBold.Click += (s, e) => ToggleSelectedBold();

            var lblColor = new Label
            {
                Text = "اللون:",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                AutoSize = true,
                Margin = new Padding(4, 6, 4, 4)
            };

            Button CreateColorBtn(string text, Color backColor, Color foreColor, string hexCode)
            {
                var btn = new Button
                {
                    Text = text,
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                    Width = 68,
                    Height = 30,
                    BackColor = backColor,
                    ForeColor = foreColor,
                    Cursor = Cursors.Hand,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(2, 2, 4, 2)
                };
                btn.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
                btn.Click += (s, e) => ApplySelectedColor(hexCode);
                return btn;
            }

            var btnBlack = CreateColorBtn("⬛ عادي", Color.White, Color.FromArgb(30, 41, 59), "#111111");
            var btnRed = CreateColorBtn("🟥 أحمر", Color.FromArgb(254, 242, 242), Color.FromArgb(220, 38, 38), "#DC2626");
            var btnBlue = CreateColorBtn("🟦 أزرق", Color.FromArgb(239, 246, 255), Color.FromArgb(37, 99, 235), "#2563EB");
            var btnGreen = CreateColorBtn("🟩 أخضر", Color.FromArgb(240, 253, 244), Color.FromArgb(22, 163, 74), "#16A34A");

            var lblQuick = new Label
            {
                Text = "قيم سريعة:",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(71, 85, 105),
                AutoSize = true,
                Margin = new Padding(8, 6, 4, 4)
            };

            Button CreateQuickBtn(string val)
            {
                var btn = new Button
                {
                    Text = val,
                    Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                    AutoSize = true,
                    Height = 30,
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(30, 41, 59),
                    Cursor = Cursors.Hand,
                    FlatStyle = FlatStyle.Flat,
                    Margin = new Padding(2, 2, 2, 2)
                };
                btn.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
                btn.Click += (s, e) => InsertQuickValue(val);
                return btn;
            }

            pnlToolbar.Controls.Add(lblFormat);
            pnlToolbar.Controls.Add(btnBold);
            pnlToolbar.Controls.Add(lblColor);
            pnlToolbar.Controls.Add(btnBlack);
            pnlToolbar.Controls.Add(btnRed);
            pnlToolbar.Controls.Add(btnBlue);
            pnlToolbar.Controls.Add(btnGreen);
            pnlToolbar.Controls.Add(lblQuick);
            pnlToolbar.Controls.Add(CreateQuickBtn("+"));
            pnlToolbar.Controls.Add(CreateQuickBtn("++"));
            pnlToolbar.Controls.Add(CreateQuickBtn("+++"));
            pnlToolbar.Controls.Add(CreateQuickBtn("Nil"));
            pnlToolbar.Controls.Add(CreateQuickBtn("Negative"));
            pnlToolbar.Controls.Add(CreateQuickBtn("Clear"));
            pnlToolbar.Controls.Add(CreateQuickBtn("Turbid"));

            pnlGridCard.Controls.Add(pnlToolbar);
            pnlToolbar.BringToFront();
        }

        private void ToggleSelectedBold()
        {
            if (dgvResults.CurrentRow?.Tag is ParameterResultEntryDto dto)
            {
                dto.IsBold = !dto.IsBold;
                var currentFont = dgvResults.CurrentRow.Cells["colResult"].Style.Font ?? dgvResults.Font;
                var newFont = new Font(currentFont, dto.IsBold ? FontStyle.Bold : FontStyle.Regular);
                dgvResults.CurrentRow.Cells["colResult"].Style.Font = newFont;
            }
        }

        private void ApplySelectedColor(string hexCode)
        {
            if (dgvResults.CurrentRow?.Tag is ParameterResultEntryDto dto)
            {
                dto.TextColor = hexCode;
                try
                {
                    dgvResults.CurrentRow.Cells["colResult"].Style.ForeColor = ColorTranslator.FromHtml(hexCode);
                }
                catch { }
            }
        }

        private void InsertQuickValue(string val)
        {
            if (dgvResults.CurrentRow?.Tag is ParameterResultEntryDto dto)
            {
                dgvResults.CurrentRow.Cells["colResult"].Value = val;
                dto.ResultValue = val;
            }
        }

        private async Task LoadVisitLabInfoAsync()
        {
            using var db = new LabContext();
            _visitLab = db.VisitLabs.FirstOrDefault(vl => vl.Id == _visitLabId);

            if (_visitLab != null)
            {
                _visit = await _visitService.GetVisitByIdAsync(_visitLab.VisitId);
                if (_visit != null)
                {
                    var currentLab = _visit.VisitLabs.FirstOrDefault(vl => vl.Id == _visitLabId)?.Lab;
                    string labName = currentLab?.Name ?? "تحليل";

                    lblInfoText.Text = $"🔬 الفحص: [{labName}]   |   المريض: {_visit.Patient.Name} (كود: {_visit.Patient.MedicalCode})   |   السن: {_visit.Patient.AgeAtRecord}   |   النوع: {_visit.Patient.Gender?.Name ?? "-"}   |   التاريخ: {_visit.VisitDate:yyyy/MM/dd}";
                }
            }
        }

        private async Task LoadParametersAndExistingResultsAsync()
        {
            dgvResults.Rows.Clear();
            var parameters = await _labReportService.GetParametersAndResultsAsync(_visitLabId);

            foreach (var p in parameters)
            {
                int rowIndex = dgvResults.Rows.Add(
                    p.ParameterId,
                    p.ParameterName,
                    p.Unit,
                    p.FormattedNormalRange,
                    p.ResultValue,
                    p.Flag.ToArabic(),
                    p.Notes
                );

                dgvResults.Rows[rowIndex].Tag = p;
            }

            ApplyAbnormalFormatting();
        }

        private void DgvResults_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvResults.Rows[e.RowIndex].Tag is ParameterResultEntryDto dto)
            {
                string newVal = dgvResults.Rows[e.RowIndex].Cells["colResult"].Value?.ToString() ?? "";
                // Relaxed: Allow numbers, letters, symbols (e.g. +, ++, +++, Nil, Turbid, etc.) freely
                dto.ResultValue = newVal;

                var flag = CalculateFlag(dto, newVal);
                dto.Flag = flag;

                ApplyRowFormatting(dgvResults.Rows[e.RowIndex], dto);
            }
        }

        private void ApplyAbnormalFormatting()
        {
            foreach (DataGridViewRow row in dgvResults.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Tag is ParameterResultEntryDto dto)
                {
                    ApplyRowFormatting(row, dto);
                }
            }
        }

        private void ApplyRowFormatting(DataGridViewRow row, ParameterResultEntryDto dto)
        {
            // Custom text color
            if (!string.IsNullOrWhiteSpace(dto.TextColor))
            {
                try
                {
                    row.Cells["colResult"].Style.ForeColor = ColorTranslator.FromHtml(dto.TextColor);
                }
                catch
                {
                    row.Cells["colResult"].Style.ForeColor = ThemeManager.MainText;
                }
            }
            else if (dto.Flag == AbnormalFlagEnum.High || dto.Flag == AbnormalFlagEnum.Low || dto.Flag == AbnormalFlagEnum.Abnormal)
            {
                row.Cells["colResult"].Style.ForeColor = ThemeManager.Danger;
            }
            else
            {
                row.Cells["colResult"].Style.ForeColor = ThemeManager.MainText;
            }

            // Custom bold
            if (dto.IsBold || dto.Flag == AbnormalFlagEnum.High || dto.Flag == AbnormalFlagEnum.Low || dto.Flag == AbnormalFlagEnum.Abnormal)
            {
                row.Cells["colResult"].Style.Font = ThemeManager.BodyBoldFont;
            }
            else
            {
                row.Cells["colResult"].Style.Font = ThemeManager.BodyFont;
            }
        }

        private AbnormalFlagEnum CalculateFlag(ParameterResultEntryDto p, string val)
        {
            if (string.IsNullOrWhiteSpace(val)) return AbnormalFlagEnum.Normal;
            if (p.IsNumeric)
            {
                if (LabReportService.TryParseNumericResult(val, out decimal num))
                {
                    if (p.NormalMin.HasValue && num < p.NormalMin.Value) return AbnormalFlagEnum.Low;
                    if (p.NormalMax.HasValue && num > p.NormalMax.Value) return AbnormalFlagEnum.High;
                }
                return AbnormalFlagEnum.Normal;
            }

            string actual = val.Trim().ToLowerInvariant();
            if (!string.IsNullOrWhiteSpace(p.NormalText))
            {
                string expected = p.NormalText.Trim().ToLowerInvariant();
                if (actual == expected) return AbnormalFlagEnum.Normal;
                if (actual.Contains("positive") || actual.Contains("إيجابي") || actual == "reactive" || actual.Contains("abnormal") || actual.Contains("+"))
                    return AbnormalFlagEnum.Abnormal;
                if ((expected == "negative" || expected == "non-reactive" || expected == "normal" || expected == "سلبي" || expected == "طبيعي") && !actual.Contains(expected))
                    return AbnormalFlagEnum.Abnormal;
            }
            else if (actual.Contains("positive") || actual.Contains("إيجابي") || actual == "reactive" || actual.Contains("abnormal"))
            {
                return AbnormalFlagEnum.Abnormal;
            }

            return AbnormalFlagEnum.Normal;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            var entries = new List<ParameterResultEntryDto>();

            foreach (DataGridViewRow row in dgvResults.Rows)
            {
                if (row.IsNewRow) continue;

                int paramId = Convert.ToInt32(row.Cells["colParameterId"].Value);
                string resVal = row.Cells["colResult"].Value?.ToString() ?? "";
                string notes = row.Cells["colNotes"].Value?.ToString() ?? "";

                bool isBold = false;
                string? textColor = null;
                if (row.Tag is ParameterResultEntryDto dto)
                {
                    isBold = dto.IsBold;
                    textColor = dto.TextColor;
                }

                entries.Add(new ParameterResultEntryDto
                {
                    ParameterId = paramId,
                    ResultValue = resVal,
                    Notes = notes,
                    IsBold = isBold,
                    TextColor = textColor
                });
            }

            try
            {
                btnSave.Enabled = false;

                await _labReportService.SaveResultsAsync(_visitLabId, entries);

                MessageBox.Show("تم حفظ وتحديث نتائج التحليل بنجاح واعتماد الحالة.", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var prompt = MessageBox.Show(
                    "هل ترغب في معاينة أو طباعة التقرير الطبي الشامل للزيارة الآن؟",
                    "طباعة التقرير الطبي",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (prompt == DialogResult.Yes)
                {
                    btnPrintReport_Click(this, EventArgs.Empty);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء حفظ النتائج:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!IsDisposed && btnSave != null && !btnSave.IsDisposed)
                    btnSave.Enabled = true;
            }
        }

        private async void btnPrintReport_Click(object? sender, EventArgs e)
        {
            if (_visitLab == null) return;

            try
            {
                var reportModel = await _labReportService.GetConsolidatedReportAsync(_visitLab.VisitId);
                var currentTest = reportModel.Tests.FirstOrDefault(t => t.VisitLabId == _visitLabId);
                var doc = new ConsolidatedLabReportDocument(reportModel, currentTest);
                string title = currentTest != null
                    ? $"{LabReportEnglishHelper.NormalizeTestName(currentTest.TestName)} - {reportModel.PatientName}"
                    : $"تقرير نتائج المريض - {reportModel.PatientName}";
                using var viewer = new DocumentViewerForm(doc, title);
                viewer.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء إعداد التقرير:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
