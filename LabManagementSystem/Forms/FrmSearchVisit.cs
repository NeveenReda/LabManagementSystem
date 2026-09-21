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
    public partial class FrmSearchVisit : Form
    {
        public event Action AddvisitClicked;
        LabContext db = new LabContext();


        public FrmSearchVisit()
        {
            InitializeComponent();
            dgvvisits.DefaultCellStyle.ForeColor = Color.Black;
            dgvvisits.DefaultCellStyle.BackColor = Color.White;

            dgvvisits.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvvisits.RowsDefaultCellStyle.BackColor = Color.White;

            dgvvisits.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvvisits.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

            dgvvisits.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvvisits.MultiSelect = true;
            dgvvisits.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;

        }

        private void FrmSearchVisit_Load(object sender, EventArgs e)
        {
            dgvvisits.AutoGenerateColumns = false;
            //for edit delete icons
            colEdit.Image = Image.FromFile("imgs/edit.png");
            colDelete.Image = Image.FromFile("imgs/delete.png");

            colReport.Image = Image.FromFile("imgs/report.png");
            colPayment.Image = Image.FromFile("imgs/payment-method.png");
            //////////////////
            chkSearchByDate.Checked = false;

            dtSearchDateTo.Enabled = false;
            dtSearchDateFrom.Enabled = false;
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            AddvisitClicked?.Invoke();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                string code = txtSearchCode.Text.Trim();
                string name = txtSearchName.Text.Trim();

                var query = db.Visits
                .Include(v => v.Patient)//patient is a navigation proerty
                .Where(v =>
                    (string.IsNullOrEmpty(code) || v.Patient.MedicalCode.Contains(code)) &&
                    (string.IsNullOrEmpty(name) || v.Patient.Name.Contains(name))
                );
             if (chkSearchByDate.Checked)
                {
                DateTime dateFrom = dtSearchDateFrom.Value.Date;
                DateTime dateTo = dtSearchDateTo.Value.Date;

                if (dateFrom > dateTo)
                {
                    MessageBox.Show("تاريخ البداية يجب أن يكون قبل تاريخ النهاية");
                    return;
                }

                DateTime dateToExclusive = dateTo.AddDays(1);

                query = query.Where(v =>
                    v.VisitDate >= dateFrom &&
                    v.VisitDate < dateToExclusive
                );


  
            }
            var visits = query.Where(v => !v.IsDeleted)

   .Select(v => new
   {
       v.Id,
       Name = v.Patient.Name,
       v.VisitDate,
       LabsCount = v.VisitLabs.Count,
       v.TotalPrice,
       v.DiscountValue,
       v.DiscountPercent,
       v.NetPrice
   })
   .OrderByDescending(v => v.VisitDate)
   .ToList();

            dgvvisits.DataSource = visits;

            dgvvisits.Columns["colVisitDate"]
                .DefaultCellStyle.Format = "dd/MM/yyyy";

            //var visits = query
            //.Select(v => new
            //{
            //    v.Id,

            //    // MedicalCode = v.Patient.MedicalCode,
            //    Name = v.Patient.Name,
            //  v.VisitDate,
            //    v.TotalPrice,
            //    //totola number of labs
            //    LabsCount = v.VisitLabs.Count,
            //    v.DiscountValue,
            //    v.DiscountPercent,
            //    v.NetPrice,
            // //   Phone = v.Patient.Phone,
            // //  Gender = v.Patient.Gender.Name,
            //  //  AgeAtRecord = v.Patient.AgeAtRecord,

            //}).OrderByDescending(v => v.VisitDate)
            //.ToList();



            // Results found
            dgvvisits.DataSource = visits;
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvvisits_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtSearchDateTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgvvisits_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int visitId = Convert.ToInt32(
                dgvvisits.Rows[e.RowIndex].Cells["colVisitId"].Value
            );

            if (dgvvisits.Columns[e.ColumnIndex].Name == "colEdit")
            {

                var visit = db.Visits
                .Include(v => v.Patient)
                .Include(v => v.Payments)
                .Include(v => v.VisitLabs)
                    .ThenInclude(vl => vl.Lab)
                .FirstOrDefault(v => v.Id == visitId);

                if (visit == null)
                {
                    MessageBox.Show("الزيارة غير موجودة");
                    return;
                }

                FrmAddVisit frm = new FrmAddVisit(visit);
                frm.ShowDialog();
            }

            if (dgvvisits.Columns[e.ColumnIndex].Name == "colDelete")
            {
                var visit = db.Visits.Find(visitId);

                if (visit == null)
                    return;

                visit.IsDeleted = true;

                db.SaveChanges();

                MessageBox.Show("تم حذف الزيارة بنجاح");

                // Refresh the grid
                button1_Click(null, null);
            }
            if (dgvvisits.Columns[e.ColumnIndex].Name == "colPayment")
            {
                var visit = db.Visits
                    .Include(v => v.Patient)
                    .Include(v => v.Payments)
                    .FirstOrDefault(v => v.Id == visitId);
                if (visit == null)
                    return;
                FrmPayments frm = new FrmPayments(visitId);
                frm.ShowDialog();
            }

        }
        //private void LoadVisits()
        //{
        //    dgvvisits.DataSource = db.Visits
        //        .Include(v => v.Patient)
        //        .ToList();
        //}
        private void chkSearchByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtSearchDateTo.Enabled = chkSearchByDate.Checked;
            dtSearchDateFrom.Enabled = chkSearchByDate.Checked;
        }
    }
}
