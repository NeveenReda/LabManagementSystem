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

        }

        private void btnAddPatient_Click(object sender, EventArgs e)
        {
            AddPatientClicked?.Invoke();
        }
    }
}
