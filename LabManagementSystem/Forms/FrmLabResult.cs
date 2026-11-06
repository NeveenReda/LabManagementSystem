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

            await LoadVisitLabInfoAsync();
            await LoadParametersAndExistingResultsAsync();
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
                dto.ResultValue = newVal;

                var flag = CalculateFlag(dto, newVal);
                dto.Flag = flag;

                dgvResults.Rows[e.RowIndex].Cells["colFlag"].Value = flag.ToArabic();
                ApplyRowFormatting(dgvResults.Rows[e.RowIndex], flag);
            }
        }

        private void ApplyAbnormalFormatting()
        {
            foreach (DataGridViewRow row in dgvResults.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Tag is ParameterResultEntryDto dto)
                {
                    ApplyRowFormatting(row, dto.Flag);
                }
            }
        }

        private void ApplyRowFormatting(DataGridViewRow row, AbnormalFlagEnum flag)
        {
            if (flag == AbnormalFlagEnum.High || flag == AbnormalFlagEnum.Low || flag == AbnormalFlagEnum.Abnormal)
            {
                row.Cells["colResult"].Style.ForeColor = ThemeManager.Danger;
                row.Cells["colResult"].Style.Font = ThemeManager.BodyBoldFont;
                row.Cells["colFlag"].Style.ForeColor = ThemeManager.Danger;
                row.Cells["colFlag"].Style.Font = ThemeManager.BodyBoldFont;
            }
            else
            {
                row.Cells["colResult"].Style.ForeColor = ThemeManager.MainText;
                row.Cells["colResult"].Style.Font = ThemeManager.BodyFont;
                row.Cells["colFlag"].Style.ForeColor = ThemeManager.Success;
                row.Cells["colFlag"].Style.Font = ThemeManager.BodyFont;
            }
        }

        private AbnormalFlagEnum CalculateFlag(ParameterResultEntryDto p, string val)
        {
            if (string.IsNullOrWhiteSpace(val)) return AbnormalFlagEnum.Normal;
            if (p.IsNumeric && decimal.TryParse(val.Trim(), out decimal num))
            {
                if (p.NormalMin.HasValue && num < p.NormalMin.Value) return AbnormalFlagEnum.Low;
                if (p.NormalMax.HasValue && num > p.NormalMax.Value) return AbnormalFlagEnum.High;
                return AbnormalFlagEnum.Normal;
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

                entries.Add(new ParameterResultEntryDto
                {
                    ParameterId = paramId,
                    ResultValue = resVal,
                    Notes = notes
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
                var doc = new ConsolidatedLabReportDocument(reportModel);
                using var viewer = new DocumentViewerForm(doc, $"تقرير نتائج المريض - {reportModel.PatientName}");
                viewer.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء إعداد التقرير:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
