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
    public partial class FrmLabResult : Form
    {
        private LabContext db = new LabContext();
        private int visitLabId;

        public FrmLabResult()
        {
            InitializeComponent();
        }

        public FrmLabResult(int visitLabId)
        {
            InitializeComponent();
            //make text box info strat from right
            txtResultInfo.RightToLeft = RightToLeft.Yes;
            txtResultInfo.TextAlign = HorizontalAlignment.Right;

            this.visitLabId = visitLabId;

            LoadParameters();

            var visitLab = db.VisitLabs
                 .Include(vl => vl.Visit)
                 .ThenInclude(v => v.Patient)
                 .Include(vl => vl.Lab)
                 .FirstOrDefault(vl => vl.Id == visitLabId);

            if (visitLab == null)
            {
                MessageBox.Show("التحليل غير موجود");
                return;
            }

            txtResultInfo.RightToLeft = RightToLeft.Yes;
            txtResultInfo.TextAlign = HorizontalAlignment.Right;

            txtResultInfo.Text =
                $"تفاصيل الزيارة: " +
                $"التاريخ: {visitLab.Visit.VisitDate:dd/MM/yyyy} | " +
                $"الاسم: {visitLab.Visit.Patient.Name} | " +
                $"الكود: {visitLab.Visit.Patient.MedicalCode}";

        }
        private void FrmLabResult_Load(object sender, EventArgs e)
        {
            dgvResults.AutoGenerateColumns = false;
            dgvResults.RightToLeft = RightToLeft.Yes;


        }
        private void LoadParameters()
        {
            var visitLab = db.VisitLabs
                .Include(vl => vl.Lab)
                .FirstOrDefault(vl => vl.Id == visitLabId);

            if (visitLab == null)
            {
                MessageBox.Show("التحليل غير موجود");
                return;
            }

            var parameters = db.LabParameters
                .Where(p => p.LabId == visitLab.LabId)
                .OrderBy(p => p.DisplayOrder)
                .ToList();

            dgvResults.Rows.Clear();

            foreach (var parameter in parameters)
            {
                string normalRange = "";

                if (parameter.NormalRangeMin.HasValue &&
                    parameter.NormalRangeMax.HasValue)
                {
                    normalRange =
                        $"{parameter.NormalRangeMin} - {parameter.NormalRangeMax}";
                }
                else
                {
                    normalRange = parameter.NormalText ?? "";
                }

                dgvResults.Rows.Add(
                    parameter.Id,
                    parameter.Name,
                    parameter.Unit,
                    normalRange,
                    "",
                    ""
                );
            }
        }
        //private void LoadParameters()
        //{
        //    var visitLab = db.VisitLabs
        //        .Include(vl => vl.Lab)
        //        .FirstOrDefault(vl => vl.Id == visitLabId);

        //    if (visitLab == null)
        //    {
        //        MessageBox.Show("التحليل غير موجود");
        //        return;
        //    }

        //    var parameters = db.LabParameters
        //        .Where(p => p.LabId == visitLab.LabId)
        //        .OrderBy(p => p.DisplayOrder)
        //        .ToList();

        //    dgvResults.Rows.Clear();

        //    foreach (var parameter in parameters)
        //    {
        //        var oldResult = db.VisitLabResults
        //            .FirstOrDefault(r =>
        //                r.VisitLabId == visitLabId &&
        //                r.LabParameterId == parameter.Id);

        //        dgvResults.Rows.Add(
        //            parameter.Id,
        //            parameter.Name,
        //            parameter.Unit,
        //            GetNormalRange(parameter),
        //            oldResult?.ResultValue ?? "",
        //            oldResult?.Notes ?? ""
        //        );
        //    }
        //}

        private string GetNormalRange(LabParameter parameter)
        {
            if (parameter.IsNumeric)
            {
                if (parameter.NormalRangeMin.HasValue &&
                    parameter.NormalRangeMax.HasValue)
                {
                    return $"{parameter.NormalRangeMin}-{parameter.NormalRangeMax}";
                }
            }

            return parameter.NormalText ?? "";
        }

       private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvResults.Rows)
            {
                if (row.IsNewRow)
                    continue;

                int parameterId = Convert.ToInt32(
                    row.Cells["colParameterId"].Value
                );

                string resultValue =
                    row.Cells["colResult"].Value?.ToString() ?? "";

                string notes =
                    row.Cells["colNotes"].Value?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(resultValue))
                    continue;

                VisitLabResult result = new VisitLabResult
                {
                    VisitLabId = visitLabId,
                    LabParameterId = parameterId,
                    ResultValue = resultValue,
                    Notes = notes
                };

                db.VisitLabResults.Add(result);
            }

            db.SaveChanges();

            MessageBox.Show("تم حفظ نتائج التحليل بنجاح");
        }
    }
}
