namespace LabManagementSystem.Forms
{
    partial class FrmSearchVisit
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
            btnNewVisit = new Button();
            pnlFilterCard = new Panel();
            lblCode = new Label();
            txtSearchCode = new TextBox();
            lblName = new Label();
            txtSearchName = new TextBox();
            chkSearchByDate = new CheckBox();
            lblFrom = new Label();
            dtSearchDateFrom = new DateTimePicker();
            lblTo = new Label();
            dtSearchDateTo = new DateTimePicker();
            btnFilter = new Button();
            lblSummary = new Label();
            pnlGridCard = new Panel();
            dgvvisits = new DataGridView();
            colVisitId = new DataGridViewTextBoxColumn();
            colMedicalCode = new DataGridViewTextBoxColumn();
            colPatientName = new DataGridViewTextBoxColumn();
            colVisitDate = new DataGridViewTextBoxColumn();
            colLabsCount = new DataGridViewTextBoxColumn();
            colTotalPrice = new DataGridViewTextBoxColumn();
            colDiscountValue = new DataGridViewTextBoxColumn();
            colNetPrice = new DataGridViewTextBoxColumn();
            colPaidAmount = new DataGridViewTextBoxColumn();
            colRemainingAmount = new DataGridViewTextBoxColumn();
            colReport = new DataGridViewImageColumn();
            colPayment = new DataGridViewImageColumn();
            colEdit = new DataGridViewImageColumn();
            colDelete = new DataGridViewImageColumn();
            pnlHeader.SuspendLayout();
            pnlFilterCard.SuspendLayout();
            pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvvisits).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(23, 43, 77);
            pnlHeader.Controls.Add(btnNewVisit);
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
            lblHeader.Text = "🧪 سجل ومتابعة الزيارات والفحوصات الطبية";
            lblHeader.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Dock = DockStyle.Bottom;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubtitle.Height = 18;
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Text = "استعراض الزيارات، إدخال النتائج، طباعة التقارير المجمعة، وسداد الدفعات المالية";
            lblSubtitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnNewVisit
            // 
            btnNewVisit.Dock = DockStyle.Left;
            btnNewVisit.BackColor = Color.FromArgb(37, 99, 235);
            btnNewVisit.FlatAppearance.BorderSize = 0;
            btnNewVisit.FlatStyle = FlatStyle.Flat;
            btnNewVisit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnNewVisit.ForeColor = Color.White;
            btnNewVisit.Name = "btnNewVisit";
            btnNewVisit.Size = new Size(160, 48);
            btnNewVisit.TabIndex = 2;
            btnNewVisit.Text = "➕ تسجيل زيارة جديدة";
            btnNewVisit.UseVisualStyleBackColor = false;
            btnNewVisit.Click += btnNewVisit_Click;
            // 
            // pnlFilterCard
            // 
            pnlFilterCard.BackColor = Color.White;
            pnlFilterCard.Controls.Add(lblSummary);
            pnlFilterCard.Controls.Add(btnFilter);
            pnlFilterCard.Controls.Add(dtSearchDateTo);
            pnlFilterCard.Controls.Add(lblTo);
            pnlFilterCard.Controls.Add(dtSearchDateFrom);
            pnlFilterCard.Controls.Add(lblFrom);
            pnlFilterCard.Controls.Add(chkSearchByDate);
            pnlFilterCard.Controls.Add(txtSearchName);
            pnlFilterCard.Controls.Add(lblName);
            pnlFilterCard.Controls.Add(txtSearchCode);
            pnlFilterCard.Controls.Add(lblCode);
            pnlFilterCard.Dock = DockStyle.Top;
            pnlFilterCard.Height = 58;
            pnlFilterCard.Name = "pnlFilterCard";
            pnlFilterCard.Padding = new Padding(12, 10, 12, 10);
            pnlFilterCard.TabIndex = 1;
            // 
            // lblCode
            // 
            lblCode.Dock = DockStyle.Right;
            lblCode.AutoSize = false;
            lblCode.Width = 50;
            lblCode.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCode.ForeColor = Color.FromArgb(30, 41, 59);
            lblCode.Name = "lblCode";
            lblCode.Text = "الكود:";
            lblCode.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtSearchCode
            // 
            txtSearchCode.Dock = DockStyle.Right;
            txtSearchCode.Font = new Font("Segoe UI", 9.5F);
            txtSearchCode.Name = "txtSearchCode";
            txtSearchCode.Size = new Size(100, 24);
            txtSearchCode.TabIndex = 0;
            txtSearchCode.TextChanged += txtSearch_TextChanged;
            // 
            // lblName
            // 
            lblName.Dock = DockStyle.Right;
            lblName.AutoSize = false;
            lblName.Width = 85;
            lblName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(30, 41, 59);
            lblName.Name = "lblName";
            lblName.Text = "اسم المريض:";
            lblName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtSearchName
            // 
            txtSearchName.Dock = DockStyle.Right;
            txtSearchName.Font = new Font("Segoe UI", 9.5F);
            txtSearchName.Name = "txtSearchName";
            txtSearchName.Size = new Size(160, 24);
            txtSearchName.TabIndex = 1;
            txtSearchName.TextChanged += txtSearch_TextChanged;
            // 
            // chkSearchByDate
            // 
            chkSearchByDate.Dock = DockStyle.Right;
            chkSearchByDate.AutoSize = false;
            chkSearchByDate.Width = 105;
            chkSearchByDate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            chkSearchByDate.ForeColor = Color.FromArgb(30, 41, 59);
            chkSearchByDate.Name = "chkSearchByDate";
            chkSearchByDate.Text = "تصفية بالتاريخ:";
            chkSearchByDate.UseVisualStyleBackColor = true;
            chkSearchByDate.CheckAlign = ContentAlignment.MiddleRight;
            chkSearchByDate.TextAlign = ContentAlignment.MiddleRight;
            chkSearchByDate.CheckedChanged += chkSearchByDate_CheckedChanged;
            // 
            // lblFrom
            // 
            lblFrom.Dock = DockStyle.Right;
            lblFrom.AutoSize = false;
            lblFrom.Width = 35;
            lblFrom.Font = new Font("Segoe UI", 9F);
            lblFrom.Name = "lblFrom";
            lblFrom.Text = "من:";
            lblFrom.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtSearchDateFrom
            // 
            dtSearchDateFrom.Dock = DockStyle.Right;
            dtSearchDateFrom.Enabled = false;
            dtSearchDateFrom.Font = new Font("Segoe UI", 9.5F);
            dtSearchDateFrom.Format = DateTimePickerFormat.Short;
            dtSearchDateFrom.Name = "dtSearchDateFrom";
            dtSearchDateFrom.Size = new Size(100, 24);
            dtSearchDateFrom.TabIndex = 3;
            // 
            // lblTo
            // 
            lblTo.Dock = DockStyle.Right;
            lblTo.AutoSize = false;
            lblTo.Width = 35;
            lblTo.Font = new Font("Segoe UI", 9F);
            lblTo.Name = "lblTo";
            lblTo.Text = "إلى:";
            lblTo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtSearchDateTo
            // 
            dtSearchDateTo.Dock = DockStyle.Right;
            dtSearchDateTo.Enabled = false;
            dtSearchDateTo.Font = new Font("Segoe UI", 9.5F);
            dtSearchDateTo.Format = DateTimePickerFormat.Short;
            dtSearchDateTo.Name = "dtSearchDateTo";
            dtSearchDateTo.Size = new Size(100, 24);
            dtSearchDateTo.TabIndex = 4;
            // 
            // btnFilter
            // 
            btnFilter.Dock = DockStyle.Right;
            btnFilter.BackColor = Color.FromArgb(37, 99, 235);
            btnFilter.FlatAppearance.BorderSize = 0;
            btnFilter.FlatStyle = FlatStyle.Flat;
            btnFilter.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnFilter.ForeColor = Color.White;
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(80, 38);
            btnFilter.TabIndex = 5;
            btnFilter.Text = "🔍 بحث";
            btnFilter.UseVisualStyleBackColor = false;
            btnFilter.Click += btnFilter_Click;
            // 
            // lblSummary
            // 
            lblSummary.Dock = DockStyle.Left;
            lblSummary.AutoSize = true;
            lblSummary.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblSummary.ForeColor = Color.FromArgb(37, 99, 235);
            lblSummary.Name = "lblSummary";
            lblSummary.Text = "ملخص الزيارات...";
            lblSummary.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlGridCard
            // 
            pnlGridCard.BackColor = Color.White;
            pnlGridCard.Controls.Add(dgvvisits);
            pnlGridCard.Dock = DockStyle.Fill;
            pnlGridCard.Location = new Point(16, 149);
            pnlGridCard.Name = "pnlGridCard";
            pnlGridCard.Padding = new Padding(12);
            pnlGridCard.TabIndex = 2;
            // 
            // dgvvisits
            // 
            dgvvisits.AllowUserToAddRows = false;
            dgvvisits.AllowUserToDeleteRows = false;
            dgvvisits.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvvisits.BackgroundColor = Color.White;
            dgvvisits.BorderStyle = BorderStyle.None;
            dgvvisits.ColumnHeadersHeight = 40;
            dgvvisits.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvvisits.Columns.AddRange(new DataGridViewColumn[] {
                colVisitId,
                colMedicalCode,
                colPatientName,
                colVisitDate,
                colLabsCount,
                colTotalPrice,
                colDiscountValue,
                colNetPrice,
                colPaidAmount,
                colRemainingAmount,
                colReport,
                colPayment,
                colEdit,
                colDelete
            });
            dgvvisits.Dock = DockStyle.Fill;
            dgvvisits.Location = new Point(12, 12);
            dgvvisits.MultiSelect = false;
            dgvvisits.Name = "dgvvisits";
            dgvvisits.ReadOnly = true;
            dgvvisits.RowHeadersVisible = false;
            dgvvisits.RowTemplate.Height = 38;
            dgvvisits.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvvisits.Size = new Size(1176, 503);
            dgvvisits.TabIndex = 0;
            dgvvisits.CellContentClick += dgvvisits_CellContentClick;
            // 
            // colVisitId
            // 
            colVisitId.DataPropertyName = "Id";
            colVisitId.FillWeight = 8F;
            colVisitId.HeaderText = "#";
            colVisitId.Name = "colVisitId";
            colVisitId.ReadOnly = true;
            // 
            // colMedicalCode
            // 
            colMedicalCode.DataPropertyName = "MedicalCode";
            colMedicalCode.FillWeight = 16F;
            colMedicalCode.HeaderText = "الكود الطبي";
            colMedicalCode.Name = "colMedicalCode";
            colMedicalCode.ReadOnly = true;
            // 
            // colPatientName
            // 
            colPatientName.DataPropertyName = "PatientName";
            colPatientName.FillWeight = 26F;
            colPatientName.HeaderText = "اسم المريض";
            colPatientName.Name = "colPatientName";
            colPatientName.ReadOnly = true;
            // 
            // colVisitDate
            // 
            colVisitDate.DataPropertyName = "VisitDate";
            colVisitDate.FillWeight = 14F;
            colVisitDate.HeaderText = "تاريخ الزيارة";
            colVisitDate.Name = "colVisitDate";
            colVisitDate.ReadOnly = true;
            // 
            // colLabsCount
            // 
            colLabsCount.DataPropertyName = "LabsCount";
            colLabsCount.FillWeight = 9F;
            colLabsCount.HeaderText = "الفحوصات";
            colLabsCount.Name = "colLabsCount";
            colLabsCount.ReadOnly = true;
            // 
            // colTotalPrice
            // 
            colTotalPrice.DataPropertyName = "TotalPrice";
            colTotalPrice.FillWeight = 11F;
            colTotalPrice.HeaderText = "الإجمالي";
            colTotalPrice.Name = "colTotalPrice";
            colTotalPrice.ReadOnly = true;
            // 
            // colDiscountValue
            // 
            colDiscountValue.DataPropertyName = "DiscountValue";
            colDiscountValue.FillWeight = 9F;
            colDiscountValue.HeaderText = "الخصم";
            colDiscountValue.Name = "colDiscountValue";
            colDiscountValue.ReadOnly = true;
            // 
            // colNetPrice
            // 
            colNetPrice.DataPropertyName = "NetPrice";
            colNetPrice.FillWeight = 11F;
            colNetPrice.HeaderText = "المطلوب";
            colNetPrice.Name = "colNetPrice";
            colNetPrice.ReadOnly = true;
            // 
            // colPaidAmount
            // 
            colPaidAmount.DataPropertyName = "PaidAmount";
            colPaidAmount.FillWeight = 11F;
            colPaidAmount.HeaderText = "المدفوع";
            colPaidAmount.Name = "colPaidAmount";
            colPaidAmount.ReadOnly = true;
            // 
            // colRemainingAmount
            // 
            colRemainingAmount.DataPropertyName = "RemainingAmount";
            colRemainingAmount.FillWeight = 11F;
            colRemainingAmount.HeaderText = "المتبقي";
            colRemainingAmount.Name = "colRemainingAmount";
            colRemainingAmount.ReadOnly = true;
            // 
            // colReport
            // 
            colReport.FillWeight = 7F;
            colReport.HeaderText = "تقرير";
            colReport.Name = "colReport";
            colReport.ReadOnly = true;
            // 
            // colPayment
            // 
            colPayment.FillWeight = 7F;
            colPayment.HeaderText = "سداد";
            colPayment.Name = "colPayment";
            colPayment.ReadOnly = true;
            // 
            // colEdit
            // 
            colEdit.FillWeight = 7F;
            colEdit.HeaderText = "تعديل";
            colEdit.Name = "colEdit";
            colEdit.ReadOnly = true;
            // 
            // colDelete
            // 
            colDelete.FillWeight = 7F;
            colDelete.HeaderText = "حذف";
            colDelete.Name = "colDelete";
            colDelete.ReadOnly = true;
            // 
            // FrmSearchVisit
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 251);
            ClientSize = new Size(1200, 680);
            Controls.Add(pnlGridCard);
            Controls.Add(pnlFilterCard);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9.5F);
            Name = "FrmSearchVisit";
            Padding = new Padding(16);
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            Text = "سجل الزيارات";
            Load += FrmSearchVisit_Load;
            pnlHeader.ResumeLayout(false);
            pnlFilterCard.ResumeLayout(false);
            pnlFilterCard.PerformLayout();
            pnlGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvvisits).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblHeader;
        private Label lblSubtitle;
        private Button btnNewVisit;
        private Panel pnlFilterCard;
        private Label lblCode;
        private TextBox txtSearchCode;
        private Label lblName;
        private TextBox txtSearchName;
        private CheckBox chkSearchByDate;
        private Label lblFrom;
        private DateTimePicker dtSearchDateFrom;
        private Label lblTo;
        private DateTimePicker dtSearchDateTo;
        private Button btnFilter;
        private Label lblSummary;
        private Panel pnlGridCard;
        private DataGridView dgvvisits;
        private DataGridViewTextBoxColumn colVisitId;
        private DataGridViewTextBoxColumn colMedicalCode;
        private DataGridViewTextBoxColumn colPatientName;
        private DataGridViewTextBoxColumn colVisitDate;
        private DataGridViewTextBoxColumn colLabsCount;
        private DataGridViewTextBoxColumn colTotalPrice;
        private DataGridViewTextBoxColumn colDiscountValue;
        private DataGridViewTextBoxColumn colNetPrice;
        private DataGridViewTextBoxColumn colPaidAmount;
        private DataGridViewTextBoxColumn colRemainingAmount;
        private DataGridViewImageColumn colReport;
        private DataGridViewImageColumn colPayment;
        private DataGridViewImageColumn colEdit;
        private DataGridViewImageColumn colDelete;
    }
}
