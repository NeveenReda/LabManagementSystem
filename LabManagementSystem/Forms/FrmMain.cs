using LabManagementSystem.Forms;

namespace LabManagementSystem
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }
        private void OpenForm(Form childForm)
        {
            pnlContent.Controls.Clear();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Add(childForm);


            childForm.Show();

        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            pnlPatients.Visible = !pnlPatients.Visible;
        }

        private void pnlMenu_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnVisits_Click(object sender, EventArgs e)
        {
            pnlVisits.Visible = !pnlVisits.Visible;

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            pnlSettings.Visible = !pnlSettings.Visible;

        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {



            OpenForm(new FrmAddPatient());

        }

        private void btnSearchPatient_Click(object sender, EventArgs e)
        {

            OpenForm(new FrmSearchPatient());

        }

        private void bnAddVisit_Click(object sender, EventArgs e)
        {

            OpenForm(new FrmAddVisit());

        }

        private void btnSearchVisit_Click(object sender, EventArgs e)
        {
            OpenForm(new FrmSearchVisit());

        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
