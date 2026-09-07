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
            colId = new DataGridViewTextBoxColumn();
            colLabName = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colExternal = new DataGridViewTextBoxColumn();
            colDelete = new DataGridViewImageColumn();
            label10 = new Label();
            button3 = new Button();
            cmbolabs = new ComboBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            btnAddLab = new Button();
            btnSave = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            label9 = new Label();
            label8 = new Label();
            DateOfVisit = new DateTimePicker();
            txtRemaining = new TextBox();
            txtAfterDiscount = new TextBox();
            textDiscountValue = new TextBox();
            label11 = new Label();
            textDiscountPercentage = new TextBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            btnAddPatient = new Button();
            txtPatientName = new TextBox();
            label12 = new Label();
            txtNotes = new TextBox();
            label1 = new Label();
            label7 = new Label();
            label5 = new Label();
            txtPayment = new TextBox();
            lbltxt = new Label();
            pnlTitle = new Panel();
            pnlData.SuspendLayout();
            tblAnalysis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVisitDetails).BeginInit();
            tableLayoutPanel3.SuspendLayout();
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
            txtPatientCode.Leave += txtPatientCode_Leave;
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
            pnlData.Size = new Size(1829, 714);
            pnlData.TabIndex = 5;
            pnlData.Paint += pnlData_Paint;
            // 
            // tblAnalysis
            // 
            tblAnalysis.AutoSize = true;
            tblAnalysis.ColumnCount = 3;
            tblAnalysis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52.0525436F));
            tblAnalysis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.9868641F));
            tblAnalysis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.96059F));
            tblAnalysis.Controls.Add(dgvVisitDetails, 0, 1);
            tblAnalysis.Controls.Add(label10, 0, 0);
            tblAnalysis.Controls.Add(button3, 2, 2);
            tblAnalysis.Controls.Add(cmbolabs, 1, 0);
            tblAnalysis.Controls.Add(tableLayoutPanel3, 2, 0);
            tblAnalysis.Dock = DockStyle.Top;
            tblAnalysis.Location = new Point(0, 268);
            tblAnalysis.Name = "tblAnalysis";
            tblAnalysis.RowCount = 3;
            tblAnalysis.RowStyles.Add(new RowStyle(SizeType.Percent, 3.12101912F));
            tblAnalysis.RowStyles.Add(new RowStyle(SizeType.Percent, 96.87898F));
            tblAnalysis.RowStyles.Add(new RowStyle(SizeType.Absolute, 13F));
            tblAnalysis.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblAnalysis.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblAnalysis.Size = new Size(1827, 1519);
            tblAnalysis.TabIndex = 22;
            tblAnalysis.Paint += tblAnalysis_Paint;
            // 
            // dgvVisitDetails
            // 
            dgvVisitDetails.AllowUserToAddRows = false;
            dgvVisitDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVisitDetails.Columns.AddRange(new DataGridViewColumn[] { colId, colLabName, colPrice, colExternal, colDelete });
            dgvVisitDetails.Dock = DockStyle.Fill;
            dgvVisitDetails.Location = new Point(879, 50);
            dgvVisitDetails.Name = "dgvVisitDetails";
            dgvVisitDetails.ReadOnly = true;
            dgvVisitDetails.RowHeadersVisible = false;
            dgvVisitDetails.RowHeadersWidth = 51;
            dgvVisitDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVisitDetails.Size = new Size(945, 1452);
            dgvVisitDetails.TabIndex = 24;
            dgvVisitDetails.CellContentClick += dgvVisitDetails_CellContentClick;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.HeaderText = "م";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Width = 125;
            // 
            // colLabName
            // 
            colLabName.DataPropertyName = "Name";
            colLabName.HeaderText = "اسم التحليل";
            colLabName.MinimumWidth = 6;
            colLabName.Name = "colLabName";
            colLabName.ReadOnly = true;
            colLabName.Width = 125;
            // 
            // colPrice
            // 
            colPrice.DataPropertyName = "Price";
            colPrice.HeaderText = "السعر";
            colPrice.MinimumWidth = 6;
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            colPrice.Width = 125;
            // 
            // colExternal
            // 
            colExternal.DataPropertyName = "ExternalType";
            colExternal.HeaderText = "نوع المعمل";
            colExternal.MinimumWidth = 6;
            colExternal.Name = "colExternal";
            colExternal.ReadOnly = true;
            colExternal.Width = 125;
            // 
            // colDelete
            // 
            colDelete.HeaderText = "حذف";
            colDelete.MinimumWidth = 6;
            colDelete.Name = "colDelete";
            colDelete.ReadOnly = true;
            colDelete.Resizable = DataGridViewTriState.True;
            colDelete.SortMode = DataGridViewColumnSortMode.Automatic;
            colDelete.Width = 125;
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
            // button3
            // 
            button3.BackColor = Color.MediumSeaGreen;
            button3.FlatStyle = FlatStyle.Flat;
            button3.ForeColor = Color.White;
            button3.Location = new Point(520, 1508);
            button3.Name = "button3";
            button3.Size = new Size(134, 8);
            button3.TabIndex = 25;
            button3.Text = "حفظ";
            button3.UseVisualStyleBackColor = false;
            // 
            // cmbolabs
            // 
            cmbolabs.Font = new Font("Tahoma", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbolabs.FormattingEnabled = true;
            cmbolabs.Location = new Point(660, 3);
            cmbolabs.Name = "cmbolabs";
            cmbolabs.Size = new Size(213, 30);
            cmbolabs.TabIndex = 15;
            cmbolabs.SelectedIndexChanged += cmbolabs_SelectedIndexChanged;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Controls.Add(btnAddLab, 0, 0);
            tableLayoutPanel3.Controls.Add(btnSave, 1, 0);
            tableLayoutPanel3.Location = new Point(404, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(250, 41);
            tableLayoutPanel3.TabIndex = 27;
            // 
            // btnAddLab
            // 
            btnAddLab.BackColor = Color.RoyalBlue;
            btnAddLab.FlatAppearance.BorderSize = 0;
            btnAddLab.FlatStyle = FlatStyle.Flat;
            btnAddLab.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAddLab.ForeColor = Color.White;
            btnAddLab.Location = new Point(129, 3);
            btnAddLab.Name = "btnAddLab";
            btnAddLab.Size = new Size(118, 35);
            btnAddLab.TabIndex = 23;
            btnAddLab.Text = "إضافة ➕  ";
            btnAddLab.UseVisualStyleBackColor = false;
            btnAddLab.Click += btnAddLab_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.MediumSeaGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(3, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(119, 35);
            btnSave.TabIndex = 26;
            btnSave.Text = "حفظ";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
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
            tableLayoutPanel1.Controls.Add(DateOfVisit, 1, 3);
            tableLayoutPanel1.Controls.Add(txtRemaining, 3, 4);
            tableLayoutPanel1.Controls.Add(txtAfterDiscount, 3, 3);
            tableLayoutPanel1.Controls.Add(textDiscountValue, 3, 1);
            tableLayoutPanel1.Controls.Add(label3, 0, 0);
            tableLayoutPanel1.Controls.Add(label4, 2, 0);
            tableLayoutPanel1.Controls.Add(txtTotal, 3, 0);
            tableLayoutPanel1.Controls.Add(lblDoctorNAme, 0, 1);
            tableLayoutPanel1.Controls.Add(txtDoctorName, 1, 1);
            tableLayoutPanel1.Controls.Add(label6, 2, 1);
            tableLayoutPanel1.Controls.Add(label11, 2, 2);
            tableLayoutPanel1.Controls.Add(textDiscountPercentage, 3, 2);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 0);
            tableLayoutPanel1.Controls.Add(txtNotes, 1, 2);
            tableLayoutPanel1.Controls.Add(label1, 0, 2);
            tableLayoutPanel1.Controls.Add(label7, 0, 3);
            tableLayoutPanel1.Controls.Add(label5, 0, 4);
            tableLayoutPanel1.Controls.Add(txtPayment, 1, 4);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50.7042236F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 49.2957764F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 39F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 69F));
            tableLayoutPanel1.Size = new Size(1827, 268);
            tableLayoutPanel1.TabIndex = 20;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.ForeColor = Color.FromArgb(55, 71, 79);
            label9.Location = new Point(510, 198);
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
            label8.Size = new Size(132, 48);
            label8.TabIndex = 25;
            label8.Text = "الاجمالى بعد الخصم";
            // 
            // DateOfVisit
            // 
            DateOfVisit.Location = new Point(1112, 153);
            DateOfVisit.Name = "DateOfVisit";
            DateOfVisit.Size = new Size(417, 38);
            DateOfVisit.TabIndex = 27;
            // 
            // txtRemaining
            // 
            txtRemaining.Location = new Point(179, 201);
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
            txtAfterDiscount.TextChanged += txtAfterDiscount_TextChanged;
            // 
            // textDiscountValue
            // 
            textDiscountValue.Location = new Point(179, 59);
            textDiscountValue.Name = "textDiscountValue";
            textDiscountValue.Size = new Size(240, 38);
            textDiscountValue.TabIndex = 23;
            textDiscountValue.TextChanged += textDiscountValue_TextChanged;
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
            // textDiscountPercentage
            // 
            textDiscountPercentage.Location = new Point(179, 114);
            textDiscountPercentage.Name = "textDiscountPercentage";
            textDiscountPercentage.Size = new Size(240, 38);
            textDiscountPercentage.TabIndex = 18;
            textDiscountPercentage.TextChanged += textDiscountPercentage_TextChanged;
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
            // txtNotes
            // 
            txtNotes.Location = new Point(1289, 114);
            txtNotes.Name = "txtNotes";
            txtNotes.Size = new Size(240, 38);
            txtNotes.TabIndex = 20;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(55, 71, 79);
            label1.Location = new Point(1724, 111);
            label1.Name = "label1";
            label1.Size = new Size(100, 31);
            label1.TabIndex = 21;
            label1.Text = "ملاحظات";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.FromArgb(55, 71, 79);
            label7.Location = new Point(1697, 150);
            label7.Name = "label7";
            label7.RightToLeft = RightToLeft.Yes;
            label7.Size = new Size(127, 31);
            label7.TabIndex = 26;
            label7.Text = "تاريخ الزيارة";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = Color.FromArgb(55, 71, 79);
            label5.Location = new Point(1733, 198);
            label5.Name = "label5";
            label5.Size = new Size(91, 31);
            label5.TabIndex = 22;
            label5.Text = "المدفوع";
            // 
            // txtPayment
            // 
            txtPayment.Location = new Point(1289, 201);
            txtPayment.Name = "txtPayment";
            txtPayment.Size = new Size(240, 38);
            txtPayment.TabIndex = 21;
            txtPayment.TextChanged += txtPayment_TextChanged;
            // 
            // lbltxt
            // 
            lbltxt.AutoSize = true;
            lbltxt.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbltxt.ForeColor = Color.RoyalBlue;
            lbltxt.Location = new Point(843, 19);
            lbltxt.Name = "lbltxt";
            lbltxt.Size = new Size(224, 38);
            lbltxt.TabIndex = 2;
            lbltxt.Text = "شاشة إضافة زيارة";
            lbltxt.Click += lbltxt_Click;
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.Gainsboro;
            pnlTitle.Controls.Add(lbltxt);
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
            tableLayoutPanel3.ResumeLayout(false);
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
        private TextBox textDiscountPercentage;
        private Label lbltxt;
        private Panel pnlTitle;
        private Label label1;
        private TextBox txtNotes;
        private TextBox txtPayment;
        private Label label5;
        private Label label9;
        private Label label8;
        private TextBox txtRemaining;
        private TextBox txtAfterDiscount;
        private TextBox textDiscountValue;
        private TableLayoutPanel tblAnalysis;
        private Button btnAddLab;
        private ComboBox cmbolabs;
        private Label label10;
        private DataGridView dgvVisitDetails;
        private Button button3;
        private Label label7;
        private DateTimePicker DateOfVisit;
        private Button btnAddPatient;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox txtPatientName;
        private Label label12;
        private Button btnSave;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colLabName;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colExternal;
        private DataGridViewImageColumn colDelete;
        private TableLayoutPanel tableLayoutPanel3;
    }
}