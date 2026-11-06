using Microsoft.Extensions.DependencyInjection;

namespace LabManagementSystem.Services
{
    public static class ServiceLocator
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        public static void Initialize(IServiceProvider provider)
        {
            ServiceProvider = provider;
        }

        public static T GetService<T>() where T : notnull
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        public static IPatientService PatientService => GetService<IPatientService>();
        public static IVisitService VisitService => GetService<IVisitService>();
        public static IBillingService BillingService => GetService<IBillingService>();
        public static ILabReportService LabReportService => GetService<ILabReportService>();
        public static ILabSettingsService LabSettingsService => GetService<ILabSettingsService>();
        public static ITaskService TaskService => GetService<ITaskService>();
        public static IInventoryService InventoryService => GetService<IInventoryService>();
        public static IFinancialReportService FinancialReportService => GetService<IFinancialReportService>();
        public static ILookupCacheService LookupCacheService => GetService<ILookupCacheService>();
    }
}

