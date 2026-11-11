using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using LabManagementSystem.Forms;
using LabManagementSystem.Models;
using LabManagementSystem.Printing;
using LabManagementSystem.Services;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Xunit;
using Size = System.Drawing.Size;
using ImageFormat = System.Drawing.Imaging.ImageFormat;

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

        [Fact]
        public void Capture_Home_And_PurchaseInvoice_Visuals() => StaTestRunner.Run(() =>
        {
            QuestPDF.Settings.License = LicenseType.Community;
            string outDir = @"C:\Users\Mina AbuSeifen\.gemini\antigravity\brain\13a26a31-abc2-4024-b486-1a4ca75d59a3";
            if (!Directory.Exists(outDir)) return;

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
            ServiceLocator.Initialize(services.BuildServiceProvider());

            // 1. Capture Startup FrmMain showing FrmHome (1366x840)
            using (var frm = new FrmMain())
            {
                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = new Point(20, 20);
                frm.Size = new Size(1366, 840);
                frm.Show();
                Application.DoEvents();
                Thread.Sleep(250);
                Application.DoEvents();

                using var bmp = new Bitmap(frm.Width, frm.Height);
                frm.DrawToBitmap(bmp, new Rectangle(0, 0, frm.Width, frm.Height));
                bmp.Save(Path.Combine(outDir, "home_startup_verify.png"), ImageFormat.Png);
                frm.Close();
            }

            // 2. Capture FrmInventory Orders tab showing View / Print / Export PDF buttons
            using (var frmInv = new FrmInventory(1))
            {
                frmInv.StartPosition = FormStartPosition.Manual;
                frmInv.Location = new Point(20, 20);
                frmInv.Size = new Size(1200, 760);
                frmInv.Show();
                Application.DoEvents();
                Thread.Sleep(250);
                Application.DoEvents();

                using var bmp = new Bitmap(frmInv.Width, frmInv.Height);
                frmInv.DrawToBitmap(bmp, new Rectangle(0, 0, frmInv.Width, frmInv.Height));
                bmp.Save(Path.Combine(outDir, "inventory_orders_verify.png"), ImageFormat.Png);
                frmInv.Close();
            }

            // 3. Render multi-page PurchaseInvoiceDocument PNGs & test DocumentViewerForm
            var unitBox = new LabManagementSystem.Models.Unit { Id = 1, Name = "علبة" };
            var catChem = new ProductCategory { Id = 1, Name = "مستلزمات كيمياء" };
            var sampleOrder = new Order
            {
                Id = 1042,
                OrderDate = new DateTime(2026, 11, 11, 11, 30, 0),
                CreateDate = new DateTime(2026, 11, 11, 11, 32, 0),
                ExpectedDeliveryDate = new DateTime(2026, 11, 14),
                Notes = "يرجى فحص تاريخ الصلاحية وسلامة التبريد عند الاستلام."
            };
            decimal grand = 0m;
            for (int i = 1; i <= 34; i++)
            {
                decimal price = 120m + i * 5m;
                int qty = (i % 4) + 1;
                grand += price * qty;
                sampleOrder.OrderDetails.Add(new OrderDetail
                {
                    Id = i,
                    OrderId = 1042,
                    ProductId = 200 + i,
                    UnitId = 1,
                    Quantity = qty,
                    Price = price,
                    Product = new Product { Id = 200 + i, Name = $"كاشف معملي تخصصي رقم {i} - عبوة قياسية", ProductCategory = catChem, BaseUnit = unitBox },
                    Unit = unitBox
                });
            }
            sampleOrder.TotalPrice = grand;
            sampleOrder.NetPrice = grand;

            var doc = new PurchaseInvoiceDocument(sampleOrder);
            var pageImages = doc.GenerateImages().ToList();
            Assert.True(pageImages.Count >= 2, $"Expected >= 2 rendered invoice pages, got {pageImages.Count}");

            File.WriteAllBytes(Path.Combine(outDir, "purchase_invoice_page1.png"), pageImages[0]);
            File.WriteAllBytes(Path.Combine(outDir, "purchase_invoice_page2.png"), pageImages[1]);

            using (var viewer = new DocumentViewerForm(doc, "معاينة فاتورة المشتريات #1042", ViewerLaunchMode.Preview))
            {
                viewer.Size = new Size(1050, 760);
                viewer.Show();
                Application.DoEvents();
                Thread.Sleep(200);
                Application.DoEvents();

                using var bmp = new Bitmap(viewer.Width, viewer.Height);
                viewer.DrawToBitmap(bmp, new Rectangle(0, 0, viewer.Width, viewer.Height));
                bmp.Save(Path.Combine(outDir, "purchase_invoice_viewer_verify.png"), ImageFormat.Png);
                viewer.Close();
            }
        });
    }
}
