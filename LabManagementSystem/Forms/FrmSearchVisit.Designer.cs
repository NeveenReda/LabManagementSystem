namespace LabManagementSystem.Forms
{
    partial class FrmSearchVisit
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            colDelete = new DataGridViewImageColumn();
            colAge = new DataGridViewTextBoxColumn();
            colGender = new DataGridViewTextBoxColumn();
            colPhone = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colMedicalCode = new DataGridViewTextBoxColumn();
            colId = new DataGridViewTextBoxColumn();
            label3 = new Label();
            colEdit = new DataGridViewImageColumn();
            dgvvisits = new DataGridView();
            btnSearch = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel5 = new TableLayoutPanel();
            dtSearchDateTo = new DateTimePicker();
            tableLayoutPanel4 = new TableLayoutPanel();
            label6 = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            dtSearchDateFrom = new DateTimePicker();
            tableLayoutPanel2 = new TableLayoutPanel();
            chkSearchByDate = new CheckBox();
            label5 = new Label();
            txtSearchName = new TextBox();
            txtSearchCode = new TextBox();
            lblSearch = new Label();
            label2 = new Label();
            button1 = new Button();
            pnlSearch = new Panel();
            label1 = new Label();
            pnlTitle = new Panel();
            pnlBottom = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvvisits).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            pnlSearch.SuspendLayout();
            pnlTitle.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // colDelete
            // 
            colDelete.HeaderText = "حذف";
            colDelete.MinimumWidth = 6;
            colDelete.Name = "colDelete";
            colDelete.ReadOnly = true;
            colDelete.Resizable = DataGridViewTriState.True;
            // 
            // colAge
            // 
            colAge.DataPropertyName = "AgeAtRecord";
            colAge.HeaderText = "السن";
            colAge.MinimumWidth = 6;
            colAge.Name = "colAge";
            colAge.ReadOnly = true;
            // 
            // colGender
            // 
            colGender.DataPropertyName = "Gender";
            colGender.HeaderText = "النوع";
            colGender.MinimumWidth = 6;
            colGender.Name = "colGender";
            colGender.ReadOnly = true;
            // 
            // colPhone
            // 
            colPhone.DataPropertyName = "Phone";
            colPhone.HeaderText = "رقم الموبايل";
            colPhone.MinimumWidth = 6;
            colPhone.Name = "colPhone";
            colPhone.ReadOnly = true;
            // 
            // colName
            // 
            colName.DataPropertyName = "Name";
            colName.HeaderText = "اسم المريض";
            colName.MinimumWidth = 6;
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colMedicalCode
            // 
            colMedicalCode.DataPropertyName = "MedicalCode";
            colMedicalCode.HeaderText = "كود المريض";
            colMedicalCode.MinimumWidth = 6;
            colMedicalCode.Name = "colMedicalCode";
            colMedicalCode.ReadOnly = true;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.HeaderText = "م";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(946, 11);
            label3.Name = "label3";
            label3.Size = new Size(123, 20);
            label3.TabIndex = 0;
            label3.Text = "إجمالي المرضى : 0";
            // 
            // colEdit
            // 
            colEdit.HeaderText = "تعديل";
            colEdit.MinimumWidth = 6;
            colEdit.Name = "colEdit";
            colEdit.ReadOnly = true;
            colEdit.Resizable = DataGridViewTriState.True;
            // 
            // dgvvisits
            // 
            dgvvisits.AllowUserToAddRows = false;
            dgvvisits.AllowUserToDeleteRows = false;
            dgvvisits.AllowUserToResizeRows = false;
            dgvvisits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvvisits.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvvisits.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvvisits.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvvisits.Columns.AddRange(new DataGridViewColumn[] { colId, colMedicalCode, colName, colPhone, colGender, colAge, colEdit, colDelete });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvvisits.DefaultCellStyle = dataGridViewCellStyle2;
            dgvvisits.Dock = DockStyle.Fill;
            dgvvisits.EnableHeadersVisualStyles = false;
            dgvvisits.Location = new Point(0, 119);
            dgvvisits.MultiSelect = false;
            dgvvisits.Name = "dgvvisits";
            dgvvisits.ReadOnly = true;
            dgvvisits.RowHeadersVisible = false;
            dgvvisits.RowHeadersWidth = 51;
            dgvvisits.RowTemplate.Height = 45;
            dgvvisits.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvvisits.Size = new Size(1841, 535);
            dgvvisits.TabIndex = 9;
            dgvvisits.CellContentClick += dgvvisits_CellContentClick_1;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.RoyalBlue;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(116, 9);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(228, 35);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "إضافة زيارة ➕  ";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 9;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 113F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 117F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 265F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 163F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 329F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 91F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 345F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 141F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel5, 7, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 6, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 5, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 4, 0);
            tableLayoutPanel1.Controls.Add(txtSearchName, 3, 0);
            tableLayoutPanel1.Controls.Add(txtSearchCode, 1, 0);
            tableLayoutPanel1.Controls.Add(lblSearch, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 2, 0);
            tableLayoutPanel1.Controls.Add(button1, 8, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1841, 70);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Controls.Add(dtSearchDateTo, 0, 1);
            tableLayoutPanel5.Location = new Point(144, 3);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new Size(339, 64);
            tableLayoutPanel5.TabIndex = 10;
            // 
            // dtSearchDateTo
            // 
            dtSearchDateTo.Location = new Point(3, 35);
            dtSearchDateTo.Name = "dtSearchDateTo";
            dtSearchDateTo.Size = new Size(333, 27);
            dtSearchDateTo.TabIndex = 11;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(label6, 0, 1);
            tableLayoutPanel4.Location = new Point(500, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(74, 64);
            tableLayoutPanel4.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(55, 71, 79);
            label6.Location = new Point(33, 32);
            label6.Name = "label6";
            label6.Size = new Size(38, 25);
            label6.TabIndex = 10;
            label6.Text = "الى";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel3.Controls.Add(dtSearchDateFrom, 0, 1);
            tableLayoutPanel3.Location = new Point(597, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(306, 64);
            tableLayoutPanel3.TabIndex = 10;
            // 
            // dtSearchDateFrom
            // 
            dtSearchDateFrom.Location = new Point(3, 35);
            dtSearchDateFrom.Name = "dtSearchDateFrom";
            dtSearchDateFrom.Size = new Size(300, 27);
            dtSearchDateFrom.TabIndex = 10;
            dtSearchDateFrom.ValueChanged += dtSearchDateTo_ValueChanged;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(chkSearchByDate, 0, 0);
            tableLayoutPanel2.Controls.Add(label5, 0, 1);
            tableLayoutPanel2.Location = new Point(909, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 56.25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 43.75F));
            tableLayoutPanel2.Size = new Size(157, 64);
            tableLayoutPanel2.TabIndex = 10;
            // 
            // chkSearchByDate
            // 
            chkSearchByDate.AutoSize = true;
            chkSearchByDate.Font = new Font("Bodoni MT Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chkSearchByDate.ForeColor = SystemColors.ActiveCaptionText;
            chkSearchByDate.Location = new Point(36, 3);
            chkSearchByDate.Name = "chkSearchByDate";
            chkSearchByDate.Size = new Size(118, 28);
            chkSearchByDate.TabIndex = 7;
            chkSearchByDate.Text = "البحث بالتاريخ";
            chkSearchByDate.UseVisualStyleBackColor = true;
            chkSearchByDate.CheckedChanged += chkSearchByDate_CheckedChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(55, 71, 79);
            label5.Location = new Point(119, 36);
            label5.Name = "label5";
            label5.Size = new Size(35, 25);
            label5.TabIndex = 6;
            label5.Text = "من";
            // 
            // txtSearchName
            // 
            txtSearchName.Location = new Point(1072, 3);
            txtSearchName.Name = "txtSearchName";
            txtSearchName.Size = new Size(259, 27);
            txtSearchName.TabIndex = 5;
            txtSearchName.TextChanged += textBox1_TextChanged;
            // 
            // txtSearchCode
            // 
            txtSearchCode.Location = new Point(1454, 3);
            txtSearchCode.Name = "txtSearchCode";
            txtSearchCode.Size = new Size(271, 27);
            txtSearchCode.TabIndex = 3;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSearch.ForeColor = Color.FromArgb(55, 71, 79);
            lblSearch.Location = new Point(1734, 0);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(104, 28);
            lblSearch.TabIndex = 2;
            lblSearch.Text = "بحث بالكود";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(55, 71, 79);
            label2.Location = new Point(1340, 0);
            label2.Name = "label2";
            label2.Size = new Size(108, 28);
            label2.TabIndex = 4;
            label2.Text = "بحث بالاسم";
            // 
            // button1
            // 
            button1.BackColor = Color.RoyalBlue;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(135, 35);
            button1.TabIndex = 5;
            button1.Text = "بحث";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = Color.White;
            pnlSearch.Controls.Add(tableLayoutPanel1);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 49);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(1841, 70);
            pnlSearch.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.RoyalBlue;
            label1.Location = new Point(656, 9);
            label1.Name = "label1";
            label1.Size = new Size(252, 38);
            label1.TabIndex = 1;
            label1.Text = "شاشة عرض الزيارات";
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.Gainsboro;
            pnlTitle.Controls.Add(label1);
            pnlTitle.Controls.Add(btnSearch);
            pnlTitle.Dock = DockStyle.Top;
            pnlTitle.Location = new Point(0, 0);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Size = new Size(1841, 49);
            pnlTitle.TabIndex = 6;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(label3);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 654);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(1841, 40);
            pnlBottom.TabIndex = 8;
            // 
            // FrmSearchVisit
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1841, 694);
            Controls.Add(dgvvisits);
            Controls.Add(pnlSearch);
            Controls.Add(pnlTitle);
            Controls.Add(pnlBottom);
            Name = "FrmSearchVisit";
            RightToLeft = RightToLeft.Yes;
            Text = "ا";
            Load += FrmSearchVisit_Load;
            ((System.ComponentModel.ISupportInitialize)dgvvisits).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            pnlSearch.ResumeLayout(false);
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            pnlBottom.ResumeLayout(false);
            pnlBottom.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridViewImageColumn colDelete;
        private DataGridViewTextBoxColumn colAge;
        private DataGridViewTextBoxColumn colGender;
        private DataGridViewTextBoxColumn colPhone;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colMedicalCode;
        private DataGridViewTextBoxColumn colId;
        private Label label3;
        private DataGridViewImageColumn colEdit;
        private DataGridView dgvvisits;
        private Button btnSearch;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox txtSearchCode;
        private Label lblSearch;
        private Panel pnlSearch;
        private Label label1;
        private Panel pnlTitle;
        private Panel pnlBottom;
        private Label label2;
        private Label label5;
        private TextBox txtSearchName;
        private Button button1;
        private DateTimePicker dtSearchDateTo;
        private DateTimePicker dtSearchDateFrom;
        private TableLayoutPanel tableLayoutPanel2;
        private CheckBox chkSearchByDate;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label6;
    }
}