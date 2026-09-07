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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
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
            dtSearchDateFrom = new DateTimePicker();
            dtSearchDateTo = new DateTimePicker();
            label6 = new Label();
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
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvvisits.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvvisits.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvvisits.Columns.AddRange(new DataGridViewColumn[] { colId, colMedicalCode, colName, colPhone, colGender, colAge, colEdit, colDelete });
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 11F);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvvisits.DefaultCellStyle = dataGridViewCellStyle4;
            dgvvisits.Dock = DockStyle.Fill;
            dgvvisits.EnableHeadersVisualStyles = false;
            dgvvisits.Location = new Point(0, 130);
            dgvvisits.MultiSelect = false;
            dgvvisits.Name = "dgvvisits";
            dgvvisits.ReadOnly = true;
            dgvvisits.RowHeadersVisible = false;
            dgvvisits.RowHeadersWidth = 51;
            dgvvisits.RowTemplate.Height = 45;
            dgvvisits.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvvisits.Size = new Size(1841, 524);
            dgvvisits.TabIndex = 9;
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
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 304F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 168F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 285F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 169F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 267F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 141F));
            tableLayoutPanel1.Controls.Add(dtSearchDateFrom, 7, 0);
            tableLayoutPanel1.Controls.Add(dtSearchDateTo, 5, 0);
            tableLayoutPanel1.Controls.Add(label6, 6, 0);
            tableLayoutPanel1.Controls.Add(label5, 4, 0);
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
            // dtSearchDateFrom
            // 
            dtSearchDateFrom.Location = new Point(155, 3);
            dtSearchDateFrom.Name = "dtSearchDateFrom";
            dtSearchDateFrom.Size = new Size(250, 27);
            dtSearchDateFrom.TabIndex = 11;
            // 
            // dtSearchDateTo
            // 
            dtSearchDateTo.Location = new Point(609, 3);
            dtSearchDateTo.Name = "dtSearchDateTo";
            dtSearchDateTo.Size = new Size(250, 27);
            dtSearchDateTo.TabIndex = 10;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(55, 71, 79);
            label6.Location = new Point(413, 0);
            label6.Name = "label6";
            label6.Size = new Size(161, 28);
            label6.TabIndex = 10;
            label6.Text = "بحث بالتاريخ الى :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(55, 71, 79);
            label5.Location = new Point(870, 0);
            label5.Name = "label5";
            label5.Size = new Size(157, 28);
            label5.TabIndex = 6;
            label5.Text = "بحث بالتاريخ من :";
            // 
            // txtSearchName
            // 
            txtSearchName.Location = new Point(1033, 3);
            txtSearchName.Name = "txtSearchName";
            txtSearchName.Size = new Size(298, 27);
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
            pnlSearch.Location = new Point(0, 60);
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
            pnlTitle.Size = new Size(1841, 60);
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
        private Label label6;
        private Label label5;
        private TextBox txtSearchName;
        private Button button1;
        private DateTimePicker dtSearchDateFrom;
        private DateTimePicker dtSearchDateTo;
    }
}