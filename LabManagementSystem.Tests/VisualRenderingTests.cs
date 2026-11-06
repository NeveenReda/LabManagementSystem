using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LabManagementSystem.Common;
using LabManagementSystem.Forms;
using LabManagementSystem.Models;
using LabManagementSystem.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LabManagementSystem.Tests
{
    public class VisualRenderingTests
    {
        public VisualRenderingTests()
        {
            var services = new ServiceCollection();
            services.AddTransient<LabContext>();
            services.AddSingleton<Func<LabContext>>(sp => () => new LabContext());
            services.AddSingleton<ILookupCacheService, LookupCacheService>();
            services.AddSingleton<IPatientService, PatientService>();
            services.AddSingleton<IVisitService, VisitService>();
            services.AddSingleton<IBillingService, BillingService>();
            services.AddSingleton<ILabReportService, LabReportService>();
            services.AddSingleton<ILabSettingsService, LabSettingsService>();
            services.AddSingleton<ITaskService, TaskService>();
            services.AddSingleton<IInventoryService, InventoryService>();
            services.AddSingleton<IFinancialReportService, FinancialReportService>();

            var sp = services.BuildServiceProvider();
            ServiceLocator.Initialize(sp);
        }

        [Fact]
        public void Render_AllKeyScreens_ShouldDrawSuccessfully()
        {
            using var frm = new FrmMain();
            frm.Size = new Size(1280, 800);
            frm.CreateControl();

            // Verify Olivia Lab header is centered
            var lblOlivia = frm.Controls.Find("lblOliviaLab", true).FirstOrDefault() as Label;
            Assert.NotNull(lblOlivia);
            Assert.Equal("Olivia Lab", lblOlivia.Text);
            Assert.Equal(ContentAlignment.MiddleCenter, lblOlivia.TextAlign);

            // Verify Footer
            var lblFooter = frm.Controls.Find("lblFooter", true).FirstOrDefault() as Label;
            Assert.NotNull(lblFooter);
            Assert.Equal("Powered by Tech Spot Canadian 2026", lblFooter.Text);
            Assert.Equal(ContentAlignment.MiddleCenter, lblFooter.TextAlign);

            // Test navigation to screens
            frm.OpenDashboard();
            Assert.NotNull(frm.Controls.Find("pnlContent", true).FirstOrDefault());

            frm.OpenSearchPatients();
            frm.OpenAddPatient();
            frm.OpenSearchVisits();
            frm.OpenAddVisit();
            frm.OpenDebts();
            frm.OpenFinancialReports();
            frm.OpenInventoryProducts();
            frm.OpenInventoryOrders();
            frm.OpenLabSettings();
            frm.OpenSystemSettings();
        }
    }
}
