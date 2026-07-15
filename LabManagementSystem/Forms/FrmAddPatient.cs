using LabManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace LabManagementSystem.Forms
{
    public partial class FrmAddPatient : Form
    {
        LabContext db = new LabContext();
        public FrmAddPatient()
        {
            InitializeComponent();
        }

        private void FrmAddPatient_Load(object sender, EventArgs e)
        {
            //fill combo
            cmboGender.DataSource = db.Genders.ToList();
            cmboGender.DisplayMember = "Name";
            cmboGender.ValueMember = "Id";
            //-----------
            string today = DateTime.Today.ToString("yyyyMMdd");

            var lastPatient = db.Patients
                .Where(p => p.MedicalCode.StartsWith(today))
                .OrderByDescending(p => p.MedicalCode)
                .FirstOrDefault();

            string medicalCode;

            if (lastPatient == null)
            {
                medicalCode = today + "001";
            }
            else
            {
                int serial = int.Parse(lastPatient.MedicalCode.Substring(8));

                serial++;

                medicalCode = today + serial.ToString("D3");
            }
            txtPatientCode.Text = medicalCode;
            txtPatientCode.ReadOnly = true;

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // string name = txtPatientName.Text;
            // string phone = txtPatienPhone.Text;
            //// int genderId = (int)cmboGender.SelectedValue;
            // DateTime birthDate = DOB.Value;
            // DateOFRegister.Value = DateTime.Now;
            // DateOFRegister.Enabled = false;

            Patient patient = new Patient();

            patient.MedicalCode = txtPatientCode.Text;
            patient.Name = txtPatientName.Text;
            patient.Phone = txtPatienPhone.Text;
            patient.DateOfBirth = DOB.Value;
            patient.AgeAtRecord = int.Parse(txtPatientAgeOnRegister.Text);
            patient.Address = address.Text;

            patient.CreateDate = DateTime.Now;

            patient.GenderId = (int)cmboGender.SelectedValue;
            db.Patients.Add(patient);
            db.SaveChanges();
            MessageBox.Show("تم حفظ المريض بنجاح");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
