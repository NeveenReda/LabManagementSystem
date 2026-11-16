using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LabManagementSystem.Common;
using LabManagementSystem.Printing;
using LabManagementSystem.Services;
using QuestPDF.Fluent;

namespace LabManagementSystem.Forms
{
    /// <summary>
    /// Dialog that presents individual tests for a multi-test visit,
    /// allowing the operator to view, print, or export each test separately
    /// with its own specific parameters and clinical layout.
    /// </summary>
    public class FrmSelectVisitTest : Form
    {
        private readonly ConsolidatedVisitReportDto _report;

        public FrmSelectVisitTest(ConsolidatedVisitReportDto report)
        {
            _report = report ?? throw new ArgumentNullException(nameof(report));
            InitializeUi();
        }

        private void InitializeUi()
        {
            FormStyler.ApplyFormTheme(this, $"اختيار التحليل للطباعة - {_report.PatientName}");
            Size = new Size(760, 560);
            MinimumSize = new Size(680, 480);
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;

            // Header Panel
            string subTitle = $"المريض: {_report.PatientName} | الكود: {_report.MedicalCode} | الزيارة #{_report.VisitId} | عدد الفحوصات: ({_report.Tests.Count})";
            var pnlHeader = FormStyler.CreateHeaderPanel("طباعة ومعاينة الفحوصات المخبرية المنفصلة", subTitle, "🔬");
            Controls.Add(pnlHeader);

            // Bottom Action Bar
            var pnlBottom = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(16, 10, 16, 10)
            };

            var btnClose = new Button
            {
                Text = "إغلاق",
                Width = 100,
                Height = 38,
                Dock = DockStyle.Left,
                Cursor = Cursors.Hand
            };
            FormStyler.StyleButton(btnClose, ButtonStyle.Secondary);
            btnClose.Click += (s, e) => Close();

            var btnExportAll = new Button
            {
                Text = "💾 تصدير الكل (ملفات PDF منفصلة)...",
                Width = 230,
                Height = 38,
                Dock = DockStyle.Right,
                Cursor = Cursors.Hand,
                Margin = new Padding(8, 0, 0, 0)
            };
            FormStyler.StyleButton(btnExportAll, ButtonStyle.Outline);
            btnExportAll.Click += BtnExportAll_Click;

            var btnOpenAll = new Button
            {
                Text = "📑 معاينة كل التحاليل منفصلة",
                Width = 200,
                Height = 38,
                Dock = DockStyle.Right,
                Cursor = Cursors.Hand,
                Margin = new Padding(8, 0, 8, 0)
            };
            FormStyler.StyleButton(btnOpenAll, ButtonStyle.Primary);
            btnOpenAll.Click += BtnOpenAll_Click;

            pnlBottom.Controls.Add(btnClose);
            pnlBottom.Controls.Add(btnOpenAll);
            pnlBottom.Controls.Add(btnExportAll);
            Controls.Add(pnlBottom);

            // Center Content: Cards container
            var flpCards = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(20, 16, 20, 16),
                BackColor = ThemeManager.Background
            };

            // Explanatory note
            var lblHint = new Label
            {
                Text = "اختر التحليل المطلوب لفتح تقريره الطبي وطباعته أو حفظه كملف PDF مستقل بباراميتراته الخاصة:",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = ThemeManager.PrimaryNavy,
                AutoSize = true,
                Margin = new Padding(4, 0, 4, 12)
            };
            flpCards.Controls.Add(lblHint);

            int testIndex = 1;
            foreach (var test in _report.Tests)
            {
                var card = CreateTestCard(test, testIndex++);
                flpCards.Controls.Add(card);
            }

