namespace LabManagementSystem.Forms
{
    partial class frmSettingsLab
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
            lblTitle = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            label5 = new Label();
            label4 = new Label();
            cmbLabCat = new ComboBox();
            chkExternal = new CheckBox();
            txtName = new TextBox();
            txtDays = new TextBox();
            label3 = new Label();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            txtPrice = new TextBox();
            dgvLabs = new DataGridView();
            Id = new DataGridViewTextBoxColumn();
            LabCategoryId = new DataGridViewTextBoxColumn();
            CategoryName = new DataGridViewTextBoxColumn();
            LabName = new DataGridViewTextBoxColumn();
            Price = new DataGridViewTextBoxColumn();
            Days = new DataGridViewTextBoxColumn();
            colEdit = new DataGridViewImageColumn();
            pnlTitle.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLabs).BeginInit();
            SuspendLayout();
            // 
            // pnlTitle
            // 
            pnlTitle.BackColor = Color.Gainsboro;
            pnlTitle.Controls.Add(lblTitle);
            pnlTitle.Dock = DockStyle.Top;
            pnlTitle.Location = new Point(0, 0);
            pnlTitle.Margin = new Padding(3, 2, 3, 2);
            pnlTitle.Name = "pnlTitle";
            pnlTitle.Size = new Size(873, 52);
            pnlTitle.TabIndex = 2;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.RoyalBlue;
            lblTitle.Location = new Point(333, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(225, 30);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "شاشة اعدادات  التحاليل";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.None;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 64.12698F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.8730164F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 248F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 141F));
            tableLayoutPanel1.Controls.Add(label5, 1, 1);
            tableLayoutPanel1.Controls.Add(label4, 3, 2);
            tableLayoutPanel1.Controls.Add(cmbLabCat, 2, 2);
            tableLayoutPanel1.Controls.Add(chkExternal, 0, 1);
            tableLayoutPanel1.Controls.Add(txtName, 2, 0);
            tableLayoutPanel1.Controls.Add(txtDays, 2, 1);
            tableLayoutPanel1.Controls.Add(label3, 3, 1);
            tableLayoutPanel1.Controls.Add(button1, 0, 2);
            tableLayoutPanel1.Controls.Add(label1, 3, 0);
            tableLayoutPanel1.Controls.Add(label2, 1, 0);
            tableLayoutPanel1.Controls.Add(txtPrice, 0, 0);
            tableLayoutPanel1.Controls.Add(dgvLabs, 0, 3);
            tableLayoutPanel1.Location = new Point(188, 63);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 52.33645F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 47.66355F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 252F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(660, 416);
            tableLayoutPanel1.TabIndex = 4;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.ForeColor = Color.Black;
            label5.Location = new Point(186, 74);
            label5.Name = "label5";
            label5.Size = new Size(71, 15);
            label5.TabIndex = 10;
            label5.Text = "معمل خارجى";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.ForeColor = Color.Black;
            label4.Location = new Point(559, 127);
            label4.Name = "label4";
            label4.Size = new Size(60, 15);
            label4.TabIndex = 9;
            label4.Text = "فئة التحليل";
            // 
            // cmbLabCat
            // 
            cmbLabCat.Anchor = AnchorStyles.None;
            cmbLabCat.ForeColor = Color.Black;
            cmbLabCat.FormattingEnabled = true;
            cmbLabCat.Location = new Point(301, 123);
            cmbLabCat.Name = "cmbLabCat";
            cmbLabCat.Size = new Size(185, 23);
            cmbLabCat.TabIndex = 11;
            // 
            // chkExternal
            // 
            chkExternal.Anchor = AnchorStyles.None;
            chkExternal.AutoSize = true;
            chkExternal.ForeColor = Color.Black;
            chkExternal.Location = new Point(79, 74);
            chkExternal.Name = "chkExternal";
            chkExternal.Size = new Size(15, 14);
            chkExternal.TabIndex = 12;
            chkExternal.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.None;
            txtName.ForeColor = Color.Black;
            txtName.Location = new Point(301, 16);
            txtName.Name = "txtName";
            txtName.Size = new Size(185, 23);
            txtName.TabIndex = 7;
            // 
            // txtDays
            // 
            txtDays.Anchor = AnchorStyles.None;
            txtDays.ForeColor = Color.Black;
            txtDays.Location = new Point(301, 70);
            txtDays.Name = "txtDays";
            txtDays.Size = new Size(185, 23);
            txtDays.TabIndex = 6;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.ForeColor = Color.Black;
            label3.Location = new Point(561, 74);
            label3.Name = "label3";
            label3.Size = new Size(56, 15);
            label3.TabIndex = 4;
            label3.Text = "عدد الأيام ";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.None;
            button1.ForeColor = SystemColors.ActiveCaptionText;
            button1.Location = new Point(19, 114);
            button1.Name = "button1";
            button1.Size = new Size(135, 42);
            button1.TabIndex = 1;
            button1.Text = "اضافة";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.ForeColor = Color.Black;
            label1.Location = new Point(572, 20);
            label1.Name = "label1";
            label1.Size = new Size(34, 15);
            label1.TabIndex = 2;
            label1.Text = "الاسم";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.ForeColor = Color.Black;
            label2.Location = new Point(203, 20);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 3;
            label2.Text = "السعر";
            // 
            // txtPrice
            // 
            txtPrice.Anchor = AnchorStyles.None;
            txtPrice.ForeColor = Color.Black;
            txtPrice.Location = new Point(25, 16);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(122, 23);
            txtPrice.TabIndex = 5;
            // 
            // dgvLabs
            // 
            dgvLabs.Anchor = AnchorStyles.None;
            dgvLabs.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dgvLabs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLabs.Columns.AddRange(new DataGridViewColumn[] { Id, LabCategoryId, CategoryName, LabName, Price, Days, colEdit });
            tableLayoutPanel1.SetColumnSpan(dgvLabs, 4);
            dgvLabs.Location = new Point(7, 191);
            dgvLabs.Name = "dgvLabs";
            dgvLabs.RightToLeft = RightToLeft.Yes;
            dgvLabs.Size = new Size(645, 197);
            dgvLabs.TabIndex = 8;
            dgvLabs.CellContentClick += dgvLabs_CellContentClick_1;
            // 
            // Id
            // 
            Id.DataPropertyName = "Id";
            Id.HeaderText = "Id";
            Id.Name = "Id";
            Id.Visible = false;
            // 
            // LabCategoryId
            // 
            LabCategoryId.DataPropertyName = "LabCategoryId";
            LabCategoryId.HeaderText = "LabCategoryId";
            LabCategoryId.Name = "LabCategoryId";
            LabCategoryId.ReadOnly = true;
            LabCategoryId.Visible = false;
            // 
            // CategoryName
            // 
            CategoryName.DataPropertyName = "CategoryName";
            CategoryName.HeaderText = "فئة التحليل";
            CategoryName.MinimumWidth = 100;
            CategoryName.Name = "CategoryName";
            CategoryName.ReadOnly = true;
            CategoryName.Width = 150;
            // 
            // LabName
            // 
            LabName.DataPropertyName = "Name";
            LabName.HeaderText = "التحليل";
            LabName.MinimumWidth = 150;
            LabName.Name = "LabName";
            LabName.ReadOnly = true;
            LabName.Width = 150;
            // 
            // Price
            // 
            Price.DataPropertyName = "Price";
            Price.HeaderText = "السعر";
            Price.Name = "Price";
            Price.ReadOnly = true;
            // 
            // Days
            // 
            Days.DataPropertyName = "Days";
            Days.HeaderText = "عدد الايام";
            Days.Name = "Days";
            Days.ReadOnly = true;
            // 
            // colEdit
            // 
            colEdit.HeaderText = "تعديل";
            colEdit.Name = "colEdit";
            // 
            // frmSettingsLab
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(873, 534);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(pnlTitle);
            Name = "frmSettingsLab";
            Text = "frmSettingsLab";
            Load += frmSettingsLab_Load;
            pnlTitle.ResumeLayout(false);
            pnlTitle.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLabs).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlTitle;
        private Label lblTitle;
        private TableLayoutPanel tableLayoutPanel1;
        private Button button1;
        private Label label1;
        private Label label3;
        private Label label2;
        private TextBox txtName;
        private TextBox txtDays;
        private TextBox txtPrice;
        private Label label5;
        private Label label4;
        private DataGridView dgvLabs;
        private ComboBox cmbLabCat;
        private CheckBox chkExternal;
        private DataGridViewTextBoxColumn Id;
        private DataGridViewTextBoxColumn LabCategoryId;
        private DataGridViewTextBoxColumn CategoryName;
        private DataGridViewTextBoxColumn LabName;
        private DataGridViewTextBoxColumn Price;
        private DataGridViewTextBoxColumn Days;
        private DataGridViewImageColumn colEdit;
    }
}