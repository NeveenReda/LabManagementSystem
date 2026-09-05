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

        public FrmAddPatient(Patient patient)//this constructor for update patient
        {
            InitializeComponent();
            ////////////////////////////////
            this.patient = patient;
            lblTitle.Text = "تعديل بيانات المريض";
            LoadGender();
            txtPatientName.Text = this.patient.Name;
            txtPatienPhone.Text = this.patient.Phone;
            txtPatientCode.Text = this.patient.MedicalCode;
            txtPatientAgeOnRegister.Text=this.patient.AgeAtRecord.ToString();
            DOB.Value = this.patient.DateOfBirth??DateTime.Today;
            address.Text = this.patient.Address;
            cmboGender.SelectedValue = this.patient.GenderId;

        }
        private void FrmAddPatient_Load(object sender, EventArgs e)
        {
            //fill combo

            LoadGender();
            // If editing a patient
            if (patient != null)
            {
                cmboGender.SelectedValue = patient.GenderId;
            }


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
                patient = new Patient();

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
                this.patient = patientFromDb;
               this.patient.MedicalCode = txtPatientCode.Text;
                this.patient.Name = txtPatientName.Text;
                this.patient.Phone = txtPatienPhone.Text;
                this.patient.DateOfBirth = DOB.Value;
                this.patient.AgeAtRecord = int.Parse(txtPatientAgeOnRegister.Text);
                this.patient.Address = address.Text;
                this.patient.GenderId = (int)cmboGender.SelectedValue;
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

        private void LoadGender()
        {
            cmboGender.DataSource=db.Genders.ToList();
            cmboGender.DisplayMember= "Name";
            cmboGender.ValueMember="Id";
        }
    }
}
