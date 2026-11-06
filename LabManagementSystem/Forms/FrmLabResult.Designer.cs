namespace LabManagementSystem.Forms
{
    partial class FrmLabResult
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
            pnlInfoCard = new Panel();
            lblInfoText = new Label();
            pnlGridCard = new Panel();
            dgvResults = new DataGridView();
            colParameterId = new DataGridViewTextBoxColumn();
            colParameterName = new DataGridViewTextBoxColumn();
            colUnit = new DataGridViewTextBoxColumn();
            colNormalRange = new DataGridViewTextBoxColumn();
            colResult = new DataGridViewTextBoxColumn();
            colFlag = new DataGridViewTextBoxColumn();
            colNotes = new DataGridViewTextBoxColumn();
            pnlBottomBar = new Panel();
            btnSave = new Button();
            btnPrintReport = new Button();
            btnClose = new Button();
            pnlHeader.SuspendLayout();
            pnlInfoCard.SuspendLayout();
            pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            pnlBottomBar.SuspendLayout();
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
            lblTitle.Text = "🔬 إدخال واعتماد نتائج الفحوصات الطبية";
            lblTitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Dock = DockStyle.Bottom;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubtitle.Height = 18;
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Text = "تسجيل النتائج المخبرية ومقارنتها تلقائياً بالمعدلات الطبيعية وإظهار المؤشرات غير الطبيعية";
            lblSubtitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlInfoCard
            // 
            pnlInfoCard.BackColor = Color.FromArgb(239, 246, 255);
            pnlInfoCard.Controls.Add(lblInfoText);
            pnlInfoCard.Dock = DockStyle.Top;
            pnlInfoCard.Height = 52;
            pnlInfoCard.Location = new Point(16, 81);
            pnlInfoCard.Name = "pnlInfoCard";
            pnlInfoCard.Padding = new Padding(16, 12, 16, 12);
            pnlInfoCard.TabIndex = 1;
            // 
            // lblInfoText
            // 
            lblInfoText.Dock = DockStyle.Fill;
            lblInfoText.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblInfoText.ForeColor = Color.FromArgb(30, 41, 59);
            lblInfoText.Location = new Point(16, 12);
            lblInfoText.Name = "lblInfoText";
            lblInfoText.Text = "جاري تحميل بيانات الفحص...";
            lblInfoText.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlGridCard
            // 
            pnlGridCard.BackColor = Color.White;
            pnlGridCard.Controls.Add(dgvResults);
            pnlGridCard.Dock = DockStyle.Fill;
            pnlGridCard.Location = new Point(16, 133);
            pnlGridCard.Name = "pnlGridCard";
            pnlGridCard.Padding = new Padding(12);
            pnlGridCard.TabIndex = 2;
            // 
            // dgvResults
            // 
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResults.BackgroundColor = Color.White;
            dgvResults.BorderStyle = BorderStyle.None;
            dgvResults.ColumnHeadersHeight = 38;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvResults.Columns.AddRange(new DataGridViewColumn[] {
                colParameterId,
                colParameterName,
                colUnit,
                colNormalRange,
                colResult,
                colFlag,
                colNotes
            });
            dgvResults.Dock = DockStyle.Fill;
            dgvResults.Location = new Point(12, 12);
            dgvResults.MultiSelect = false;
            dgvResults.Name = "dgvResults";
            dgvResults.RowHeadersVisible = false;
            dgvResults.RowTemplate.Height = 36;
            dgvResults.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvResults.Size = new Size(1016, 380);
            dgvResults.TabIndex = 0;
            // 
            // colParameterId
            // 
            colParameterId.DataPropertyName = "Id";
            colParameterId.HeaderText = "ID";
            colParameterId.Name = "colParameterId";
            colParameterId.ReadOnly = true;
            colParameterId.Visible = false;
            // 
            // colParameterName
            // 
            colParameterName.FillWeight = 28F;
            colParameterName.HeaderText = "الباراميتر / الفحص";
            colParameterName.Name = "colParameterName";
            colParameterName.ReadOnly = true;
            // 
            // colUnit
            // 
            colUnit.FillWeight = 14F;
            colUnit.HeaderText = "الوحدة (Unit)";
            colUnit.Name = "colUnit";
            colUnit.ReadOnly = true;
            // 
            // colNormalRange
            // 
            colNormalRange.FillWeight = 22F;
            colNormalRange.HeaderText = "المعدل الطبيعي (Reference)";
            colNormalRange.Name = "colNormalRange";
            colNormalRange.ReadOnly = true;
            // 
            // colResult
            // 
            colResult.FillWeight = 18F;
            colResult.HeaderText = "النتيجة المخبرية *";
            colResult.Name = "colResult";
            // 
            // colFlag
            // 
            colFlag.FillWeight = 12F;
            colFlag.HeaderText = "الحالة (Flag)";
            colFlag.Name = "colFlag";
            colFlag.ReadOnly = true;
            // 
            // colNotes
            // 
            colNotes.FillWeight = 22F;
            colNotes.HeaderText = "ملاحظات الفحص";
            colNotes.Name = "colNotes";
            // 
            // pnlBottomBar
            // 
            pnlBottomBar.BackColor = Color.White;
            pnlBottomBar.Controls.Add(btnClose);
            pnlBottomBar.Controls.Add(btnPrintReport);
            pnlBottomBar.Controls.Add(btnSave);
            pnlBottomBar.Dock = DockStyle.Bottom;
            pnlBottomBar.Height = 65;
            pnlBottomBar.Location = new Point(16, 513);
            pnlBottomBar.Name = "pnlBottomBar";
            pnlBottomBar.Padding = new Padding(12);
            pnlBottomBar.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.Dock = DockStyle.Right;
            btnSave.BackColor = Color.FromArgb(22, 163, 74);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(840, 13);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(185, 38);
            btnSave.TabIndex = 0;
            btnSave.Text = "💾 حفظ واعتماد النتائج";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnPrintReport
            // 
            btnPrintReport.Dock = DockStyle.Right;
            btnPrintReport.BackColor = Color.FromArgb(37, 99, 235);
            btnPrintReport.FlatAppearance.BorderSize = 0;
            btnPrintReport.FlatStyle = FlatStyle.Flat;
            btnPrintReport.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnPrintReport.ForeColor = Color.White;
            btnPrintReport.Location = new Point(560, 13);
            btnPrintReport.Name = "btnPrintReport";
            btnPrintReport.Size = new Size(265, 38);
            btnPrintReport.TabIndex = 1;
            btnPrintReport.Text = "📄 طباعة التقرير الطبي الشامل للزيارة (A4)";
            btnPrintReport.UseVisualStyleBackColor = false;
            btnPrintReport.Click += btnPrintReport_Click;
            // 
            // btnClose
            // 
            btnClose.Dock = DockStyle.Left;
            btnClose.BackColor = Color.White;
            btnClose.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 9.5F);
            btnClose.ForeColor = Color.FromArgb(100, 116, 139);
            btnClose.Location = new Point(12, 13);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 38);
            btnClose.TabIndex = 2;
            btnClose.Text = "إغلاق";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += (s, e) => Close();
            // 
            // FrmLabResult
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 251);
            ClientSize = new Size(1060, 590);
            Controls.Add(pnlGridCard);
            Controls.Add(pnlBottomBar);
            Controls.Add(pnlInfoCard);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLabResult";
            Padding = new Padding(16);
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "نتائج الفحوصات الطبية";
            Load += FrmLabResult_Load;
            pnlHeader.ResumeLayout(false);
            pnlInfoCard.ResumeLayout(false);
            pnlGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            pnlBottomBar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlInfoCard;
        private Label lblInfoText;
        private Panel pnlGridCard;
        private DataGridView dgvResults;
        private DataGridViewTextBoxColumn colParameterId;
        private DataGridViewTextBoxColumn colParameterName;
        private DataGridViewTextBoxColumn colUnit;
        private DataGridViewTextBoxColumn colNormalRange;
        private DataGridViewTextBoxColumn colResult;
        private DataGridViewTextBoxColumn colFlag;
        private DataGridViewTextBoxColumn colNotes;
        private Panel pnlBottomBar;
        private Button btnSave;
        private Button btnPrintReport;
        private Button btnClose;
    }
}
