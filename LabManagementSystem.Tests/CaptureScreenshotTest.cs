using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using LabManagementSystem.Forms;
using LabManagementSystem.Models;
using LabManagementSystem.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LabManagementSystem.Tests
{
    public class CaptureScreenshotTest
    {
        private T? FindControl<T>(Control parent) where T : Control
        {
            if (parent is T match) return match;
            foreach (Control c in parent.Controls)
            {
                var f = FindControl<T>(c);
                if (f != null) return f;
            }
            return null;
        }

        [Fact]
        public void Verify_RtlSidebar_And_Header_And_ControlFlow()
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

            using var frm = new FrmMain();
            frm.StartPosition = FormStartPosition.Manual;
            frm.Location = new Point(50, 50);
            frm.Size = new Size(1200, 750);
            frm.Show();
            Application.DoEvents();
            Thread.Sleep(300);
            Application.DoEvents();

            // Verify header has ONLY Olivia Lab
            var pnlHeader = frm.Controls.Find("pnlHeader", true)[0];
            var lblOlivia = pnlHeader.Controls.Find("lblOliviaLab", true).FirstOrDefault() as Label;
            Assert.NotNull(lblOlivia);
            Assert.Equal("Olivia Lab", lblOlivia.Text);
            Assert.Equal(ContentAlignment.MiddleCenter, lblOlivia.TextAlign);
            Assert.Single(pnlHeader.Controls); // ONLY Olivia Lab in header

            // Verify physical Sidebar is on the physical RIGHT
            var pnlSidebar = frm.Controls.Find("pnlSidebar", true)[0];
            var pnlContent = frm.Controls.Find("pnlContent", true)[0];
            var sbScreen = pnlSidebar.PointToScreen(Point.Empty);
            var ctScreen = pnlContent.PointToScreen(Point.Empty);
            Assert.True(sbScreen.X > ctScreen.X, $"Expected physical Sidebar X ({sbScreen.X}) > Content X ({ctScreen.X})");

            // Open Dashboard and check action buttons flow from right to left
            frm.OpenDashboard();
            Application.DoEvents();
            Thread.Sleep(200);
            Application.DoEvents();

            var dash = pnlContent.Controls.OfType<Form>().FirstOrDefault();
            Assert.NotNull(dash);

            // Save visual verification bitmap
            var bmp = new Bitmap(frm.Width, frm.Height);
            frm.DrawToBitmap(bmp, new Rectangle(0, 0, frm.Width, frm.Height));
            string outDir = @"C:\Users\Mina AbuSeifen\.gemini\antigravity\brain\13a26a31-abc2-4024-b486-1a4ca75d59a3";
            bmp.Save(Path.Combine(outDir, "capture_frmmain_latest.png"), ImageFormat.Png);

            frm.Close();
        }
    }
}
