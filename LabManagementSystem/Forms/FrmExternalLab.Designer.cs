namespace LabManagementSystem.Forms
{
    partial class FrmExternalLab
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
            txtPatientInfo = new TextBox();
            lbltxt = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            dtpDateReceive = new DateTimePicker();
            dtpDateSend = new DateTimePicker();
            txtExpextedDays = new TextBox();
            txtPhone = new TextBox();
            txtContactPerson = new TextBox();
            cmbExternalLab = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            lblDoctorNAme = new Label();
            btnSave = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtPatientInfo
            // 
            txtPatientInfo.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtPatientInfo.Location = new Point(737, 64);
            txtPatientInfo.Name = "txtPatientInfo";
            txtPatientInfo.RightToLeft = RightToLeft.Yes;
            txtPatientInfo.Size = new Size(701, 31);
            txtPatientInfo.TabIndex = 0;
            txtPatientInfo.TextAlign = HorizontalAlignment.Right;
            // 
            // lbltxt
            // 
            lbltxt.AutoSize = true;
            lbltxt.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbltxt.ForeColor = Color.RoyalBlue;
            lbltxt.Location = new Point(686, -7);
            lbltxt.Name = "lbltxt";
            lbltxt.Size = new Size(343, 38);
            lbltxt.TabIndex = 3;
            lbltxt.Text = "شاشة التحويل لمعمل خارجى";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.AutoScroll = true;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(dtpDateReceive, 0, 5);
            tableLayoutPanel1.Controls.Add(dtpDateSend, 0, 3);
            tableLayoutPanel1.Controls.Add(txtExpextedDays, 0, 4);
            tableLayoutPanel1.Controls.Add(txtPhone, 0, 2);
            tableLayoutPanel1.Controls.Add(txtContactPerson, 0, 1);
            tableLayoutPanel1.Controls.Add(cmbExternalLab, 0, 0);
            tableLayoutPanel1.Controls.Add(label1, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 1, 2);
            tableLayoutPanel1.Controls.Add(label3, 1, 3);
            tableLayoutPanel1.Controls.Add(label4, 1, 4);
            tableLayoutPanel1.Controls.Add(label5, 1, 5);
            tableLayoutPanel1.Controls.Add(lblDoctorNAme, 1, 1);
            tableLayoutPanel1.Location = new Point(835, 151);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 54.4642868F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 45.5357132F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 71F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel1.Size = new Size(537, 313);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // dtpDateReceive
            // 
            dtpDateReceive.Location = new Point(3, 260);
            dtpDateReceive.Name = "dtpDateReceive";
            dtpDateReceive.Size = new Size(262, 27);
            dtpDateReceive.TabIndex = 29;
            // 
            // dtpDateSend
            // 
            dtpDateSend.Location = new Point(3, 186);
            dtpDateSend.Name = "dtpDateSend";
            dtpDateSend.Size = new Size(262, 27);
            dtpDateSend.TabIndex = 28;
            // 
            // txtExpextedDays
            // 
            txtExpextedDays.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtExpextedDays.Location = new Point(25, 221);
            txtExpextedDays.Name = "txtExpextedDays";
            txtExpextedDays.Size = new Size(240, 27);
            txtExpextedDays.TabIndex = 25;
            // 
            // txtPhone
            // 
            txtPhone.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtPhone.Location = new Point(25, 115);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(240, 27);
            txtPhone.TabIndex = 23;
            // 
            // txtContactPerson
            // 
            txtContactPerson.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtContactPerson.Location = new Point(25, 64);
            txtContactPerson.Name = "txtContactPerson";
            txtContactPerson.Size = new Size(240, 27);
            txtContactPerson.TabIndex = 22;
            // 
            // cmbExternalLab
            // 
            cmbExternalLab.Font = new Font("Tahoma", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbExternalLab.FormattingEnabled = true;
            cmbExternalLab.Location = new Point(3, 3);
            cmbExternalLab.Name = "cmbExternalLab";
            cmbExternalLab.Size = new Size(251, 30);
            cmbExternalLab.TabIndex = 16;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(55, 71, 79);
            label1.Location = new Point(425, 0);
            label1.Name = "label1";
            label1.Size = new Size(109, 20);
            label1.TabIndex = 3;
            label1.Text = "المعمل الخارجى";
            label1.TextAlign = ContentAlignment.TopRight;
            label1.Click += label10_Click;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.ForeColor = Color.FromArgb(55, 71, 79);
            label2.Location = new Point(458, 112);
            label2.Name = "label2";
            label2.Size = new Size(76, 20);
            label2.TabIndex = 18;
            label2.Text = "رقم الهاتف";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.ForeColor = Color.FromArgb(55, 71, 79);
            label3.Location = new Point(445, 183);
            label3.Name = "label3";
            label3.Size = new Size(89, 20);
            label3.TabIndex = 19;
            label3.Text = "تاريخ التسليم";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label4.AutoSize = true;
            label4.ForeColor = Color.FromArgb(55, 71, 79);
            label4.Location = new Point(405, 218);
            label4.Name = "label4";
            label4.Size = new Size(129, 20);
            label4.TabIndex = 20;
            label4.Text = "عدد الايام المتوقعه";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label5.AutoSize = true;
            label5.ForeColor = Color.FromArgb(55, 71, 79);
            label5.Location = new Point(442, 257);
            label5.Name = "label5";
            label5.Size = new Size(92, 20);
            label5.TabIndex = 21;
            label5.Text = "تاريخ الاستلام";
            label5.Click += label5_Click;
            // 
            // lblDoctorNAme
            // 
            lblDoctorNAme.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDoctorNAme.AutoSize = true;
            lblDoctorNAme.ForeColor = Color.FromArgb(55, 71, 79);
            lblDoctorNAme.Location = new Point(446, 61);
            lblDoctorNAme.Name = "lblDoctorNAme";
            lblDoctorNAme.Size = new Size(88, 20);
            lblDoctorNAme.TabIndex = 17;
            lblDoctorNAme.Text = "اسم المسؤل";
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.MediumSeaGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(656, 429);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(119, 35);
            btnSave.TabIndex = 27;
            btnSave.Text = "حفظ";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // FrmExternalLab
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1476, 520);
            Controls.Add(btnSave);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(lbltxt);
            Controls.Add(txtPatientInfo);
            Name = "FrmExternalLab";
            Text = "FrmExternalLab";
            Load += FrmExternalLab_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPatientInfo;
        private Label lbltxt;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private ComboBox cmbExternalLab;
        private Label lblDoctorNAme;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtExpextedDays;
        private TextBox txtPhone;
        private TextBox txtContactPerson;
        private Button btnSave;
        private DateTimePicker dtpDateSend;
        protected DateTimePicker dtpDateReceive;
    }
}