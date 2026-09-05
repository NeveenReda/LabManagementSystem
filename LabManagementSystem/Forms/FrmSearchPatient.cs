using LabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace LabManagementSystem.Forms
{
    public partial class FrmSearchPatient : Form
    {
        public event Action? AddPatientClicked;
        LabContext db = new LabContext();
        public FrmSearchPatient()
        {
            InitializeComponent();
            dgvPatients.DefaultCellStyle.ForeColor = Color.Black;
            dgvPatients.DefaultCellStyle.BackColor = Color.White;

            dgvPatients.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvPatients.RowsDefaultCellStyle.BackColor = Color.White;

            dgvPatients.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvPatients.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

            dgvPatients.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvPatients.MultiSelect = true;
            dgvPatients.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;


        }
        private void SearchPatients()
        {
            string search = txtSearch.Text.Trim();

            var patients = db.Patients
                .Include(p => p.Gender)
                .Where(p => p.Name.Contains(search) || p.MedicalCode.Contains(search))
                .Select(p => new
                {
                    p.Id,
                    p.MedicalCode,
                    p.Name,
                    p.Phone,
                    Gender = p.Gender.Name,
                    p.AgeAtRecord
                })
                .ToList();

            dgvPatients.DataSource = patients;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddPatientClicked?.Invoke();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dgvPatients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();

            if (string.IsNullOrWhiteSpace(search))
            {
                dgvPatients.DataSource = null;
                return;
            }
            var patients = db.Patients
                .Include(p => p.Gender)
                .Where(p => p.Name.Contains(search) || p.MedicalCode.Contains(search))
                .Select(p => new
                {
                    p.Id,
                    p.MedicalCode,
                    p.Name,
                    p.Phone,
                    Gender = p.Gender.Name,
                    p.AgeAtRecord
                })
                .ToList();

            dgvPatients.DataSource = patients;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmSearchPatient_Load(object sender, EventArgs e)
        {
            dgvPatients.AutoGenerateColumns = false;


            /////////////////////
            //for edit delete icons
            colEdit.Image = Image.FromFile("imgs/edit.png");
            colDelete.Image = Image.FromFile("imgs/delete.png");
        }

        private void pnlTitle_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvPatients_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }

        private void dgvPatients_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int patientId = Convert.ToInt32(dgvPatients.Rows[e.RowIndex].Cells["colId"].Value);

            Patient patient = db.Patients.Find(patientId);

            if (e.RowIndex < 0) return;
            if (dgvPatients.Columns[e.ColumnIndex].Name == "colEdit")
            {
                //MessageBox.Show("Edit is clicked");

                FrmAddPatient frm = new FrmAddPatient(patient);
                frm.ShowDialog();
            }
            if (dgvPatients.Columns[e.ColumnIndex].Name == "colDelete")
            {
                db.Patients.Remove(patient);
                db.SaveChanges();
                MessageBox.Show("تم حذف المريض بنجاح");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SearchPatients();
        }
    }
}
