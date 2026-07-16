namespace LabManagementSystem.Forms
{
    partial class FrmAddPatient
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
            pnlTitle = new Panel();
            label2 = new Label();
            pnlData = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            label3 = new Label();
            txtPatientCode = new TextBox();
            label4 = new Label();
            txtPatientName = new TextBox();
            label5 = new Label();
            txtPatienPhone = new TextBox();
            label6 = new Label();
            cmboGender = new ComboBox();
            label9 = new Label();
            txtPatientAgeOnRegister = new TextBox();
            label11 = new Label();
            address = new TextBox();
            DOB = new DateTimePicker();
            label10 = new Label();
            DateOFRegister = new DateTimePicker();
            label7 = new Label();
            button1 = new Button();
            label1 = new Label();
            pnlTitle.SuspendLayout();
            pnlData.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.Gainsboro;
            pnlTitle.Controls.Add(label2);
            pnlTitle.Dock = DockStyle.Top;
            pnlTitle.Location = new Point(0, 0);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Size = new Size(1829, 70);
            pnlTitle.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.RoyalBlue;
            label2.Location = new Point(843, 19);
            label2.Name = "label2";
            label2.Size = new Size(246, 38);
            label2.TabIndex = 2;
            label2.Text = "شاشة إضافة مريض";
            // 
            // pnlData
            // 
            pnlData.BackColor = SystemColors.Window;
            pnlData.BorderStyle = BorderStyle.FixedSingle;
            pnlData.Controls.Add(tableLayoutPanel1);
            pnlData.Controls.Add(button1);
            pnlData.Dock = DockStyle.Top;
            pnlData.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            pnlData.Location = new Point(0, 70);
            pnlData.Name = "pnlData";
            pnlData.Size = new Size(1829, 436);
            pnlData.TabIndex = 2;
            pnlData.Paint += pnlData_Paint;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.81818F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76.1818161F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 164F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 421F));
            tableLayoutPanel1.Controls.Add(label3, 0, 0);
            tableLayoutPanel1.Controls.Add(txtPatientCode, 1, 0);
            tableLayoutPanel1.Controls.Add(label4, 2, 0);
            tableLayoutPanel1.Controls.Add(txtPatientName, 3, 0);
            tableLayoutPanel1.Controls.Add(label5, 0, 1);
            tableLayoutPanel1.Controls.Add(txtPatienPhone, 1, 1);
            tableLayoutPanel1.Controls.Add(label6, 2, 1);
            tableLayoutPanel1.Controls.Add(cmboGender, 3, 1);
            tableLayoutPanel1.Controls.Add(label9, 0, 2);
            tableLayoutPanel1.Controls.Add(txtPatientAgeOnRegister, 1, 2);
            tableLayoutPanel1.Controls.Add(label11, 2, 2);
            tableLayoutPanel1.Controls.Add(address, 3, 2);
            tableLayoutPanel1.Controls.Add(DOB, 1, 3);
            tableLayoutPanel1.Controls.Add(label10, 2, 3);
            tableLayoutPanel1.Controls.Add(DateOFRegister, 3, 3);
            tableLayoutPanel1.Controls.Add(label7, 0, 3);
            tableLayoutPanel1.Location = new Point(280, 22);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 118F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 65F));
            tableLayoutPanel1.Size = new Size(1374, 360);
            tableLayoutPanel1.TabIndex = 20;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.FromArgb(55, 71, 79);
            label3.Location = new Point(1237, 0);
            label3.Name = "label3";
            label3.Size = new Size(134, 31);
            label3.TabIndex = 1;
            label3.Text = "كود المريض";
            // 
            // txtPatientCode
            // 
            txtPatientCode.Location = new Point(944, 3);
            txtPatientCode.Name = "txtPatientCode";
            txtPatientCode.Size = new Size(240, 38);
            txtPatientCode.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.FromArgb(55, 71, 79);
            label4.Location = new Point(447, 0);
            label4.Name = "label4";
            label4.Size = new Size(136, 31);
            label4.TabIndex = 3;
            label4.Text = "اسم المريض";
            label4.Click += label4_Click;
            // 
            // txtPatientName
            // 
            txtPatientName.Location = new Point(179, 3);
            txtPatientName.Name = "txtPatientName";
            txtPatientName.Size = new Size(240, 38);
            txtPatientName.TabIndex = 2;
            txtPatientName.TextChanged += textBox2_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.FromArgb(55, 71, 79);
            label5.Location = new Point(1255, 88);
            label5.Name = "label5";
            label5.Size = new Size(116, 31);
            label5.TabIndex = 5;
            label5.Text = "رقم الهاتف";
            // 
            // txtPatienPhone
            // 
            txtPatienPhone.Location = new Point(944, 91);
            txtPatienPhone.Name = "txtPatienPhone";
            txtPatienPhone.Size = new Size(240, 38);
            txtPatienPhone.TabIndex = 4;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.FromArgb(55, 71, 79);
            label6.Location = new Point(521, 88);
            label6.Name = "label6";
            label6.Size = new Size(62, 31);
            label6.TabIndex = 7;
            label6.Text = "النوع";
            // 
            // cmboGender
            // 
            cmboGender.Font = new Font("Tahoma", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmboGender.FormattingEnabled = true;
            cmboGender.Location = new Point(179, 91);
            cmboGender.Name = "cmboGender";
            cmboGender.Size = new Size(240, 36);
            cmboGender.TabIndex = 14;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.FromArgb(55, 71, 79);
            label9.Location = new Point(1270, 176);
            label9.Name = "label9";
            label9.Size = new Size(101, 62);
            label9.TabIndex = 13;
            label9.Text = "السن عند التسجيل";
            label9.Click += label9_Click;
            // 
            // txtPatientAgeOnRegister
            // 
            txtPatientAgeOnRegister.Location = new Point(944, 179);
            txtPatientAgeOnRegister.Name = "txtPatientAgeOnRegister";
            txtPatientAgeOnRegister.Size = new Size(240, 38);
            txtPatientAgeOnRegister.TabIndex = 12;
            txtPatientAgeOnRegister.TextChanged += textBox7_TextChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = Color.FromArgb(55, 71, 79);
            label11.Location = new Point(501, 176);
            label11.Name = "label11";
            label11.Size = new Size(82, 31);
            label11.TabIndex = 19;
            label11.Text = "العنوان";
            // 
            // address
            // 
            address.Location = new Point(179, 179);
            address.Name = "address";
            address.Size = new Size(240, 38);
            address.TabIndex = 18;
            // 
            // DOB
            // 
            DOB.Location = new Point(767, 297);
            DOB.Name = "DOB";
            DOB.Size = new Size(417, 38);
            DOB.TabIndex = 15;
            DOB.ValueChanged += DOB_ValueChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = Color.FromArgb(55, 71, 79);
            label10.Location = new Point(439, 294);
            label10.Name = "label10";
            label10.Size = new Size(144, 31);
            label10.TabIndex = 11;
            label10.Text = "تاريخ التسجيل";
            // 
            // DateOFRegister
            // 
            DateOFRegister.CalendarFont = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            DateOFRegister.Location = new Point(25, 297);
            DateOFRegister.Name = "DateOFRegister";
            DateOFRegister.Size = new Size(394, 38);
            DateOFRegister.TabIndex = 16;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.FromArgb(55, 71, 79);
            label7.Location = new Point(1243, 294);
            label7.Name = "label7";
            label7.RightToLeft = RightToLeft.Yes;
            label7.Size = new Size(128, 31);
            label7.TabIndex = 9;
            label7.Text = "تاريخ الميلاد";
            // 
            // button1
            // 
            button1.BackColor = Color.MediumSeaGreen;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(56, 346);
            button1.Name = "button1";
            button1.Size = new Size(160, 50);
            button1.TabIndex = 17;
            button1.Text = "حفظ";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(397, 19);
            label1.Name = "label1";
            label1.Size = new Size(93, 20);
            label1.TabIndex = 0;
            label1.Text = "اضافة مريض";
            // 
            // FrmAddPatient
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1829, 778);
            Controls.Add(pnlData);
            Controls.Add(pnlTitle);
            Controls.Add(label1);
            Name = "FrmAddPatient";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Text = "FrmAddPatient";
            Load += FrmAddPatient_Load;
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            pnlData.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel pnlTitle;
        private Label label2;
        private Panel pnlData;
        private Label label9;
        private TextBox txtPatientAgeOnRegister;
        private Label label7;
        private Label label6;
        private Label label5;
        private TextBox txtPatienPhone;
        private Label label4;
        private Button button1;
        private DateTimePicker DateOFRegister;
        private DateTimePicker DOB;
        private ComboBox cmboGender;
        private Label label10;
        private Label label11;
        private TextBox address;
        private Label label1;
        private TextBox txtPatientName;
        private TextBox txtPatientCode;
        private Label label3;
        private TableLayoutPanel tableLayoutPanel1;
    }
}