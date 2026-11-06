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
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(16, 6, 16, 6);
            pnlHeader.Size = new Size(1200, 65);
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
            pnlHeaderRight.Controls.Add(lblLabName);
            pnlHeaderRight.Dock = DockStyle.Fill;
            pnlHeaderRight.Location = new Point(412, 3);
            pnlHeaderRight.Name = "pnlHeaderRight";
            pnlHeaderRight.RightToLeft = RightToLeft.Yes;
            pnlHeaderRight.Size = new Size(753, 47);
            pnlHeaderRight.TabIndex = 0;
            // 
            // lblLabName
            // 
            lblLabName.Dock = DockStyle.Top;
            lblLabName.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblLabName.ForeColor = Color.FromArgb(23, 43, 77);
            lblLabName.Location = new Point(0, 0);
            lblLabName.Name = "lblLabName";
            lblLabName.RightToLeft = RightToLeft.Yes;
            lblLabName.Size = new Size(753, 28);
            lblLabName.TabIndex = 0;
            lblLabName.Text = "OliviaLab";
            lblLabName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pnlHeaderLeft
            // 
            pnlHeaderLeft.Controls.Add(lblClock);
            pnlHeaderLeft.Controls.Add(lblCurrentSection);
            pnlHeaderLeft.Dock = DockStyle.Fill;
            pnlHeaderLeft.Location = new Point(3, 3);
            pnlHeaderLeft.Name = "pnlHeaderLeft";
            pnlHeaderLeft.RightToLeft = RightToLeft.Yes;
            pnlHeaderLeft.Size = new Size(403, 47);
            pnlHeaderLeft.TabIndex = 1;
            // 
            // lblClock
            // 
            lblClock.Dock = DockStyle.Bottom;
            lblClock.Font = new Font("Segoe UI", 8.5F);
            lblClock.ForeColor = Color.FromArgb(100, 116, 139);
            lblClock.Location = new Point(0, 27);
            lblClock.Name = "lblClock";
            lblClock.RightToLeft = RightToLeft.Yes;
            lblClock.Size = new Size(403, 20);
            lblClock.TabIndex = 1;
            lblClock.Text = "2026/11/06 --:--";
            lblClock.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblCurrentSection
            // 
            lblCurrentSection.Dock = DockStyle.Top;
            lblCurrentSection.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblCurrentSection.ForeColor = Color.FromArgb(37, 99, 235);
            lblCurrentSection.Location = new Point(0, 0);
            lblCurrentSection.Name = "lblCurrentSection";
            lblCurrentSection.RightToLeft = RightToLeft.Yes;
            lblCurrentSection.Size = new Size(403, 26);
            lblCurrentSection.TabIndex = 0;
            lblCurrentSection.Text = "لوحة التحكم والعمليات";
            lblCurrentSection.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pnlSidebar
            // 
            pnlSidebar.AutoScroll = true;
            pnlSidebar.BackColor = Color.FromArgb(15, 23, 42);
            pnlSidebar.Dock = DockStyle.Right;
            pnlSidebar.Location = new Point(960, 65);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(240, 655);
            pnlSidebar.TabIndex = 1;
            pnlSidebar.Paint += pnlSidebar_Paint;
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
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 720);
            Controls.Add(pnlContent);
            Controls.Add(pnlSidebar);
            Controls.Add(pnlHeader);
            Font = new Font("Segoe UI", 9.5F);
            MinimumSize = new Size(1100, 650);
            Name = "FrmMain";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "نظام إدارة المعامل الطبية - Lab Management System";
            WindowState = FormWindowState.Maximized;
            Load += FrmMain_Load;
            pnlHeader.ResumeLayout(false);
            tlpHeader.ResumeLayout(false);
            pnlHeaderRight.ResumeLayout(false);
            pnlHeaderLeft.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private TableLayoutPanel tlpHeader;
        private Panel pnlHeaderRight;
        private Label lblLabName;
        private Panel pnlHeaderLeft;
        private Label lblCurrentSection;
        private Label lblClock;
        private Panel pnlSidebar;
        private Panel pnlContent;
    }
}
