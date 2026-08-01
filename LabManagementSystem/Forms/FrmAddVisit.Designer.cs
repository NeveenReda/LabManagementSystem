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
            txtPatientName = new TextBox();
            lblDoctorNAme = new Label();
            txtDoctorName = new TextBox();
            label6 = new Label();
            pnlData = new Panel();
            btnSearch = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            label9 = new Label();
            label8 = new Label();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label5 = new Label();
            label1 = new Label();
            txtNotes = new TextBox();
            label11 = new Label();
            address = new TextBox();
            label7 = new Label();
            DateOfVisit = new DateTimePicker();
            label2 = new Label();
            pnlTitle = new Panel();
            pnlData.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
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
            label4.Location = new Point(490, 0);
            label4.Name = "label4";
            label4.Size = new Size(93, 31);
            label4.TabIndex = 3;
            label4.Text = "الاجمالى";
            // 
            // txtPatientName
            // 
            txtPatientName.Location = new Point(179, 3);
            txtPatientName.Name = "txtPatientName";
            txtPatientName.Size = new Size(240, 38);
            txtPatientName.TabIndex = 2;
            // 
            // lblDoctorNAme
            // 
            lblDoctorNAme.AutoSize = true;
            lblDoctorNAme.ForeColor = Color.FromArgb(55, 71, 79);
            lblDoctorNAme.Location = new Point(1245, 42);
            lblDoctorNAme.Name = "lblDoctorNAme";
            lblDoctorNAme.Size = new Size(126, 31);
            lblDoctorNAme.TabIndex = 5;
            lblDoctorNAme.Text = "اسم الدكتور";
            // 
            // txtDoctorName
            // 
            txtDoctorName.Location = new Point(944, 45);
            txtDoctorName.Name = "txtDoctorName";
            txtDoctorName.Size = new Size(240, 38);
            txtDoctorName.TabIndex = 4;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = Color.FromArgb(55, 71, 79);
            label6.Location = new Point(455, 42);
            label6.Name = "label6";
            label6.Size = new Size(128, 31);
            label6.TabIndex = 7;
            label6.Text = "قيمة الخصم";
            // 
            // pnlData
            // 
            pnlData.BackColor = SystemColors.Window;
            pnlData.BorderStyle = BorderStyle.FixedSingle;
            pnlData.Controls.Add(btnSearch);
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
            // btnSearch
            // 
            btnSearch.BackColor = Color.RoyalBlue;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(1222, 8);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(154, 42);
            btnSearch.TabIndex = 21;
            btnSearch.Text = "إضافة مريض ➕  ";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.81818F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76.1818161F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 164F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 421F));
            tableLayoutPanel1.Controls.Add(label9, 2, 4);
            tableLayoutPanel1.Controls.Add(label8, 2, 3);
            tableLayoutPanel1.Controls.Add(textBox4, 3, 4);
            tableLayoutPanel1.Controls.Add(textBox3, 3, 3);
            tableLayoutPanel1.Controls.Add(textBox2, 3, 1);
            tableLayoutPanel1.Controls.Add(textBox1, 1, 4);
            tableLayoutPanel1.Controls.Add(label5, 0, 4);
            tableLayoutPanel1.Controls.Add(label1, 0, 3);
            tableLayoutPanel1.Controls.Add(txtNotes, 1, 3);
            tableLayoutPanel1.Controls.Add(label3, 0, 0);
            tableLayoutPanel1.Controls.Add(label4, 2, 0);
            tableLayoutPanel1.Controls.Add(txtPatientName, 3, 0);
            tableLayoutPanel1.Controls.Add(lblDoctorNAme, 0, 1);
            tableLayoutPanel1.Controls.Add(txtDoctorName, 1, 1);
            tableLayoutPanel1.Controls.Add(label6, 2, 1);
            tableLayoutPanel1.Controls.Add(label11, 2, 2);
            tableLayoutPanel1.Controls.Add(address, 3, 2);
            tableLayoutPanel1.Controls.Add(label7, 0, 2);
            tableLayoutPanel1.Controls.Add(DateOfVisit, 1, 2);
            tableLayoutPanel1.Controls.Add(txtPatientCode, 1, 0);
            tableLayoutPanel1.Location = new Point(219, 53);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 36.9863F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 63.0137F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 59F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 91F));
            tableLayoutPanel1.Size = new Size(1374, 356);
            tableLayoutPanel1.TabIndex = 20;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.FromArgb(55, 71, 79);
            label9.Location = new Point(510, 264);
            label9.Name = "label9";
            label9.Size = new Size(73, 31);
            label9.TabIndex = 7;
            label9.Text = "الباقى";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.FromArgb(55, 71, 79);
            label8.Location = new Point(451, 174);
            label8.Name = "label8";
            label8.Size = new Size(132, 62);
            label8.TabIndex = 25;
            label8.Text = "الاجمالى بعد الخصم";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(179, 267);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(240, 38);
            textBox4.TabIndex = 6;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(179, 177);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(240, 38);
            textBox3.TabIndex = 24;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(179, 45);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(240, 38);
            textBox2.TabIndex = 23;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(944, 267);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(240, 38);
            textBox1.TabIndex = 21;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.FromArgb(55, 71, 79);
            label5.Location = new Point(1280, 264);
            label5.Name = "label5";
            label5.Size = new Size(91, 31);
            label5.TabIndex = 22;
            label5.Text = "المدفوع";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(55, 71, 79);
            label1.Location = new Point(1271, 174);
            label1.Name = "label1";
            label1.Size = new Size(100, 31);
            label1.TabIndex = 21;
            label1.Text = "ملاحظات";
            // 
            // txtNotes
            // 
            txtNotes.Location = new Point(944, 177);
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(240, 38);
            txtNotes.TabIndex = 20;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.ForeColor = Color.FromArgb(55, 71, 79);
            label11.Location = new Point(427, 115);
            label11.Name = "label11";
            label11.Size = new Size(156, 31);
            label11.TabIndex = 19;
            label11.Text = "نسبة الخصم %";
            // 
            // address
            // 
            address.Location = new Point(179, 118);
            address.Name = "address";
            address.Size = new Size(240, 38);
            address.TabIndex = 18;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.FromArgb(55, 71, 79);
            label7.Location = new Point(1244, 115);
            label7.Name = "label7";
            label7.RightToLeft = RightToLeft.Yes;
            label7.Size = new Size(127, 31);
            label7.TabIndex = 9;
            label7.Text = "تاريخ الزيارة";
            // 
            // DateOfVisit
            // 
            DateOfVisit.Location = new Point(767, 118);
            DateOfVisit.Name = "DateOfVisit";
            DateOfVisit.Size = new Size(417, 38);
            DateOfVisit.TabIndex = 15;
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
            pnlData.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Button button1;
        private Label label3;
        private TextBox txtPatientCode;
        private Label label4;
        private TextBox txtPatientName;
        private Label lblDoctorNAme;
        private TextBox txtDoctorName;
        private Label label6;
        private Panel pnlData;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label11;
        private TextBox address;
        private DateTimePicker DateOfVisit;
        private Label label7;
        private Label label2;
        private Panel pnlTitle;
        private Label label1;
        private TextBox txtNotes;
        private TextBox textBox1;
        private Label label5;
        private Label label9;
        private Label label8;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private Button btnSearch;
    }
}