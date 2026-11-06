using System.Text.Json;
using LabManagementSystem.Models;

namespace LabManagementSystem.Configuration
{
    public class LabSettings
    {
        public string LabName { get; set; } = "معمل الأمل للتحاليل الطبية";
        public string LabNameEn { get; set; } = "Al-Amal Clinical Laboratory";
        public string LabSubtitle { get; set; } = "Al-Amal Clinical Laboratory & Diagnostic Center";
        public string Phone { get; set; } = "01000000000";
        public string AdditionalPhone { get; set; } = "";
        public string Address { get; set; } = "شارع الجمهورية - برج الأطباء - الدور الثاني";
        public string Email { get; set; } = "info@alamallab.com";
        public string DirectorName { get; set; } = "د. أخصائي التحاليل الطبية";
        public string ReportFooterNote { get; set; } = "النتائج معتمدة إلكترونياً. تمنياتنا لكم بالشفاء العاجل.";

        public static LabSettings FromProfile(LaboratoryProfile profile)
        {
            return new LabSettings
            {
                LabName = profile.LabName,
                LabNameEn = profile.LabNameEn ?? "Al-Amal Clinical Laboratory",
                LabSubtitle = profile.Subtitle ?? profile.LabNameEn ?? "",
                Phone = profile.Phone ?? "",
                AdditionalPhone = profile.AdditionalPhone ?? "",
                Address = profile.Address ?? "",
                Email = profile.Email ?? "",
                DirectorName = profile.DirectorName ?? "",
                ReportFooterNote = profile.ReportFooterNote ?? "النتائج معتمدة إلكترونياً. تمنياتنا لكم بالشفاء العاجل."
            };
        }
    }

    public static class AppConfig
    {
        public const string DefaultConnectionString = "Server=db43945.public.databaseasp.net;Database=db43945;User Id=db43945;Password=8y@LwB9!6+Hd;TrustServerCertificate=True;";
        private static LabSettings? _settings;

        public static event Action<LabSettings>? LabSettingsChanged;

        public static string GetConnectionString()
        {
            try
            {
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
                if (File.Exists(configPath))
                {
                    string json = File.ReadAllText(configPath);
                    using var doc = JsonDocument.Parse(json);
                    if (doc.RootElement.TryGetProperty("ConnectionStrings", out var connSection) &&
                        connSection.TryGetProperty("LabConnection", out var connStr))
                    {
                        string val = connStr.GetString() ?? "";
                        if (!string.IsNullOrWhiteSpace(val))
                            return val;
                    }
                }
            }
            catch
            {
                // Fallback to default
            }

            return DefaultConnectionString;
        }

        public static LabSettings GetLabSettings()
        {
            if (_settings != null) return _settings;

            // 1. Try loading from database if ServiceLocator is ready
            try
            {
                var service = Services.ServiceLocator.LabSettingsService;
                if (service != null)
                {
                    var profile = service.GetLaboratoryProfile();
                    if (profile != null)
                    {
                        _settings = LabSettings.FromProfile(profile);
                        return _settings;
                    }
                }
            }
            catch
            {
                // Fallback to config file
            }

            // 2. Try loading from appsettings.json
            try
            {
                string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
                if (File.Exists(configPath))
                {
                    string json = File.ReadAllText(configPath);
                    using var doc = JsonDocument.Parse(json);
                    if (doc.RootElement.TryGetProperty("LabSettings", out var sec))
                    {
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        _settings = JsonSerializer.Deserialize<LabSettings>(sec.GetRawText(), options);
                    }
                }
            }
            catch
            {
                // ignore
            }

            return _settings ??= new LabSettings();
        }

        public static void UpdateLabSettings(LabSettings settings)
        {
            _settings = settings;
            LabSettingsChanged?.Invoke(settings);
        }

        public static void UpdateLabSettings(LaboratoryProfile profile)
        {
            var settings = LabSettings.FromProfile(profile);
            UpdateLabSettings(settings);
        }
    }
}
