namespace LabManagementSystem
{
    partial class FrmMain
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
            tlpHeader = new TableLayoutPanel();
            pnlHeaderRight = new Panel();
            lblLabSubtitle = new Label();
            lblLabName = new Label();
            pnlHeaderLeft = new Panel();
            lblClock = new Label();
            lblCurrentSection = new Label();
            pnlSidebar = new Panel();
            pnlContent = new Panel();
            pnlHeader.SuspendLayout();
            tlpHeader.SuspendLayout();
            pnlHeaderRight.SuspendLayout();
            pnlHeaderLeft.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(tlpHeader);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 65;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(16, 6, 16, 6);
            pnlHeader.TabIndex = 0;
            // 
            // tlpHeader
            // 
            tlpHeader.ColumnCount = 2;
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tlpHeader.Controls.Add(pnlHeaderRight, 0, 0);
            tlpHeader.Controls.Add(pnlHeaderLeft, 1, 0);
            tlpHeader.Dock = DockStyle.Fill;
            tlpHeader.Location = new Point(16, 6);
            tlpHeader.Name = "tlpHeader";
            tlpHeader.RightToLeft = RightToLeft.Yes;
            tlpHeader.RowCount = 1;
            tlpHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpHeader.Size = new Size(1168, 53);
            tlpHeader.TabIndex = 0;
            // 
            // pnlHeaderRight
            // 
            pnlHeaderRight.Controls.Add(lblLabSubtitle);
            pnlHeaderRight.Controls.Add(lblLabName);
            pnlHeaderRight.Dock = DockStyle.Fill;
            pnlHeaderRight.Location = new Point(412, 0);
            pnlHeaderRight.Name = "pnlHeaderRight";
            pnlHeaderRight.RightToLeft = RightToLeft.Yes;
            pnlHeaderRight.Size = new Size(756, 53);
            pnlHeaderRight.TabIndex = 0;
            // 
            // lblLabName
            // 
            lblLabName.AutoSize = false;
            lblLabName.Dock = DockStyle.Top;
            lblLabName.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblLabName.ForeColor = Color.FromArgb(23, 43, 77);
            lblLabName.Height = 28;
            lblLabName.Location = new Point(0, 0);
            lblLabName.Name = "lblLabName";
            lblLabName.RightToLeft = RightToLeft.Yes;
            lblLabName.Size = new Size(756, 28);
            lblLabName.TabIndex = 0;
            lblLabName.Text = "معمل الأمل للتحاليل الطبية والتشخيص المخبري";
            lblLabName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblLabSubtitle
            // 
            lblLabSubtitle.AutoSize = false;
            lblLabSubtitle.Dock = DockStyle.Bottom;
            lblLabSubtitle.Font = new Font("Segoe UI", 8.5F);
            lblLabSubtitle.ForeColor = Color.FromArgb(100, 116, 139);
            lblLabSubtitle.Height = 20;
            lblLabSubtitle.Location = new Point(0, 33);
            lblLabSubtitle.Name = "lblLabSubtitle";
            lblLabSubtitle.RightToLeft = RightToLeft.Yes;
            lblLabSubtitle.Size = new Size(756, 20);
            lblLabSubtitle.TabIndex = 1;
            lblLabSubtitle.Text = "Al-Amal Clinical Laboratory & Diagnostic Center";
            lblLabSubtitle.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlHeaderLeft
            // 
            pnlHeaderLeft.Controls.Add(lblClock);
            pnlHeaderLeft.Controls.Add(lblCurrentSection);
            pnlHeaderLeft.Dock = DockStyle.Fill;
            pnlHeaderLeft.Location = new Point(0, 0);
            pnlHeaderLeft.Name = "pnlHeaderLeft";
            pnlHeaderLeft.RightToLeft = RightToLeft.Yes;
            pnlHeaderLeft.Size = new Size(406, 53);
            pnlHeaderLeft.TabIndex = 1;
            // 
            // lblCurrentSection
            // 
            lblCurrentSection.AutoSize = false;
            lblCurrentSection.Dock = DockStyle.Top;
            lblCurrentSection.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblCurrentSection.ForeColor = Color.FromArgb(37, 99, 235);
            lblCurrentSection.Height = 26;
            lblCurrentSection.Location = new Point(0, 0);
            lblCurrentSection.Name = "lblCurrentSection";
            lblCurrentSection.RightToLeft = RightToLeft.Yes;
            lblCurrentSection.Size = new Size(406, 26);
            lblCurrentSection.TabIndex = 0;
            lblCurrentSection.Text = "لوحة التحكم والعمليات";
            lblCurrentSection.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblClock
            // 
            lblClock.AutoSize = false;
            lblClock.Dock = DockStyle.Bottom;
            lblClock.Font = new Font("Segoe UI", 8.5F);
            lblClock.ForeColor = Color.FromArgb(100, 116, 139);
            lblClock.Height = 20;
            lblClock.Location = new Point(0, 33);
            lblClock.Name = "lblClock";
            lblClock.RightToLeft = RightToLeft.Yes;
            lblClock.Size = new Size(406, 20);
            lblClock.TabIndex = 1;
            lblClock.Text = "2026/11/06 --:--";
            lblClock.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlSidebar
            // 
            pnlSidebar.AutoScroll = true;
            pnlSidebar.BackColor = Color.FromArgb(15, 23, 42);
            pnlSidebar.Dock = DockStyle.Right; // Navigation sidebar on the physical RIGHT for Arabic RTL
            pnlSidebar.Location = new Point(960, 65);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(240, 655);
            pnlSidebar.TabIndex = 1;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(244, 247, 251);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 65);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(960, 655);
            pnlContent.TabIndex = 2;
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 720);
            Controls.Add(pnlContent);
            Controls.Add(pnlSidebar);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9.5F);
            MinimumSize = new Size(1100, 650);
            Name = "FrmMain";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "نظام إدارة المعامل الطبية - Lab Management System";
            WindowState = FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            tlpHeader.ResumeLayout(false);
            pnlHeaderRight.ResumeLayout(false);
            pnlHeaderRight.PerformLayout();
            pnlHeaderLeft.ResumeLayout(false);
            pnlHeaderLeft.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private TableLayoutPanel tlpHeader;
        private Panel pnlHeaderRight;
        private Label lblLabName;
        private Label lblLabSubtitle;
        private Panel pnlHeaderLeft;
        private Label lblCurrentSection;
        private Label lblClock;
        private Panel pnlSidebar;
        private Panel pnlContent;
    }
}