            Controls.Add(flpCards);
            pnlHeader.BringToFront();
            pnlBottom.BringToFront();
        }

        private Panel CreateTestCard(ConsolidatedTestDto test, int index)
        {
            string englishName = LabReportEnglishHelper.NormalizeTestName(test.TestName);

            var pnlCard = new Panel
            {
                Width = 690,
                Height = 84,
                BackColor = Color.White,
                Margin = new Padding(4, 4, 4, 10),
                Padding = new Padding(14, 10, 14, 10)
            };

            pnlCard.Paint += (s, e) =>
            {
                using var pen = new Pen(Color.FromArgb(215, 222, 230), 1.5f);
                e.Graphics.DrawRectangle(pen, 0, 0, pnlCard.Width - 1, pnlCard.Height - 1);
            };

            // Left Action Buttons (In RTL, Left is physically on the left side)
            var pnlButtons = new Panel
            {
                Dock = DockStyle.Left,
                Width = 260,
                BackColor = Color.Transparent
            };

            var btnPreview = new Button
            {
                Text = "👁️ معاينة وطباعة",
                Width = 130,
                Height = 36,
                Location = new Point(125, 14),
                Cursor = Cursors.Hand
            };
            FormStyler.StyleButton(btnPreview, ButtonStyle.Primary);
            btnPreview.Click += (s, e) => OpenTestReport(test);

            var btnPdf = new Button
            {
                Text = "💾 حفظ PDF",
                Width = 115,
                Height = 36,
                Location = new Point(4, 14),
                Cursor = Cursors.Hand
            };
            FormStyler.StyleButton(btnPdf, ButtonStyle.Outline);
            btnPdf.Click += (s, e) => SaveTestPdf(test);

            pnlButtons.Controls.Add(btnPreview);
            pnlButtons.Controls.Add(btnPdf);

            // Right Info Block (Test Name & details)
            var pnlInfo = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(8, 4, 8, 4)
            };

            var lblTitle = new Label
            {
                Text = $"{index}. {test.TestName}  —  {englishName}",
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = ThemeManager.PrimaryNavy,
                Dock = DockStyle.Top,
                Height = 26,
                AutoEllipsis = true
            };

            string statusText = !string.IsNullOrWhiteSpace(test.StatusName) ? test.StatusName : "مكتمل";
            var lblDetails = new Label
            {
                Text = $"القسم: {test.CategoryName}   |   عدد النتائج المسجلة: {test.Parameters.Count}   |   الحالة: {statusText}",
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                ForeColor = Color.FromArgb(100, 110, 125),
                Dock = DockStyle.Top,
                Height = 24,
                AutoEllipsis = true
            };

            pnlInfo.Controls.Add(lblDetails);
            pnlInfo.Controls.Add(lblTitle);

            pnlCard.Controls.Add(pnlInfo);
            pnlCard.Controls.Add(pnlButtons);

            return pnlCard;
        }

        private void OpenTestReport(ConsolidatedTestDto test)
        {
            try
            {
                var doc = new ConsolidatedLabReportDocument(_report, test);
                string englishName = LabReportEnglishHelper.NormalizeTestName(test.TestName);
                string title = $"{englishName} - {_report.PatientName}";
                using var viewer = new DocumentViewerForm(doc, title);
                viewer.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء إعداد تقرير التحليل:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveTestPdf(ConsolidatedTestDto test)
        {
            try
            {
                string englishName = LabReportEnglishHelper.NormalizeTestName(test.TestName);
                string safePatient = string.Join("_", _report.PatientName.Split(Path.GetInvalidFileNameChars())).Trim();
                string safeTest = string.Join("_", englishName.Split(Path.GetInvalidFileNameChars())).Trim();
                string defaultName = $"{safePatient}_{safeTest}_Visit{_report.VisitId}.pdf";

                using var sfd = new SaveFileDialog
                {
                    Filter = "ملف PDF (*.pdf)|*.pdf",
                    FileName = defaultName,
                    Title = $"حفظ تقرير {englishName} كملف PDF"
                };

                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
                    var doc = new ConsolidatedLabReportDocument(_report, test);
                    byte[] pdfBytes = doc.GeneratePdf();
                    File.WriteAllBytes(sfd.FileName, pdfBytes);
                    MessageBox.Show("تم حفظ ملف الـ PDF بنجاح.", "تم الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء حفظ ملف الـ PDF:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnOpenAll_Click(object? sender, EventArgs e)
        {
            foreach (var test in _report.Tests)
            {
                OpenTestReport(test);
            }
        }

        private void BtnExportAll_Click(object? sender, EventArgs e)
        {
            try
            {
                using var fbd = new FolderBrowserDialog
                {
                    Description = "اختر المجلد لحفظ تقارير التحاليل كملفات PDF منفصلة"
                };

                if (fbd.ShowDialog(this) == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    int exportedCount = 0;
                    string safePatient = string.Join("_", _report.PatientName.Split(Path.GetInvalidFileNameChars())).Trim();

                    foreach (var test in _report.Tests)
                    {
                        var doc = new ConsolidatedLabReportDocument(_report, test);
                        byte[] pdfBytes = doc.GeneratePdf();
                        string safeTest = string.Join("_", LabReportEnglishHelper.NormalizeTestName(test.TestName).Split(Path.GetInvalidFileNameChars())).Trim();
                        string fileName = $"{safePatient}_{safeTest}_Visit{_report.VisitId}.pdf";
                        string fullPath = Path.Combine(fbd.SelectedPath, fileName);
                        File.WriteAllBytes(fullPath, pdfBytes);
                        exportedCount++;
                    }

                    MessageBox.Show($"تم حفظ ({exportedCount}) ملفات PDF منفصلة بنجاح في المجلد المحدد.", "تم التصدير بنجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء تصدير ملفات الـ PDF:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
