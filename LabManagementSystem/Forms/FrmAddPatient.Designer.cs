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
            label1 = new Label();
            pnlTitle = new Panel();
            label2 = new Label();
            pnlData = new Panel();
            label11 = new Label();
            address = new TextBox();
            button1 = new Button();
            DateOFRegister = new DateTimePicker();
            DOB = new DateTimePicker();
            cmboGender = new ComboBox();
            label9 = new Label();
            txtPatientAgeOnRegister = new TextBox();
            label10 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            txtPatienPhone = new TextBox();
            label4 = new Label();
            txtPatientName = new TextBox();
            label3 = new Label();
            txtPatientCode = new TextBox();
            pnlTitle.SuspendLayout();
            pnlData.SuspendLayout();
            SuspendLayout();
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
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.Gainsboro;
            pnlTitle.Controls.Add(label2);
            pnlTitle.Dock = DockStyle.Top;
            pnlTitle.Location = new Point(0, 0);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Size = new Size(1493, 70);
            pnlTitle.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(0, 131, 143);
            label2.Location = new Point(703, 19);
            label2.Name = "label2";
            label2.Size = new Size(267, 41);
            label2.TabIndex = 2;
            label2.Text = "شاشة إضافة مريض";
            // 
            // pnlData
            // 
            pnlData.BackColor = SystemColors.Window;
            pnlData.BorderStyle = BorderStyle.FixedSingle;
            pnlData.Controls.Add(label11);
            pnlData.Controls.Add(address);
            pnlData.Controls.Add(button1);
            pnlData.Controls.Add(DateOFRegister);
            pnlData.Controls.Add(DOB);
            pnlData.Controls.Add(cmboGender);
            pnlData.Controls.Add(label9);
            pnlData.Controls.Add(txtPatientAgeOnRegister);
            pnlData.Controls.Add(label10);
            pnlData.Controls.Add(label7);
            pnlData.Controls.Add(label6);
            pnlData.Controls.Add(label5);
            pnlData.Controls.Add(txtPatienPhone);
            pnlData.Controls.Add(label4);
            pnlData.Controls.Add(txtPatientName);
            pnlData.Controls.Add(label3);
            pnlData.Controls.Add(txtPatientCode);
            pnlData.Dock = DockStyle.Top;
            pnlData.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            pnlData.Location = new Point(0, 70);
            pnlData.Name = "pnlData";
            pnlData.Size = new Size(1493, 420);
            pnlData.TabIndex = 2;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = Color.FromArgb(55, 71, 79);
            label11.Location = new Point(711, 211);
            label11.Name = "label11";
            label11.Size = new Size(82, 31);
            label11.TabIndex = 19;
            label11.Text = "العنوان";
         //   label11.Click += this.label11_Click;
            // 
            // address
            // 
            address.Location = new Point(499, 245);
            address.Name = "address";
            address.Size = new Size(295, 38);
            address.TabIndex = 18;
            // 
            // button1
            // 
            button1.BackColor = Color.MediumSeaGreen;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = Color.White;
            button1.Location = new Point(48, 346);
            button1.Name = "button1";
            button1.Size = new Size(120, 50);
            button1.TabIndex = 17;
            button1.Text = "حفظ";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // DateOFRegister
            // 
            DateOFRegister.Location = new Point(373, 346);
            DateOFRegister.Name = "DateOFRegister";
            DateOFRegister.Size = new Size(429, 38);
            DateOFRegister.TabIndex = 16;
            // 
            // DOB
            // 
            DOB.Location = new Point(960, 324);
            DOB.Name = "DOB";
            DOB.Size = new Size(405, 38);
            DOB.TabIndex = 15;
            // 
            // cmboGender
            // 
            cmboGender.Font = new Font("Tahoma", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmboGender.FormattingEnabled = true;
            cmboGender.Location = new Point(499, 146);
            cmboGender.Name = "cmboGender";
            cmboGender.Size = new Size(302, 36);
            cmboGender.TabIndex = 14;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.FromArgb(55, 71, 79);
            label9.Location = new Point(1140, 197);
            label9.Name = "label9";
            label9.Size = new Size(184, 31);
            label9.TabIndex = 13;
            label9.Text = "السن عند التسجيل";
            label9.Click += label9_Click;
            // 
            // txtPatientAgeOnRegister
            // 
            txtPatientAgeOnRegister.Location = new Point(1038, 231);
            txtPatientAgeOnRegister.Name = "txtPatientAgeOnRegister";
            txtPatientAgeOnRegister.Size = new Size(286, 38);
            txtPatientAgeOnRegister.TabIndex = 12;
            txtPatientAgeOnRegister.TextChanged += textBox7_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.ForeColor = Color.FromArgb(55, 71, 79);
            label10.Location = new Point(658, 309);
            label10.Name = "label10";
            label10.Size = new Size(144, 31);
            label10.TabIndex = 11;
            label10.Text = "تاريخ التسجيل";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.FromArgb(55, 71, 79);
            label7.Location = new Point(1237, 290);
            label7.Name = "label7";
            label7.RightToLeft = RightToLeft.Yes;
            label7.Size = new Size(128, 31);
            label7.TabIndex = 9;
            label7.Text = "تاريخ الميلاد";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.FromArgb(55, 71, 79);
            label6.Location = new Point(739, 113);
            label6.Name = "label6";
            label6.Size = new Size(62, 31);
            label6.TabIndex = 7;
            label6.Text = "النوع";
         //   label6.Click += this.label6_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.FromArgb(55, 71, 79);
            label5.Location = new Point(1219, 113);
            label5.Name = "label5";
            label5.Size = new Size(116, 31);
            label5.TabIndex = 5;
            label5.Text = "رقم الهاتف";
            // 
            // txtPatienPhone
            // 
            txtPatienPhone.Location = new Point(1038, 147);
            txtPatienPhone.Name = "txtPatienPhone";
            txtPatienPhone.Size = new Size(293, 38);
            txtPatienPhone.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.FromArgb(55, 71, 79);
            label4.Location = new Point(658, 27);
            label4.Name = "label4";
            label4.Size = new Size(136, 31);
            label4.TabIndex = 3;
            label4.Text = "اسم المريض";
            label4.Click += label4_Click;
            // 
            // txtPatientName
            // 
            txtPatientName.Location = new Point(499, 61);
            txtPatientName.Name = "txtPatientName";
            txtPatientName.Size = new Size(295, 38);
            txtPatientName.TabIndex = 2;
            txtPatientName.TextChanged += textBox2_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = Color.FromArgb(55, 71, 79);
            label3.Location = new Point(1190, 27);
            label3.Name = "label3";
            label3.Size = new Size(134, 31);
            label3.TabIndex = 1;
            label3.Text = "كود المريض";
            // 
            // txtPatientCode
            // 
            txtPatientCode.Location = new Point(1038, 61);
            txtPatientCode.Name = "txtPatientCode";
            txtPatientCode.Size = new Size(282, 38);
            txtPatientCode.TabIndex = 0;
            // 
            // FrmAddPatient
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1493, 479);
            Controls.Add(pnlData);
            Controls.Add(pnlTitle);
            Controls.Add(label1);
            Name = "FrmAddPatient";
            Text = "FrmAddPatient";
            Load += FrmAddPatient_Load;
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            pnlData.ResumeLayout(false);
            pnlData.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel pnlTitle;
        private Label label2;
        private Panel pnlData;
        private Label label3;
        private TextBox txtPatientCode;
        private Label label9;
        private TextBox txtPatientAgeOnRegister;
        private Label label7;
        private Label label6;
        private Label label5;
        private TextBox txtPatienPhone;
        private Label label4;
        private TextBox txtPatientName;
        private Button button1;
        private DateTimePicker DateOFRegister;
        private DateTimePicker DOB;
        private ComboBox cmboGender;
        private Label label10;
        private Label label11;
        private TextBox address;
    }
}