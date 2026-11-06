namespace LabManagementSystem.Forms
{
    partial class FrmSearchPatient
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblHeader = new Label();
            lblSubtitle = new Label();
            btnAddPatient = new Button();
            pnlSearchCard = new Panel();
            lblSearchPrompt = new Label();
            txtSearch = new TextBox();
            btnRefresh = new Button();
            lblResultsCount = new Label();
            pnlGridCard = new Panel();
            dgvPatients = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colMedicalCode = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colPhone = new DataGridViewTextBoxColumn();
            colGender = new DataGridViewTextBoxColumn();
            colAge = new DataGridViewTextBoxColumn();
            colEdit = new DataGridViewImageColumn();
            colDelete = new DataGridViewImageColumn();
            pnlHeader.SuspendLayout();
            pnlSearchCard.SuspendLayout();
            pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPatients).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(23, 43, 77);
            pnlHeader.Controls.Add(btnAddPatient);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblHeader);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 68;
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 10, 20, 10);
            pnlHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            lblHeader.Dock = DockStyle.Top;
            lblHeader.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblHeader.ForeColor = Color.White;
            lblHeader.Height = 28;
            lblHeader.Name = "lblHeader";
            lblHeader.Text = "👥 سجل وبحث بيانات المرضى";
            lblHeader.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Dock = DockStyle.Bottom;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubtitle.Height = 18;
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Text = "البحث الفوري بالكود الطبي، الاسم، أو رقم الهاتف وتعديل أو حذف السجلات";
            lblSubtitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnAddPatient
            // 
            btnAddPatient.Dock = DockStyle.Left;
            btnAddPatient.BackColor = Color.FromArgb(37, 99, 235);
            btnAddPatient.FlatAppearance.BorderSize = 0;
            btnAddPatient.FlatStyle = FlatStyle.Flat;
            btnAddPatient.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddPatient.ForeColor = Color.White;
            btnAddPatient.Name = "btnAddPatient";
            btnAddPatient.Size = new Size(160, 40);
            btnAddPatient.TabIndex = 2;
            btnAddPatient.Text = "➕ مريض جديد";
            btnAddPatient.UseVisualStyleBackColor = false;
            btnAddPatient.Click += btnAddPatient_Click;
            // 
            // pnlSearchCard
            // 
            pnlSearchCard.BackColor = Color.White;
            pnlSearchCard.Controls.Add(lblResultsCount);
            pnlSearchCard.Controls.Add(btnRefresh);
            pnlSearchCard.Controls.Add(txtSearch);
            pnlSearchCard.Controls.Add(lblSearchPrompt);
            pnlSearchCard.Dock = DockStyle.Top;
            pnlSearchCard.Height = 62;
            pnlSearchCard.Name = "pnlSearchCard";
            pnlSearchCard.Padding = new Padding(16, 12, 16, 12);
            pnlSearchCard.TabIndex = 1;
            // 
            // lblSearchPrompt
            // 
            lblSearchPrompt.Dock = DockStyle.Right;
            lblSearchPrompt.AutoSize = false;
            lblSearchPrompt.Width = 85;
            lblSearchPrompt.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSearchPrompt.ForeColor = Color.FromArgb(30, 41, 59);
            lblSearchPrompt.Name = "lblSearchPrompt";
            lblSearchPrompt.Text = "بحث سريع:";
            lblSearchPrompt.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtSearch
            // 
            txtSearch.Dock = DockStyle.Right;
            txtSearch.Font = new Font("Segoe UI", 10.5F);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "اكتب اسم المريض، الكود الطبي، أو الهاتف...";
            txtSearch.Size = new Size(380, 26);
            txtSearch.TabIndex = 0;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // btnRefresh
            // 
            btnRefresh.Dock = DockStyle.Right;
            btnRefresh.BackColor = Color.White;
            btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnRefresh.FlatStyle = FlatStyle.Flat;
            btnRefresh.Font = new Font("Segoe UI", 9.5F);
            btnRefresh.ForeColor = Color.FromArgb(30, 41, 59);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(110, 38);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "🔄 تحديث";
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // lblResultsCount
            // 
            lblResultsCount.Dock = DockStyle.Left;
            lblResultsCount.AutoSize = true;
            lblResultsCount.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblResultsCount.ForeColor = Color.FromArgb(100, 116, 139);
            lblResultsCount.Name = "lblResultsCount";
            lblResultsCount.Text = "المرضى المعروضين: 0";
            lblResultsCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlGridCard
            // 
            pnlGridCard.BackColor = Color.White;
            pnlGridCard.Controls.Add(dgvPatients);
            pnlGridCard.Dock = DockStyle.Fill;
            pnlGridCard.Padding = new Padding(12);
            pnlGridCard.Name = "pnlGridCard";
            pnlGridCard.TabIndex = 2;
            // 
            // dgvPatients
            // 
            dgvPatients.AllowUserToAddRows = false;
            dgvPatients.AllowUserToDeleteRows = false;
            dgvPatients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPatients.BackgroundColor = Color.White;
            dgvPatients.BorderStyle = BorderStyle.None;
            dgvPatients.ColumnHeadersHeight = 40;
            dgvPatients.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPatients.Columns.AddRange(new DataGridViewColumn[] {
                colId,
                colMedicalCode,
                colName,
                colPhone,
                colGender,
                colAge,
                colEdit,
                colDelete
            });
            dgvPatients.Dock = DockStyle.Fill;
            dgvPatients.Location = new Point(12, 12);
            dgvPatients.MultiSelect = false;
            dgvPatients.Name = "dgvPatients";
            dgvPatients.ReadOnly = true;
            dgvPatients.RowHeadersVisible = false;
            dgvPatients.RowTemplate.Height = 38;
            dgvPatients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPatients.Size = new Size(1176, 510);
            dgvPatients.TabIndex = 0;
            dgvPatients.CellContentClick += dgvPatients_CellContentClick;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.FillWeight = 10F;
            colId.HeaderText = "#";
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colMedicalCode
            // 
            colMedicalCode.DataPropertyName = "MedicalCode";
            colMedicalCode.FillWeight = 20F;
            colMedicalCode.HeaderText = "الكود الطبي";
            colMedicalCode.Name = "colMedicalCode";
            colMedicalCode.ReadOnly = true;
            // 
            // colName
            // 
            colName.DataPropertyName = "Name";
            colName.FillWeight = 32F;
            colName.HeaderText = "اسم المريض";
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colPhone
            // 
            colPhone.DataPropertyName = "Phone";
            colPhone.FillWeight = 18F;
            colPhone.HeaderText = "رقم الهاتف";
            colPhone.Name = "colPhone";
            colPhone.ReadOnly = true;
            // 
            // colGender
            // 
            colGender.DataPropertyName = "Gender";
            colGender.FillWeight = 12F;
            colGender.HeaderText = "النوع";
            colGender.Name = "colGender";
            colGender.ReadOnly = true;
            // 
            // colAge
            // 
            colAge.DataPropertyName = "AgeAtRecord";
            colAge.FillWeight = 12F;
            colAge.HeaderText = "السن";
            colAge.Name = "colAge";
            colAge.ReadOnly = true;
            // 
            // colEdit
            // 
            colEdit.FillWeight = 10F;
            colEdit.HeaderText = "تعديل";
            colEdit.Name = "colEdit";
            colEdit.ReadOnly = true;
            // 
            // colDelete
            // 
            colDelete.FillWeight = 10F;
            colDelete.HeaderText = "حذف";
            colDelete.Name = "colDelete";
            colDelete.ReadOnly = true;
            // 
            // FrmSearchPatient
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 251);
            ClientSize = new Size(1200, 680);
            Controls.Add(pnlGridCard);
            Controls.Add(pnlSearchCard);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9.5F);
            Name = "FrmSearchPatient";
            Padding = new Padding(16);
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Text = "سجل وبيانات المرضى";
            Load += FrmSearchPatient_Load;
            pnlHeader.ResumeLayout(false);
            pnlSearchCard.ResumeLayout(false);
            pnlSearchCard.PerformLayout();
            pnlGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPatients).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeader;
        private Label lblSubtitle;
        private Button btnAddPatient;
        private Panel pnlSearchCard;
        private Label lblSearchPrompt;
        private TextBox txtSearch;
        private Button btnRefresh;
        private Label lblResultsCount;
        private Panel pnlGridCard;
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
