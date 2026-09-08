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
            colEdit.Image = Image.FromFile("imgs/edit.png");
            colDelete.Image = Image.FromFile("imgs/delete.png");
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
                .Include(v => v.Patient)
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
            var visits = query
            .Select(v => new
            {
                v.Id,
                MedicalCode = v.Patient.MedicalCode,
                Name = v.Patient.Name,
                Phone = v.Patient.Phone,
                Gender = v.Patient.Gender.Name,
                AgeAtRecord = v.Patient.AgeAtRecord,
                v.VisitDate
            })
            .ToList();

            

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

        }

        private void chkSearchByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtSearchDateTo.Enabled = chkSearchByDate.Checked;
            dtSearchDateFrom.Enabled = chkSearchByDate.Checked;
        }
    }
}
