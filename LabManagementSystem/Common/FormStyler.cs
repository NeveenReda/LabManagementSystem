using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace LabManagementSystem.Common
{
    public enum ButtonStyle
    {
        Primary,
        Secondary,
        Success,
        Danger,
        Warning,
        Outline
    }

    public static class FormStyler
    {
        public static void ApplyFormTheme(Form form, string? title = null)
        {
            if (form == null) return;

            form.RightToLeft = RightToLeft.Yes;
            form.RightToLeftLayout = true;
            form.Font = ThemeManager.BodyFont;
            form.BackColor = ThemeManager.Background;
            form.ForeColor = ThemeManager.MainText;
            form.StartPosition = FormStartPosition.CenterScreen;

            if (!string.IsNullOrWhiteSpace(title))
                form.Text = title;

            // Enable double buffering for the form
            try
            {
                var prop = typeof(Form).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                prop?.SetValue(form, true, null);
            }
            catch { }
        }

        public static Panel CreateHeaderPanel(string title, string subtitle, string icon = "📋")
        {
            var pnl = new Panel
            {
                Dock = DockStyle.Top,
                Height = 68,
                BackColor = ThemeManager.PrimaryNavy,
                Padding = new Padding(20, 10, 20, 10),
                RightToLeft = RightToLeft.Yes
            };

            var lblTitle = new Label
            {
                Text = $"{icon}  {title}",
                Font = ThemeManager.TitleFont,
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                Height = 28,
                AutoSize = false,
                RightToLeft = RightToLeft.No,
                TextAlign = ContentAlignment.MiddleRight
            };

            var lblSub = new Label
            {
                Text = subtitle,
                Font = ThemeManager.SmallFont,
                ForeColor = ColorTranslator.FromHtml("#94A3B8"),
                Dock = DockStyle.Bottom,
                Height = 20,
                AutoSize = false,
                RightToLeft = RightToLeft.No,
                TextAlign = ContentAlignment.MiddleRight
            };

            pnl.Controls.Add(lblTitle);
            pnl.Controls.Add(lblSub);

            return pnl;
        }

        public static Panel CreateFooterPanel()
        {
            var pnl = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 30,
                BackColor = ColorTranslator.FromHtml("#F8FAFC"),
                Padding = new Padding(12, 0, 12, 0)
            };

            pnl.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeManager.Border, 1);
                e.Graphics.DrawLine(pen, 0, 0, pnl.Width, 0);
            };

            var lbl = new Label
            {
                Text = "Powered by Tech Spot Canadian 2026",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font(ThemeManager.PreferredFontFamily, 8.5f, FontStyle.Regular),
                ForeColor = ColorTranslator.FromHtml("#64748B")
            };

            pnl.Controls.Add(lbl);
            return pnl;
        }

        public static void StyleHeaderPanel(Panel pnlHeader, Label lblTitle, Label? lblSubtitle = null, Button? actionButton = null)
        {
            pnlHeader.Height = 68;
            pnlHeader.BackColor = ThemeManager.PrimaryNavy;
            pnlHeader.Padding = new Padding(20, 10, 20, 10);
            pnlHeader.RightToLeft = RightToLeft.Yes;

            lblTitle.Font = ThemeManager.TitleFont;
            lblTitle.ForeColor = Color.White;
            lblTitle.TextAlign = ContentAlignment.MiddleRight;
            lblTitle.RightToLeft = RightToLeft.No;
            lblTitle.AutoSize = false;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 28;

            if (lblSubtitle != null)
            {
                lblSubtitle.Font = ThemeManager.SmallFont;
                lblSubtitle.ForeColor = ColorTranslator.FromHtml("#94A3B8");
                lblSubtitle.TextAlign = ContentAlignment.MiddleRight;
                lblSubtitle.RightToLeft = RightToLeft.No;
                lblSubtitle.AutoSize = false;
                lblSubtitle.Dock = DockStyle.Bottom;
                lblSubtitle.Height = 20;
            }

            if (actionButton != null)
            {
                actionButton.Dock = DockStyle.Left;
                actionButton.Height = 40;
                actionButton.Cursor = Cursors.Hand;
            }
        }

        public static Panel CreateCardPanel(DockStyle dock = DockStyle.None, Padding? padding = null)
        {
            var pnl = new Panel
            {
                Dock = dock,
                BackColor = ThemeManager.CardBackground,
                Padding = padding ?? new Padding(16)
            };

            pnl.Paint += (s, e) =>
            {
                using var pen = new Pen(ThemeManager.Border, 1);
                var rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);
                e.Graphics.DrawRectangle(pen, rect);
            };

            return pnl;
        }

        public static Panel CreateKpiCard(string title, string value, Color accentColor, string? subText = null)
        {
            var card = new Panel
            {
                BackColor = ThemeManager.CardBackground,
                Padding = new Padding(14, 10, 14, 10),
                Margin = new Padding(6)
            };

            var lblValue = new Label
            {
                Text = value,
                Font = ThemeManager.KpiNumberFont,
                ForeColor = accentColor,
                Dock = DockStyle.Top,
                Height = 32,
                TextAlign = ContentAlignment.MiddleRight
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = ThemeManager.BodyBoldFont,
                ForeColor = ThemeManager.MainText,
                Dock = DockStyle.Top,
                Height = 22,
                TextAlign = ContentAlignment.MiddleRight
            };

            card.Controls.Add(lblValue);
            card.Controls.Add(lblTitle);

            if (!string.IsNullOrWhiteSpace(subText))
            {
                var lblSub = new Label
                {
                    Text = subText,
                    Font = ThemeManager.SmallFont,
                    ForeColor = ThemeManager.SecondaryText,
                    Dock = DockStyle.Bottom,
                    Height = 16,
                    TextAlign = ContentAlignment.MiddleRight
                };
                card.Controls.Add(lblSub);
            }

            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Subtle card border
                using var pen = new Pen(ThemeManager.Border, 1);
                var rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                e.Graphics.DrawRectangle(pen, rect);

                // Right accent indicator bar
                using var accentBrush = new SolidBrush(accentColor);
                e.Graphics.FillRectangle(accentBrush, card.Width - 5, 0, 5, card.Height);
            };

            return card;
        }

        public static void StyleButton(Button btn, ButtonStyle style = ButtonStyle.Primary)
        {
            if (btn == null) return;

            btn.FlatStyle = FlatStyle.Flat;
            btn.Cursor = Cursors.Hand;
            btn.Font = ThemeManager.BodyBoldFont;
            btn.Height = Math.Max(btn.Height, 36);

            switch (style)
            {
                case ButtonStyle.Primary:
                    btn.BackColor = ThemeManager.PrimaryBlue;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = ThemeManager.PrimaryHover;
                    break;

                case ButtonStyle.Secondary:
                    btn.BackColor = Color.White;
                    btn.ForeColor = ThemeManager.MainText;
                    btn.FlatAppearance.BorderColor = ThemeManager.Border;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.MouseOverBackColor = ThemeManager.Background;
                    break;

                case ButtonStyle.Success:
                    btn.BackColor = ThemeManager.Success;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = ThemeManager.SuccessDark;
                    break;

                case ButtonStyle.Danger:
                    btn.BackColor = ThemeManager.Danger;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#B91C1C");
                    break;

                case ButtonStyle.Warning:
                    btn.BackColor = ThemeManager.Warning;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#B45309");
                    break;

                case ButtonStyle.Outline:
                    btn.BackColor = Color.Transparent;
                    btn.ForeColor = ThemeManager.PrimaryBlue;
                    btn.FlatAppearance.BorderColor = ThemeManager.PrimaryBlue;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.MouseOverBackColor = ThemeManager.PrimaryLight;
                    break;
            }
        }

        public static void StyleInput(Control ctrl)
        {
            if (ctrl == null) return;

            ctrl.Font = ThemeManager.BodyFont;
            ctrl.ForeColor = ThemeManager.MainText;

            if (ctrl is TextBox txt)
            {
                txt.BorderStyle = BorderStyle.FixedSingle;
                txt.BackColor = txt.ReadOnly ? ColorTranslator.FromHtml("#F1F5F9") : Color.White;
            }
            else if (ctrl is ComboBox cmb)
            {
                cmb.FlatStyle = FlatStyle.Flat;
            }
        }

        public static void StyleGroupBox(GroupBox gb)
        {
            if (gb == null) return;

            gb.Font = ThemeManager.SectionFont;
            gb.ForeColor = ThemeManager.PrimaryNavy;
            gb.BackColor = ThemeManager.CardBackground;
            gb.Padding = new Padding(12);
        }
    }
}

