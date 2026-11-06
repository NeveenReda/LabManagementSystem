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
            lblSubtitle = new Label();
            lblTitle = new Label();
            pnlMainCard = new Panel();
            pnlActions = new Panel();
            btnCancel = new Button();
            btnSave = new Button();
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
            pnlHeader.SuspendLayout();
            pnlMainCard.SuspendLayout();
            pnlActions.SuspendLayout();
            tblForm.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(23, 43, 77);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(16, 16);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(20, 10, 20, 10);
            pnlHeader.Size = new Size(788, 68);
            pnlHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            lblSubtitle.Dock = DockStyle.Bottom;
            lblSubtitle.Font = new Font("Segoe UI", 9F);
            lblSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubtitle.Location = new Point(20, 40);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(748, 18);
            lblSubtitle.TabIndex = 0;
            lblSubtitle.Text = "إدخال وتحديث البيانات الشخصية والطبية للمريض وتوليد الكود الطبي المعتمد";
            lblSubtitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(20, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(748, 28);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "👤 تسجيل مريض جديد";
            lblTitle.TextAlign = ContentAlignment.MiddleRight;
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
            pnlMainCard.Size = new Size(788, 340);
            pnlMainCard.TabIndex = 1;
            // 
            // pnlActions
            // 
            pnlActions.Controls.Add(btnCancel);
            pnlActions.Controls.Add(btnSave);
            pnlActions.Dock = DockStyle.Top;
            pnlActions.Location = new Point(24, 264);
            pnlActions.Name = "pnlActions";
            pnlActions.Padding = new Padding(0, 16, 0, 0);
            pnlActions.Size = new Size(740, 70);
            pnlActions.TabIndex = 1;
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
            tblForm.Location = new Point(24, 24);
            tblForm.Name = "tblForm";
            tblForm.RowCount = 4;
            tblForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tblForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tblForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tblForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            tblForm.Size = new Size(740, 240);
            tblForm.TabIndex = 0;
            // 
            // lblCode
            // 
            lblCode.Dock = DockStyle.Fill;
            lblCode.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCode.ForeColor = Color.FromArgb(30, 41, 59);
            lblCode.Location = new Point(613, 0);
            lblCode.Name = "lblCode";
            lblCode.Size = new Size(124, 55);
            lblCode.TabIndex = 0;
            lblCode.Text = "الكود الطبي:";
            lblCode.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPatientCode
            // 
            txtPatientCode.Dock = DockStyle.Fill;
            txtPatientCode.Font = new Font("Segoe UI", 10.5F);
            txtPatientCode.Location = new Point(378, 12);
            txtPatientCode.Margin = new Padding(8, 12, 8, 12);
            txtPatientCode.Name = "txtPatientCode";
            txtPatientCode.ReadOnly = true;
            txtPatientCode.Size = new Size(224, 31);
            txtPatientCode.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.Dock = DockStyle.Fill;
            lblName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblName.ForeColor = Color.FromArgb(30, 41, 59);
            lblName.Location = new Point(243, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(124, 55);
            lblName.TabIndex = 1;
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
            txtPatientName.Size = new Size(224, 31);
            txtPatientName.TabIndex = 1;
            // 
            // lblPhone
            // 
            lblPhone.Dock = DockStyle.Fill;
            lblPhone.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPhone.ForeColor = Color.FromArgb(30, 41, 59);
            lblPhone.Location = new Point(613, 55);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(124, 55);
            lblPhone.TabIndex = 2;
            lblPhone.Text = "رقم الهاتف:";
            lblPhone.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPatienPhone
            // 
            txtPatienPhone.Dock = DockStyle.Fill;
            txtPatienPhone.Font = new Font("Segoe UI", 10.5F);
            txtPatienPhone.Location = new Point(378, 67);
            txtPatienPhone.Margin = new Padding(8, 12, 8, 12);
            txtPatienPhone.Name = "txtPatienPhone";
            txtPatienPhone.Size = new Size(224, 31);
            txtPatienPhone.TabIndex = 2;
            // 
            // lblGender
            // 
            lblGender.Dock = DockStyle.Fill;
            lblGender.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblGender.ForeColor = Color.FromArgb(30, 41, 59);
            lblGender.Location = new Point(243, 55);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(124, 55);
            lblGender.TabIndex = 3;
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
            cmboGender.Size = new Size(224, 31);
            cmboGender.TabIndex = 3;
            // 
            // lblAge
            // 
            lblAge.Dock = DockStyle.Fill;
            lblAge.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAge.ForeColor = Color.FromArgb(30, 41, 59);
            lblAge.Location = new Point(613, 110);
            lblAge.Name = "lblAge";
            lblAge.Size = new Size(124, 55);
            lblAge.TabIndex = 4;
            lblAge.Text = "السن الحالي *:";
            lblAge.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPatientAgeOnRegister
            // 
            txtPatientAgeOnRegister.Dock = DockStyle.Fill;
            txtPatientAgeOnRegister.Font = new Font("Segoe UI", 10.5F);
            txtPatientAgeOnRegister.Location = new Point(378, 122);
            txtPatientAgeOnRegister.Margin = new Padding(8, 12, 8, 12);
            txtPatientAgeOnRegister.Name = "txtPatientAgeOnRegister";
            txtPatientAgeOnRegister.Size = new Size(224, 31);
            txtPatientAgeOnRegister.TabIndex = 4;
            txtPatientAgeOnRegister.TextChanged += txtPatientAgeOnRegister_TextChanged;
            // 
            // lblDob
            // 
            lblDob.Dock = DockStyle.Fill;
            lblDob.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDob.ForeColor = Color.FromArgb(30, 41, 59);
            lblDob.Location = new Point(243, 110);
            lblDob.Name = "lblDob";
            lblDob.Size = new Size(124, 55);
            lblDob.TabIndex = 5;
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
            DOB.Size = new Size(224, 31);
            DOB.TabIndex = 5;
            DOB.ValueChanged += DOB_ValueChanged;
            // 
            // lblAddress
            // 
            lblAddress.Dock = DockStyle.Fill;
            lblAddress.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAddress.ForeColor = Color.FromArgb(30, 41, 59);
            lblAddress.Location = new Point(613, 165);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(124, 75);
            lblAddress.TabIndex = 6;
            lblAddress.Text = "العنوان:";
            lblAddress.TextAlign = ContentAlignment.MiddleRight;
            // 
            // address
            // 
            address.Dock = DockStyle.Fill;
            address.Font = new Font("Segoe UI", 10.5F);
            address.Location = new Point(378, 177);
            address.Margin = new Padding(8, 12, 8, 12);
            address.Name = "address";
            address.Size = new Size(224, 31);
            address.TabIndex = 6;
            // 
            // lblRegDate
            // 
            lblRegDate.Dock = DockStyle.Fill;
            lblRegDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblRegDate.ForeColor = Color.FromArgb(30, 41, 59);
            lblRegDate.Location = new Point(243, 165);
            lblRegDate.Name = "lblRegDate";
            lblRegDate.Size = new Size(124, 75);
            lblRegDate.TabIndex = 7;
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
            DateOFRegister.Size = new Size(224, 31);
            DateOFRegister.TabIndex = 7;
            // 
            // FrmAddPatient
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
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
            pnlActions.ResumeLayout(false);
            tblForm.ResumeLayout(false);
            tblForm.PerformLayout();
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
