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
    public partial class FrmPayments : Form
    {
        private LabContext db = new LabContext();
        private List<decimal> newPayments = new List<decimal>();

        private int visitId;
        private Visit? visit;

        public FrmPayments()
        {
            InitializeComponent();
        }
        public FrmPayments(int visitId)
        {
            InitializeComponent();
            this.visitId = visitId;
            LoadVisit();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadVisit()
        {
            visit = db.Visits
                .Include(v => v.Patient)
                .Include(v => v.VisitLabs)
                .Include(v => v.Payments)
                .FirstOrDefault(v => v.Id == visitId);

            if (visit == null)
            {
                MessageBox.Show("الزيارة غير موجودة");
                Close();
                return;
            }

            txtVisitCode.Text = visit.Id.ToString();

            txtPatientName.Text = visit.Patient.Name;

            txtLabsCount.Text = visit.VisitLabs.Count.ToString();

            txtTotal.Text = visit.TotalPrice.ToString("0.00");

            txtDiscount.Text = visit.DiscountValue?.ToString("0.00") ?? "0.00";

            textAfterDiscount.Text = visit.NetPrice.ToString("0.00");

            decimal totalPaid = visit.Payments.Sum(p => p.Amount);

            txtPaid.Text = totalPaid.ToString("0.00");

            txtRemaining.Text =
                (visit.NetPrice - totalPaid).ToString("0.00");

            dtPaymentDate.Value = DateTime.Now;

            LoadPayments();
        }

        private void FrmPayments_Load(object sender, EventArgs e)
        {
            txtVisitCode.ReadOnly = true;
            txtPatientName.ReadOnly = true;
            txtLabsCount.ReadOnly = true;
            txtTotal.ReadOnly = true;
            txtDiscount.ReadOnly = true;
            textAfterDiscount.ReadOnly = true;
            txtRemaining.ReadOnly = true;
        }

        private void LoadPayments()//load old payment
        {
            dgvPayments.Rows.Clear();

            foreach (var payment in visit!.Payments.OrderBy(p => p.PaymentDate))
            {
                dgvPayments.Rows.Add(
                    payment.PaymentDate.ToString("dd/MM/yyyy"),
                    payment.Amount.ToString("0.00"),
                    payment.Notes
                );
            }
        }

      

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (visit == null)
                return;

            foreach (decimal amount in newPayments)
            {
                Payment payment = new Payment
                {
                    PaymentDate = DateTime.Now,
                    Amount = amount,
                    VisitId = visit.Id
                };

                db.Payments.Add(payment);
            }

            db.SaveChanges();

            MessageBox.Show("تم حفظ الدفع بنجاح");

            newPayments.Clear();

            LoadVisit();
        }

        private void btnAddLab_Click(object sender, EventArgs e)
        {

            if (!decimal.TryParse(txtNewPayment.Text, out decimal amount))
            {
                MessageBox.Show("من فضلك أدخل مبلغ صحيح");
                return;
            }

            if (amount <= 0)
            {
                MessageBox.Show("المبلغ يجب أن يكون أكبر من صفر");
                return;
            }

            decimal remaining = decimal.Parse(txtRemaining.Text);

            if (amount > remaining)
            {
                MessageBox.Show("المبلغ أكبر من المبلغ المتبقي");
                return;
            }

            dgvPayments.Rows.Add(
                dtPaymentDate.Value.ToString("dd/MM/yyyy"),
                amount.ToString("0.00"),
                ""
            );
            txtNewPayment.Clear();

            // تحديث المدفوع والمتبقي
            newPayments.Add(amount);

            decimal currentPaid = decimal.Parse(txtPaid.Text);

            currentPaid += amount;

            txtPaid.Text = currentPaid.ToString("0.00");

            txtRemaining.Text =
                (visit!.NetPrice - currentPaid).ToString("0.00");

          //  txtPaid.Clear();
        }
    }
}
