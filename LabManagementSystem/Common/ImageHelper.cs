using System.Drawing.Drawing2D;

namespace LabManagementSystem.Common
{
    public static class ImageHelper
    {
        private static readonly Dictionary<string, Image> _cache = new();

        public static Image? LoadSafeImage(string relativePath, int fallbackWidth = 16, int fallbackHeight = 16, Color? fallbackColor = null)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return CreateFallbackImage(fallbackWidth, fallbackHeight, fallbackColor ?? Color.Gray);

            if (_cache.TryGetValue(relativePath, out var cached))
                return cached;

            try
            {
                // Try 1: Relative to current directory
                if (File.Exists(relativePath))
                {
                    using var stream = new FileStream(relativePath, FileMode.Open, FileAccess.Read);
                    var img = Image.FromStream(stream);
                    _cache[relativePath] = img;
                    return img;
                }

                // Try 2: Relative to AppDomain.CurrentDomain.BaseDirectory
                string basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
                if (File.Exists(basePath))
                {
                    using var stream = new FileStream(basePath, FileMode.Open, FileAccess.Read);
                    var img = Image.FromStream(stream);
                    _cache[relativePath] = img;
                    return img;
                }

                // Try 3: Search in imgs folder
                string fileName = Path.GetFileName(relativePath);
                string appImgs = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "imgs", fileName);
                if (File.Exists(appImgs))
                {
                    using var stream = new FileStream(appImgs, FileMode.Open, FileAccess.Read);
                    var img = Image.FromStream(stream);
                    _cache[relativePath] = img;
                    return img;
                }
            }
            catch
            {
                // Ignore exception and return fallback
            }

            var fallback = CreateFallbackImage(fallbackWidth, fallbackHeight, fallbackColor ?? Color.SteelBlue);
            _cache[relativePath] = fallback;
            return fallback;
        }

        public static Image CreateFallbackImage(int width, int height, Color color)
        {
            var bmp = new Bitmap(width, height);
            using var g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using var brush = new SolidBrush(color);
            g.FillEllipse(brush, 1, 1, width - 2, height - 2);
            return bmp;
        }
    }
}

