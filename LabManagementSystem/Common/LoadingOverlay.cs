using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace LabManagementSystem.Common
{
    public class LoadingOverlay : Control
    {
        private readonly Timer _animTimer;
        private int _startAngle = 0;
        private string _message = "جاري تحميل البيانات...";

        public LoadingOverlay()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);

            BackColor = Color.FromArgb(170, 244, 247, 251); // Semi-transparent overlay
            Visible = false;

            _animTimer = new Timer { Interval = 35 };
            _animTimer.Tick += (s, e) =>
            {
                _startAngle = (_startAngle + 20) % 360;
                Invalidate();
            };
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                Invalidate();
            }
        }

        public void ShowOverlay(string message = "جاري تحميل البيانات...")
        {
            _message = message;
            if (Parent != null)
            {
                Bounds = Parent.ClientRectangle;
                BringToFront();
            }
            Visible = true;
            _animTimer.Start();
        }

        public void HideOverlay()
        {
            _animTimer.Stop();
            Visible = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int centerX = Width / 2;
            int centerY = Height / 2 - 15;
            int radius = 22;
            int diameter = radius * 2;

            // Draw spinner background ring
            using (var penBg = new Pen(ColorTranslator.FromHtml("#CBD5E1"), 4))
            {
                g.DrawEllipse(penBg, centerX - radius, centerY - radius, diameter, diameter);
            }

            // Draw spinning arc
            using (var penArc = new Pen(ThemeManager.PrimaryBlue, 4))
            {
                penArc.StartCap = LineCap.Round;
                penArc.EndCap = LineCap.Round;
                g.DrawArc(penArc, centerX - radius, centerY - radius, diameter, diameter, _startAngle, 100);
            }

            // Draw text
            if (!string.IsNullOrEmpty(_message))
            {
                using var brush = new SolidBrush(ThemeManager.MainText);
                using var sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Near
                };
                var textRect = new Rectangle(0, centerY + radius + 14, Width, 40);
                g.DrawString(_message, ThemeManager.SubtitleFont, brush, textRect, sf);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _animTimer.Dispose();
            }
            base.Dispose(disposing);
        }

        public static LoadingOverlay AttachTo(Control parent)
        {
            var overlay = new LoadingOverlay
            {
                Dock = DockStyle.Fill
            };
            parent.Controls.Add(overlay);
            overlay.BringToFront();
            return overlay;
        }
    }
}

