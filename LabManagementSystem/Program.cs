using LabManagementSystem.Models;
using LabManagementSystem.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LabManagementSystem
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Setup global exception handling with file logging
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            
            Action<string, Exception> logException = (source, ex) =>
            {
                try
                {
                    string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{source}] {ex.GetType().FullName}: {ex.Message}\n{ex.StackTrace}\n";
                    if (ex.InnerException != null)
                        logEntry += $"Inner: {ex.InnerException.GetType().FullName}: {ex.InnerException.Message}\n{ex.InnerException.StackTrace}\n";
                    logEntry += "\n";

                    File.AppendAllText("error.log", logEntry);

                    string appDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LabManagementSystem", "logs");
                    Directory.CreateDirectory(appDataDir);
                    File.AppendAllText(Path.Combine(appDataDir, "error.log"), logEntry);
                }
                catch { }
            };

            Application.ThreadException += (s, e) =>
            {
                logException("ThreadException", e.Exception);
                MessageBox.Show($"حدث خطأ في النظام:\n{e.Exception.Message}\n\nنوع الخطأ: {e.Exception.GetType().Name}\nيرجى مراجعة ملف السجل للتفاصيل.", "تنبيه النظام", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                if (e.ExceptionObject is Exception ex)
                {
                    logException("UnhandledException", ex);
                    MessageBox.Show($"حدث خطأ حرج في النظام:\n{ex.Message}\n\nنوع الخطأ: {ex.GetType().Name}", "خطأ حرج", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            };

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                logException("UnobservedTaskException", e.Exception);
                e.SetObserved(); // Prevent unobserved task exceptions from crashing process
            };

            // Global PDF engine license initialization (run once for instant document generation)
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            // Configure Dependency Injection
            var services = new ServiceCollection();

            services.AddTransient<LabContext>();
            services.AddSingleton<Func<LabContext>>(sp => () => sp.GetRequiredService<LabContext>());

            services.AddSingleton<ILookupCacheService, LookupCacheService>();
            services.AddSingleton<IPatientService, PatientService>();
            services.AddSingleton<IVisitService, VisitService>();
            services.AddSingleton<IBillingService, BillingService>();
            services.AddSingleton<ILabReportService, LabReportService>();
            services.AddSingleton<ILabSettingsService, LabSettingsService>();
            services.AddSingleton<ITaskService, TaskService>();
            services.AddSingleton<IInventoryService, InventoryService>();
            services.AddSingleton<IFinancialReportService, FinancialReportService>();

            var serviceProvider = services.BuildServiceProvider();
            ServiceLocator.Initialize(serviceProvider);

            Application.Run(new FrmMain());
        }
    }
}
