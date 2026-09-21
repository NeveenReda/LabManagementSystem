namespace LabManagementSystem.Forms
{
    partial class FrmPayments
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            textAfterDiscount = new TextBox();
            txtPaid = new TextBox();
            txtRemaining = new TextBox();
            label7 = new Label();
            label10 = new Label();
            txtDiscount = new TextBox();
            label5 = new Label();
            txtTotal = new TextBox();
            label9 = new Label();
            label3 = new Label();
            label4 = new Label();
            txtLabsCount = new TextBox();
            label8 = new Label();
            label6 = new Label();
            dtPaymentDate = new DateTimePicker();
            txtVisitCode = new TextBox();
            txtPatientName = new TextBox();
            label2 = new Label();
            btnSave = new Button();
            dgvPayments = new DataGridView();
            colPaymentDate = new DataGridViewTextBoxColumn();
            colPaidAmount = new DataGridViewTextBoxColumn();
            colPaymentNotes = new DataGridViewTextBoxColumn();
            btnAddLab = new Button();
            label11 = new Label();
            txtNewPayment = new TextBox();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayments).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.RoyalBlue;
            label1.Location = new Point(619, 9);
            label1.Name = "label1";
            label1.Size = new Size(140, 38);
            label1.TabIndex = 2;
            label1.Text = "المدفوعات";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30.4812832F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 69.5187149F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 163F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 258F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 98F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 266F));
            tableLayoutPanel1.Controls.Add(textAfterDiscount, 1, 2);
            tableLayoutPanel1.Controls.Add(txtRemaining, 5, 2);
            tableLayoutPanel1.Controls.Add(label7, 4, 1);
            tableLayoutPanel1.Controls.Add(label10, 4, 2);
            tableLayoutPanel1.Controls.Add(txtDiscount, 5, 1);
            tableLayoutPanel1.Controls.Add(label5, 0, 0);
            tableLayoutPanel1.Controls.Add(txtTotal, 3, 1);
            tableLayoutPanel1.Controls.Add(label9, 2, 2);
            tableLayoutPanel1.Controls.Add(label3, 2, 0);
            tableLayoutPanel1.Controls.Add(label4, 4, 0);
            tableLayoutPanel1.Controls.Add(txtLabsCount, 1, 1);
            tableLayoutPanel1.Controls.Add(label8, 0, 2);
            tableLayoutPanel1.Controls.Add(label6, 2, 1);
            tableLayoutPanel1.Controls.Add(dtPaymentDate, 5, 0);
            tableLayoutPanel1.Controls.Add(txtVisitCode, 1, 0);
            tableLayoutPanel1.Controls.Add(txtPatientName, 3, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label11, 2, 3);
            tableLayoutPanel1.Controls.Add(txtPaid, 3, 2);
            tableLayoutPanel1.Controls.Add(txtNewPayment, 3, 3);
            tableLayoutPanel1.Location = new Point(160, 106);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RightToLeft = RightToLeft.Yes;
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 43F));
            tableLayoutPanel1.Size = new Size(1160, 244);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // textAfterDiscount
            // 
            textAfterDiscount.Location = new Point(789, 129);
            textAfterDiscount.Name = "textAfterDiscount";
            textAfterDiscount.Size = new Size(254, 27);
            textAfterDiscount.TabIndex = 23;
            // 
            // txtPaid
            // 
            txtPaid.Location = new Point(368, 129);
            txtPaid.Name = "txtPaid";
            txtPaid.Size = new Size(252, 27);
            txtPaid.TabIndex = 22;
            // 
            // txtRemaining
            // 
            txtRemaining.Location = new Point(3, 129);
            txtRemaining.Name = "txtRemaining";
            txtRemaining.Size = new Size(261, 27);
            txtRemaining.TabIndex = 21;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(55, 71, 79);
            label7.Location = new Point(294, 63);
            label7.Name = "label7";
            label7.Size = new Size(68, 28);
            label7.TabIndex = 14;
            label7.Text = "الخصم";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.FromArgb(55, 71, 79);
            label10.Location = new Point(285, 126);
            label10.Name = "label10";
            label10.Size = new Size(77, 28);
            label10.TabIndex = 17;
            label10.Text = "المتبقى";
            // 
            // txtDiscount
            // 
            txtDiscount.Location = new Point(3, 66);
            txtDiscount.Name = "txtDiscount";
            txtDiscount.Size = new Size(261, 27);
            txtDiscount.TabIndex = 18;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(55, 71, 79);
            label5.Location = new Point(1059, 0);
            label5.Name = "label5";
            label5.Size = new Size(98, 28);
            label5.TabIndex = 12;
            label5.Text = "كود الزيارة";
            // 
            // txtTotal
            // 
            txtTotal.Location = new Point(368, 66);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(252, 27);
            txtTotal.TabIndex = 19;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.FromArgb(55, 71, 79);
            label9.Location = new Point(655, 126);
            label9.Name = "label9";
            label9.Size = new Size(128, 28);
            label9.TabIndex = 16;
            label9.Text = "المدفوع سابقا";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(55, 71, 79);
            label3.Location = new Point(663, 0);
            label3.Name = "label3";
            label3.Size = new Size(120, 28);
            label3.TabIndex = 9;
            label3.Text = "اسم المريض";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(55, 71, 79);
            label4.Location = new Point(297, 0);
            label4.Name = "label4";
            label4.Size = new Size(65, 28);
            label4.TabIndex = 10;
            label4.Text = "التاريخ";
            // 
            // txtLabsCount
            // 
            txtLabsCount.Location = new Point(789, 66);
            txtLabsCount.Name = "txtLabsCount";
            txtLabsCount.Size = new Size(254, 27);
            txtLabsCount.TabIndex = 20;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.FromArgb(55, 71, 79);
            label8.Location = new Point(1054, 126);
            label8.Name = "label8";
            label8.Size = new Size(103, 56);
            label8.TabIndex = 15;
            label8.Text = "الاجمالى بعد الخصم";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(55, 71, 79);
            label6.Location = new Point(644, 63);
            label6.Name = "label6";
            label6.Size = new Size(139, 28);
            label6.TabIndex = 13;
            label6.Text = "اجمالى التحاليل";
            // 
            // dtPaymentDate
            // 
            dtPaymentDate.Location = new Point(3, 3);
            dtPaymentDate.Name = "dtPaymentDate";
            dtPaymentDate.Size = new Size(261, 27);
            dtPaymentDate.TabIndex = 11;
            // 
            // txtVisitCode
            // 
            txtVisitCode.Location = new Point(789, 3);
            txtVisitCode.Name = "txtVisitCode";
            txtVisitCode.Size = new Size(254, 27);
            txtVisitCode.TabIndex = 8;
            txtVisitCode.TextChanged += textBox2_TextChanged;
            // 
            // txtPatientName
            // 
            txtPatientName.Location = new Point(368, 3);
            txtPatientName.Name = "txtPatientName";
            txtPatientName.Size = new Size(252, 27);
            txtPatientName.TabIndex = 6;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(55, 71, 79);
            label2.Location = new Point(1082, 63);
            label2.Name = "label2";
            label2.Size = new Size(75, 56);
            label2.TabIndex = 5;
            label2.Text = "عدد التحاليل";
            label2.Click += label2_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.MediumSeaGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(28, 521);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(160, 50);
            btnSave.TabIndex = 18;
            btnSave.Text = "حفظ";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // dgvPayments
            // 
            dgvPayments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPayments.Columns.AddRange(new DataGridViewColumn[] { colPaymentDate, colPaidAmount, colPaymentNotes });
            dgvPayments.Location = new Point(504, 411);
            dgvPayments.Name = "dgvPayments";
            dgvPayments.RightToLeft = RightToLeft.Yes;
            dgvPayments.RowHeadersWidth = 51;
            dgvPayments.Size = new Size(545, 188);
            dgvPayments.TabIndex = 19;
            // 
            // colPaymentDate
            // 
            colPaymentDate.HeaderText = "التاريخ";
            colPaymentDate.MinimumWidth = 6;
            colPaymentDate.Name = "colPaymentDate";
            colPaymentDate.Width = 125;
            // 
            // colPaidAmount
            // 
            colPaidAmount.HeaderText = "المبلغ المدفوع";
            colPaidAmount.MinimumWidth = 6;
            colPaidAmount.Name = "colPaidAmount";
            colPaidAmount.Width = 125;
            // 
            // colPaymentNotes
            // 
            colPaymentNotes.HeaderText = "ملاحظات";
            colPaymentNotes.MinimumWidth = 6;
            colPaymentNotes.Name = "colPaymentNotes";
            colPaymentNotes.Width = 125;
            // 
            // btnAddLab
            // 
            btnAddLab.BackColor = Color.RoyalBlue;
            btnAddLab.FlatAppearance.BorderSize = 0;
            btnAddLab.FlatStyle = FlatStyle.Flat;
            btnAddLab.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAddLab.ForeColor = Color.White;
            btnAddLab.Location = new Point(315, 388);
            btnAddLab.Name = "btnAddLab";
            btnAddLab.Size = new Size(109, 35);
            btnAddLab.TabIndex = 24;
            btnAddLab.Text = "إضافة ➕  ";
            btnAddLab.UseVisualStyleBackColor = false;
            btnAddLab.Click += btnAddLab_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.FromArgb(55, 71, 79);
            label11.Location = new Point(674, 200);
            label11.Name = "label11";
            label11.Size = new Size(109, 28);
            label11.TabIndex = 24;
            label11.Text = "دفعة جديدة";
            // 
            // txtNewPayment
            // 
            txtNewPayment.Location = new Point(368, 203);
            txtNewPayment.Name = "txtNewPayment";
            txtNewPayment.Size = new Size(252, 27);
            txtNewPayment.TabIndex = 26;
            // 
            // FrmPayments
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1379, 620);
            Controls.Add(btnAddLab);
            Controls.Add(dgvPayments);
            Controls.Add(btnSave);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(label1);
            Name = "FrmPayments";
            Text = "FrmPayments";
            Load += FrmPayments_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayments).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label2;
        private TextBox txtVisitCode;
        private TextBox txtPatientName;
        private Label label3;
        private Label label4;
        private DateTimePicker dtPaymentDate;
        private Label label7;
        private TextBox txtDiscount;
        private Label label5;
        private TextBox txtTotal;
        private TextBox txtLabsCount;
        private Label label6;
        private Label label8;
        private Label label9;
        private Label label10;
        private TextBox txtRemaining;
        private TextBox txtPaid;
        private TextBox textAfterDiscount;
        private Button btnSave;
        private DataGridView dgvPayments;
        private DataGridViewTextBoxColumn colPaymentDate;
        private DataGridViewTextBoxColumn colPaidAmount;
        private DataGridViewTextBoxColumn colPaymentNotes;
        private Button btnAddLab;
        private Label label11;
        private TextBox txtNewPayment;
    }
}