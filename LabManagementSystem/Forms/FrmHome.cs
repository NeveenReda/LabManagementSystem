using System.Drawing;
using System.Windows.Forms;
using LabManagementSystem.Common;

namespace LabManagementSystem.Forms
{
    /// <summary>
    /// Minimal startup home form displaying the laboratory logo and Arabic title centered.
    /// </summary>
    public class FrmHome : Form
    {
        private PictureBox picLogo = null!;
        private Label lblLabTitle = null!;

        public FrmHome()
        {
            InitializeUi();
        }

        private void InitializeUi()
        {
            FormStyler.ApplyFormTheme(this, "معمل د/أوليفيا للتحاليل");
            Padding = new Padding(24);

            var tlpRoot = new TableLayoutPanel
            {
                Name = "tlpHomeCenter",
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 3,
                BackColor = ThemeManager.Background,
                RightToLeft = RightToLeft.Yes
            };
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));

            var pnlCenter = new Panel
            {
                Name = "pnlWelcomeCenter",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Transparent,
                Padding = new Padding(24)
            };

            picLogo = new PictureBox
            {
                Name = "picHomeLogo",
                Size = new Size(260, 260),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Top,
                Image = ImageHelper.LoadSafeImage("imgs/logo.jpeg", 220, 220, ThemeManager.PrimaryNavy)
            };

            var pnlAccent = new Panel
            {
                Name = "pnlAccentLine",
                Size = new Size(140, 4),
                BackColor = ThemeManager.PrimaryBlue
            };

            lblLabTitle = new Label
            {
                Name = "lblHomeTitle",
                Text = "معمل د/أوليفيا للتحاليل",
                Font = new Font(ThemeManager.PreferredFontFamily, 22f, FontStyle.Bold),
                ForeColor = ThemeManager.PrimaryNavy,
                AutoSize = true,
                RightToLeft = RightToLeft.No,
                TextAlign = ContentAlignment.MiddleCenter
            };

            var tlpStack = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.Transparent
            };
            tlpStack.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpStack.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpStack.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpStack.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            picLogo.Anchor = AnchorStyles.None;
            picLogo.Margin = new Padding(0, 0, 0, 18);

            pnlAccent.Anchor = AnchorStyles.None;
            pnlAccent.Margin = new Padding(0, 0, 0, 16);

            lblLabTitle.Anchor = AnchorStyles.None;
            lblLabTitle.Margin = new Padding(0);

            tlpStack.Controls.Add(picLogo, 0, 0);
            tlpStack.Controls.Add(pnlAccent, 0, 1);
            tlpStack.Controls.Add(lblLabTitle, 0, 2);

            pnlCenter.Controls.Add(tlpStack);
            tlpRoot.Controls.Add(pnlCenter, 1, 1);
            Controls.Add(tlpRoot);
        }
    }
}

