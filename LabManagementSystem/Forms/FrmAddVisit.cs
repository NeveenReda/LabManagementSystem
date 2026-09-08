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

namespace LabManagementSystem.Forms
{
    public partial class FrmAddVisit : Form
    {
        public event Action? AddPatientClicked;//declare an action
        LabContext db = new LabContext();
        Visit? visit;
        Patient? selectedPatient;
        Lab? selectedLab;
        public FrmAddVisit()
        {
            InitializeComponent();
            lbltxt.Text = "شاشة اضافة زيارة";

            //dgvVisitDetails.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //dgvVisitDetails.MultiSelect = true;
            //dgvVisitDetails.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
        }
        public FrmAddVisit(Visit visit)//this constructor for update visit
        {
            InitializeComponent();
            ////////////
            this.visit = visit;
            txtNotes.Text = this.visit.Notes;
            txtDoctorName.Text = this.visit.DrName;
            txtPatientCode.Text = this.visit.Patient.MedicalCode;
            txtPayment.Text = this.visit.Payments;

          

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

            cmbolabs.DataSource = db.Labs.ToList();
            cmbolabs.DisplayMember = "Name";
            cmbolabs.ValueMember = "Id";


            dgvVisitDetails.AutoGenerateColumns = false;

            colDelete.Image = Image.FromFile("imgs/delete.png");

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

        private void txtAfterDiscount_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbltxt_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(selectedPatient==null)
            {
                MessageBox.Show("من فضلك اختار المريض اولا"); return;
            }
            if(dgvVisitDetails.Rows.Count==0)
            {
                MessageBox.Show("من فضلك ادخل تحليل واحد على الاقل"); return;
            }
            Visit vis = new Visit();
            vis.PatientId=selectedPatient.Id;
            vis.VisitDate = DateTime.Now;
            vis.CreateDate= DateTime.Now;


            vis.DrName = txtDoctorName.Text;
            vis.Notes = txtNotes.Text;

            vis.TotalPrice= Convert.ToDecimal(txtTotal.Text);
            if (decimal.TryParse(textDiscountPercentage.Text, out decimal discountPercentage))
                visit.DiscountPercent = discountPercentage;

            if (decimal.TryParse(textDiscountValue.Text, out decimal discountValue))
                vis.DiscountValue = discountValue;

            vis.NetPrice = decimal.Parse(txtAfterDiscount.Text);

            db.Visits.Add(vis);

            db.SaveChanges();

            MessageBox.Show("تم حفظ الزيارة بنجاح");

            foreach (DataGridViewRow row in dgvVisitDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                VisitLab visitLab = new VisitLab();

                visitLab.VisitId = vis.Id;
                visitLab.LabId = Convert.ToInt32(row.Cells["colId"].Value);
                visitLab.LabPrice = Convert.ToDecimal(row.Cells["colPrice"].Value);
                visitLab.IsExternal = Convert.ToBoolean(row.Cells["colExternal"].Value);

                // You need a valid VisitLabStatusId
                visitLab.VisitLabStatusId = 1;

                vis.VisitLabs.Add(visitLab);
            }

        }

        private void txtPatientCode_Leave(object sender, EventArgs e)
        {
            string code = txtPatientCode.Text.Trim();
            if (string.IsNullOrEmpty(code))
            {
                selectedPatient = null;
                txtPatientName.Clear();
                return;
            }
            selectedPatient = db.Patients.FirstOrDefault(x => x.MedicalCode == code);
            if (selectedPatient == null)
            {
                MessageBox.Show("المريض غير موجود");
                txtPatientName.Clear();
                return;
            }
            txtPatientName.Text = selectedPatient.Name;
        }

        private void cmbolabs_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbolabs.SelectedItem is not Lab lab)
                return;
            selectedLab = lab;

        }

        private void dgvVisitDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int labId = Convert.ToInt32(dgvVisitDetails.Rows[e.RowIndex].Cells["colId"].Value);

            //Lab lab = db.Labs.Find(labId);
            //if (dgvVisitDetails.Columns[e.ColumnIndex].Name == "colDelete")
            //{
            //    db.Labs.Remove(lab);
            //    db.SaveChanges();
            //    MessageBox.Show("تم حذف التحليل بنجاح");
            //}
            if (e.RowIndex < 0)
                return;

            if (dgvVisitDetails.Columns[e.ColumnIndex].Name == "colDelete")
            {
                dgvVisitDetails.Rows.RemoveAt(e.RowIndex);

                CalculateTotal();
                CalculateAfterDiscount();
            }
        }

        private void btnAddLab_Click(object sender, EventArgs e)
        {
            if (selectedLab == null)
            {
                MessageBox.Show("  من فضلك اختار التحليل اولا");
                return;
            }
            foreach (DataGridViewRow row in dgvVisitDetails.Rows)
            {
                if (row.IsNewRow) continue;
                if (Convert.ToInt32(row.Cells["colId"].Value) == selectedLab.Id)
                {
                    MessageBox.Show("هذا التحليل تمت اضافته بالفعل ");
                    return;
                }
            }
            dgvVisitDetails.Rows.Add(selectedLab.Id, selectedLab.Name, selectedLab.Price, selectedLab.IsExternal);
            CalculateTotal();
            CalculateAfterDiscount();

        }

        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvVisitDetails.Rows)
            {
                if (row.IsNewRow) continue;
                total += Convert.ToDecimal(row.Cells["colPrice"].Value);

            }
            txtTotal.Text = total.ToString();
        }

        private void CalculateAfterDiscount()
        {
            decimal total = 0;
            decimal discountValue = 0;
            decimal discountPercentage = 0;
            decimal paid = 0;

            decimal.TryParse(txtTotal.Text, out total);
            decimal.TryParse(textDiscountPercentage.Text, out discountPercentage);
            decimal.TryParse(textDiscountValue.Text, out discountValue);
            decimal.TryParse(txtPayment.Text, out paid);

            if (discountPercentage > 0)
            {
                discountValue = total * discountPercentage / 100;
                textDiscountValue.Text = discountValue.ToString("0.00");
            }
            decimal afetrDiscount = total - discountValue;
            txtAfterDiscount.Text = afetrDiscount.ToString("0.00");
            decimal remain = afetrDiscount - paid;
            txtRemaining.Text = remain.ToString("0.00");

        }

        private void textDiscountValue_TextChanged(object sender, EventArgs e)
        {
            CalculateAfterDiscount();
        }

        private void textDiscountPercentage_TextChanged(object sender, EventArgs e)
        {
            CalculateAfterDiscount();
        }

        private void txtPayment_TextChanged(object sender, EventArgs e)
        {
            CalculateAfterDiscount();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}


