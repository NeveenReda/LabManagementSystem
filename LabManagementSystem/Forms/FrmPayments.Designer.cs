namespace LabManagementSystem.Forms
{
    partial class FrmPayments
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
            pnlSummaryCard = new Panel();
            tblSummary = new TableLayoutPanel();
            lblVisitNo = new Label();
            txtVisitCode = new TextBox();
            lblName = new Label();
            txtPatientName = new TextBox();
            lblTests = new Label();
            txtLabsCount = new TextBox();
            lblGross = new Label();
            txtTotal = new TextBox();
            lblDisc = new Label();
            txtDiscount = new TextBox();
            lblNet = new Label();
            textAfterDiscount = new TextBox();
            lblPaid = new Label();
            txtPaid = new TextBox();
            lblRemaining = new Label();
            txtRemaining = new TextBox();
            pnlPayInputCard = new Panel();
            lblNewPayPrompt = new Label();
            txtNewPayment = new TextBox();
            lblMethodPrompt = new Label();
            cmbPaymentMethod = new ComboBox();
            lblDatePrompt = new Label();
            dtPaymentDate = new DateTimePicker();
            btnAddPayment = new Button();
            pnlGridCard = new Panel();
            dgvPayments = new DataGridView();
            colPaymentDate = new DataGridViewTextBoxColumn();
            colPaidAmount = new DataGridViewTextBoxColumn();
            colPaymentNotes = new DataGridViewTextBoxColumn();
            pnlBottomBar = new Panel();
            btnPrintReceipt = new Button();
            btnPrintInvoice = new Button();
            btnClose = new Button();
            pnlHeader.SuspendLayout();
            pnlSummaryCard.SuspendLayout();
            tblSummary.SuspendLayout();
            pnlPayInputCard.SuspendLayout();
            pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPayments).BeginInit();
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
            lblTitle.Text = "💳 الخزينة وسداد دفعات الزيارة";
            lblTitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Dock = DockStyle.Bottom;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubtitle.Height = 18;
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Text = "تسجيل المقبوضات النقدية والإلكترونية، استعراض سجل المدفوعات، وطباعة الإيصالات والفواتير";
            lblSubtitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlSummaryCard
            // 
            pnlSummaryCard.BackColor = Color.White;
            pnlSummaryCard.Controls.Add(tblSummary);
            pnlSummaryCard.Dock = DockStyle.Top;
            pnlSummaryCard.Height = 110;
            pnlSummaryCard.Location = new Point(16, 81);
            pnlSummaryCard.Name = "pnlSummaryCard";
            pnlSummaryCard.Padding = new Padding(14, 10, 14, 10);
            pnlSummaryCard.TabIndex = 1;
            // 
            // tblSummary
            // 
            tblSummary.ColumnCount = 8;
            tblSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tblSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tblSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tblSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tblSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85F));
            tblSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16F));
            tblSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85F));
            tblSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            tblSummary.Controls.Add(lblVisitNo, 0, 0);
            tblSummary.Controls.Add(txtVisitCode, 1, 0);
            tblSummary.Controls.Add(lblName, 2, 0);
            tblSummary.Controls.Add(txtPatientName, 3, 0);
            tblSummary.Controls.Add(lblGross, 4, 0);
            tblSummary.Controls.Add(txtTotal, 5, 0);
            tblSummary.Controls.Add(lblDisc, 6, 0);
            tblSummary.Controls.Add(txtDiscount, 7, 0);
            tblSummary.Controls.Add(lblTests, 0, 1);
            tblSummary.Controls.Add(txtLabsCount, 1, 1);
            tblSummary.Controls.Add(lblNet, 2, 1);
            tblSummary.Controls.Add(textAfterDiscount, 3, 1);
            tblSummary.Controls.Add(lblPaid, 4, 1);
            tblSummary.Controls.Add(txtPaid, 5, 1);
            tblSummary.Controls.Add(lblRemaining, 6, 1);
            tblSummary.Controls.Add(txtRemaining, 7, 1);
            tblSummary.Dock = DockStyle.Fill;
            tblSummary.Name = "tblSummary";
            tblSummary.RowCount = 2;
            tblSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblSummary.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblSummary.TabIndex = 0;
            // 
            // lblVisitNo
            // 
            lblVisitNo.Dock = DockStyle.Fill;
            lblVisitNo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblVisitNo.ForeColor = Color.FromArgb(30, 41, 59);
            lblVisitNo.Name = "lblVisitNo";
            lblVisitNo.Text = "رقم الزيارة:";
            lblVisitNo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtVisitCode
            // 
            txtVisitCode.Dock = DockStyle.Fill;
            txtVisitCode.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            txtVisitCode.Margin = new Padding(3, 6, 3, 6);
            txtVisitCode.Name = "txtVisitCode";
            txtVisitCode.ReadOnly = true;
            // 
            // lblName
            // 
            lblName.Dock = DockStyle.Fill;
            lblName.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(30, 41, 59);
            lblName.Name = "lblName";
            lblName.Text = "اسم المريض:";
            lblName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPatientName
            // 
            txtPatientName.Dock = DockStyle.Fill;
            txtPatientName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            txtPatientName.Margin = new Padding(3, 6, 3, 6);
            txtPatientName.Name = "txtPatientName";
            txtPatientName.ReadOnly = true;
            // 
            // lblGross
            // 
            lblGross.Dock = DockStyle.Fill;
            lblGross.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGross.ForeColor = Color.FromArgb(100, 116, 139);
            lblGross.Name = "lblGross";
            lblGross.Text = "الإجمالي:";
            lblGross.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtTotal
            // 
            txtTotal.Dock = DockStyle.Fill;
            txtTotal.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            txtTotal.Margin = new Padding(3, 6, 3, 6);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            // 
            // lblDisc
            // 
            lblDisc.Dock = DockStyle.Fill;
            lblDisc.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDisc.ForeColor = Color.FromArgb(100, 116, 139);
            lblDisc.Name = "lblDisc";
            lblDisc.Text = "الخصم:";
            lblDisc.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDiscount
            // 
            txtDiscount.Dock = DockStyle.Fill;
            txtDiscount.Font = new Font("Segoe UI", 9.5F);
            txtDiscount.Margin = new Padding(3, 6, 3, 6);
            txtDiscount.Name = "txtDiscount";
            txtDiscount.ReadOnly = true;
            // 
            // lblTests
            // 
            lblTests.Dock = DockStyle.Fill;
            lblTests.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTests.ForeColor = Color.FromArgb(30, 41, 59);
            lblTests.Name = "lblTests";
            lblTests.Text = "الفحوصات:";
            lblTests.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtLabsCount
            // 
            txtLabsCount.Dock = DockStyle.Fill;
            txtLabsCount.Font = new Font("Segoe UI", 9.5F);
            txtLabsCount.Margin = new Padding(3, 6, 3, 6);
            txtLabsCount.Name = "txtLabsCount";
            txtLabsCount.ReadOnly = true;
            // 
            // lblNet
            // 
            lblNet.Dock = DockStyle.Fill;
            lblNet.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNet.ForeColor = Color.FromArgb(37, 99, 235);
            lblNet.Name = "lblNet";
            lblNet.Text = "صافي المطلوب:";
            lblNet.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textAfterDiscount
            // 
            textAfterDiscount.Dock = DockStyle.Fill;
            textAfterDiscount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            textAfterDiscount.ForeColor = Color.FromArgb(37, 99, 235);
            textAfterDiscount.Margin = new Padding(3, 6, 3, 6);
            textAfterDiscount.Name = "textAfterDiscount";
            textAfterDiscount.ReadOnly = true;
            // 
            // lblPaid
            // 
            lblPaid.Dock = DockStyle.Fill;
            lblPaid.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPaid.ForeColor = Color.FromArgb(22, 163, 74);
            lblPaid.Name = "lblPaid";
            lblPaid.Text = "المدفوع:";
            lblPaid.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPaid
            // 
            txtPaid.Dock = DockStyle.Fill;
            txtPaid.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            txtPaid.ForeColor = Color.FromArgb(22, 163, 74);
            txtPaid.Margin = new Padding(3, 6, 3, 6);
            txtPaid.Name = "txtPaid";
            txtPaid.ReadOnly = true;
            // 
            // lblRemaining
            // 
            lblRemaining.Dock = DockStyle.Fill;
            lblRemaining.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRemaining.ForeColor = Color.FromArgb(220, 38, 38);
            lblRemaining.Name = "lblRemaining";
            lblRemaining.Text = "المتبقي (دين):";
            lblRemaining.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtRemaining
            // 
            txtRemaining.Dock = DockStyle.Fill;
            txtRemaining.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            txtRemaining.ForeColor = Color.FromArgb(220, 38, 38);
            txtRemaining.Margin = new Padding(3, 6, 3, 6);
            txtRemaining.Name = "txtRemaining";
            txtRemaining.ReadOnly = true;
            // 
            // pnlPayInputCard
            // 
            pnlPayInputCard.BackColor = Color.White;
            pnlPayInputCard.Controls.Add(btnAddPayment);
            pnlPayInputCard.Controls.Add(dtPaymentDate);
            pnlPayInputCard.Controls.Add(lblDatePrompt);
            pnlPayInputCard.Controls.Add(cmbPaymentMethod);
            pnlPayInputCard.Controls.Add(lblMethodPrompt);
            pnlPayInputCard.Controls.Add(txtNewPayment);
            pnlPayInputCard.Controls.Add(lblNewPayPrompt);
            pnlPayInputCard.Dock = DockStyle.Top;
            pnlPayInputCard.Height = 64;
            pnlPayInputCard.Location = new Point(16, 199);
            pnlPayInputCard.Name = "pnlPayInputCard";
            pnlPayInputCard.Padding = new Padding(12);
            pnlPayInputCard.TabIndex = 2;
            // 
            // lblNewPayPrompt
            // 
            lblNewPayPrompt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblNewPayPrompt.AutoSize = true;
            lblNewPayPrompt.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblNewPayPrompt.ForeColor = Color.FromArgb(30, 41, 59);
            lblNewPayPrompt.Location = new Point(830, 22);
            lblNewPayPrompt.Name = "lblNewPayPrompt";
            lblNewPayPrompt.Text = "قيمة الدفعة (ج.م):";
            // 
            // txtNewPayment
            // 
            txtNewPayment.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtNewPayment.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            txtNewPayment.ForeColor = Color.FromArgb(22, 163, 74);
            txtNewPayment.Location = new Point(710, 18);
            txtNewPayment.Name = "txtNewPayment";
            txtNewPayment.Size = new Size(115, 26);
            txtNewPayment.TabIndex = 0;
            // 
            // lblMethodPrompt
            // 
            lblMethodPrompt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblMethodPrompt.AutoSize = true;
            lblMethodPrompt.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMethodPrompt.ForeColor = Color.FromArgb(30, 41, 59);
            lblMethodPrompt.Location = new Point(625, 22);
            lblMethodPrompt.Name = "lblMethodPrompt";
            lblMethodPrompt.Text = "طريقة الدفع:";
            // 
            // cmbPaymentMethod
            // 
            cmbPaymentMethod.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaymentMethod.Font = new Font("Segoe UI", 10F);
            cmbPaymentMethod.Location = new Point(460, 19);
            cmbPaymentMethod.Name = "cmbPaymentMethod";
            cmbPaymentMethod.Size = new Size(160, 25);
            cmbPaymentMethod.TabIndex = 1;
            // 
            // lblDatePrompt
            // 
            lblDatePrompt.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDatePrompt.AutoSize = true;
            lblDatePrompt.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDatePrompt.ForeColor = Color.FromArgb(30, 41, 59);
            lblDatePrompt.Location = new Point(375, 22);
            lblDatePrompt.Name = "lblDatePrompt";
            lblDatePrompt.Text = "تاريخ السداد:";
            // 
            // dtPaymentDate
            // 
            dtPaymentDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtPaymentDate.Font = new Font("Segoe UI", 9.5F);
            dtPaymentDate.Format = DateTimePickerFormat.Short;
            dtPaymentDate.Location = new Point(255, 20);
            dtPaymentDate.Name = "dtPaymentDate";
            dtPaymentDate.Size = new Size(115, 24);
            dtPaymentDate.TabIndex = 2;
            // 
            // btnAddPayment
            // 
            btnAddPayment.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddPayment.BackColor = Color.FromArgb(37, 99, 235);
            btnAddPayment.FlatAppearance.BorderSize = 0;
            btnAddPayment.FlatStyle = FlatStyle.Flat;
            btnAddPayment.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAddPayment.ForeColor = Color.White;
            btnAddPayment.Location = new Point(100, 15);
            btnAddPayment.Name = "btnAddPayment";
            btnAddPayment.Size = new Size(140, 34);
            btnAddPayment.TabIndex = 3;
            btnAddPayment.Text = "💵 تسجيل الدفعة";
            btnAddPayment.UseVisualStyleBackColor = false;
            btnAddPayment.Click += btnAddPayment_Click;
            // 
            // pnlGridCard
            // 
            pnlGridCard.BackColor = Color.White;
            pnlGridCard.Controls.Add(dgvPayments);
            pnlGridCard.Dock = DockStyle.Fill;
            pnlGridCard.Location = new Point(16, 271);
            pnlGridCard.Name = "pnlGridCard";
            pnlGridCard.Padding = new Padding(12);
            pnlGridCard.TabIndex = 3;
            // 
            // dgvPayments
            // 
            dgvPayments.AllowUserToAddRows = false;
            dgvPayments.AllowUserToDeleteRows = false;
            dgvPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPayments.BackgroundColor = Color.White;
            dgvPayments.BorderStyle = BorderStyle.None;
            dgvPayments.ColumnHeadersHeight = 38;
            dgvPayments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvPayments.Columns.AddRange(new DataGridViewColumn[] {
                colPaymentDate,
                colPaidAmount,
                colPaymentNotes
            });
            dgvPayments.Dock = DockStyle.Fill;
            dgvPayments.Location = new Point(12, 12);
            dgvPayments.MultiSelect = false;
            dgvPayments.Name = "dgvPayments";
            dgvPayments.ReadOnly = true;
            dgvPayments.RowHeadersVisible = false;
            dgvPayments.RowTemplate.Height = 36;
            dgvPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPayments.Size = new Size(916, 220);
            dgvPayments.TabIndex = 0;
            // 
            // colPaymentDate
            // 
            colPaymentDate.FillWeight = 25F;
            colPaymentDate.HeaderText = "تاريخ ووقت السداد";
            colPaymentDate.Name = "colPaymentDate";
            colPaymentDate.ReadOnly = true;
            // 
            // colPaidAmount
            // 
            colPaidAmount.FillWeight = 20F;
            colPaidAmount.HeaderText = "المبلغ المسدد (ج.م)";
            colPaidAmount.Name = "colPaidAmount";
            colPaidAmount.ReadOnly = true;
            // 
            // colPaymentNotes
            // 
            colPaymentNotes.FillWeight = 55F;
            colPaymentNotes.HeaderText = "طريقة الدفع والتفاصيل";
            colPaymentNotes.Name = "colPaymentNotes";
            colPaymentNotes.ReadOnly = true;
            // 
            // pnlBottomBar
            // 
            pnlBottomBar.BackColor = Color.White;
            pnlBottomBar.Controls.Add(btnClose);
            pnlBottomBar.Controls.Add(btnPrintInvoice);
            pnlBottomBar.Controls.Add(btnPrintReceipt);
            pnlBottomBar.Dock = DockStyle.Bottom;
            pnlBottomBar.Height = 65;
            pnlBottomBar.Location = new Point(16, 499);
            pnlBottomBar.Name = "pnlBottomBar";
            pnlBottomBar.Padding = new Padding(12);
            pnlBottomBar.TabIndex = 4;
            // 
            // btnPrintReceipt
            // 
            btnPrintReceipt.Dock = DockStyle.Right;
            btnPrintReceipt.BackColor = Color.FromArgb(37, 99, 235);
            btnPrintReceipt.FlatAppearance.BorderSize = 0;
            btnPrintReceipt.FlatStyle = FlatStyle.Flat;
            btnPrintReceipt.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnPrintReceipt.ForeColor = Color.White;
            btnPrintReceipt.Location = new Point(740, 14);
            btnPrintReceipt.Name = "btnPrintReceipt";
            btnPrintReceipt.Size = new Size(195, 39);
            btnPrintReceipt.TabIndex = 0;
            btnPrintReceipt.Text = "🧾 طباعة إيصال حراري (80mm)";
            btnPrintReceipt.UseVisualStyleBackColor = false;
            btnPrintReceipt.Click += btnPrintReceipt_Click;
            // 
            // btnPrintInvoice
            // 
            btnPrintInvoice.Dock = DockStyle.Right;
            btnPrintInvoice.BackColor = Color.FromArgb(23, 43, 77);
            btnPrintInvoice.FlatAppearance.BorderSize = 0;
            btnPrintInvoice.FlatStyle = FlatStyle.Flat;
            btnPrintInvoice.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnPrintInvoice.ForeColor = Color.White;
            btnPrintInvoice.Location = new Point(535, 14);
            btnPrintInvoice.Name = "btnPrintInvoice";
            btnPrintInvoice.Size = new Size(195, 39);
            btnPrintInvoice.TabIndex = 1;
            btnPrintInvoice.Text = "📄 طباعة فاتورة رسمية (A5)";
            btnPrintInvoice.UseVisualStyleBackColor = false;
            btnPrintInvoice.Click += btnPrintInvoice_Click;
            // 
            // btnClose
            // 
            btnClose.Dock = DockStyle.Left;
            btnClose.BackColor = Color.White;
            btnClose.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 9.5F);
            btnClose.ForeColor = Color.FromArgb(100, 116, 139);
            btnClose.Location = new Point(12, 14);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 38);
            btnClose.TabIndex = 2;
            btnClose.Text = "إغلاق";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += (s, e) => Close();
            // 
            // FrmPayments
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 251);
            ClientSize = new Size(960, 580);
            Controls.Add(pnlGridCard);
            Controls.Add(pnlBottomBar);
            Controls.Add(pnlPayInputCard);
            Controls.Add(pnlSummaryCard);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmPayments";
            Padding = new Padding(16);
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "خزينة وسداد دفعات الزيارة";
            Load += FrmPayments_Load;
            pnlHeader.ResumeLayout(false);
            pnlSummaryCard.ResumeLayout(false);
            tblSummary.ResumeLayout(false);
            tblSummary.PerformLayout();
            pnlPayInputCard.ResumeLayout(false);
            pnlPayInputCard.PerformLayout();
            pnlGridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPayments).EndInit();
            pnlBottomBar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlSummaryCard;
        private TableLayoutPanel tblSummary;
        private Label lblVisitNo;
        private TextBox txtVisitCode;
        private Label lblName;
        private TextBox txtPatientName;
        private Label lblTests;
        private TextBox txtLabsCount;
        private Label lblGross;
        private TextBox txtTotal;
        private Label lblDisc;
        private TextBox txtDiscount;
        private Label lblNet;
        private TextBox textAfterDiscount;
        private Label lblPaid;
        private TextBox txtPaid;
        private Label lblRemaining;
        private TextBox txtRemaining;
        private Panel pnlPayInputCard;
        private Label lblNewPayPrompt;
        private TextBox txtNewPayment;
        private Label lblMethodPrompt;
        private ComboBox cmbPaymentMethod;
        private Label lblDatePrompt;
        private DateTimePicker dtPaymentDate;
        private Button btnAddPayment;
        private Panel pnlGridCard;
        private DataGridView dgvPayments;
        private DataGridViewTextBoxColumn colPaymentDate;
        private DataGridViewTextBoxColumn colPaidAmount;
        private DataGridViewTextBoxColumn colPaymentNotes;
        private Panel pnlBottomBar;
        private Button btnPrintReceipt;
        private Button btnPrintInvoice;
        private Button btnClose;
    }
}
