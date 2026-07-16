namespace LabManagementSystem
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            label1 = new Label();
            pnlContent = new Panel();
            pnlMenu = new Panel();
            btnPurchases = new Button();
            btnReports = new Button();
            pnlSettings = new Panel();
            btnSettingLab = new Button();
            btnSettingOutLab = new Button();
            btnSettingProducts = new Button();
            btnSettings = new Button();
            pnlVisits = new Panel();
            btnSearchVisit = new Button();
            bnAddVisit = new Button();
            btnVisits = new Button();
            pnlPatients = new Panel();
            btnSearchPatient = new Button();
            btnAddPatient = new Button();
            btnPatients = new Button();
            pnlHeader.SuspendLayout();
            pnlMenu.SuspendLayout();
            pnlSettings.SuspendLayout();
            pnlVisits.SuspendLayout();
            pnlPatients.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.AutoSize = true;
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(label1);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1262, 38);
            pnlHeader.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.RoyalBlue;
            label1.Location = new Point(790, -3);
            label1.Name = "label1";
            label1.Size = new Size(381, 41);
            label1.TabIndex = 0;
            label1.Text = "معمل الراعى وام النور بالبرشا";
            label1.Click += label1_Click;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.WhiteSmoke;
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            pnlContent.ForeColor = Color.White;
            pnlContent.Location = new Point(0, 38);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1022, 635);
            pnlContent.TabIndex = 1;
            pnlContent.Paint += pnlContent_Paint;
            // 
            // pnlMenu
            // 
            pnlMenu.BackColor = Color.RoyalBlue;
            pnlMenu.Controls.Add(btnPurchases);
            pnlMenu.Controls.Add(btnReports);
            pnlMenu.Controls.Add(pnlSettings);
            pnlMenu.Controls.Add(btnSettings);
            pnlMenu.Controls.Add(pnlVisits);
            pnlMenu.Controls.Add(btnVisits);
            pnlMenu.Controls.Add(pnlPatients);
            pnlMenu.Controls.Add(btnPatients);
            pnlMenu.Dock = DockStyle.Right;
            pnlMenu.Location = new Point(1022, 38);
            pnlMenu.Name = "pnlMenu";
            pnlMenu.Size = new Size(240, 635);
            pnlMenu.TabIndex = 2;
            pnlMenu.Paint += pnlMenu_Paint;
            // 
            // btnPurchases
            // 
            btnPurchases.Dock = DockStyle.Top;
            btnPurchases.FlatAppearance.BorderSize = 0;
            btnPurchases.FlatStyle = FlatStyle.Flat;
            btnPurchases.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPurchases.ForeColor = Color.White;
            btnPurchases.Location = new Point(0, 510);
            btnPurchases.Name = "btnPurchases";
            btnPurchases.Size = new Size(240, 50);
            btnPurchases.TabIndex = 4;
            btnPurchases.Text = "💰 الطلبيات";
            btnPurchases.UseVisualStyleBackColor = true;
            // 
            // btnReports
            // 
            btnReports.Dock = DockStyle.Top;
            btnReports.FlatAppearance.BorderSize = 0;
            btnReports.FlatStyle = FlatStyle.Flat;
            btnReports.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReports.ForeColor = Color.White;
            btnReports.Location = new Point(0, 460);
            btnReports.Name = "btnReports";
            btnReports.Size = new Size(240, 50);
            btnReports.TabIndex = 3;
            btnReports.Text = "📊 التقارير";
            btnReports.UseVisualStyleBackColor = true;
            // 
            // pnlSettings
            // 
            pnlSettings.Controls.Add(btnSettingLab);
            pnlSettings.Controls.Add(btnSettingOutLab);
            pnlSettings.Controls.Add(btnSettingProducts);
            pnlSettings.Dock = DockStyle.Top;
            pnlSettings.Location = new Point(0, 350);
            pnlSettings.Name = "pnlSettings";
            pnlSettings.Padding = new Padding(25, 0, 0, 0);
            pnlSettings.Size = new Size(240, 110);
            pnlSettings.TabIndex = 1;
            pnlSettings.Visible = false;
            // 
            // btnSettingLab
            // 
            btnSettingLab.Dock = DockStyle.Top;
            btnSettingLab.FlatAppearance.BorderSize = 0;
            btnSettingLab.FlatStyle = FlatStyle.Flat;
            btnSettingLab.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSettingLab.ForeColor = Color.White;
            btnSettingLab.Location = new Point(25, 100);
            btnSettingLab.Name = "btnSettingLab";
            btnSettingLab.Size = new Size(215, 50);
            btnSettingLab.TabIndex = 8;
            btnSettingLab.Text = "📋  التحاليل";
            btnSettingLab.UseVisualStyleBackColor = true;
            // 
            // btnSettingOutLab
            // 
            btnSettingOutLab.Dock = DockStyle.Top;
            btnSettingOutLab.FlatAppearance.BorderSize = 0;
            btnSettingOutLab.FlatStyle = FlatStyle.Flat;
            btnSettingOutLab.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSettingOutLab.ForeColor = Color.White;
            btnSettingOutLab.Location = new Point(25, 50);
            btnSettingOutLab.Name = "btnSettingOutLab";
            btnSettingOutLab.Size = new Size(215, 50);
            btnSettingOutLab.TabIndex = 9;
            btnSettingOutLab.Text = "  🏥المعامل الخارجيه ";
            btnSettingOutLab.UseVisualStyleBackColor = true;
            // 
            // btnSettingProducts
            // 
            btnSettingProducts.Dock = DockStyle.Top;
            btnSettingProducts.FlatAppearance.BorderSize = 0;
            btnSettingProducts.FlatStyle = FlatStyle.Flat;
            btnSettingProducts.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSettingProducts.ForeColor = Color.White;
            btnSettingProducts.Location = new Point(25, 0);
            btnSettingProducts.Name = "btnSettingProducts";
            btnSettingProducts.Size = new Size(215, 50);
            btnSettingProducts.TabIndex = 10;
            btnSettingProducts.Text = "📦 المنتجات";
            btnSettingProducts.UseVisualStyleBackColor = true;
            // 
            // btnSettings
            // 
            btnSettings.Dock = DockStyle.Top;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSettings.ForeColor = Color.White;
            btnSettings.Location = new Point(0, 300);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(240, 50);
            btnSettings.TabIndex = 2;
            btnSettings.Text = "⚙️ الإعدادات";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // pnlVisits
            // 
            pnlVisits.Controls.Add(btnSearchVisit);
            pnlVisits.Controls.Add(bnAddVisit);
            pnlVisits.Dock = DockStyle.Top;
            pnlVisits.Location = new Point(0, 200);
            pnlVisits.Name = "pnlVisits";
            pnlVisits.Padding = new Padding(25, 0, 0, 0);
            pnlVisits.Size = new Size(240, 100);
            pnlVisits.TabIndex = 5;
            pnlVisits.Visible = false;
            // 
            // btnSearchVisit
            // 
            btnSearchVisit.Dock = DockStyle.Top;
            btnSearchVisit.FlatAppearance.BorderSize = 0;
            btnSearchVisit.FlatStyle = FlatStyle.Flat;
            btnSearchVisit.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearchVisit.ForeColor = Color.White;
            btnSearchVisit.Location = new Point(25, 50);
            btnSearchVisit.Name = "btnSearchVisit";
            btnSearchVisit.Size = new Size(215, 50);
            btnSearchVisit.TabIndex = 8;
            btnSearchVisit.Text = "🔍 البحث عن زيارة";
            btnSearchVisit.UseVisualStyleBackColor = true;
            btnSearchVisit.Click += btnSearchVisit_Click;
            // 
            // bnAddVisit
            // 
            bnAddVisit.Dock = DockStyle.Top;
            bnAddVisit.FlatAppearance.BorderSize = 0;
            bnAddVisit.FlatStyle = FlatStyle.Flat;
            bnAddVisit.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            bnAddVisit.ForeColor = Color.Cornsilk;
            bnAddVisit.Location = new Point(25, 0);
            bnAddVisit.Name = "bnAddVisit";
            bnAddVisit.Size = new Size(215, 50);
            bnAddVisit.TabIndex = 9;
            bnAddVisit.Text = "➕ إضافة زيارة";
            bnAddVisit.UseVisualStyleBackColor = true;
            bnAddVisit.Click += bnAddVisit_Click;
            // 
            // btnVisits
            // 
            btnVisits.Dock = DockStyle.Top;
            btnVisits.FlatAppearance.BorderSize = 0;
            btnVisits.FlatStyle = FlatStyle.Flat;
            btnVisits.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVisits.ForeColor = Color.White;
            btnVisits.Location = new Point(0, 150);
            btnVisits.Name = "btnVisits";
            btnVisits.Size = new Size(240, 50);
            btnVisits.TabIndex = 1;
            btnVisits.Text = "\U0001f9ea الزيارات";
            btnVisits.UseVisualStyleBackColor = true;
            btnVisits.Click += btnVisits_Click;
            // 
            // pnlPatients
            // 
            pnlPatients.Controls.Add(btnSearchPatient);
            pnlPatients.Controls.Add(btnAddPatient);
            pnlPatients.Dock = DockStyle.Top;
            pnlPatients.Location = new Point(0, 50);
            pnlPatients.Name = "pnlPatients";
            pnlPatients.Padding = new Padding(25, 0, 0, 0);
            pnlPatients.Size = new Size(240, 100);
            pnlPatients.TabIndex = 0;
            pnlPatients.Visible = false;
            // 
            // btnSearchPatient
            // 
            btnSearchPatient.Dock = DockStyle.Top;
            btnSearchPatient.FlatAppearance.BorderSize = 0;
            btnSearchPatient.FlatStyle = FlatStyle.Flat;
            btnSearchPatient.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSearchPatient.ForeColor = Color.White;
            btnSearchPatient.Location = new Point(25, 50);
            btnSearchPatient.Name = "btnSearchPatient";
            btnSearchPatient.Size = new Size(215, 50);
            btnSearchPatient.TabIndex = 6;
            btnSearchPatient.Text = "🔍 البحث عن مريض";
            btnSearchPatient.UseVisualStyleBackColor = true;
            btnSearchPatient.Click += btnSearchPatient_Click;
            // 
            // btnAddPatient
            // 
            btnAddPatient.Dock = DockStyle.Top;
            btnAddPatient.FlatAppearance.BorderSize = 0;
            btnAddPatient.FlatStyle = FlatStyle.Flat;
            btnAddPatient.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAddPatient.ForeColor = Color.Cornsilk;
            btnAddPatient.Location = new Point(25, 0);
            btnAddPatient.Name = "btnAddPatient";
            btnAddPatient.Size = new Size(215, 50);
            btnAddPatient.TabIndex = 7;
            btnAddPatient.Text = "➕ إضافة مريض";
            btnAddPatient.UseVisualStyleBackColor = true;
            btnAddPatient.Click += btnAddPatient_Click;
            // 
            // btnPatients
            // 
            btnPatients.Dock = DockStyle.Top;
            btnPatients.FlatAppearance.BorderSize = 0;
            btnPatients.FlatStyle = FlatStyle.Flat;
            btnPatients.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPatients.ForeColor = Color.White;
            btnPatients.Location = new Point(0, 0);
            btnPatients.Name = "btnPatients";
            btnPatients.Size = new Size(240, 50);
            btnPatients.TabIndex = 0;
            btnPatients.Text = "👥 المرضى";
            btnPatients.UseVisualStyleBackColor = true;
            btnPatients.Click += btnPatients_Click;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1262, 673);
            Controls.Add(pnlContent);
            Controls.Add(pnlMenu);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1280, 720);
            Name = "FrmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Lab Management System";
            WindowState = FormWindowState.Maximized;
            Load += FrmMain_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlMenu.ResumeLayout(false);
            pnlSettings.ResumeLayout(false);
            pnlVisits.ResumeLayout(false);
            pnlPatients.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlHeader;
        private Panel pnlContent;
        private Panel pnlMenu;
        private Label label1;
        private Button btnPatients;
        private Button btnPurchases;
        private Button btnReports;
        private Button btnSettings;
        private Button btnVisits;
        private Panel pnlPatients;
        private Panel pnlSettings;
        private Panel pnlVisits;
        private Button btnSearchPatient;
        private Button btnAddPatient;
        private Button btnSearchVisit;
        private Button bnAddVisit;
        private Button btnSettingLab;
        private Button btnSettingOutLab;
        private Button btnSettingProducts;
    }
}
