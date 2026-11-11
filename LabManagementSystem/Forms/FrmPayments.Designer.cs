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
            tblPayInput = new TableLayoutPanel();
            lblNewPayPrompt = new Label();
            txtNewPayment = new TextBox();
            lblMethodPrompt = new Label();
            cmbPaymentMethod = new ComboBox();
            lblDatePrompt = new Label();
            dtPaymentDate = new DateTimePicker();
            lblRemainingPreview = new Label();
            btnPayFull = new Button();
            btnAddPayment = new Button();
            pnlGridCard = new Panel();
            dgvPayments = new DataGridView();
            colPaymentDate = new DataGridViewTextBoxColumn();
            colPaidAmount = new DataGridViewTextBoxColumn();
            colPaymentNotes = new DataGridViewTextBoxColumn();
            pnlBottomBar = new Panel();
            btnPrintReceipt = new Button();
            pnlBottomSpacer = new Panel();
            btnPrintInvoice = new Button();
            btnClose = new Button();
            pnlHeader.SuspendLayout();
            pnlSummaryCard.SuspendLayout();
            tblSummary.SuspendLayout();
            pnlPayInputCard.SuspendLayout();
            tblPayInput.SuspendLayout();
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
            pnlSummaryCard.Height = 118;
            pnlSummaryCard.Location = new Point(16, 86);
            pnlSummaryCard.Name = "pnlSummaryCard";
            pnlSummaryCard.Padding = new Padding(14, 10, 14, 10);
            pnlSummaryCard.TabIndex = 1;
            // 
            // tblSummary
            // 
            tblSummary.ColumnCount = 8;
            tblSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 118F));
            tblSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tblSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 118F));
            tblSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tblSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
            tblSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112F));
            tblSummary.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
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
            tblSummary.RightToLeft = RightToLeft.Yes;
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
            lblNet.Text = "إجمالي المديونية:";
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
            lblPaid.Text = "المبلغ المدفوع:";
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
            lblRemaining.Text = "المبلغ المتبقي:";
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
            pnlPayInputCard.Controls.Add(tblPayInput);
            pnlPayInputCard.Dock = DockStyle.Top;
            pnlPayInputCard.Height = 114;
            pnlPayInputCard.Location = new Point(16, 204);
            pnlPayInputCard.Name = "pnlPayInputCard";
            pnlPayInputCard.Padding = new Padding(14, 10, 14, 10);
            pnlPayInputCard.TabIndex = 2;
            // 
            // tblPayInput
            // 
            tblPayInput.ColumnCount = 6;
            tblPayInput.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 145F));
            tblPayInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tblPayInput.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            tblPayInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36F));
            tblPayInput.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            tblPayInput.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tblPayInput.Controls.Add(lblNewPayPrompt, 0, 0);
            tblPayInput.Controls.Add(txtNewPayment, 1, 0);
            tblPayInput.Controls.Add(lblMethodPrompt, 2, 0);
            tblPayInput.Controls.Add(cmbPaymentMethod, 3, 0);
            tblPayInput.Controls.Add(lblDatePrompt, 4, 0);
            tblPayInput.Controls.Add(dtPaymentDate, 5, 0);
            tblPayInput.Controls.Add(lblRemainingPreview, 0, 1);
            tblPayInput.Controls.Add(btnPayFull, 4, 1);
            tblPayInput.Controls.Add(btnAddPayment, 5, 1);
            tblPayInput.SetColumnSpan(lblRemainingPreview, 4);
            tblPayInput.Dock = DockStyle.Fill;
            tblPayInput.Name = "tblPayInput";
            tblPayInput.RightToLeft = RightToLeft.Yes;
            tblPayInput.RowCount = 2;
            tblPayInput.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            tblPayInput.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblPayInput.TabIndex = 0;
            // 
            // lblNewPayPrompt
            // 
            lblNewPayPrompt.Dock = DockStyle.Fill;
            lblNewPayPrompt.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblNewPayPrompt.ForeColor = Color.FromArgb(30, 41, 59);
            lblNewPayPrompt.Name = "lblNewPayPrompt";
            lblNewPayPrompt.RightToLeft = RightToLeft.No;
            lblNewPayPrompt.Text = "المبلغ المراد سداده:";
            lblNewPayPrompt.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtNewPayment
            // 
            txtNewPayment.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtNewPayment.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            txtNewPayment.ForeColor = Color.FromArgb(22, 163, 74);
            txtNewPayment.Margin = new Padding(6, 4, 6, 4);
            txtNewPayment.Name = "txtNewPayment";
            txtNewPayment.TabIndex = 0;
            txtNewPayment.TextChanged += txtNewPayment_TextChanged;
            // 
            // lblMethodPrompt
            // 
            lblMethodPrompt.Dock = DockStyle.Fill;
            lblMethodPrompt.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblMethodPrompt.ForeColor = Color.FromArgb(30, 41, 59);
            lblMethodPrompt.Name = "lblMethodPrompt";
            lblMethodPrompt.RightToLeft = RightToLeft.No;
            lblMethodPrompt.Text = "طريقة الدفع:";
            lblMethodPrompt.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbPaymentMethod
            // 
            cmbPaymentMethod.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPaymentMethod.Font = new Font("Segoe UI", 10F);
            cmbPaymentMethod.Margin = new Padding(6, 4, 6, 4);
            cmbPaymentMethod.Name = "cmbPaymentMethod";
            cmbPaymentMethod.TabIndex = 1;
            // 
            // lblDatePrompt
            // 
            lblDatePrompt.Dock = DockStyle.Fill;
            lblDatePrompt.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDatePrompt.ForeColor = Color.FromArgb(30, 41, 59);
            lblDatePrompt.Name = "lblDatePrompt";
            lblDatePrompt.RightToLeft = RightToLeft.No;
            lblDatePrompt.Text = "تاريخ السداد:";
            lblDatePrompt.TextAlign = ContentAlignment.MiddleRight;
            // 
            // dtPaymentDate
            // 
            dtPaymentDate.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            dtPaymentDate.Font = new Font("Segoe UI", 9.5F);
            dtPaymentDate.Format = DateTimePickerFormat.Short;
            dtPaymentDate.Margin = new Padding(6, 4, 6, 4);
            dtPaymentDate.Name = "dtPaymentDate";
            dtPaymentDate.TabIndex = 2;
            // 
            // lblRemainingPreview
            // 
            lblRemainingPreview.Dock = DockStyle.Fill;
            lblRemainingPreview.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblRemainingPreview.ForeColor = Color.FromArgb(37, 99, 235);
            lblRemainingPreview.Name = "lblRemainingPreview";
            lblRemainingPreview.RightToLeft = RightToLeft.No;
            lblRemainingPreview.Text = "المتبقي بعد السداد: 0.00 ج.م";
            lblRemainingPreview.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnPayFull
            // 
            btnPayFull.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnPayFull.BackColor = Color.White;
            btnPayFull.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235);
            btnPayFull.FlatStyle = FlatStyle.Flat;
            btnPayFull.Font = new Font("Segoe UI", 8.5F, FontStyle.Bold);
            btnPayFull.ForeColor = Color.FromArgb(37, 99, 235);
            btnPayFull.Height = 36;
            btnPayFull.Margin = new Padding(4, 2, 4, 2);
            btnPayFull.Name = "btnPayFull";
            btnPayFull.TabIndex = 3;
            btnPayFull.Text = "سداد كامل";
            btnPayFull.UseVisualStyleBackColor = false;
            btnPayFull.Click += btnPayFull_Click;
            // 
            // btnAddPayment
            // 
            btnAddPayment.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnAddPayment.BackColor = Color.FromArgb(22, 163, 74);
            btnAddPayment.FlatAppearance.BorderSize = 0;
            btnAddPayment.FlatStyle = FlatStyle.Flat;
            btnAddPayment.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAddPayment.ForeColor = Color.White;
            btnAddPayment.Height = 38;
            btnAddPayment.Margin = new Padding(6, 2, 6, 2);
            btnAddPayment.Name = "btnAddPayment";
            btnAddPayment.TabIndex = 4;
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
            pnlBottomBar.Controls.Add(pnlBottomSpacer);
            pnlBottomBar.Controls.Add(btnPrintReceipt);
            pnlBottomBar.Dock = DockStyle.Bottom;
            pnlBottomBar.Height = 65;
            pnlBottomBar.Location = new Point(16, 560);
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
            btnPrintReceipt.Name = "btnPrintReceipt";
            btnPrintReceipt.Size = new Size(205, 40);
            btnPrintReceipt.TabIndex = 0;
            btnPrintReceipt.Text = "🧾 طباعة إيصال حراري (80mm)";
            btnPrintReceipt.UseVisualStyleBackColor = false;
            btnPrintReceipt.Click += btnPrintReceipt_Click;
            // 
            // pnlBottomSpacer
            // 
            pnlBottomSpacer.Dock = DockStyle.Right;
            pnlBottomSpacer.Name = "pnlBottomSpacer";
            pnlBottomSpacer.Size = new Size(12, 40);
            // 
            // btnPrintInvoice
            // 
            btnPrintInvoice.Dock = DockStyle.Right;
            btnPrintInvoice.BackColor = Color.FromArgb(23, 43, 77);
            btnPrintInvoice.FlatAppearance.BorderSize = 0;
            btnPrintInvoice.FlatStyle = FlatStyle.Flat;
            btnPrintInvoice.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnPrintInvoice.ForeColor = Color.White;
            btnPrintInvoice.Name = "btnPrintInvoice";
            btnPrintInvoice.Size = new Size(205, 40);
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
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 40);
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
            ClientSize = new Size(1020, 640);
            MinimumSize = new Size(920, 580);
            Controls.Add(pnlGridCard);
            Controls.Add(pnlBottomBar);
            Controls.Add(pnlPayInputCard);
            Controls.Add(pnlSummaryCard);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;
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
            tblPayInput.ResumeLayout(false);
            tblPayInput.PerformLayout();
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
        private TableLayoutPanel tblPayInput;
        private Label lblNewPayPrompt;
        private TextBox txtNewPayment;
        private Label lblMethodPrompt;
        private ComboBox cmbPaymentMethod;
        private Label lblDatePrompt;
        private DateTimePicker dtPaymentDate;
        private Label lblRemainingPreview;
        private Button btnPayFull;
        private Button btnAddPayment;
        private Panel pnlGridCard;
        private DataGridView dgvPayments;
        private DataGridViewTextBoxColumn colPaymentDate;
        private DataGridViewTextBoxColumn colPaidAmount;
        private DataGridViewTextBoxColumn colPaymentNotes;
        private Panel pnlBottomBar;
        private Button btnPrintReceipt;
        private Panel pnlBottomSpacer;
        private Button btnPrintInvoice;
        private Button btnClose;
    }
}
