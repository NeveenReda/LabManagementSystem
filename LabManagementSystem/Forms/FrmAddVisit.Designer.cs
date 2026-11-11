namespace LabManagementSystem.Forms
{
    partial class FrmAddVisit
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
            pnlPatientCard = new Panel();
            tblPatient = new TableLayoutPanel();
            lblCode = new Label();
            txtPatientCode = new TextBox();
            btnPickPatient = new Button();
            btnAddPatient = new Button();
            lblName = new Label();
            txtPatientName = new TextBox();
            lblDoctor = new Label();
            txtDoctorName = new TextBox();
            lblDate = new Label();
            DateOfVisit = new DateTimePicker();
            lblNotes = new Label();
            txtNotes = new TextBox();
            pnlLabsCard = new Panel();
            pnlAddLabBar = new Panel();
            tblAddLabBar = new TableLayoutPanel();
            lblSelectLab = new Label();
            cmbolabs = new ComboBox();
            btnAddLab = new Button();
            dgvVisitDetails = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colExternal = new DataGridViewButtonColumn();
            colResult = new DataGridViewButtonColumn();
            colDelete = new DataGridViewImageColumn();
            colVisitLabId = new DataGridViewTextBoxColumn();
            pnlFinancialCard = new Panel();
            tblFinancial = new TableLayoutPanel();
            lblGross = new Label();
            txtTotal = new TextBox();
            lblDiscPct = new Label();
            textDiscountPercentage = new TextBox();
            lblDiscVal = new Label();
            textDiscountValue = new TextBox();
            lblNet = new Label();
            txtAfterDiscount = new TextBox();
            lblPaid = new Label();
            txtPayment = new TextBox();
            lblRemaining = new Label();
            txtRemaining = new TextBox();
            pnlVisitActions = new Panel();
            btnSave = new Button();
            pnlBtnSpacer = new Panel();
            btnPrintReceipt = new Button();
            pnlHeader.SuspendLayout();
            pnlPatientCard.SuspendLayout();
            tblPatient.SuspendLayout();
            pnlLabsCard.SuspendLayout();
            pnlAddLabBar.SuspendLayout();
            tblAddLabBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVisitDetails).BeginInit();
            pnlFinancialCard.SuspendLayout();
            tblFinancial.SuspendLayout();
            pnlVisitActions.SuspendLayout();
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
            lblTitle.Text = "🧪 تسجيل زيارة وفحوصات طبية";
            lblTitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Dock = DockStyle.Bottom;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubtitle.Height = 18;
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Text = "تحديد المريض، إضافة التحاليل المطلوبة، احتساب الخصومات، والتحصيل المالي";
            lblSubtitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlPatientCard
            // 
            pnlPatientCard.BackColor = Color.White;
            pnlPatientCard.Controls.Add(tblPatient);
            pnlPatientCard.Dock = DockStyle.Top;
            pnlPatientCard.Height = 115;
            pnlPatientCard.Location = new Point(16, 81);
            pnlPatientCard.Name = "pnlPatientCard";
            pnlPatientCard.Padding = new Padding(14, 10, 14, 10);
            pnlPatientCard.TabIndex = 1;
            // 
            // tblPatient
            // 
            tblPatient.ColumnCount = 7;
            tblPatient.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tblPatient.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tblPatient.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            tblPatient.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110F));
            tblPatient.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 95F));
            tblPatient.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblPatient.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblPatient.Controls.Add(lblCode, 0, 0);
            tblPatient.Controls.Add(txtPatientCode, 1, 0);
            tblPatient.Controls.Add(btnPickPatient, 2, 0);
            tblPatient.Controls.Add(btnAddPatient, 3, 0);
            tblPatient.Controls.Add(lblName, 4, 0);
            tblPatient.Controls.Add(txtPatientName, 5, 0);
            tblPatient.Controls.Add(lblDoctor, 0, 1);
            tblPatient.Controls.Add(txtDoctorName, 1, 1);
            tblPatient.Controls.Add(lblDate, 2, 1);
            tblPatient.Controls.Add(DateOfVisit, 3, 1);
            tblPatient.Controls.Add(lblNotes, 4, 1);
            tblPatient.Controls.Add(txtNotes, 5, 1);
            tblPatient.Dock = DockStyle.Fill;
            tblPatient.Name = "tblPatient";
            tblPatient.RowCount = 2;
            tblPatient.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblPatient.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tblPatient.TabIndex = 0;
            // 
            // lblCode
            // 
            lblCode.Dock = DockStyle.Fill;
            lblCode.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCode.ForeColor = Color.FromArgb(30, 41, 59);
            lblCode.Name = "lblCode";
            lblCode.Text = "الكود الطبي:";
            lblCode.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPatientCode
            // 
            txtPatientCode.Dock = DockStyle.Fill;
            txtPatientCode.Font = new Font("Segoe UI", 10F);
            txtPatientCode.Margin = new Padding(4, 8, 4, 8);
            txtPatientCode.Name = "txtPatientCode";
            txtPatientCode.PlaceholderText = "الكود الطبي...";
            txtPatientCode.TabIndex = 0;
            txtPatientCode.Leave += txtPatientCode_Leave;
            // 
            // btnPickPatient
            // 
            btnPickPatient.BackColor = Color.FromArgb(37, 99, 235);
            btnPickPatient.Dock = DockStyle.Fill;
            btnPickPatient.FlatAppearance.BorderSize = 0;
            btnPickPatient.FlatStyle = FlatStyle.Flat;
            btnPickPatient.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPickPatient.ForeColor = Color.White;
            btnPickPatient.Margin = new Padding(3, 6, 3, 6);
            btnPickPatient.Name = "btnPickPatient";
            btnPickPatient.Text = "🔍 اختيار";
            btnPickPatient.UseVisualStyleBackColor = false;
            btnPickPatient.Click += btnPickPatient_Click;
            // 
            // btnAddPatient
            // 
            btnAddPatient.BackColor = Color.FromArgb(22, 163, 74);
            btnAddPatient.Dock = DockStyle.Fill;
            btnAddPatient.FlatAppearance.BorderSize = 0;
            btnAddPatient.FlatStyle = FlatStyle.Flat;
            btnAddPatient.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAddPatient.ForeColor = Color.White;
            btnAddPatient.Margin = new Padding(3, 6, 3, 6);
            btnAddPatient.Name = "btnAddPatient";
            btnAddPatient.Text = "➕ مريض جديد";
            btnAddPatient.UseVisualStyleBackColor = false;
            btnAddPatient.Click += btnAddPatient_Click;
            // 
            // lblName
            // 
            lblName.Dock = DockStyle.Fill;
            lblName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(30, 41, 59);
            lblName.Name = "lblName";
            lblName.Text = "اسم المريض:";
            lblName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPatientName
            // 
            tblPatient.SetColumnSpan(txtPatientName, 2);
            txtPatientName.Dock = DockStyle.Fill;
            txtPatientName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            txtPatientName.Margin = new Padding(4, 8, 4, 8);
            txtPatientName.Name = "txtPatientName";
            txtPatientName.ReadOnly = true;
            txtPatientName.TabIndex = 1;
            // 
            // lblDoctor
            // 
            lblDoctor.Dock = DockStyle.Fill;
            lblDoctor.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDoctor.ForeColor = Color.FromArgb(30, 41, 59);
            lblDoctor.Name = "lblDoctor";
            lblDoctor.Text = "الطبيب المعالج:";
            lblDoctor.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtDoctorName
            // 
            txtDoctorName.Dock = DockStyle.Fill;
            txtDoctorName.Font = new Font("Segoe UI", 10F);
            txtDoctorName.Margin = new Padding(4, 8, 4, 8);
            txtDoctorName.Name = "txtDoctorName";
            txtDoctorName.PlaceholderText = "اسم الطبيب المحول...";
            txtDoctorName.TabIndex = 2;
            // 
            // lblDate
            // 
            lblDate.Dock = DockStyle.Fill;
            lblDate.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblDate.ForeColor = Color.FromArgb(30, 41, 59);
            lblDate.Name = "lblDate";
            lblDate.Text = "تاريخ الزيارة:";
            lblDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // DateOfVisit
            // 
            DateOfVisit.Dock = DockStyle.Fill;
            DateOfVisit.Font = new Font("Segoe UI", 9.5F);
            DateOfVisit.Format = DateTimePickerFormat.Short;
            DateOfVisit.Margin = new Padding(4, 8, 4, 8);
            DateOfVisit.Name = "DateOfVisit";
            DateOfVisit.TabIndex = 3;
            // 
            // lblNotes
            // 
            lblNotes.Dock = DockStyle.Fill;
            lblNotes.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblNotes.ForeColor = Color.FromArgb(30, 41, 59);
            lblNotes.Name = "lblNotes";
            lblNotes.Text = "ملاحظات الزيارة:";
            lblNotes.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtNotes
            // 
            tblPatient.SetColumnSpan(txtNotes, 2);
            txtNotes.Dock = DockStyle.Fill;
            txtNotes.Font = new Font("Segoe UI", 10F);
            txtNotes.Margin = new Padding(4, 8, 4, 8);
            txtNotes.Name = "txtNotes";
            txtNotes.PlaceholderText = "أي ملاحظات سريرية أو تشخيصية...";
            txtNotes.TabIndex = 4;
            // 
            // pnlLabsCard
            // 
            pnlLabsCard.BackColor = Color.White;
            pnlLabsCard.Controls.Add(dgvVisitDetails);
            pnlLabsCard.Controls.Add(pnlAddLabBar);
            pnlLabsCard.Dock = DockStyle.Fill;
            pnlLabsCard.Location = new Point(16, 204);
            pnlLabsCard.Name = "pnlLabsCard";
            pnlLabsCard.Padding = new Padding(12);
            pnlLabsCard.TabIndex = 2;
            // 
            // pnlAddLabBar
            // 
            pnlAddLabBar.Controls.Add(tblAddLabBar);
            pnlAddLabBar.Dock = DockStyle.Top;
            pnlAddLabBar.Height = 56;
            pnlAddLabBar.Name = "pnlAddLabBar";
            pnlAddLabBar.Padding = new Padding(6, 6, 6, 8);
            pnlAddLabBar.TabIndex = 0;
            // 
            // tblAddLabBar
            // 
            tblAddLabBar.ColumnCount = 4;
            tblAddLabBar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 105F));
            tblAddLabBar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 380F));
            tblAddLabBar.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 165F));
            tblAddLabBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblAddLabBar.Controls.Add(lblSelectLab, 0, 0);
            tblAddLabBar.Controls.Add(cmbolabs, 1, 0);
            tblAddLabBar.Controls.Add(btnAddLab, 2, 0);
            tblAddLabBar.Dock = DockStyle.Fill;
            tblAddLabBar.Name = "tblAddLabBar";
            tblAddLabBar.RightToLeft = RightToLeft.Yes;
            tblAddLabBar.RowCount = 1;
            tblAddLabBar.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblAddLabBar.TabIndex = 0;
            // 
            // lblSelectLab
            // 
            lblSelectLab.AutoSize = false;
            lblSelectLab.Dock = DockStyle.Fill;
            lblSelectLab.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSelectLab.ForeColor = Color.FromArgb(30, 41, 59);
            lblSelectLab.Margin = new Padding(4, 0, 4, 0);
            lblSelectLab.Name = "lblSelectLab";
            lblSelectLab.RightToLeft = RightToLeft.No;
            lblSelectLab.Text = "إضافة تحليل";
            lblSelectLab.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbolabs
            // 
            cmbolabs.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbolabs.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbolabs.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbolabs.DropDownStyle = ComboBoxStyle.DropDown;
            cmbolabs.Font = new Font("Segoe UI", 10.5F);
            cmbolabs.Margin = new Padding(6, 0, 6, 0);
            cmbolabs.Name = "cmbolabs";
            cmbolabs.RightToLeft = RightToLeft.Yes;
            cmbolabs.Size = new Size(368, 28);
            cmbolabs.TabIndex = 0;
            cmbolabs.SelectedIndexChanged += cmbolabs_SelectedIndexChanged;
            // 
            // btnAddLab
            // 
            btnAddLab.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            btnAddLab.BackColor = Color.FromArgb(37, 99, 235);
            btnAddLab.FlatAppearance.BorderSize = 0;
            btnAddLab.FlatStyle = FlatStyle.Flat;
            btnAddLab.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnAddLab.ForeColor = Color.White;
            btnAddLab.Margin = new Padding(6, 0, 6, 0);
            btnAddLab.Name = "btnAddLab";
            btnAddLab.Size = new Size(153, 36);
            btnAddLab.TabIndex = 1;
            btnAddLab.Text = "➕ إضافة للزيارة";
            btnAddLab.UseVisualStyleBackColor = false;
            btnAddLab.Click += btnAddLab_Click;
            // 
            // dgvVisitDetails
            // 
            dgvVisitDetails.AllowUserToAddRows = false;
            dgvVisitDetails.AllowUserToDeleteRows = false;
            dgvVisitDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVisitDetails.BackgroundColor = Color.White;
            dgvVisitDetails.BorderStyle = BorderStyle.None;
            dgvVisitDetails.ColumnHeadersHeight = 38;
            dgvVisitDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvVisitDetails.Columns.AddRange(new DataGridViewColumn[] {
                colId,
                colName,
                colPrice,
                colExternal,
                colResult,
                colDelete,
                colVisitLabId
            });
            dgvVisitDetails.Dock = DockStyle.Fill;
            dgvVisitDetails.Location = new Point(12, 60);
            dgvVisitDetails.MultiSelect = false;
            dgvVisitDetails.Name = "dgvVisitDetails";
            dgvVisitDetails.ReadOnly = true;
            dgvVisitDetails.RowHeadersVisible = false;
            dgvVisitDetails.RowTemplate.Height = 36;
            dgvVisitDetails.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVisitDetails.Size = new Size(1144, 250);
            dgvVisitDetails.TabIndex = 1;
            dgvVisitDetails.CellContentClick += dgvVisitDetails_CellContentClick;
            // 
            // colId
            // 
            colId.FillWeight = 10F;
            colId.HeaderText = "#";
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colName
            // 
            colName.FillWeight = 45F;
            colName.HeaderText = "اسم التحليل الطبي";
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colPrice
            // 
            colPrice.FillWeight = 15F;
            colPrice.HeaderText = "السعر (ج.م)";
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            // 
            // colExternal
            // 
            colExternal.FillWeight = 12F;
            colExternal.HeaderText = "إحالة خارجي";
            colExternal.Name = "colExternal";
            colExternal.ReadOnly = true;
            colExternal.Text = "معمل خارجي";
            colExternal.UseColumnTextForButtonValue = true;
            // 
            // colResult
            // 
            colResult.FillWeight = 12F;
            colResult.HeaderText = "النتائج";
            colResult.Name = "colResult";
            colResult.ReadOnly = true;
            colResult.Text = "إدخال النتائج";
            colResult.UseColumnTextForButtonValue = true;
            // 
            // colDelete
            // 
            colDelete.FillWeight = 8F;
            colDelete.HeaderText = "حذف";
            colDelete.Name = "colDelete";
            colDelete.ReadOnly = true;
            // 
            // colVisitLabId
            // 
            colVisitLabId.HeaderText = "VisitLabId";
            colVisitLabId.Name = "colVisitLabId";
            colVisitLabId.ReadOnly = true;
            colVisitLabId.Visible = false;
            // 
            // pnlFinancialCard
            // 
            pnlFinancialCard.BackColor = Color.White;
            pnlFinancialCard.Controls.Add(pnlVisitActions);
            pnlFinancialCard.Controls.Add(tblFinancial);
            pnlFinancialCard.Dock = DockStyle.Bottom;
            pnlFinancialCard.Height = 142;
            pnlFinancialCard.Location = new Point(16, 522);
            pnlFinancialCard.Name = "pnlFinancialCard";
            pnlFinancialCard.Padding = new Padding(14, 10, 14, 12);
            pnlFinancialCard.TabIndex = 3;
            // 
            // tblFinancial
            // 
            tblFinancial.ColumnCount = 6;
            tblFinancial.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            tblFinancial.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            tblFinancial.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            tblFinancial.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            tblFinancial.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            tblFinancial.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.66F));
            tblFinancial.Controls.Add(lblGross, 0, 0);
            tblFinancial.Controls.Add(txtTotal, 0, 1);
            tblFinancial.Controls.Add(lblDiscPct, 1, 0);
            tblFinancial.Controls.Add(textDiscountPercentage, 1, 1);
            tblFinancial.Controls.Add(lblDiscVal, 2, 0);
            tblFinancial.Controls.Add(textDiscountValue, 2, 1);
            tblFinancial.Controls.Add(lblNet, 3, 0);
            tblFinancial.Controls.Add(txtAfterDiscount, 3, 1);
            tblFinancial.Controls.Add(lblPaid, 4, 0);
            tblFinancial.Controls.Add(txtPayment, 4, 1);
            tblFinancial.Controls.Add(lblRemaining, 5, 0);
            tblFinancial.Controls.Add(txtRemaining, 5, 1);
            tblFinancial.Dock = DockStyle.Top;
            tblFinancial.Height = 64;
            tblFinancial.Name = "tblFinancial";
            tblFinancial.RowCount = 2;
            tblFinancial.RowStyles.Add(new RowStyle(SizeType.Absolute, 26F));
            tblFinancial.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tblFinancial.TabIndex = 0;
            // 
            // lblGross
            // 
            lblGross.Dock = DockStyle.Fill;
            lblGross.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblGross.ForeColor = Color.FromArgb(100, 116, 139);
            lblGross.Name = "lblGross";
            lblGross.Text = "إجمالي الفحوصات";
            lblGross.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtTotal
            // 
            txtTotal.Dock = DockStyle.Fill;
            txtTotal.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            txtTotal.Name = "txtTotal";
            txtTotal.ReadOnly = true;
            txtTotal.Text = "0.00";
            txtTotal.TextAlign = HorizontalAlignment.Center;
            // 
            // lblDiscPct
            // 
            lblDiscPct.Dock = DockStyle.Fill;
            lblDiscPct.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDiscPct.ForeColor = Color.FromArgb(100, 116, 139);
            lblDiscPct.Name = "lblDiscPct";
            lblDiscPct.Text = "خصم %";
            lblDiscPct.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textDiscountPercentage
            // 
            textDiscountPercentage.Dock = DockStyle.Fill;
            textDiscountPercentage.Font = new Font("Segoe UI", 10.5F);
            textDiscountPercentage.Name = "textDiscountPercentage";
            textDiscountPercentage.Text = "0";
            textDiscountPercentage.TextAlign = HorizontalAlignment.Center;
            textDiscountPercentage.TextChanged += textDiscountPercentage_TextChanged;
            // 
            // lblDiscVal
            // 
            lblDiscVal.Dock = DockStyle.Fill;
            lblDiscVal.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDiscVal.ForeColor = Color.FromArgb(100, 116, 139);
            lblDiscVal.Name = "lblDiscVal";
            lblDiscVal.Text = "قيمة الخصم (ج.م)";
            lblDiscVal.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textDiscountValue
            // 
            textDiscountValue.Dock = DockStyle.Fill;
            textDiscountValue.Font = new Font("Segoe UI", 10.5F);
            textDiscountValue.Name = "textDiscountValue";
            textDiscountValue.Text = "0.00";
            textDiscountValue.TextAlign = HorizontalAlignment.Center;
            textDiscountValue.TextChanged += textDiscountValue_TextChanged;
            // 
            // lblNet
            // 
            lblNet.Dock = DockStyle.Fill;
            lblNet.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblNet.ForeColor = Color.FromArgb(37, 99, 235);
            lblNet.Name = "lblNet";
            lblNet.Text = "صافي المطلوب";
            lblNet.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtAfterDiscount
            // 
            txtAfterDiscount.Dock = DockStyle.Fill;
            txtAfterDiscount.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            txtAfterDiscount.ForeColor = Color.FromArgb(37, 99, 235);
            txtAfterDiscount.Name = "txtAfterDiscount";
            txtAfterDiscount.ReadOnly = true;
            txtAfterDiscount.Text = "0.00";
            txtAfterDiscount.TextAlign = HorizontalAlignment.Center;
            // 
            // lblPaid
            // 
            lblPaid.Dock = DockStyle.Fill;
            lblPaid.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPaid.ForeColor = Color.FromArgb(22, 163, 74);
            lblPaid.Name = "lblPaid";
            lblPaid.Text = "المدفوع حالياً (ج.م)";
            lblPaid.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtPayment
            // 
            txtPayment.Dock = DockStyle.Fill;
            txtPayment.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            txtPayment.ForeColor = Color.FromArgb(22, 163, 74);
            txtPayment.Name = "txtPayment";
            txtPayment.Text = "0.00";
            txtPayment.TextAlign = HorizontalAlignment.Center;
            txtPayment.TextChanged += txtPayment_TextChanged;
            // 
            // lblRemaining
            // 
            lblRemaining.Dock = DockStyle.Fill;
            lblRemaining.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblRemaining.ForeColor = Color.FromArgb(220, 38, 38);
            lblRemaining.Name = "lblRemaining";
            lblRemaining.Text = "المتبقي (دين)";
            lblRemaining.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtRemaining
            // 
            txtRemaining.Dock = DockStyle.Fill;
            txtRemaining.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            txtRemaining.ForeColor = Color.FromArgb(220, 38, 38);
            txtRemaining.Name = "txtRemaining";
            txtRemaining.ReadOnly = true;
            txtRemaining.Text = "0.00";
            txtRemaining.TextAlign = HorizontalAlignment.Center;
            // 
            // pnlVisitActions
            // 
            pnlVisitActions.Controls.Add(btnPrintReceipt);
            pnlVisitActions.Controls.Add(pnlBtnSpacer);
            pnlVisitActions.Controls.Add(btnSave);
            pnlVisitActions.Dock = DockStyle.Bottom;
            pnlVisitActions.Height = 48;
            pnlVisitActions.Name = "pnlVisitActions";
            pnlVisitActions.Padding = new Padding(0, 8, 0, 0);
            pnlVisitActions.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(22, 163, 74);
            btnSave.Dock = DockStyle.Left;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(210, 40);
            btnSave.TabIndex = 0;
            btnSave.Text = "💾 حفظ الزيارة والطباعة";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // pnlBtnSpacer
            // 
            pnlBtnSpacer.Dock = DockStyle.Left;
            pnlBtnSpacer.Name = "pnlBtnSpacer";
            pnlBtnSpacer.Size = new Size(10, 40);
            // 
            // btnPrintReceipt
            // 
            btnPrintReceipt.BackColor = Color.FromArgb(37, 99, 235);
            btnPrintReceipt.Dock = DockStyle.Left;
            btnPrintReceipt.FlatAppearance.BorderSize = 0;
            btnPrintReceipt.FlatStyle = FlatStyle.Flat;
            btnPrintReceipt.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnPrintReceipt.ForeColor = Color.White;
            btnPrintReceipt.Name = "btnPrintReceipt";
            btnPrintReceipt.Size = new Size(165, 40);
            btnPrintReceipt.TabIndex = 1;
            btnPrintReceipt.Text = "🖨️ طباعة الإيصال";
            btnPrintReceipt.UseVisualStyleBackColor = false;
            btnPrintReceipt.Click += btnPrintReceipt_Click;
            // 
            // FrmAddVisit
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 251);
            ClientSize = new Size(1200, 680);
            Controls.Add(pnlLabsCard);
            Controls.Add(pnlFinancialCard);
            Controls.Add(pnlPatientCard);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9.5F);
            Name = "FrmAddVisit";
            Padding = new Padding(16);
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "تسجيل زيارة جديدة";
            Load += FrmAddVisit_Load;
            pnlHeader.ResumeLayout(false);
            pnlPatientCard.ResumeLayout(false);
            tblPatient.ResumeLayout(false);
            tblPatient.PerformLayout();
            pnlLabsCard.ResumeLayout(false);
            pnlAddLabBar.ResumeLayout(false);
            tblAddLabBar.ResumeLayout(false);
            tblAddLabBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvVisitDetails).EndInit();
            pnlFinancialCard.ResumeLayout(false);
            tblFinancial.ResumeLayout(false);
            tblFinancial.PerformLayout();
            pnlVisitActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlPatientCard;
        private TableLayoutPanel tblPatient;
        private Label lblCode;
        private TextBox txtPatientCode;
        private Button btnPickPatient;
        private Button btnAddPatient;
        private Label lblName;
        private TextBox txtPatientName;
        private Label lblDoctor;
        private TextBox txtDoctorName;
        private Label lblDate;
        private DateTimePicker DateOfVisit;
        private Label lblNotes;
        private TextBox txtNotes;
        private Panel pnlLabsCard;
        private Panel pnlAddLabBar;
        private TableLayoutPanel tblAddLabBar;
        private Label lblSelectLab;
        private ComboBox cmbolabs;
        private Button btnAddLab;
        private DataGridView dgvVisitDetails;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewButtonColumn colExternal;
        private DataGridViewButtonColumn colResult;
        private DataGridViewImageColumn colDelete;
        private DataGridViewTextBoxColumn colVisitLabId;
        private Panel pnlFinancialCard;
        private TableLayoutPanel tblFinancial;
        private Label lblGross;
        private TextBox txtTotal;
        private Label lblDiscPct;
        private TextBox textDiscountPercentage;
        private Label lblDiscVal;
        private TextBox textDiscountValue;
        private Label lblNet;
        private TextBox txtAfterDiscount;
        private Label lblPaid;
        private TextBox txtPayment;
        private Label lblRemaining;
        private TextBox txtRemaining;
        private Panel pnlVisitActions;
        private Panel pnlBtnSpacer;
        private Button btnSave;
        private Button btnPrintReceipt;
    }
}
