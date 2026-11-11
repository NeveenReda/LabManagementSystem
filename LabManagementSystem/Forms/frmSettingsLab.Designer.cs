namespace LabManagementSystem.Forms
{
    partial class frmSettingsLab
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
            lblSubtitle = new Label();
            lblTitle = new Label();
            pnlInputsCard = new Panel();
            grpLabForm = new GroupBox();
            tblInputs = new TableLayoutPanel();
            lblLabName = new Label();
            txtName = new TextBox();
            lblCategory = new Label();
            cmbLabCat = new ComboBox();
            lblPrice = new Label();
            txtPrice = new TextBox();
            lblDays = new Label();
            txtDays = new TextBox();
            chkExternal = new CheckBox();
            lblValidation = new Label();
            pnlActions = new Panel();
            flpActions = new FlowLayoutPanel();
            btnSave = new Button();
            btnNew = new Button();
            btnOpenParams = new Button();
            pnlGridCard = new Panel();
            dgvLabs = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colCategory = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colDays = new DataGridViewTextBoxColumn();
            colExternal = new DataGridViewTextBoxColumn();
            colEdit = new DataGridViewImageColumn();
            pnlHeader.SuspendLayout();
            pnlInputsCard.SuspendLayout();
            grpLabForm.SuspendLayout();
            tblInputs.SuspendLayout();
            pnlActions.SuspendLayout();
            flpActions.SuspendLayout();
            pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLabs).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(23, 43, 77);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 65;
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 10, 20, 10);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Height = 28;
            lblTitle.Name = "lblTitle";
            lblTitle.Text = "⚙️ إعدادات الفحوصات والتحاليل الطبية";
            lblTitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Dock = DockStyle.Bottom;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubtitle.Height = 18;
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Text = "تعريف التحاليل، تسعير الفحوصات، ربط الأقسام، وتحديد باراميترات ومعدلات النتائج الطبيعية";
            lblSubtitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlInputsCard
            // 
            pnlInputsCard.BackColor = Color.White;
            pnlInputsCard.Controls.Add(grpLabForm);
            pnlInputsCard.Dock = DockStyle.Top;
            pnlInputsCard.Height = 205;
            pnlInputsCard.Location = new Point(16, 86);
            pnlInputsCard.Name = "pnlInputsCard";
            pnlInputsCard.Padding = new Padding(12);
            pnlInputsCard.TabIndex = 1;
            // 
            // grpLabForm
            // 
            grpLabForm.Controls.Add(pnlActions);
            grpLabForm.Controls.Add(lblValidation);
            grpLabForm.Controls.Add(tblInputs);
            grpLabForm.Dock = DockStyle.Fill;
            grpLabForm.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpLabForm.ForeColor = Color.FromArgb(23, 43, 77);
            grpLabForm.Name = "grpLabForm";
            grpLabForm.Padding = new Padding(12, 8, 12, 8);
            grpLabForm.RightToLeft = RightToLeft.Yes;
            grpLabForm.TabIndex = 0;
            grpLabForm.TabStop = false;
            grpLabForm.Text = "إضافة / تعديل بيانات التحليل";
            // 
            // tblInputs
            // 
            tblInputs.ColumnCount = 5;
            tblInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            tblInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tblInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            tblInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tblInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblInputs.Controls.Add(lblLabName, 0, 0);
            tblInputs.Controls.Add(txtName, 1, 0);
            tblInputs.Controls.Add(lblCategory, 2, 0);
            tblInputs.Controls.Add(cmbLabCat, 3, 0);
            tblInputs.Controls.Add(lblPrice, 0, 1);
            tblInputs.Controls.Add(txtPrice, 1, 1);
            tblInputs.Controls.Add(lblDays, 2, 1);
            tblInputs.Controls.Add(txtDays, 3, 1);
            tblInputs.Controls.Add(chkExternal, 4, 0);
            tblInputs.Dock = DockStyle.Top;
            tblInputs.Height = 85;
            tblInputs.Name = "tblInputs";
            tblInputs.RowCount = 2;
            tblInputs.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblInputs.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblInputs.TabIndex = 0;
            // 
            // lblLabName
            // 
            lblLabName.Dock = DockStyle.Fill;
            lblLabName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblLabName.ForeColor = Color.FromArgb(30, 41, 59);
            lblLabName.Name = "lblLabName";
            lblLabName.Text = "اسم التحليل:";
            lblLabName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            txtName.Dock = DockStyle.Fill;
            txtName.Font = new Font("Segoe UI", 10F);
            txtName.Margin = new Padding(4, 6, 4, 6);
            txtName.Name = "txtName";
            txtName.PlaceholderText = "مثال: Complete Blood Picture (CBC)...";
            txtName.TabIndex = 0;
            // 
            // lblCategory
            // 
            lblCategory.Dock = DockStyle.Fill;
            lblCategory.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCategory.ForeColor = Color.FromArgb(30, 41, 59);
            lblCategory.Name = "lblCategory";
            lblCategory.Text = "قسم التحليل:";
            lblCategory.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbLabCat
            // 
            cmbLabCat.Dock = DockStyle.Fill;
            cmbLabCat.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLabCat.Font = new Font("Segoe UI", 10F);
            cmbLabCat.Margin = new Padding(4, 6, 4, 6);
            cmbLabCat.Name = "cmbLabCat";
            cmbLabCat.TabIndex = 1;
            // 
            // lblPrice
            // 
            lblPrice.Dock = DockStyle.Fill;
            lblPrice.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPrice.ForeColor = Color.FromArgb(30, 41, 59);
            lblPrice.Name = "lblPrice";
            lblPrice.Text = "السعر (ج.م):";
            lblPrice.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPrice
            // 
            txtPrice.Dock = DockStyle.Fill;
            txtPrice.Font = new Font("Segoe UI", 10F);
            txtPrice.Margin = new Padding(4, 6, 4, 6);
            txtPrice.Name = "txtPrice";
            txtPrice.PlaceholderText = "0.00";
            txtPrice.TabIndex = 2;
            // 
            // lblDays
            // 
            lblDays.Dock = DockStyle.Fill;
            lblDays.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDays.ForeColor = Color.FromArgb(30, 41, 59);
            lblDays.Name = "lblDays";
            lblDays.Text = "أيام الاستلام:";
            lblDays.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDays
            // 
            txtDays.Dock = DockStyle.Fill;
            txtDays.Font = new Font("Segoe UI", 10F);
            txtDays.Margin = new Padding(4, 6, 4, 6);
            txtDays.Name = "txtDays";
            txtDays.Text = "1";
            txtDays.TabIndex = 3;
            // 
            // chkExternal
            // 
            chkExternal.Dock = DockStyle.Fill;
            chkExternal.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            chkExternal.ForeColor = Color.FromArgb(30, 41, 59);
            chkExternal.Location = new Point(4, 6);
            chkExternal.Name = "chkExternal";
            chkExternal.Size = new Size(180, 30);
            chkExternal.TabIndex = 4;
            chkExternal.Text = "تحليل خارجي (External)";
            chkExternal.UseVisualStyleBackColor = true;
            // 
            // lblValidation
            // 
            lblValidation.Dock = DockStyle.Top;
            lblValidation.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblValidation.ForeColor = Color.FromArgb(220, 38, 38);
            lblValidation.Height = 20;
            lblValidation.Name = "lblValidation";
            lblValidation.RightToLeft = RightToLeft.No;
            lblValidation.Text = "";
            lblValidation.TextAlign = ContentAlignment.MiddleRight;
            lblValidation.Visible = false;
            // 
            // pnlActions
            // 
            pnlActions.Controls.Add(flpActions);
            pnlActions.Dock = DockStyle.Bottom;
            pnlActions.Height = 48;
            pnlActions.Name = "pnlActions";
            pnlActions.Padding = new Padding(0, 6, 0, 0);
            pnlActions.TabIndex = 1;
            // 
            // flpActions
            // 
            flpActions.Dock = DockStyle.Fill;
            flpActions.FlowDirection = FlowDirection.LeftToRight;
            flpActions.Name = "flpActions";
            flpActions.RightToLeft = RightToLeft.Yes;
            flpActions.WrapContents = false;
            flpActions.Controls.Add(btnSave);
            flpActions.Controls.Add(btnNew);
            flpActions.Controls.Add(btnOpenParams);
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(22, 163, 74);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Margin = new Padding(4, 2, 4, 2);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(155, 36);
            btnSave.TabIndex = 0;
            btnSave.Text = "💾 حفظ التحليل";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnNew
            // 
            btnNew.BackColor = Color.White;
            btnNew.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnNew.FlatStyle = FlatStyle.Flat;
            btnNew.Font = new Font("Segoe UI", 9.5F);
            btnNew.ForeColor = Color.FromArgb(30, 41, 59);
            btnNew.Margin = new Padding(4, 2, 4, 2);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(120, 36);
            btnNew.TabIndex = 1;
            btnNew.Text = "➕ تحليل جديد";
            btnNew.UseVisualStyleBackColor = false;
            btnNew.Click += btnNew_Click;
            // 
            // btnOpenParams
            // 
            btnOpenParams.BackColor = Color.FromArgb(37, 99, 235);
            btnOpenParams.FlatAppearance.BorderSize = 0;
            btnOpenParams.FlatStyle = FlatStyle.Flat;
            btnOpenParams.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnOpenParams.ForeColor = Color.White;
            btnOpenParams.Margin = new Padding(4, 2, 4, 2);
            btnOpenParams.Name = "btnOpenParams";
            btnOpenParams.Size = new Size(255, 36);
            btnOpenParams.TabIndex = 2;
            btnOpenParams.Text = "⚙️ إعداد باراميترات ومعدلات التحليل";
            btnOpenParams.UseVisualStyleBackColor = false;
            btnOpenParams.Click += btnOpenParams_Click;
            // 
            // pnlGridCard
            // 
            pnlGridCard.BackColor = Color.White;
            pnlGridCard.Controls.Add(dgvLabs);
            pnlGridCard.Dock = DockStyle.Fill;
            pnlGridCard.Location = new Point(16, 246);
            pnlGridCard.Name = "pnlGridCard";
            pnlGridCard.Padding = new Padding(12);
            pnlGridCard.TabIndex = 2;
            // 
            // dgvLabs
            // 
            dgvLabs.AllowUserToAddRows = false;
            dgvLabs.AllowUserToDeleteRows = false;
            dgvLabs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLabs.BackgroundColor = Color.White;
            dgvLabs.BorderStyle = BorderStyle.None;
            dgvLabs.ColumnHeadersHeight = 38;
            dgvLabs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvLabs.Columns.AddRange(new DataGridViewColumn[] {
                colId,
                colName,
                colCategory,
                colPrice,
                colDays,
                colExternal,
                colEdit
            });
            dgvLabs.Dock = DockStyle.Fill;
            dgvLabs.Location = new Point(12, 12);
            dgvLabs.MultiSelect = false;
            dgvLabs.Name = "dgvLabs";
            dgvLabs.ReadOnly = true;
            dgvLabs.RowHeadersVisible = false;
            dgvLabs.RowTemplate.Height = 36;
            dgvLabs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLabs.Size = new Size(1056, 380);
            dgvLabs.TabIndex = 0;
            dgvLabs.CellContentClick += dgvLabs_CellContentClick;
            // 
            // colId
            // 
            colId.DataPropertyName = "Id";
            colId.FillWeight = 10F;
            colId.HeaderText = "#";
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colName
            // 
            colName.DataPropertyName = "Name";
            colName.FillWeight = 38F;
            colName.HeaderText = "اسم التحليل الطبي";
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colCategory
            // 
            colCategory.DataPropertyName = "CategoryName";
            colCategory.FillWeight = 22F;
            colCategory.HeaderText = "القسم / التصنيف";
            colCategory.Name = "colCategory";
            colCategory.ReadOnly = true;
            // 
            // colPrice
            // 
            colPrice.DataPropertyName = "Price";
            colPrice.FillWeight = 14F;
            colPrice.HeaderText = "السعر (ج.م)";
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            // 
            // colDays
            // 
            colDays.DataPropertyName = "Days";
            colDays.FillWeight = 12F;
            colDays.HeaderText = "أيام الاستلام";
            colDays.Name = "colDays";
            colDays.ReadOnly = true;
            // 
            // colExternal
            // 
            colExternal.DataPropertyName = "IsExternalText";
            colExternal.FillWeight = 12F;
            colExternal.HeaderText = "طبيعة الفحص";
            colExternal.Name = "colExternal";
            colExternal.ReadOnly = true;
            // 
            // colEdit
            // 
            colEdit.FillWeight = 8F;
            colEdit.HeaderText = "تعديل";
            colEdit.Name = "colEdit";
            colEdit.ReadOnly = true;
            // 
            // frmSettingsLab
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 251);
            ClientSize = new Size(1110, 650);
            Controls.Add(pnlGridCard);
            Controls.Add(pnlInputsCard);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9.5F);
            Name = "frmSettingsLab";
            Padding = new Padding(16);
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "إعدادات الفحوصات والتحاليل";
            Load += frmSettingsLab_Load;
            pnlHeader.ResumeLayout(false);
            pnlInputsCard.ResumeLayout(false);
            grpLabForm.ResumeLayout(false);
            tblInputs.ResumeLayout(false);
            tblInputs.PerformLayout();
            pnlActions.ResumeLayout(false);
            flpActions.ResumeLayout(false);
            pnlGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLabs).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlInputsCard;
        private GroupBox grpLabForm;
        private TableLayoutPanel tblInputs;
        private Label lblValidation;
        private Label lblLabName;
        private TextBox txtName;
        private Label lblCategory;
        private ComboBox cmbLabCat;
        private Label lblPrice;
        private TextBox txtPrice;
        private Label lblDays;
        private TextBox txtDays;
        private CheckBox chkExternal;
        private Panel pnlActions;
        private FlowLayoutPanel flpActions;
        private Button btnSave;
        private Button btnNew;
        private Button btnOpenParams;
        private Panel pnlGridCard;
        private DataGridView dgvLabs;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colCategory;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colDays;
        private DataGridViewTextBoxColumn colExternal;
        private DataGridViewImageColumn colEdit;
    }
}
