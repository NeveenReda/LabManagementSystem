using LabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabManagementSystem.Forms
{
    public partial class FrmExternalLab : Form
    {
        private int visitLabId;
        private VisitLab? visitLab;
        private LabContext db = new LabContext();

        public FrmExternalLab()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }
        public FrmExternalLab(int visitLabId)
        {
            InitializeComponent();
            //make text box starts from 
            txtPatientInfo.RightToLeft = RightToLeft.Yes;
            txtPatientInfo.TextAlign = HorizontalAlignment.Right;

            this.visitLabId = visitLabId;

            visitLab = db.VisitLabs
                .Include(vl => vl.Visit)
                .ThenInclude(v => v.Patient)
                .Include(vl => vl.Lab)
                .FirstOrDefault(vl => vl.Id == visitLabId);

            if (visitLab == null)
            {
                MessageBox.Show("التحليل غير موجود");
                return;
            }

            txtPatientInfo.RightToLeft = RightToLeft.Yes;
            txtPatientInfo.TextAlign = HorizontalAlignment.Right;

            txtPatientInfo.Text =
                $"تفاصيل الزيارة " +
                $"التاريخ: {visitLab.Visit.VisitDate:dd/MM/yyyy} | " +
                $"الاسم: {visitLab.Visit.Patient.Name} | " +
                $"الكود: {visitLab.Visit.Patient.MedicalCode}";
             
         

            // Get ExpectedDays from Lab
            txtExpextedDays.Text =
                visitLab.Lab.Days.ToString();

            //recieve ddate
            dtpDateReceive.Value = dtpDateSend.Value.AddDays(visitLab.Lab.Days);
          
        }
        private void FrmExternalLab_Load(object sender, EventArgs e)
        {
            LoadExternalLab();
            dtpDateReceive.Enabled = false;
            // Make ExpectedDays read-only
            txtExpextedDays.ReadOnly = true;

            

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void LoadExternalLab()
        {
            cmbExternalLab.DataSource = db.BaseExternalLabs.ToList();
            cmbExternalLab.DisplayMember = "Name";
            cmbExternalLab.ValueMember = "Id";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbExternalLab.SelectedValue == null)
            {
                MessageBox.Show("من فضلك اختر المعمل الخارجي");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtContactPerson.Text))
            {
                MessageBox.Show("من فضلك أدخل اسم المسؤول");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("من فضلك أدخل رقم الهاتف");
                return;
            }

            if (!int.TryParse(txtExpextedDays.Text, out int expectedDays))
            {
                MessageBox.Show("من فضلك أدخل عدد الأيام المتوقعه بشكل صحيح");
                return;
            }

            ExternalLab externalLab = new ExternalLab
            {
                VistLabId = visitLabId,

                BaseExternalLabId =
                    Convert.ToInt32(cmbExternalLab.SelectedValue),

                ContactPerson = txtContactPerson.Text,

                Phone = txtPhone.Text,

                DateTimeSend = dtpDateSend.Value,

                //ExpectedDays = expectedDays,
                

                DateTimeReceive = dtpDateReceive.Value
            };

            db.ExternalLabs.Add(externalLab);

            // Mark this VisitLab as transferred to external lab
            visitLab.IsExternal = true;

            db.SaveChanges();

            MessageBox.Show("تم تحويل التحليل للمعمل الخارجي بنجاح");

            this.Close();
        }
    }
}
