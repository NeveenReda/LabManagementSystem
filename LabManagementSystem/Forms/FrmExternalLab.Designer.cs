namespace LabManagementSystem.Forms
{
    partial class FrmExternalLab
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
            pnlInfoCard = new Panel();
            lblPatientInfo = new Label();
            pnlFormCard = new Panel();
            tlpInputs = new TableLayoutPanel();
            lblExternalLab = new Label();
            cmbExternalLab = new ComboBox();
            lblContactPerson = new Label();
            txtContactPerson = new TextBox();
            lblPhone = new Label();
            txtPhone = new TextBox();
            lblDateSend = new Label();
            dtpDateSend = new DateTimePicker();
            lblDays = new Label();
            txtExpextedDays = new TextBox();
            lblDateReceive = new Label();
            dtpDateReceive = new DateTimePicker();
            pnlBottom = new Panel();
            btnSave = new Button();
            btnCancel = new Button();
            pnlHeader.SuspendLayout();
            pnlInfoCard.SuspendLayout();
            pnlFormCard.SuspendLayout();
            tlpInputs.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(23, 43, 77);
            pnlHeader.Controls.Add(lblSubtitle);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(16, 10, 16, 10);
            pnlHeader.Size = new Size(680, 60);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(16, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(185, 21);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "تحويل تحليل لمعمل خارجي";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Dock = DockStyle.Bottom;
            lblSubtitle.Font = new Font("Segoe UI", 8.5F);
            lblSubtitle.ForeColor = Color.FromArgb(203, 213, 225);
            lblSubtitle.Location = new Point(16, 35);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(244, 15);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "تحديد المعمل المرجعي وتواريخ الإرسال والاستلام";
            // 
            // pnlInfoCard
            // 
            pnlInfoCard.BackColor = Color.FromArgb(239, 246, 255);
            pnlInfoCard.BorderStyle = BorderStyle.FixedSingle;
            pnlInfoCard.Controls.Add(lblPatientInfo);
            pnlInfoCard.Dock = DockStyle.Top;
            pnlInfoCard.Location = new Point(0, 60);
            pnlInfoCard.Name = "pnlInfoCard";
            pnlInfoCard.Padding = new Padding(14, 10, 14, 10);
            pnlInfoCard.Size = new Size(680, 52);
            pnlInfoCard.TabIndex = 1;
            // 
            // lblPatientInfo
            // 
            lblPatientInfo.Dock = DockStyle.Fill;
            lblPatientInfo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblPatientInfo.ForeColor = Color.FromArgb(30, 64, 175);
            lblPatientInfo.Location = new Point(14, 10);
            lblPatientInfo.Name = "lblPatientInfo";
            lblPatientInfo.Size = new Size(650, 30);
            lblPatientInfo.TabIndex = 0;
            lblPatientInfo.Text = "بيانات الزيارة والتحليل...";
            lblPatientInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlFormCard
            // 
            pnlFormCard.BackColor = Color.White;
            pnlFormCard.Controls.Add(tlpInputs);
            pnlFormCard.Dock = DockStyle.Fill;
            pnlFormCard.Location = new Point(0, 112);
            pnlFormCard.Name = "pnlFormCard";
            pnlFormCard.Padding = new Padding(24, 16, 24, 16);
            pnlFormCard.Size = new Size(680, 278);
            pnlFormCard.TabIndex = 2;
            // 
            // tlpInputs
            // 
            tlpInputs.ColumnCount = 2;
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32F));
            tlpInputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 68F));
            tlpInputs.Controls.Add(lblExternalLab, 0, 0);
            tlpInputs.Controls.Add(cmbExternalLab, 1, 0);
            tlpInputs.Controls.Add(lblContactPerson, 0, 1);
            tlpInputs.Controls.Add(txtContactPerson, 1, 1);
            tlpInputs.Controls.Add(lblPhone, 0, 2);
            tlpInputs.Controls.Add(txtPhone, 1, 2);
            tlpInputs.Controls.Add(lblDateSend, 0, 3);
            tlpInputs.Controls.Add(dtpDateSend, 1, 3);
            tlpInputs.Controls.Add(lblDays, 0, 4);
            tlpInputs.Controls.Add(txtExpextedDays, 1, 4);
            tlpInputs.Controls.Add(lblDateReceive, 0, 5);
            tlpInputs.Controls.Add(dtpDateReceive, 1, 5);
            tlpInputs.Dock = DockStyle.Fill;
            tlpInputs.Location = new Point(24, 16);
            tlpInputs.Name = "tlpInputs";
            tlpInputs.RowCount = 6;
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tlpInputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tlpInputs.Size = new Size(632, 246);
            tlpInputs.TabIndex = 0;
            // 
            // lblExternalLab
            // 
            lblExternalLab.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblExternalLab.AutoSize = true;
            lblExternalLab.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblExternalLab.Location = new Point(433, 10);
            lblExternalLab.Name = "lblExternalLab";
            lblExternalLab.Size = new Size(196, 17);
            lblExternalLab.TabIndex = 0;
            lblExternalLab.Text = "المعمل الخارجي المرجعي:";
            // 
            // cmbExternalLab
            // 
            cmbExternalLab.Dock = DockStyle.Fill;
            cmbExternalLab.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbExternalLab.Font = new Font("Segoe UI", 10F);
            cmbExternalLab.Location = new Point(3, 3);
            cmbExternalLab.Name = "cmbExternalLab";
            cmbExternalLab.Size = new Size(424, 25);
            cmbExternalLab.TabIndex = 1;
            // 
            // lblContactPerson
            // 
            lblContactPerson.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblContactPerson.AutoSize = true;
            lblContactPerson.Font = new Font("Segoe UI", 9.5F);
            lblContactPerson.Location = new Point(433, 48);
            lblContactPerson.Name = "lblContactPerson";
            lblContactPerson.Size = new Size(196, 17);
            lblContactPerson.TabIndex = 2;
            lblContactPerson.Text = "اسم الشخص المسؤول:";
            // 
            // txtContactPerson
            // 
            txtContactPerson.Dock = DockStyle.Fill;
            txtContactPerson.Font = new Font("Segoe UI", 10F);
            txtContactPerson.Location = new Point(3, 41);
            txtContactPerson.Name = "txtContactPerson";
            txtContactPerson.Size = new Size(424, 25);
            txtContactPerson.TabIndex = 3;
            // 
            // lblPhone
            // 
            lblPhone.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 9.5F);
            lblPhone.Location = new Point(433, 86);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(196, 17);
            lblPhone.TabIndex = 4;
            lblPhone.Text = "رقم هاتف المعمل:";
            // 
            // txtPhone
            // 
            txtPhone.Dock = DockStyle.Fill;
            txtPhone.Font = new Font("Segoe UI", 10F);
            txtPhone.Location = new Point(3, 79);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(424, 25);
            txtPhone.TabIndex = 5;
            // 
            // lblDateSend
            // 
            lblDateSend.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblDateSend.AutoSize = true;
            lblDateSend.Font = new Font("Segoe UI", 9.5F);
            lblDateSend.Location = new Point(433, 124);
            lblDateSend.Name = "lblDateSend";
            lblDateSend.Size = new Size(196, 17);
            lblDateSend.TabIndex = 6;
            lblDateSend.Text = "تاريخ ووقت الإرسال:";
            // 
            // dtpDateSend
            // 
            dtpDateSend.CustomFormat = "yyyy/MM/dd  hh:mm tt";
            dtpDateSend.Dock = DockStyle.Fill;
            dtpDateSend.Font = new Font("Segoe UI", 10F);
            dtpDateSend.Format = DateTimePickerFormat.Custom;
            dtpDateSend.Location = new Point(3, 117);
            dtpDateSend.Name = "dtpDateSend";
            dtpDateSend.Size = new Size(424, 25);
            dtpDateSend.TabIndex = 7;
            // 
            // lblDays
            // 
            lblDays.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblDays.AutoSize = true;
            lblDays.Font = new Font("Segoe UI", 9.5F);
            lblDays.Location = new Point(433, 162);
            lblDays.Name = "lblDays";
            lblDays.Size = new Size(196, 17);
            lblDays.TabIndex = 8;
            lblDays.Text = "أيام التنفيذ المتوقعة:";
            // 
            // txtExpextedDays
            // 
            txtExpextedDays.Dock = DockStyle.Fill;
            txtExpextedDays.Font = new Font("Segoe UI", 10F);
            txtExpextedDays.Location = new Point(3, 155);
            txtExpextedDays.Name = "txtExpextedDays";
            txtExpextedDays.ReadOnly = true;
            txtExpextedDays.Size = new Size(424, 25);
            txtExpextedDays.TabIndex = 9;
            // 
            // lblDateReceive
            // 
            lblDateReceive.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblDateReceive.AutoSize = true;
            lblDateReceive.Font = new Font("Segoe UI", 9.5F);
            lblDateReceive.Location = new Point(433, 201);
            lblDateReceive.Name = "lblDateReceive";
            lblDateReceive.Size = new Size(196, 17);
            lblDateReceive.TabIndex = 10;
            lblDateReceive.Text = "التاريخ المتوقع للاستلام:";
            // 
            // dtpDateReceive
            // 
            dtpDateReceive.CustomFormat = "yyyy/MM/dd  hh:mm tt";
            dtpDateReceive.Dock = DockStyle.Fill;
            dtpDateReceive.Enabled = false;
            dtpDateReceive.Font = new Font("Segoe UI", 10F);
            dtpDateReceive.Format = DateTimePickerFormat.Custom;
            dtpDateReceive.Location = new Point(3, 193);
            dtpDateReceive.Name = "dtpDateReceive";
            dtpDateReceive.Size = new Size(424, 25);
            dtpDateReceive.TabIndex = 11;
            // 
            // pnlBottom
            // 
            pnlBottom.BackColor = Color.FromArgb(244, 247, 251);
            pnlBottom.Controls.Add(btnSave);
            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 390);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Padding = new Padding(16, 10, 16, 10);
            pnlBottom.Size = new Size(680, 56);
            pnlBottom.TabIndex = 3;
            // 
            // btnSave
            // 
            btnSave.Dock = DockStyle.Right;
            btnSave.Location = new Point(504, 10);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(160, 36);
            btnSave.TabIndex = 0;
            btnSave.Text = "💾 تأكيد التحويل";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Dock = DockStyle.Left;
            btnCancel.Location = new Point(16, 10);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 36);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "إلغاء";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // FrmExternalLab
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(680, 446);
            Controls.Add(pnlFormCard);
            Controls.Add(pnlBottom);
            Controls.Add(pnlInfoCard);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9.5F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmExternalLab";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterParent;
            Text = "تحويل تحليل لمعمل خارجي";
            Load += FrmExternalLab_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlInfoCard.ResumeLayout(false);
            pnlFormCard.ResumeLayout(false);
            tlpInputs.ResumeLayout(false);
            tlpInputs.PerformLayout();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlInfoCard;
        private Label lblPatientInfo;
        private Panel pnlFormCard;
        private TableLayoutPanel tlpInputs;
        private Label lblExternalLab;
        private ComboBox cmbExternalLab;
        private Label lblContactPerson;
        private TextBox txtContactPerson;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblDateSend;
        private DateTimePicker dtpDateSend;
        private Label lblDays;
        private TextBox txtExpextedDays;
        private Label lblDateReceive;
        private DateTimePicker dtpDateReceive;
        private Panel pnlBottom;
        private Button btnSave;
        private Button btnCancel;
    }
}
