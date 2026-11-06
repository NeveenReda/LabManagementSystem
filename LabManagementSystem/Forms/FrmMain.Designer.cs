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
            lblOliviaLab = new Label();
            pnlSidebar = new Panel();
            pnlFooter = new Panel();
            lblFooter = new Label();
            pnlContent = new Panel();
            pnlHeader.SuspendLayout();
            pnlFooter.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblOliviaLab);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 60;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(16, 6, 16, 6);
            pnlHeader.Size = new Size(1200, 60);
            pnlHeader.TabIndex = 0;
            // 
            // lblOliviaLab
            // 
            lblOliviaLab.AutoSize = false;
            lblOliviaLab.Dock = DockStyle.Fill;
            lblOliviaLab.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblOliviaLab.ForeColor = Color.FromArgb(23, 43, 77);
            lblOliviaLab.Location = new Point(16, 6);
            lblOliviaLab.Name = "lblOliviaLab";
            lblOliviaLab.Size = new Size(1168, 48);
            lblOliviaLab.TabIndex = 0;
            lblOliviaLab.Text = "Olivia Lab";
            lblOliviaLab.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.FromArgb(248, 250, 252);
            pnlFooter.Controls.Add(lblFooter);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Height = 30;
            pnlFooter.Location = new Point(0, 690);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Size = new Size(1200, 30);
            pnlFooter.TabIndex = 3;
            // 
            // lblFooter
            // 
            lblFooter.Dock = DockStyle.Fill;
            lblFooter.Font = new Font("Segoe UI", 8.5F);
            lblFooter.ForeColor = Color.FromArgb(100, 116, 139);
            lblFooter.Location = new Point(0, 0);
            lblFooter.Name = "lblFooter";
            lblFooter.Size = new Size(1200, 30);
            lblFooter.TabIndex = 0;
            lblFooter.Text = "Powered by Tech Spot Canadian 2026";
            lblFooter.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlSidebar
            // 
            pnlSidebar.AutoScroll = true;
            pnlSidebar.BackColor = Color.FromArgb(15, 23, 42);
            pnlSidebar.Dock = DockStyle.Left; // With RightToLeftLayout = true, DockStyle.Left positions sidebar at the physical RIGHT
            pnlSidebar.Location = new Point(0, 65);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(250, 625);
            pnlSidebar.TabIndex = 1;
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(244, 247, 251);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 65);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(950, 625);
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
            Controls.Add(pnlFooter);
            Font = new Font("Segoe UI", 9.5F);
            MinimumSize = new Size(1100, 650);
            Name = "FrmMain";
            RightToLeft = RightToLeft.Yes;
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Olivia Lab — نظام إدارة المعامل الطبية";
            WindowState = FormWindowState.Maximized;
            pnlHeader.ResumeLayout(false);
            pnlFooter.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlHeader;
        private Label lblOliviaLab;
        private Panel pnlFooter;
        private Label lblFooter;
        private Panel pnlSidebar;
        private Panel pnlContent;
    }
}
