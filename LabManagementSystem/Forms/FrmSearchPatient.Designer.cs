namespace LabManagementSystem.Forms
{
    partial class FrmSearchPatient
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
            pnlTitle = new Panel();
            label1 = new Label();
            txtSearch = new TextBox();
            pnlSearch = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            picSearch = new PictureBox();
            btnSearch = new Button();
            lblSearch = new Label();
            pnlBottom = new Panel();
            label3 = new Label();
            dgvPatients = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colMedicalCode = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colPhone = new DataGridViewTextBoxColumn();
            colGender = new DataGridViewTextBoxColumn();
            colAge = new DataGridViewTextBoxColumn();
            colEdit = new DataGridViewImageColumn();
            colDelete = new DataGridViewImageColumn();
            pnlTitle.SuspendLayout();
            pnlSearch.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picSearch).BeginInit();
            pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPatients).BeginInit();
            SuspendLayout();
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.Gainsboro;
            pnlTitle.Controls.Add(label1);
            pnlTitle.Dock = DockStyle.Top;
            pnlTitle.Location = new Point(0, 0);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Size = new Size(1841, 60);
            pnlTitle.TabIndex = 0;
            pnlTitle.Paint += pnlTitle_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.RoyalBlue;
            label1.Location = new Point(656, 9);
            label1.Name = "label1";
            label1.Size = new Size(286, 38);
            label1.TabIndex = 1;
            label1.Text = "شاشة البحث عن مريض";
            label1.Click += label1_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(1190, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(349, 27);
            txtSearch.TabIndex = 3;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // pnlSearch
            // 
            pnlSearch.BackColor = Color.White;
            pnlSearch.Controls.Add(tableLayoutPanel1);
            pnlSearch.Dock = DockStyle.Top;
            pnlSearch.Location = new Point(0, 60);
            pnlSearch.Name = "pnlSearch";
            pnlSearch.Size = new Size(1841, 70);
            pnlSearch.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 299F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 274F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 902F));
            tableLayoutPanel1.Controls.Add(txtSearch, 1, 0);
            tableLayoutPanel1.Controls.Add(picSearch, 2, 0);
            tableLayoutPanel1.Controls.Add(btnSearch, 3, 0);
            tableLayoutPanel1.Controls.Add(lblSearch, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1841, 70);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // picSearch
            // 
            picSearch.Image = Properties.Resources.searchIcon1;
            picSearch.Location = new Point(1133, 3);
            picSearch.Name = "picSearch";
            picSearch.Size = new Size(40, 36);
            picSearch.SizeMode = PictureBoxSizeMode.StretchImage;
            picSearch.TabIndex = 2;
            picSearch.TabStop = false;
            picSearch.Click += pictureBox1_Click;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.RoyalBlue;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(745, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(154, 42);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "إضافة مريض ➕  ";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += button1_Click;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSearch.ForeColor = Color.FromArgb(55, 71, 79);
            lblSearch.Location = new Point(1599, 0);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(239, 28);
            lblSearch.TabIndex = 2;
            lblSearch.Text = "بحث بالاسم او كود المريض";
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(label3);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 654);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(1841, 40);
            pnlBottom.TabIndex = 4;
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
            // dgvPatients
            // 
            dgvPatients.AllowUserToAddRows = false;
            dgvPatients.AllowUserToDeleteRows = false;
            dgvPatients.AllowUserToResizeRows = false;
            dgvPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPatients.BackgroundColor = SystemColors.Control;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvPatients.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvPatients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPatients.Columns.AddRange(new DataGridViewColumn[] { colId, colMedicalCode, colName, colPhone, colGender, colAge, colEdit, colDelete });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvPatients.DefaultCellStyle = dataGridViewCellStyle2;
            dgvPatients.Dock = DockStyle.Fill;
            dgvPatients.EnableHeadersVisualStyles = false;
            dgvPatients.Location = new Point(0, 130);
            dgvPatients.MultiSelect = false;
            dgvPatients.Name = "dgvPatients";
            dgvPatients.ReadOnly = true;
            dgvPatients.RowHeadersVisible = false;
            dgvPatients.RowHeadersWidth = 51;
            dgvPatients.RowTemplate.Height = 45;
            dgvPatients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPatients.Size = new Size(1841, 524);
            dgvPatients.TabIndex = 5;
            dgvPatients.CellContentClick += dgvPatients_CellContentClick_1;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.HeaderText = "م";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colMedicalCode
            // 
            colMedicalCode.DataPropertyName = "MedicalCode";
            colMedicalCode.HeaderText = "كود المريض";
            colMedicalCode.MinimumWidth = 6;
            colMedicalCode.Name = "colMedicalCode";
            colMedicalCode.ReadOnly = true;
            // 
            // colName
            // 
            colName.DataPropertyName = "Name";
            colName.HeaderText = "اسم المريض";
            colName.MinimumWidth = 6;
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colPhone
            // 
            colPhone.DataPropertyName = "Phone";
            colPhone.HeaderText = "رقم الموبايل";
            colPhone.MinimumWidth = 6;
            colPhone.Name = "colPhone";
            colPhone.ReadOnly = true;
            // 
            // colGender
            // 
            colGender.DataPropertyName = "Gender";
            colGender.HeaderText = "النوع";
            colGender.MinimumWidth = 6;
            colGender.Name = "colGender";
            colGender.ReadOnly = true;
            // 
            // colAge
            // 
            colAge.DataPropertyName = "AgeAtRecord";
            colAge.HeaderText = "السن";
            colAge.MinimumWidth = 6;
            colAge.Name = "colAge";
            colAge.ReadOnly = true;
            // 
            // colEdit
            // 
            colEdit.HeaderText = "تعديل";
            colEdit.MinimumWidth = 6;
            colEdit.Name = "colEdit";
            colEdit.ReadOnly = true;
            colEdit.Resizable = DataGridViewTriState.True;
            // 
            // colDelete
            // 
            colDelete.HeaderText = "حذف";
            colDelete.MinimumWidth = 6;
            colDelete.Name = "colDelete";
            colDelete.ReadOnly = true;
            colDelete.Resizable = DataGridViewTriState.True;
            // 
            // FrmSearchPatient
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1841, 694);
            Controls.Add(dgvPatients);
            Controls.Add(pnlSearch);
            Controls.Add(pnlTitle);
            Controls.Add(pnlBottom);
            Name = "FrmSearchPatient";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Text = "FrmSearchPatient";
            Load += FrmSearchPatient_Load;
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            pnlSearch.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picSearch).EndInit();
            pnlBottom.ResumeLayout(false);
            pnlBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPatients).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTitle;
        private Label label1;
        private Panel pnlSearch;
        private TextBox txtSearch;
        private Button btnSearch;
        private PictureBox picSearch;
        private Label lblSearch;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel pnlBottom;
        private Label label3;
        private DataGridView dgvPatients;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colMedicalCode;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colPhone;
        private DataGridViewTextBoxColumn colGender;
        private DataGridViewTextBoxColumn colAge;
        private DataGridViewImageColumn colEdit;
        private DataGridViewImageColumn colDelete;
      
    }
}