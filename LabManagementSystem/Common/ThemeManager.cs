using System.Drawing;
using System.Drawing.Drawing2D;

namespace LabManagementSystem.Common
{
    public static class ThemeManager
    {
        // Color Palette
        public static readonly Color PrimaryNavy = ColorTranslator.FromHtml("#172B4D");
        public static readonly Color PrimaryBlue = ColorTranslator.FromHtml("#2563EB");
        public static readonly Color PrimaryHover = ColorTranslator.FromHtml("#1D4ED8");
        public static readonly Color PrimaryLight = ColorTranslator.FromHtml("#EFF6FF");
        public static readonly Color SecondaryBlue = ColorTranslator.FromHtml("#3B82F6");

        public static readonly Color Background = ColorTranslator.FromHtml("#F4F7FB");
        public static readonly Color CardBackground = Color.White;
        public static readonly Color SidebarBackground = ColorTranslator.FromHtml("#0F172A");
        public static readonly Color SidebarHover = ColorTranslator.FromHtml("#1E293B");
        public static readonly Color SidebarActive = ColorTranslator.FromHtml("#2563EB");

        public static readonly Color MainText = ColorTranslator.FromHtml("#1E293B");
        public static readonly Color SecondaryText = ColorTranslator.FromHtml("#64748B");
        public static readonly Color MutedText = ColorTranslator.FromHtml("#94A3B8");

        public static readonly Color Border = ColorTranslator.FromHtml("#E2E8F0");
        public static readonly Color BorderHover = ColorTranslator.FromHtml("#CBD5E1");
        public static readonly Color BorderFocus = ColorTranslator.FromHtml("#3B82F6");

        public static readonly Color Success = ColorTranslator.FromHtml("#16A34A");
        public static readonly Color SuccessLight = ColorTranslator.FromHtml("#DCFCE7");
        public static readonly Color SuccessDark = ColorTranslator.FromHtml("#14532D");

        public static readonly Color Warning = ColorTranslator.FromHtml("#D97706");
        public static readonly Color WarningLight = ColorTranslator.FromHtml("#FEF3C7");

        public static readonly Color Danger = ColorTranslator.FromHtml("#DC2626");
        public static readonly Color DangerLight = ColorTranslator.FromHtml("#FEE2E2");

        // Typography
        public static string PreferredFontFamily { get; private set; } = "Segoe UI";

        public static Font TitleFont { get; private set; } = null!;
        public static Font SubtitleFont { get; private set; } = null!;
        public static Font SectionFont { get; private set; } = null!;
        public static Font BodyFont { get; private set; } = null!;
        public static Font BodyBoldFont { get; private set; } = null!;
        public static Font SmallFont { get; private set; } = null!;
        public static Font KpiNumberFont { get; private set; } = null!;

        static ThemeManager()
        {
            InitializeFonts();
        }

        private static void InitializeFonts()
        {
            try
            {
                var installedFamilies = FontFamily.Families;
                bool hasCairo = installedFamilies.Any(f => f.Name.Equals("Cairo", StringComparison.OrdinalIgnoreCase));
                PreferredFontFamily = hasCairo ? "Cairo" : "Segoe UI";
            }
            catch
            {
                PreferredFontFamily = "Segoe UI";
            }

            TitleFont = new Font(PreferredFontFamily, 14f, FontStyle.Bold);
            SubtitleFont = new Font(PreferredFontFamily, 11.5f, FontStyle.Bold);
            SectionFont = new Font(PreferredFontFamily, 10.5f, FontStyle.Bold);
            BodyFont = new Font(PreferredFontFamily, 9.5f, FontStyle.Regular);
            BodyBoldFont = new Font(PreferredFontFamily, 9.5f, FontStyle.Bold);
            SmallFont = new Font(PreferredFontFamily, 8.5f, FontStyle.Regular);
            KpiNumberFont = new Font(PreferredFontFamily, 16f, FontStyle.Bold);
        }

        public static GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            float r2 = radius / 2f;
            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            int diameter = radius * 2;
            var arc = new Rectangle(rect.Location, new Size(diameter, diameter));

            // Top left
            path.AddArc(arc, 180, 90);

            // Top right
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Bottom right
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Bottom left
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}

