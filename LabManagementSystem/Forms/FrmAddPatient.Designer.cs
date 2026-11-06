namespace LabManagementSystem.Forms
{
    partial class FrmAddPatient
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
            lblTitle = new Label();
            lblSubtitle = new Label();
            pnlMainCard = new Panel();
            tblForm = new TableLayoutPanel();
            lblCode = new Label();
            txtPatientCode = new TextBox();
            lblName = new Label();
            txtPatientName = new TextBox();
            lblPhone = new Label();
            txtPatienPhone = new TextBox();
            lblGender = new Label();
            cmboGender = new ComboBox();
            lblAge = new Label();
            txtPatientAgeOnRegister = new TextBox();
            lblDob = new Label();
            DOB = new DateTimePicker();
            lblAddress = new Label();
            address = new TextBox();
            lblRegDate = new Label();
            DateOFRegister = new DateTimePicker();
            pnlActions = new Panel();
            btnSave = new Button();
            btnCancel = new Button();
            pnlHeader.SuspendLayout();
            pnlMainCard.SuspendLayout();
            tblForm.SuspendLayout();
            pnlActions.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(23, 43, 77);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 68;
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
            lblTitle.Text = "👤 تسجيل مريض جديد";
            lblTitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Dock = DockStyle.Bottom;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubtitle.Height = 18;
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Text = "إدخال وتحديث البيانات الشخصية والطبية للمريض وتوليد الكود الطبي المعتمد";
            lblSubtitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlMainCard
            // 
            pnlMainCard.BackColor = Color.White;
            pnlMainCard.Controls.Add(pnlActions);
            pnlMainCard.Controls.Add(tblForm);
            pnlMainCard.Dock = DockStyle.Fill;
            pnlMainCard.Location = new Point(16, 84);
            pnlMainCard.Name = "pnlMainCard";
            pnlMainCard.Padding = new Padding(24);
            pnlMainCard.TabIndex = 1;
            // 
            // tblForm
            // 
            tblForm.ColumnCount = 4;
            tblForm.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tblForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblForm.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tblForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblForm.Controls.Add(lblCode, 0, 0);
            tblForm.Controls.Add(txtPatientCode, 1, 0);
            tblForm.Controls.Add(lblName, 2, 0);
            tblForm.Controls.Add(txtPatientName, 3, 0);
            tblForm.Controls.Add(lblPhone, 0, 1);
            tblForm.Controls.Add(txtPatienPhone, 1, 1);
            tblForm.Controls.Add(lblGender, 2, 1);
            tblForm.Controls.Add(cmboGender, 3, 1);
            tblForm.Controls.Add(lblAge, 0, 2);
            tblForm.Controls.Add(txtPatientAgeOnRegister, 1, 2);
            tblForm.Controls.Add(lblDob, 2, 2);
            tblForm.Controls.Add(DOB, 3, 2);
            tblForm.Controls.Add(lblAddress, 0, 3);
            tblForm.Controls.Add(address, 1, 3);
            tblForm.Controls.Add(lblRegDate, 2, 3);
            tblForm.Controls.Add(DateOFRegister, 3, 3);
            tblForm.Dock = DockStyle.Top;
            tblForm.Height = 240;
            tblForm.Name = "tblForm";
            tblForm.RowCount = 4;
            tblForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tblForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tblForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tblForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tblForm.TabIndex = 0;
            // 
            // lblCode
            // 
            lblCode.Dock = DockStyle.Fill;
            lblCode.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCode.ForeColor = Color.FromArgb(30, 41, 59);
            lblCode.Location = new Point(660, 0);
            lblCode.Name = "lblCode";
            lblCode.Text = "الكود الطبي:";
            lblCode.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPatientCode
            // 
            txtPatientCode.Dock = DockStyle.Fill;
            txtPatientCode.Font = new Font("Segoe UI", 10.5F);
            txtPatientCode.Location = new Point(410, 12);
            txtPatientCode.Margin = new Padding(8, 12, 8, 12);
            txtPatientCode.Name = "txtPatientCode";
            txtPatientCode.ReadOnly = true;
            txtPatientCode.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.Dock = DockStyle.Fill;
            lblName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(30, 41, 59);
            lblName.Location = new Point(272, 0);
            lblName.Name = "lblName";
            lblName.Text = "اسم المريض *:";
            lblName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPatientName
            // 
            txtPatientName.Dock = DockStyle.Fill;
            txtPatientName.Font = new Font("Segoe UI", 10.5F);
            txtPatientName.Location = new Point(8, 12);
            txtPatientName.Margin = new Padding(8, 12, 8, 12);
            txtPatientName.Name = "txtPatientName";
            txtPatientName.TabIndex = 1;
            // 
            // lblPhone
            // 
            lblPhone.Dock = DockStyle.Fill;
            lblPhone.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPhone.ForeColor = Color.FromArgb(30, 41, 59);
            lblPhone.Location = new Point(660, 55);
            lblPhone.Name = "lblPhone";
            lblPhone.Text = "رقم الهاتف:";
            lblPhone.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPatienPhone
            // 
            txtPatienPhone.Dock = DockStyle.Fill;
            txtPatienPhone.Font = new Font("Segoe UI", 10.5F);
            txtPatienPhone.Location = new Point(410, 67);
            txtPatienPhone.Margin = new Padding(8, 12, 8, 12);
            txtPatienPhone.Name = "txtPatienPhone";
            txtPatienPhone.TabIndex = 2;
            // 
            // lblGender
            // 
            lblGender.Dock = DockStyle.Fill;
            lblGender.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblGender.ForeColor = Color.FromArgb(30, 41, 59);
            lblGender.Location = new Point(272, 55);
            lblGender.Name = "lblGender";
            lblGender.Text = "النوع *:";
            lblGender.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmboGender
            // 
            cmboGender.Dock = DockStyle.Fill;
            cmboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmboGender.Font = new Font("Segoe UI", 10.5F);
            cmboGender.Location = new Point(8, 67);
            cmboGender.Margin = new Padding(8, 12, 8, 12);
            cmboGender.Name = "cmboGender";
            cmboGender.TabIndex = 3;
            // 
            // lblAge
            // 
            lblAge.Dock = DockStyle.Fill;
            lblAge.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAge.ForeColor = Color.FromArgb(30, 41, 59);
            lblAge.Location = new Point(660, 110);
            lblAge.Name = "lblAge";
            lblAge.Text = "السن الحالي *:";
            lblAge.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPatientAgeOnRegister
            // 
            txtPatientAgeOnRegister.Dock = DockStyle.Fill;
            txtPatientAgeOnRegister.Font = new Font("Segoe UI", 10.5F);
            txtPatientAgeOnRegister.Location = new Point(410, 122);
            txtPatientAgeOnRegister.Margin = new Padding(8, 12, 8, 12);
            txtPatientAgeOnRegister.Name = "txtPatientAgeOnRegister";
            txtPatientAgeOnRegister.TabIndex = 4;
            txtPatientAgeOnRegister.TextChanged += txtPatientAgeOnRegister_TextChanged;
            // 
            // lblDob
            // 
            lblDob.Dock = DockStyle.Fill;
            lblDob.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDob.ForeColor = Color.FromArgb(30, 41, 59);
            lblDob.Location = new Point(272, 110);
            lblDob.Name = "lblDob";
            lblDob.Text = "تاريخ الميلاد:";
            lblDob.TextAlign = ContentAlignment.MiddleRight;
            // 
            // DOB
            // 
            DOB.Dock = DockStyle.Fill;
            DOB.Font = new Font("Segoe UI", 10.5F);
            DOB.Format = DateTimePickerFormat.Short;
            DOB.Location = new Point(8, 122);
            DOB.Margin = new Padding(8, 12, 8, 12);
            DOB.Name = "DOB";
            DOB.TabIndex = 5;
            DOB.ValueChanged += DOB_ValueChanged;
            // 
            // lblAddress
            // 
            lblAddress.Dock = DockStyle.Fill;
            lblAddress.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAddress.ForeColor = Color.FromArgb(30, 41, 59);
            lblAddress.Location = new Point(660, 165);
            lblAddress.Name = "lblAddress";
            lblAddress.Text = "العنوان:";
            lblAddress.TextAlign = ContentAlignment.MiddleRight;
            // 
            // address
            // 
            address.Dock = DockStyle.Fill;
            address.Font = new Font("Segoe UI", 10.5F);
            address.Location = new Point(410, 177);
            address.Margin = new Padding(8, 12, 8, 12);
            address.Name = "address";
            address.TabIndex = 6;
            // 
            // lblRegDate
            // 
            lblRegDate.Dock = DockStyle.Fill;
            lblRegDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRegDate.ForeColor = Color.FromArgb(30, 41, 59);
            lblRegDate.Location = new Point(272, 165);
            lblRegDate.Name = "lblRegDate";
            lblRegDate.Text = "تاريخ التسجيل:";
            lblRegDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // DateOFRegister
            // 
            DateOFRegister.Dock = DockStyle.Fill;
            DateOFRegister.Enabled = false;
            DateOFRegister.Font = new Font("Segoe UI", 10.5F);
            DateOFRegister.Format = DateTimePickerFormat.Short;
            DateOFRegister.Location = new Point(8, 177);
            DateOFRegister.Margin = new Padding(8, 12, 8, 12);
            DateOFRegister.Name = "DateOFRegister";
            DateOFRegister.TabIndex = 7;
            // 
            // pnlActions
            // 
            pnlActions.Controls.Add(btnCancel);
            pnlActions.Controls.Add(btnSave);
            pnlActions.Dock = DockStyle.Top;
            pnlActions.Height = 70;
            pnlActions.Name = "pnlActions";
            pnlActions.Padding = new Padding(0, 16, 0, 0);
            pnlActions.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(22, 163, 74);
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(140, 16);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(180, 44);
            btnSave.TabIndex = 0;
            btnSave.Text = "💾 حفظ بيانات المريض";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.White;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.ForeColor = Color.FromArgb(100, 116, 139);
            btnCancel.Location = new Point(20, 16);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(105, 44);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "إلغاء";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // FrmAddPatient
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 251);
            ClientSize = new Size(820, 440);
            Controls.Add(pnlMainCard);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmAddPatient";
            Padding = new Padding(16);
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "بيانات المريض";
            Load += FrmAddPatient_Load;
            pnlHeader.ResumeLayout(false);
            pnlMainCard.ResumeLayout(false);
            tblForm.ResumeLayout(false);
            tblForm.PerformLayout();
            pnlActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlMainCard;
        private TableLayoutPanel tblForm;
        private Label lblCode;
        private TextBox txtPatientCode;
        private Label lblName;
        private TextBox txtPatientName;
        private Label lblPhone;
        private TextBox txtPatienPhone;
        private Label lblGender;
        private ComboBox cmboGender;
        private Label lblAge;
        private TextBox txtPatientAgeOnRegister;
        private Label lblDob;
        private DateTimePicker DOB;
        private Label lblAddress;
        private TextBox address;
        private Label lblRegDate;
        private DateTimePicker DateOFRegister;
        private Panel pnlActions;
        private Button btnSave;
        private Button btnCancel;
    }
}
