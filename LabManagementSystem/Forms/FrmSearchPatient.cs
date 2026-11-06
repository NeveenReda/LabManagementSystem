using LabManagementSystem.Common;
using LabManagementSystem.Services;

namespace LabManagementSystem.Forms
{
    public partial class FrmSearchPatient : Form
    {
        public event Action? AddPatientClicked;

        private readonly IPatientService _patientService;
        private readonly DebounceHelper _debouncer;
        private LoadingOverlay _loadingOverlay = null!;
        private CancellationTokenSource? _searchCts;

        public FrmSearchPatient()
        {
            InitializeComponent();
            _patientService = ServiceLocator.PatientService;
            _debouncer = new DebounceHelper(280);

            FormStyler.ApplyFormTheme(this, "سجل وبحث المرضى");
            FormStyler.StyleButton(btnAddPatient, ButtonStyle.Primary);
            FormStyler.StyleButton(btnRefresh, ButtonStyle.Secondary);
            FormStyler.StyleInput(txtSearch);
            DataGridViewStyler.Apply(dgvPatients);

            _loadingOverlay = LoadingOverlay.AttachTo(pnlGridCard);
        }

        private async void FrmSearchPatient_Load(object sender, EventArgs e)
        {
            colEdit.Image = ImageHelper.LoadSafeImage("imgs/edit.png", 16, 16, Color.SteelBlue);
            colDelete.Image = ImageHelper.LoadSafeImage("imgs/delete.png", 16, 16, Color.Crimson);

            await SearchPatientsAsync();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _debouncer.Debounce(async () =>
            {
                await SearchPatientsAsync();
            });
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await SearchPatientsAsync();
        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            AddPatientClicked?.Invoke();
        }

        private async Task SearchPatientsAsync()
        {
            _searchCts?.Cancel();
            _searchCts?.Dispose();
            _searchCts = new CancellationTokenSource();
            var token = _searchCts.Token;

            _loadingOverlay.ShowOverlay("جاري البحث عن المرضى...");
            try
            {
                string search = txtSearch.Text.Trim();
                var patients = await _patientService.SearchPatientsAsync(search, 150, token);

                if (token.IsCancellationRequested) return;

                dgvPatients.DataSource = patients;
                lblResultsCount.Text = $"المرضى المعروضين: {patients.Count}";
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ أثناء البحث عن المرضى:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _loadingOverlay.HideOverlay();
            }
        }

        private async void dgvPatients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvPatients.Rows[e.RowIndex].Cells["colId"].Value == null) return;

            int patientId = Convert.ToInt32(dgvPatients.Rows[e.RowIndex].Cells["colId"].Value);
            string colName = dgvPatients.Columns[e.ColumnIndex].Name;

            if (colName == "colEdit")
            {
                var patient = await _patientService.GetPatientByIdAsync(patientId);
                if (patient == null)
                {
                    MessageBox.Show("لم يتم العثور على بيانات المريض.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var frm = new FrmAddPatient(patient);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await SearchPatientsAsync();
                }
            }
            else if (colName == "colDelete")
            {
                if (!_patientService.CanDeletePatient(patientId, out string reason))
                {
                    MessageBox.Show(reason, "لا يمكن الحذف", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string patientName = dgvPatients.Rows[e.RowIndex].Cells["colName"].Value?.ToString() ?? "";
                string patientCode = dgvPatients.Rows[e.RowIndex].Cells["colMedicalCode"].Value?.ToString() ?? "";

                var confirm = MessageBox.Show(
                    $"هل أنت متأكد تماماً من رغبتك في حذف المريض:\n({patientName}) - كود: {patientCode}؟\nلا يمكن التراجع عن هذه العملية.",
                    "تأكيد الحذف",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        await _patientService.DeletePatientAsync(patientId);
                        MessageBox.Show("تم حذف المريض بنجاح.", "تم الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await SearchPatientsAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"حدث خطأ أثناء حذف المريض:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
