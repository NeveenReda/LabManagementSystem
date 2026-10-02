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
        public FrmAddVisit(Visit visit)
        {
            InitializeComponent();

            lbltxt.Text = "تعديل زيارة";

            int visitId = visit.Id;
            //get this.visit from db
            this.visit = db.Visits
                .Include(v => v.Patient)
                .Include(v => v.Payments)
                .Include(v => v.VisitLabs)
                    .ThenInclude(vl => vl.Lab)
                .Include(v => v.VisitLabs)
                    .ThenInclude(vl => vl.VisitLabResults)
                .Include(v => v.VisitLabs)
                    .ThenInclude(vl => vl.ExternalLabs)
                .FirstOrDefault(v => v.Id == visitId);
            if (this.visit == null)
            {
                MessageBox.Show("الزيارة غير موجودة");
                Close();
                return;
            }
            //fill controls with data from db
            selectedPatient =this. visit.Patient;
            DateOfVisit.Value = this.visit.VisitDate;

            txtNotes.Text = this.visit.Notes;
            txtDoctorName.Text = this.visit.DrName;

            txtPatientCode.Text = this.visit.Patient.MedicalCode;
            txtPatientName.Text = this.visit.Patient.Name;

            txtPayment.Text = this.visit.Payments
                .Sum(p => p.Amount)
                .ToString("0.00");
            textDiscountValue.Text = this.visit.DiscountValue.ToString();
            textDiscountPercentage.Text=this.visit.DiscountPercent.ToString();  


            LoadVisitLabs();
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

            //////////////////
            ////////////grid font size
            dgvVisitDetails.DefaultCellStyle.Font = new Font("Arial", 9);

            dgvVisitDetails.ColumnHeadersDefaultCellStyle.Font =
                new Font("Arial", 12, FontStyle.Bold);
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
            // =====================================================
            // 1. التأكد من وجود المريض
            // =====================================================

            if (selectedPatient == null)
            {
                MessageBox.Show("من فضلك اختار المريض أولا");
                return;
            }

            // =====================================================
            // 2. التأكد من وجود تحليل واحد على الأقل
            // =====================================================

            if (dgvVisitDetails.Rows.Count == 0)
            {
                MessageBox.Show("من فضلك ادخل تحليل واحد على الاقل");
                return;
            }


            // =====================================================
            // =====================================================
            //              ADD NEW VISIT
            // =====================================================
            // =====================================================

            if (visit == null)
            {
                // =========================
                // Create Visit
                // =========================

                Visit vis = new Visit();

                vis.PatientId = selectedPatient.Id;
                vis.VisitDate = DateTime.Now;
                vis.CreateDate = DateTime.Now;

                vis.DrName = txtDoctorName.Text;
                vis.Notes = txtNotes.Text;

                vis.TotalPrice = Convert.ToDecimal(txtTotal.Text);

                if (decimal.TryParse(
                    textDiscountPercentage.Text,
                    out decimal discountPercentage))
                {
                    vis.DiscountPercent = discountPercentage;
                }

                if (decimal.TryParse(
                    textDiscountValue.Text,
                    out decimal discountValue))
                {
                    vis.DiscountValue = discountValue;
                }

                vis.NetPrice = decimal.Parse(txtAfterDiscount.Text);

                db.Visits.Add(vis);

                // Save Visit first to get Visit.Id
                db.SaveChanges();


                // =========================
                // Create VisitLabs
                // =========================

                foreach (DataGridViewRow row in dgvVisitDetails.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    VisitLab visitLab = new VisitLab();

                    visitLab.VisitId = vis.Id;

                    visitLab.LabId =
                        Convert.ToInt32(row.Cells["colId"].Value);

                    visitLab.LabPrice =
                        Convert.ToDecimal(row.Cells["colPrice"].Value);

                    visitLab.IsExternal =false;
                       // Convert.ToBoolean(row.Cells["colExternal"].Value);

                    // استخدمي هنا ID صحيح من VisitLabStatus
                    visitLab.VisitLabStatusId = 1;

                    db.VisitLabs.Add(visitLab);

                    row.Tag = visitLab;
                }


                // Save VisitLabs
                db.SaveChanges();


                // =========================
                // Put VisitLabId in Grid
                // =========================

                foreach (DataGridViewRow row in dgvVisitDetails.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    VisitLab visitLab = row.Tag as VisitLab;

                    if (visitLab != null)
                    {
                        row.Cells["colVisitLabId"].Value =
                            visitLab.Id;
                    }
                }


                // =========================
                // Add Payment ONCE
                // =========================

                if (decimal.TryParse(
                    txtPayment.Text,
                    out decimal paymentAmount)
                    && paymentAmount > 0)
                {
                    Payment payment = new Payment
                    {
                        PaymentDate = DateTime.Now,
                        Amount = paymentAmount,
                        VisitId = vis.Id
                    };

                    db.Payments.Add(payment);
                }


                db.SaveChanges();

                MessageBox.Show("تم حفظ الزيارة بنجاح");

                return;
            }


            // =====================================================
            // =====================================================
            //              UPDATE EXISTING VISIT
            // =====================================================
            // =====================================================


            // =========================
            // 3. Update Visit information
            // =========================

            visit.DrName = txtDoctorName.Text;
            visit.Notes = txtNotes.Text;
           // visit.VisitDate = DateOfVisit.Value;

            visit.TotalPrice =
                Convert.ToDecimal(txtTotal.Text);

            if (decimal.TryParse(
                textDiscountPercentage.Text,
                out decimal discountPercentageUpdate))
            {
                visit.DiscountPercent =
                    discountPercentageUpdate;
            }
            else
            {
                visit.DiscountPercent = null;
            }

            if (decimal.TryParse(
                textDiscountValue.Text,
                out decimal discountValueUpdate))
            {
                visit.DiscountValue =
                    discountValueUpdate;
            }
            else
            {
                visit.DiscountValue = null;
            }

            visit.NetPrice =
                decimal.Parse(txtAfterDiscount.Text);


            // =====================================================
            // 4. Get Lab IDs currently موجودة في Grid
            // =====================================================

            HashSet<int> currentLabIds = new HashSet<int>();

            foreach (DataGridViewRow row in dgvVisitDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                int labId =
                    Convert.ToInt32(row.Cells["colId"].Value);

                currentLabIds.Add(labId);
            }


            // =====================================================
            // 5. Add NEW VisitLabs
            // =====================================================

            foreach (DataGridViewRow row in dgvVisitDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                int labId =
                    Convert.ToInt32(row.Cells["colId"].Value);


                // هل التحليل موجود بالفعل في الزيارة؟
                VisitLab? existingVisitLab =
                    visit.VisitLabs
                        .FirstOrDefault(vl => vl.LabId == labId);


                if (existingVisitLab != null)
                {
                    // التحليل موجود بالفعل
                    // لا ننشئ VisitLab جديد
                    row.Cells["colVisitLabId"].Value =
                        existingVisitLab.Id;

                    continue;
                }


                // =========================
                // التحليل جديد
                // =========================

                VisitLab newVisitLab = new VisitLab();

                newVisitLab.VisitId = visit.Id;

                newVisitLab.LabId = labId;

                newVisitLab.LabPrice =
                    Convert.ToDecimal(
                        row.Cells["colPrice"].Value);

                newVisitLab.IsExternal = false;
                   

                // استخدمي ID صحيح من VisitLabStatus
                newVisitLab.VisitLabStatusId = 1;

                db.VisitLabs.Add(newVisitLab);

                // بعد SaveChanges سيأخذ Id
                row.Tag = newVisitLab;
            }


            // نحفظ VisitLabs الجديدة
            db.SaveChanges();


            // =====================================================
            // 6. Put VisitLabId for NEW VisitLabs
            // =====================================================

            foreach (DataGridViewRow row in dgvVisitDetails.Rows)
            {
                if (row.IsNewRow)
                    continue;

                VisitLab? visitLab =
                    row.Tag as VisitLab;

                if (visitLab != null)
                {
                    row.Cells["colVisitLabId"].Value =
                        visitLab.Id;
                }
            }


            // =====================================================
            // 7. Delete VisitLabs التي تم حذفها من Grid
            // =====================================================

            var oldVisitLabs =
                visit.VisitLabs.ToList();

            foreach (VisitLab oldVisitLab in oldVisitLabs)
            {
                // ما زال موجودًا في Grid
                if (currentLabIds.Contains(oldVisitLab.LabId))
                    continue;


                // ==============================================
                // التحليل تم حذفه من Grid
                // ==============================================

                bool hasResults =
                    oldVisitLab.VisitLabResults.Any();

                bool hasExternalLab =
                    oldVisitLab.ExternalLabs.Any();


                // لا نحذف VisitLab إذا كان له Results
                // أو بيانات معمل خارجي
                if (hasResults || hasExternalLab)
                {
                    MessageBox.Show(
                        "لا يمكن حذف أحد التحاليل لأنه يحتوي على نتائج " +
                        "أو بيانات معمل خارجي.\n\n" +
                        "إذا أردت حذفه يجب التعامل مع هذه البيانات أولا.",
                        "تنبيه",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }


                db.VisitLabs.Remove(oldVisitLab);
            }


            // =====================================================
            // 8. لا نعدل Payments القديمة
            // =====================================================
            //
            // Payments يتم التعامل معها من FrmPayment
            //
            // لذلك لا يوجد هنا:
            //
            // db.Payments.Remove(...)
            //
            // ولا ننشئ Payment جديدة عند تعديل الزيارة.
            // =====================================================


            // =====================================================
            // 9. Save everything
            // =====================================================

            db.SaveChanges();


            MessageBox.Show("تم تعديل الزيارة بنجاح");

            this.Close();
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
            // Check if clicked column is the external lab button
            if (e.ColumnIndex == dgvVisitDetails.Columns["colExternal"].Index)
            {
                // Get VisitLabId from hidden column
                if (dgvVisitDetails.Rows[e.RowIndex].Cells["colVisitLabId"].Value == null)
                {
                    MessageBox.Show("من فضلك احفظ الزيارة أولاً");
                    return;
                }

                int visitLabId = Convert.ToInt32(
                    dgvVisitDetails.Rows[e.RowIndex].Cells["colVisitLabId"].Value
                );

                // Open External Lab form
                FrmExternalLab frm = new FrmExternalLab(visitLabId);

                frm.ShowDialog();
            }

            ///for lab result
            //if (dgvVisitDetails.Columns[e.ColumnIndex].Name == "colResult")
            //{
            //    int visitLabId = Convert.ToInt32(
            //        dgvVisitDetails.Rows[e.RowIndex]
            //            .Cells["colVisitLabId"].Value
            //    );

            //    FrmLabResult frm = new FrmLabResult(visitLabId);
            //    frm.ShowDialog();
            //}
            if (dgvVisitDetails.Columns[e.ColumnIndex].Name == "colResult")
            {
                object value = dgvVisitDetails.Rows[e.RowIndex]
                    .Cells["colVisitLabId"].Value;

                if (value == null || value == DBNull.Value)
                {
                    MessageBox.Show("احفظ الزيارة أولاً");
                    return;
                }

                int visitLabId = Convert.ToInt32(value);

                FrmLabResult frm = new FrmLabResult(visitLabId);
                frm.ShowDialog();
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
            dgvVisitDetails.Rows.Add(selectedLab.Id, selectedLab.Name, selectedLab.Price, selectedLab.IsExternal,null,null,null,null);
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

        //private void LoadVisitLabs()
        //{
        //    dgvVisitDetails.Rows.Clear();

        //    foreach (var visitLab in visit!.VisitLabs)
        //    {
        //        dgvVisitDetails.Rows.Add(
        //            visitLab.LabId,
        //            visitLab.Lab.Name,
        //            visitLab.LabPrice,
        //            visitLab.IsExternal
        //        );
        //    }

        //    CalculateTotal();
        //    CalculateAfterDiscount();
        //}

        private void LoadVisitLabs()
        {
            dgvVisitDetails.Rows.Clear();

            foreach (var visitLab in visit!.VisitLabs)
            {
                int rowIndex = dgvVisitDetails.Rows.Add(
                    visitLab.LabId,
                    visitLab.Lab.Name,
                    visitLab.LabPrice,
                    visitLab.IsExternal
                );

                dgvVisitDetails.Rows[rowIndex]
                    .Cells["colVisitLabId"]
                    .Value = visitLab.Id;
            }

            CalculateTotal();
            CalculateAfterDiscount();
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}


