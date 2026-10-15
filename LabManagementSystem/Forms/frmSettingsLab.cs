using LabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabManagementSystem.Forms
{
    public partial class frmSettingsLab : Form
    {

        LabContext db = new LabContext();
        bool editingMode = false;
        private Lab? thelab;

        public frmSettingsLab()
        {
            InitializeComponent();

            dgvLabs.DefaultCellStyle.ForeColor = Color.Black;
            dgvLabs.DefaultCellStyle.BackColor = Color.White;

            dgvLabs.RowsDefaultCellStyle.ForeColor = Color.Black;
            dgvLabs.RowsDefaultCellStyle.BackColor = Color.White;

            dgvLabs.AlternatingRowsDefaultCellStyle.ForeColor = Color.Black;
            dgvLabs.AlternatingRowsDefaultCellStyle.BackColor = Color.White;

            dgvLabs.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvLabs.MultiSelect = true;
            dgvLabs.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;

            //        var Labs = db.Labs.AsNoTracking()
            //            .Select(l => new
            //            {
            //                l.Id,
            //                l.Name,
            //                l.Days,
            //                l.Price
            //            })
            //.ToList();





            var labCtgrs = db.LabCategorys.AsNoTracking()
                .Select(lc => new 
                {
                    lc.Id,
                    lc.Name
                })
                .ToList();

            cmbLabCat.DataSource = labCtgrs;
            cmbLabCat.DisplayMember = "Name";
            cmbLabCat.ValueMember = "Id";
            cmbLabCat.SelectedIndex = -1  ;

            fresh();

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (!editingMode)
            {
                Lab newLab = new Lab();

                newLab.Name = txtName.Text;
                newLab.Price = decimal.Parse(txtPrice.Text);
                newLab.Days = int.Parse(txtDays.Text);
                newLab.LabCategoryId = (int)cmbLabCat.SelectedValue;
                newLab.IsExternal = chkExternal.Checked;
                db.Labs.Add(newLab);
                db.SaveChanges();
                MessageBox.Show("تم حفظ التحليل بنجاح");
                fresh();
            }
            else
            {
                // Fill properties
                thelab.Name = txtName.Text;
                thelab.Price = decimal.Parse(txtPrice.Text);
                thelab.Days = int.Parse(txtDays.Text);
                thelab.LabCategoryId = (int)cmbLabCat.SelectedValue;
                thelab.IsExternal = chkExternal.Checked;

                db.SaveChanges();
                MessageBox.Show("تم تعديل بيانات التحليل بنجاح");
                fresh();

            }
        }


        private void fresh()
        {
            editingMode = false;
            thelab = null;
            var Labs = db.Labs.AsNoTracking()
.Select(l => new
{
    l.Id,
    l.Name,
    l.Days,
    l.Price,
    l.LabCategoryId,
    CategoryName = l.LabCategory.Name // بتجيب الاسم علطول من العلاقة
})
.ToList();

            dgvLabs.DataSource = Labs;


            // تفريغ النص من الحقول لتجهيزها للإدخال القادم
            txtName.Clear();
            txtPrice.Clear();
            txtDays.Clear();

            cmbLabCat.SelectedIndex = -1;
        }

        private void frmSettingsLab_Load(object sender, EventArgs e)
        {

        }

        

        private void dgvLabs_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvLabs.Columns[e.ColumnIndex].Name == "colEdit")
            {
                int labId = Convert.ToInt32(dgvLabs.Rows[e.RowIndex].Cells["Id"].Value);

                thelab = (Lab)db.Labs.Where(l => l.Id == labId).First();

                txtName.Text = thelab.Name;
                txtPrice.Text = thelab.Price.ToString();
                txtDays.Text = thelab.Days.ToString();

                cmbLabCat.SelectedValue = thelab.LabCategoryId;

                editingMode = true;
            }
        }
    }
}
