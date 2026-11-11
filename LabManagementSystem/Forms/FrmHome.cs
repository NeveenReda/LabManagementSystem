using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LabManagementSystem.Common;

namespace LabManagementSystem.Forms
{
    /// <summary>
    /// Minimalist welcome screen displaying the transparent laboratory emblem directly
    /// on the light blue-gray canvas, followed by a subtle golden separator and centered Arabic title.
    /// </summary>
    public class FrmHome : Form
    {
        private static readonly Color CanvasBackground = ColorTranslator.FromHtml("#F5F7FA");
        private static readonly Color TitleNavy = ColorTranslator.FromHtml("#0F274A");
        private static readonly Color AccentGold = ColorTranslator.FromHtml("#C89B2B");

        private PictureBox picLogo = null!;
        private Panel pnlAccent = null!;
        private Label lblLabTitle = null!;
        private Image? _logoImage;
        private double _logoAspectRatio = 1024.0 / 1535.0;

        public FrmHome()
        {
            DoubleBuffered = true;
            InitializeUi();
            Load += (s, e) => UpdateResponsiveLayout();
            Resize += (s, e) => UpdateResponsiveLayout();
        }

        private void InitializeUi()
        {
            FormStyler.ApplyFormTheme(this, "معمل د/أوليفيا للتحاليل");
            Padding = new Padding(24);
            BackColor = CanvasBackground;

            _logoImage = ImageHelper.LoadSafeImage("imgs/logo.png", 300, 450, TitleNavy);
            if (_logoImage != null && _logoImage.Height > 0)
            {
                _logoAspectRatio = (double)_logoImage.Width / _logoImage.Height;
            }

            var tlpRoot = new TableLayoutPanel
            {
                Name = "tlpHomeCenter",
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 3,
                BackColor = CanvasBackground,
                RightToLeft = RightToLeft.Yes
            };
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 48F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 52F));

            var tlpStack = new TableLayoutPanel
            {
                Name = "tlpWelcomeStack",
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 3,
                BackColor = Color.Transparent,
                RightToLeft = RightToLeft.Yes,
                Padding = new Padding(12)
            };
            tlpStack.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlpStack.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpStack.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tlpStack.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            picLogo = new PictureBox
            {
                Name = "picHomeLogo",
                Size = new Size(270, 405),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None,
                Anchor = AnchorStyles.None,
                Margin = new Padding(0, 0, 0, 18),
                Image = _logoImage
            };

            pnlAccent = new Panel
            {
                Name = "pnlAccentLine",
                Size = new Size(104, 2),
                BackColor = AccentGold,
                Anchor = AnchorStyles.None,
                Margin = new Padding(0, 0, 0, 14)
            };

            string arabicFont = ResolvePreferredArabicFont();
            lblLabTitle = new Label
            {
                Name = "lblHomeTitle",
                Text = "معمل د/أوليفيا للتحاليل",
                Font = new Font(arabicFont, 24f, FontStyle.Bold),
                ForeColor = TitleNavy,
                BackColor = Color.Transparent,
                AutoSize = true,
                UseCompatibleTextRendering = false,
                RightToLeft = RightToLeft.Yes,
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.None,
                Margin = new Padding(12, 0, 12, 6)
            };

            tlpStack.Controls.Add(picLogo, 0, 0);
            tlpStack.Controls.Add(pnlAccent, 0, 1);
            tlpStack.Controls.Add(lblLabTitle, 0, 2);

            tlpRoot.Controls.Add(tlpStack, 1, 1);
            Controls.Add(tlpRoot);

            UpdateResponsiveLayout();
        }

        private static string ResolvePreferredArabicFont()
        {
            try
            {
                var families = FontFamily.Families.Select(f => f.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
                foreach (var candidate in new[] { "Cairo", "Tajawal", "Segoe UI", "Tahoma", "Arial" })
                {
                    if (families.Contains(candidate))
                        return candidate;
                }
            }
            catch { }

            return ThemeManager.PreferredFontFamily;
        }

        private void UpdateResponsiveLayout()
        {
            if (IsDisposed || picLogo == null || lblLabTitle == null) return;

            int availW = Math.Max(320, ClientSize.Width - 100);
            int availH = Math.Max(340, ClientSize.Height - 150);

            // Balanced proportional emblem sizing without overwhelming or clipping
            int targetH = Math.Clamp((int)(availH * 0.64), 240, 440);
            int targetW = (int)Math.Round(targetH * _logoAspectRatio);

            int maxAllowedW = Math.Max(200, availW - 60);
            if (targetW > maxAllowedW)
            {
                targetW = maxAllowedW;
                targetH = (int)Math.Round(targetW / _logoAspectRatio);
            }

            if (picLogo.Size.Width != targetW || picLogo.Size.Height != targetH)
            {
                picLogo.Size = new Size(targetW, targetH);
            }

            float fontSize = Math.Clamp(ClientSize.Height / 27f, 20f, 26f);
            if (Math.Abs(lblLabTitle.Font.Size - fontSize) > 0.5f)
            {
                string family = lblLabTitle.Font.FontFamily.Name;
                var oldFont = lblLabTitle.Font;
                lblLabTitle.Font = new Font(family, fontSize, FontStyle.Bold);
                oldFont.Dispose();
            }

            int accentW = Math.Clamp((int)(targetW * 0.40), 84, 132);
            if (pnlAccent.Width != accentW)
            {
                pnlAccent.Size = new Size(accentW, 2);
            }
        }
    }
}
