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
        private Patient? patient;
        LabContext db = new LabContext();
        public FrmAddPatient()
        {
            InitializeComponent();
            lblTitle.Text = "شاشة اضافة مريض";

        }

        public FrmAddPatient(Patient patient)
        {
            InitializeComponent();
            this.patient = patient;
            lblTitle.Text = "تعديل بيانات المريض";

            txtPatientName.Text = patient.Name;
            txtPatienPhone.Text = patient.Phone;
            txtPatientCode.Text = patient.MedicalCode;
            txtPatientAgeOnRegister.Text=patient.AgeAtRecord.ToString();

            DOB.Value = patient.DateOfBirth??DateTime.Today;
            address.Text = patient.Address;

            cmboGender.SelectedValue = patient.GenderId;

            // Fill the rest of the controls...
        }
        private void FrmAddPatient_Load(object sender, EventArgs e)
        {
            //fill combo
            cmboGender.DataSource = db.Genders.ToList();
            cmboGender.DisplayMember = "Name";
            cmboGender.ValueMember = "Id";
            ///////////////////////////////////////
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
            ////////////////////////////////////////////////

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txtPatientAgeOnRegister.Text, out int age))
            {
                int birthYear = DateTime.Today.Year - age;
                DOB.Value = new DateTime(birthYear, 1, 1);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (patient == null)
            {
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
            else
            {
                // Fill properties
                 var patientFromDb = db.Patients.Find(patient.Id);
                patientFromDb.MedicalCode = txtPatientCode.Text;
                patientFromDb.Name = txtPatientName.Text;
                patientFromDb.Phone = txtPatienPhone.Text;
                patientFromDb.DateOfBirth = DOB.Value;
                patientFromDb.AgeAtRecord = int.Parse(txtPatientAgeOnRegister.Text);
                patientFromDb.Address = address.Text;
                patientFromDb.GenderId = (int)cmboGender.SelectedValue;
                db.SaveChanges();
                MessageBox.Show("تم تعديل المريض بنجاح");

            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void address_TextChanged(object sender, EventArgs e)
        {

        }

        private void pnlData_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DOB_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
