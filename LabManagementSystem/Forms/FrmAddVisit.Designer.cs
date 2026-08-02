namespace LabManagementSystem.Forms
{
    partial class FrmAddVisit
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
            button1 = new Button();
            label3 = new Label();
            txtPatientCode = new TextBox();
            label4 = new Label();
            txtTotal = new TextBox();
            lblDoctorNAme = new Label();
            txtDoctorName = new TextBox();
            label6 = new Label();
            pnlData = new Panel();
            tblAnalysis = new TableLayoutPanel();
            dgvVisitDetails = new DataGridView();
            label10 = new Label();
            cmboGender = new ComboBox();
            button3 = new Button();
            button2 = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            label7 = new Label();
            DateOfVisit = new DateTimePicker();
            label9 = new Label();
            label8 = new Label();
            txtRemaining = new TextBox();
            txtAfterDiscount = new TextBox();
            textBox2 = new TextBox();
            label1 = new Label();
            label11 = new Label();
            address = new TextBox();
            txtNotes = new TextBox();
            textBox1 = new TextBox();
            label5 = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnAddPatient = new Button();
            txtPatientName = new TextBox();
            label12 = new Label();
            label2 = new Label();
            pnlTitle = new Panel();
            pnlData.SuspendLayout();
            tblAnalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVisitDetails).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            pnlTitle.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.MediumSeaGreen;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(11, 359);
            button1.Name = "button1";
            button1.Size = new Size(160, 50);
            button1.TabIndex = 17;
            button1.Text = "حفظ";
            button1.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.FromArgb(55, 71, 79);
            label3.Location = new Point(1690, 0);
            label3.Name = "label3";
            label3.Size = new Size(134, 31);
            label3.TabIndex = 1;
            label3.Text = "كود المريض";
            // 
            // txtPatientCode
            // 
            txtPatientCode.Location = new Point(724, 3);
            txtPatientCode.Name = "txtPatientCode";
            txtPatientCode.Size = new Size(213, 38);
            txtPatientCode.TabIndex = 0;
            txtPatientCode.TextChanged += txtPatientCode_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.FromArgb(55, 71, 79);
            label4.Location = new Point(490, 0);
            label4.Name = "label4";
            label4.Size = new Size(93, 31);
            label4.TabIndex = 3;
            label4.Text = "الاجمالى";
            // 
            // txtTotal
            // 
            txtTotal.Location = new Point(179, 3);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(240, 38);
            txtTotal.TabIndex = 2;
            // 
            // lblDoctorNAme
            // 
            lblDoctorNAme.AutoSize = true;
            lblDoctorNAme.ForeColor = Color.FromArgb(55, 71, 79);
            lblDoctorNAme.Location = new Point(1698, 56);
            lblDoctorNAme.Name = "lblDoctorNAme";
            lblDoctorNAme.Size = new Size(126, 31);
            lblDoctorNAme.TabIndex = 5;
            lblDoctorNAme.Text = "اسم الدكتور";
            // 
            // txtDoctorName
            // 
            txtDoctorName.Location = new Point(1289, 59);
            txtDoctorName.Name = "txtDoctorName";
            txtDoctorName.Size = new Size(240, 38);
            txtDoctorName.TabIndex = 4;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.FromArgb(55, 71, 79);
            label6.Location = new Point(455, 56);
            label6.Name = "label6";
            label6.Size = new Size(128, 31);
            label6.TabIndex = 7;
            label6.Text = "قيمة الخصم";
            // 
            // pnlData
            // 
            pnlData.BackColor = SystemColors.Window;
            pnlData.BorderStyle = BorderStyle.FixedSingle;
            pnlData.Controls.Add(tblAnalysis);
            pnlData.Controls.Add(tableLayoutPanel1);
            pnlData.Controls.Add(button1);
            pnlData.Dock = DockStyle.Top;
            pnlData.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            pnlData.Location = new Point(0, 70);
            pnlData.Name = "pnlData";
            pnlData.Size = new Size(1829, 436);
            pnlData.TabIndex = 5;
            pnlData.Paint += pnlData_Paint;
            // 
            // tblAnalysis
            // 
            tblAnalysis.AutoSize = true;
            tblAnalysis.ColumnCount = 3;
            tblAnalysis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.296114F));
            tblAnalysis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.94855F));
            tblAnalysis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60.7553368F));
            tblAnalysis.Controls.Add(dgvVisitDetails, 0, 1);
            tblAnalysis.Controls.Add(label10, 0, 0);
            tblAnalysis.Controls.Add(cmboGender, 1, 0);
            tblAnalysis.Controls.Add(button3, 2, 2);
            tblAnalysis.Controls.Add(button2, 2, 0);
            tblAnalysis.Dock = DockStyle.Top;
            tblAnalysis.Location = new Point(0, 268);
            tblAnalysis.Name = "tblAnalysis";
            tblAnalysis.RowCount = 3;
            tblAnalysis.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblAnalysis.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblAnalysis.RowStyles.Add(new RowStyle(SizeType.Absolute, 399F));
            tblAnalysis.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblAnalysis.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblAnalysis.Size = new Size(1827, 489);
            tblAnalysis.TabIndex = 22;
            tblAnalysis.Paint += tblAnalysis_Paint;
            // 
            // dgvVisitDetails
            // 
            dgvVisitDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVisitDetails.Dock = DockStyle.Fill;
            dgvVisitDetails.Location = new Point(1514, 48);
            dgvVisitDetails.Name = "dgvVisitDetails";
            dgvVisitDetails.RowHeadersWidth = 51;
            dgvVisitDetails.Size = new Size(310, 39);
            dgvVisitDetails.TabIndex = 24;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = Color.FromArgb(55, 71, 79);
            label10.Location = new Point(1732, 0);
            label10.Name = "label10";
            label10.Size = new Size(92, 31);
            label10.TabIndex = 2;
            label10.Text = "التحليل :";
            // 
            // cmboGender
            // 
            cmboGender.Font = new Font("Tahoma", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmboGender.FormattingEnabled = true;
            cmboGender.Location = new Point(1268, 3);
            cmboGender.Name = "cmboGender";
            cmboGender.Size = new Size(240, 36);
            cmboGender.TabIndex = 15;
            // 
            // button3
            // 
            button3.BackColor = Color.MediumSeaGreen;
            button3.FlatStyle = FlatStyle.Flat;
            button3.ForeColor = Color.White;
            button3.Location = new Point(973, 93);
            button3.Name = "button3";
            button3.Size = new Size(134, 48);
            button3.TabIndex = 25;
            button3.Text = "حفظ";
            button3.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.RoyalBlue;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.Location = new Point(989, 3);
            button2.Name = "button2";
            button2.Size = new Size(118, 36);
            button2.TabIndex = 23;
            button2.Text = "إضافة ➕  ";
            button2.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.81818F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76.1818161F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 164F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 421F));
            tableLayoutPanel1.Controls.Add(label7, 0, 4);
            tableLayoutPanel1.Controls.Add(DateOfVisit, 1, 4);
            tableLayoutPanel1.Controls.Add(label9, 2, 4);
            tableLayoutPanel1.Controls.Add(label8, 2, 3);
            tableLayoutPanel1.Controls.Add(txtRemaining, 3, 4);
            tableLayoutPanel1.Controls.Add(txtAfterDiscount, 3, 3);
            tableLayoutPanel1.Controls.Add(textBox2, 3, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 3);
            tableLayoutPanel1.Controls.Add(label3, 0, 0);
            tableLayoutPanel1.Controls.Add(label4, 2, 0);
            tableLayoutPanel1.Controls.Add(txtTotal, 3, 0);
            tableLayoutPanel1.Controls.Add(lblDoctorNAme, 0, 1);
            tableLayoutPanel1.Controls.Add(txtDoctorName, 1, 1);
            tableLayoutPanel1.Controls.Add(label6, 2, 1);
            tableLayoutPanel1.Controls.Add(label11, 2, 2);
            tableLayoutPanel1.Controls.Add(address, 3, 2);
            tableLayoutPanel1.Controls.Add(txtNotes, 1, 3);
            tableLayoutPanel1.Controls.Add(textBox1, 1, 2);
            tableLayoutPanel1.Controls.Add(label5, 0, 2);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50.7042236F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 49.2957764F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 63F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1827, 268);
            tableLayoutPanel1.TabIndex = 20;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.FromArgb(55, 71, 79);
            label7.Location = new Point(1697, 204);
            label7.Name = "label7";
            label7.RightToLeft = RightToLeft.Yes;
            label7.Size = new Size(127, 31);
            label7.TabIndex = 26;
            label7.Text = "تاريخ الزيارة";
            // 
            // DateOfVisit
            // 
            DateOfVisit.Location = new Point(1112, 207);
            DateOfVisit.Name = "DateOfVisit";
            DateOfVisit.Size = new Size(417, 38);
            DateOfVisit.TabIndex = 27;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.FromArgb(55, 71, 79);
            label9.Location = new Point(510, 204);
            label9.Name = "label9";
            label9.Size = new Size(73, 31);
            label9.TabIndex = 7;
            label9.Text = "الباقى";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.FromArgb(55, 71, 79);
            label8.Location = new Point(451, 150);
            label8.Name = "label8";
            label8.Size = new Size(132, 54);
            label8.TabIndex = 25;
            label8.Text = "الاجمالى بعد الخصم";
            // 
            // txtRemaining
            // 
            txtRemaining.Location = new Point(179, 207);
            txtRemaining.Name = "txtRemaining";
            txtRemaining.Size = new Size(240, 38);
            txtRemaining.TabIndex = 6;
            // 
            // txtAfterDiscount
            // 
            txtAfterDiscount.Location = new Point(179, 153);
            txtAfterDiscount.Name = "txtAfterDiscount";
            txtAfterDiscount.Size = new Size(240, 38);
            txtAfterDiscount.TabIndex = 24;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(179, 59);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(240, 38);
            textBox2.TabIndex = 23;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(55, 71, 79);
            label1.Location = new Point(1724, 150);
            label1.Name = "label1";
            label1.Size = new Size(100, 31);
            label1.TabIndex = 21;
            label1.Text = "ملاحظات";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = Color.FromArgb(55, 71, 79);
            label11.Location = new Point(427, 111);
            label11.Name = "label11";
            label11.Size = new Size(156, 31);
            label11.TabIndex = 19;
            label11.Text = "نسبة الخصم %";
            // 
            // address
            // 
            address.Location = new Point(179, 114);
            address.Name = "address";
            address.Size = new Size(240, 38);
            address.TabIndex = 18;
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(1289, 153);
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(240, 38);
            txtNotes.TabIndex = 20;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(1289, 114);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(240, 38);
            textBox1.TabIndex = 21;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.FromArgb(55, 71, 79);
            label5.Location = new Point(1733, 111);
            label5.Name = "label5";
            label5.Size = new Size(91, 31);
            label5.TabIndex = 22;
            label5.Text = "المدفوع";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.3394852F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.6605148F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 291F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 194F));
            tableLayoutPanel2.Controls.Add(btnAddPatient, 3, 0);
            tableLayoutPanel2.Controls.Add(txtPatientName, 2, 0);
            tableLayoutPanel2.Controls.Add(txtPatientCode, 0, 0);
            tableLayoutPanel2.Controls.Add(label12, 1, 0);
            tableLayoutPanel2.Location = new Point(589, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(940, 50);
            tableLayoutPanel2.TabIndex = 6;
            // 
            // btnAddPatient
            // 
            btnAddPatient.BackColor = Color.RoyalBlue;
            btnAddPatient.FlatAppearance.BorderSize = 0;
            btnAddPatient.FlatStyle = FlatStyle.Flat;
            btnAddPatient.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAddPatient.ForeColor = Color.White;
            btnAddPatient.Location = new Point(38, 3);
            btnAddPatient.Name = "btnAddPatient";
            btnAddPatient.Size = new Size(154, 42);
            btnAddPatient.TabIndex = 21;
            btnAddPatient.Text = "إضافة مريض ➕  ";
            btnAddPatient.UseVisualStyleBackColor = false;
            btnAddPatient.Click += btnAddPatient_Click;
            // 
            // txtPatientName
            // 
            txtPatientName.Location = new Point(242, 3);
            txtPatientName.Name = "txtPatientName";
            txtPatientName.Size = new Size(241, 38);
            txtPatientName.TabIndex = 3;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.ForeColor = Color.FromArgb(55, 71, 79);
            label12.Location = new Point(582, 0);
            label12.Name = "label12";
            label12.Size = new Size(136, 31);
            label12.TabIndex = 2;
            label12.Text = "اسم المريض";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.RoyalBlue;
            label2.Location = new Point(843, 19);
            label2.Name = "label2";
            label2.Size = new Size(224, 38);
            label2.TabIndex = 2;
            label2.Text = "شاشة إضافة زيارة";
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.Gainsboro;
            pnlTitle.Controls.Add(label2);
            pnlTitle.Dock = DockStyle.Top;
            pnlTitle.Location = new Point(0, 0);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Size = new Size(1829, 70);
            pnlTitle.TabIndex = 4;
            // 
            // FrmAddVisit
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1829, 778);
            Controls.Add(pnlData);
            Controls.Add(pnlTitle);
            Name = "FrmAddVisit";
            RightToLeft = RightToLeft.Yes;
            Text = "FrmAddVisit";
            Load += FrmAddVisit_Load;
            pnlData.ResumeLayout(false);
            pnlData.PerformLayout();
            tblAnalysis.ResumeLayout(false);
            tblAnalysis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVisitDetails).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button button1;
        private Label label3;
        private TextBox txtPatientCode;
        private Label label4;
        private TextBox txtTotal;
        private Label lblDoctorNAme;
        private TextBox txtDoctorName;
        private Label label6;
        private Panel pnlData;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label11;
        private TextBox address;
        private Label label2;
        private Panel pnlTitle;
        private Label label1;
        private TextBox txtNotes;
        private TextBox textBox1;
        private Label label5;
        private Label label9;
        private Label label8;
        private TextBox txtRemaining;
        private TextBox txtAfterDiscount;
        private TextBox textBox2;
        private TableLayoutPanel tblAnalysis;
        private Button button2;
        private ComboBox cmboGender;
        private Label label10;
        private DataGridView dgvVisitDetails;
        private Button button3;
        private Label label7;
        private DateTimePicker DateOfVisit;
        private Button btnAddPatient;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox txtPatientName;
        private Label label12;
    }
}