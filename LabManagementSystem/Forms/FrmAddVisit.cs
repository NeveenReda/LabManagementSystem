using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabManagementSystem.Forms
{
    public partial class FrmAddVisit : Form
    {
        public event Action? AddPatientClicked;//declare an action

        public FrmAddVisit()
        {
            InitializeComponent();
        }

        private void pnlData_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmAddVisit_Load(object sender, EventArgs e)
        {
            //tableLayoutPanel1.SetColumn(txtPatientCode, 1);
            //tableLayoutPanel1.SetColumn(btnAddPatient, 2);
            //tableLayoutPanel1.SetColumn(label3, 0);
            tblAnalysis.SetColumnSpan(dgvVisitDetails, 3);
            txtPatientName.ReadOnly = true;
            txtTotal.ReadOnly = true;
            txtAfterDiscount.ReadOnly = true;
            txtRemaining.ReadOnly = true;
        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            AddPatientClicked?.Invoke();
        }

        private void tblAnalysis_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtPatientCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
