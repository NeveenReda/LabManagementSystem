using LabManagementSystem.Common;
using LabManagementSystem.Models;
using LabManagementSystem.Printing;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public partial class FrmSearchVisit : Form
    {
        public event Action? AddvisitClicked;

        private readonly IVisitService _visitService;
        private readonly ILabReportService _labReportService;
        private readonly DebounceHelper _debouncer;
        private LoadingOverlay _loadingOverlay = null!;
        private CancellationTokenSource? _searchCts;

        public FrmSearchVisit()
        {
            InitializeComponent();
            _visitService = ServiceLocator.VisitService;
            _labReportService = ServiceLocator.LabReportService;
            _debouncer = new DebounceHelper(280);

            FormStyler.ApplyFormTheme(this, "سجل الزيارات والفحوصات");
            FormStyler.StyleButton(btnNewVisit, ButtonStyle.Primary);
            FormStyler.StyleButton(btnFilter, ButtonStyle.Primary);
            FormStyler.StyleInput(txtSearchCode);
            FormStyler.StyleInput(txtSearchName);
            DataGridViewStyler.Apply(dgvvisits);

            _loadingOverlay = LoadingOverlay.AttachTo(pnlGridCard);
        }

        private async void FrmSearchVisit_Load(object sender, EventArgs e)
        {
            colEdit.Image = ImageHelper.LoadSafeImage("imgs/edit.png", 16, 16, Color.SteelBlue);
            colDelete.Image = ImageHelper.LoadSafeImage("imgs/delete.png", 16, 16, Color.Crimson);
            colReport.Image = ImageHelper.LoadSafeImage("imgs/report.png", 16, 16, Color.DarkSeaGreen);
            colPayment.Image = ImageHelper.LoadSafeImage("imgs/payment-method.png", 16, 16, Color.Goldenrod);

            dtSearchDateFrom.Value = DateTime.Today.AddDays(-7);
            dtSearchDateTo.Value = DateTime.Today;

            await SearchVisitsAsync();
        }

        private void btnNewVisit_Click(object sender, EventArgs e)
        {
            AddvisitClicked?.Invoke();
        }

        private void txtSearch_TextChanged(object? sender, EventArgs? e)
        {
            _debouncer.Debounce(async () =>
            {
                await SearchVisitsAsync();
            });
        }

        private async void btnFilter_Click(object sender, EventArgs e)
        {
            await SearchVisitsAsync();
        }

        private void chkSearchByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtSearchDateFrom.Enabled = chkSearchByDate.Checked;
            dtSearchDateTo.Enabled = chkSearchByDate.Checked;
            txtSearch_TextChanged(this, EventArgs.Empty);
        }

        private async Task SearchVisitsAsync()
        {
            _searchCts?.Cancel();
            _searchCts?.Dispose();
            _searchCts = new CancellationTokenSource();
            var token = _searchCts.Token;

            _loadingOverlay.ShowOverlay("جاري البحث في سجلات الزيارات...");
            try
            {
                var filter = new VisitSearchFilter
                {
                    PatientCode = txtSearchCode.Text.Trim(),
                    PatientName = txtSearchName.Text.Trim(),
                    IncludeDeleted = false
                };

                if (chkSearchByDate.Checked)
                {
                    if (dtSearchDateFrom.Value.Date > dtSearchDateTo.Value.Date)
                    {
                        MessageBox.Show("تاريخ البداية يجب أن يكون قبل أو يساوي تاريخ النهاية.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    filter.DateFrom = dtSearchDateFrom.Value.Date;
                    filter.DateTo = dtSearchDateTo.Value.Date;
                }

                var list = await _visitService.SearchVisitsAsync(filter, 250, token);

                if (token.IsCancellationRequested) return;

                dgvvisits.DataSource = list;

                if (dgvvisits.Columns["colVisitDate"] != null)
                {
                    dgvvisits.Columns["colVisitDate"].DefaultCellStyle.Format = "yyyy/MM/dd HH:mm";
                }

                decimal totalNet = list.Sum(v => v.NetPrice);
                decimal totalRem = list.Sum(v => v.RemainingAmount);
                lblSummary.Text = $"الزيارات: {list.Count}  |  المطلوب: {totalNet:0.00} ج.م  |  المتبقي: {totalRem:0.00} ج.م";

                // Highlight remaining balance
                foreach (DataGridViewRow row in dgvvisits.Rows)
                {
                    if (row.DataBoundItem is VisitSummaryDto item)
                    {
                        if (item.RemainingAmount > 0)
                        {
                            row.Cells["colRemainingAmount"].Style.ForeColor = ThemeManager.Danger;
                            row.Cells["colRemainingAmount"].Style.Font = ThemeManager.BodyBoldFont;
                        }
                        else
                        {
                            row.Cells["colRemainingAmount"].Style.ForeColor = ThemeManager.Success;
                        }
                    }
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء البحث عن الزيارات:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _loadingOverlay.HideOverlay();
            }
        }

        private async void dgvvisits_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvvisits.Rows[e.RowIndex].Cells["colVisitId"].Value == null) return;

            int visitId = Convert.ToInt32(dgvvisits.Rows[e.RowIndex].Cells["colVisitId"].Value);
            string colName = dgvvisits.Columns[e.ColumnIndex].Name;

            if (colName == "colEdit")
            {
                var visit = await _visitService.GetVisitByIdAsync(visitId);
                if (visit == null)
                {
                    MessageBox.Show("الزيارة غير موجودة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var frm = new FrmAddVisit(visit);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await SearchVisitsAsync();
                }
            }
            else if (colName == "colDelete")
            {
                var confirm = MessageBox.Show(
                    $"هل أنت متأكد من رغبتك في حذف الزيارة رقم #{visitId}؟",
                    "تأكيد الحذف",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        await _visitService.DeleteVisitAsync(visitId);
                        MessageBox.Show("تم حذف الزيارة بنجاح.", "تم الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await SearchVisitsAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"حدث خطأ أثناء حذف الزيارة:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else if (colName == "colPayment")
            {
                using var frm = new FrmPayments(visitId);
                frm.ShowDialog();
                await SearchVisitsAsync();
            }
            else if (colName == "colReport")
            {
                try
                {
                    _loadingOverlay.ShowOverlay("جاري إعداد ومعاينة تقرير الزيارة الطبي...");
                    var reportModel = await _labReportService.GetConsolidatedReportAsync(visitId);
                    if (reportModel == null || reportModel.Tests.Count == 0)
                    {
                        MessageBox.Show("لا توجد تحاليل مسجلة في هذه الزيارة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var doc = new ConsolidatedLabReportDocument(reportModel);
                    using var viewer = new DocumentViewerForm(doc, $"تقرير زيارة مريض - {reportModel.PatientName}");
                    viewer.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"حدث خطأ أثناء إعداد تقرير الزيارة:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _loadingOverlay.HideOverlay();
                }
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _debouncer.Dispose();
            _searchCts?.Cancel();
            _searchCts?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
