using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using LabManagementSystem.Common;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Color = System.Drawing.Color;
using Size = System.Drawing.Size;
using Image = System.Drawing.Image;

namespace LabManagementSystem.Printing
{
    public enum ViewerLaunchMode
    {
        Preview,
        Print
    }

    public class DocumentViewerForm : Form
    {
        private readonly IDocument _document;
        private readonly string _documentTitle;
        private readonly ViewerLaunchMode _launchMode;
        private byte[]? _pdfBytes;
        private List<byte[]>? _pageImages;
        private string? _tempPdfPath;

        private Button btnPrint = null!;
        private Button btnSavePdf = null!;
        private Button btnOpenPdf = null!;
        private Button btnClose = null!;
        private Label lblInfo = null!;
        private Panel pnlHeader = null!;
        private Panel pnlContent = null!;

        public DocumentViewerForm(IDocument document, string documentTitle, ViewerLaunchMode launchMode = ViewerLaunchMode.Preview)
        {
            _document = document;
            _documentTitle = documentTitle;
            _launchMode = launchMode;
            InitializeUi();
            Shown += (s, e) =>
            {
                GenerateDocument();
                if (_launchMode == ViewerLaunchMode.Print)
                {
                    BeginInvoke(new Action(() => BtnPrint_Click(this, EventArgs.Empty)));
                }
            };
        }

        private void InitializeUi()
        {
            FormStyler.ApplyFormTheme(this, $"معاينة وطباعة المستند - {_documentTitle}");
            Size = new Size(1000, 800);
            MinimumSize = new Size(820, 620);
            StartPosition = FormStartPosition.CenterScreen;

            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 68,
                BackColor = ThemeManager.PrimaryNavy,
                Padding = new Padding(16, 12, 16, 12),
                RightToLeft = RightToLeft.Yes
            };

            btnPrint = new Button
            {
                Text = "🖨️ طباعة فورية",
                Width = 135,
                Height = 40,
                Cursor = Cursors.Hand,
                Margin = new Padding(4, 0, 4, 0)
            };
            FormStyler.StyleButton(btnPrint, ButtonStyle.Success);
            btnPrint.Click += BtnPrint_Click;

            btnSavePdf = new Button
            {
                Text = "💾 حفظ PDF",
                Width = 125,
                Height = 40,
                Cursor = Cursors.Hand,
                Margin = new Padding(4, 0, 4, 0)
            };
            FormStyler.StyleButton(btnSavePdf, ButtonStyle.Primary);
            btnSavePdf.Click += BtnSavePdf_Click;

            btnOpenPdf = new Button
            {
                Text = "👁️ فتح بالقارئ الخارجي",
                Width = 160,
                Height = 40,
                Cursor = Cursors.Hand,
                Margin = new Padding(4, 0, 4, 0)
            };
            FormStyler.StyleButton(btnOpenPdf, ButtonStyle.Outline);
            btnOpenPdf.ForeColor = Color.White;
            btnOpenPdf.Click += BtnOpenPdf_Click;

            btnClose = new Button
            {
                Text = "إغلاق",
                Width = 85,
                Height = 40,
                Cursor = Cursors.Hand,
                Margin = new Padding(4, 0, 4, 0)
            };
            FormStyler.StyleButton(btnClose, ButtonStyle.Secondary);
            btnClose.Click += (s, e) => Close();

            var flpActions = new FlowLayoutPanel
            {
                Dock = DockStyle.Left,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.Transparent,
                Margin = new Padding(0)
            };
            flpActions.Controls.AddRange(new Control[] { btnClose, btnOpenPdf, btnSavePdf, btnPrint });

            lblInfo = new Label
            {
                Text = $"📄 {_documentTitle}",
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Font = ThemeManager.SubtitleFont,
                TextAlign = ContentAlignment.MiddleRight,
                RightToLeft = RightToLeft.Yes
            };

            pnlHeader.Controls.Add(lblInfo);
            pnlHeader.Controls.Add(flpActions);

            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ColorTranslator.FromHtml("#334155"),
                AutoScroll = true,
                Padding = new Padding(24)
            };

            Controls.Add(pnlContent);
            Controls.Add(pnlHeader);
        }

        private void GenerateDocument()
        {
            try
            {
                QuestPDF.Settings.License = LicenseType.Community;
                _pdfBytes = _document.GeneratePdf();

                // Save temp PDF for external viewer
                try
                {
                    string tempDir = Path.Combine(Path.GetTempPath(), "LabManagementSystemReports");
                    Directory.CreateDirectory(tempDir);
                    _tempPdfPath = Path.Combine(tempDir, $"{Guid.NewGuid()}.pdf");
                    File.WriteAllBytes(_tempPdfPath, _pdfBytes);
                }
                catch { }

                // Generate page images for native WinForms rendering
                _pageImages = _document.GenerateImages().ToList();

                RenderNativePages();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء إنشاء المستند:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowFallbackCard();
            }
        }

        private void RenderNativePages()
        {
            if (_pageImages == null || _pageImages.Count == 0)
            {
                ShowFallbackCard();
                return;
            }

            pnlContent.SuspendLayout();
            pnlContent.Controls.Clear();

            var flowPages = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            foreach (var imgBytes in _pageImages)
            {
                try
                {
                    var ms = new MemoryStream(imgBytes);
                    var img = Image.FromStream(ms);

                    // Compute display width centered in container
                    int displayWidth = Math.Min(img.Width, Math.Max(500, pnlContent.Width - 80));
                    int displayHeight = (int)((double)img.Height / img.Width * displayWidth);

                    var pbox = new PictureBox
                    {
                        Image = img,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Dock = DockStyle.Fill,
                        BackColor = Color.White
                    };

                    // Add subtle border / shadow card
                    var pnlPageWrap = new Panel
                    {
                        Size = new Size(displayWidth + 4, displayHeight + 4),
                        BackColor = Color.White,
                        Padding = new Padding(2),
                        Margin = new Padding(Math.Max(10, (pnlContent.ClientSize.Width - displayWidth) / 2), 12, 10, 12)
                    };
                    pnlPageWrap.Controls.Add(pbox);
                    flowPages.Controls.Add(pnlPageWrap);
                }
                catch
                {
                    // If image parsing fails for a page, skip
                }
            }

            pnlContent.Controls.Add(flowPages);
            pnlContent.ResumeLayout();
        }

        private void ShowFallbackCard()
        {
            pnlContent.Controls.Clear();
            var pnlCard = FormStyler.CreateCardPanel();
            pnlCard.Size = new Size(620, 240);
            pnlCard.Location = new Point(Math.Max(20, (pnlContent.Width - 620) / 2), Math.Max(20, (pnlContent.Height - 240) / 2));
            pnlCard.Anchor = AnchorStyles.None;

            var lbl = new Label
            {
                Text = "تم إنشاء التقرير بنجاح بصيغة PDF.\n\nيمكنك النقر على زر 'طباعة فورية' لإرساله إلى الطابعة، أو 'حفظ ملف PDF' لحفظه في جهازك، أو 'فتح بالقارئ الخارجي' لعرضه ببرنامج PDF المفضل لديك.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = ThemeManager.TitleFont,
                ForeColor = ThemeManager.MainText,
                Padding = new Padding(20)
            };
            pnlCard.Controls.Add(lbl);
            pnlContent.Controls.Add(pnlCard);
        }

        private void BtnPrint_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_pageImages == null || _pageImages.Count == 0)
                {
                    // Fallback to Shell print if images not present
                    if (!string.IsNullOrEmpty(_tempPdfPath) && File.Exists(_tempPdfPath))
                    {
                        Process.Start(new ProcessStartInfo { FileName = _tempPdfPath, UseShellExecute = true });
                    }
                    else
                    {
                        MessageBox.Show("المستند غير متوفر للطباعة.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return;
                }

                using var pd = new PrintDocument { DocumentName = _documentTitle };
                using var printDlg = new PrintDialog
                {
                    Document = pd,
                    UseEXDialog = true,
                    AllowSomePages = true,
                    AllowSelection = false
                };

                if (printDlg.ShowDialog() == DialogResult.OK)
                {
                    int currentPageIndex = 0;
                    var bitmaps = _pageImages.Select(bytes => new Bitmap(new MemoryStream(bytes))).ToList();

                    pd.PrintPage += (s, ev) =>
                    {
                        if (currentPageIndex < bitmaps.Count && ev.Graphics != null)
                        {
                            var bmp = bitmaps[currentPageIndex];
                            var bounds = ev.MarginBounds;

                            // Scale proportionally into printable page bounds
                            double scale = Math.Min((double)bounds.Width / bmp.Width, (double)bounds.Height / bmp.Height);
                            int destW = (int)(bmp.Width * scale);
                            int destH = (int)(bmp.Height * scale);
                            int destX = bounds.Left + (bounds.Width - destW) / 2;
                            int destY = bounds.Top + (bounds.Height - destH) / 2;

                            ev.Graphics.DrawImage(bmp, new Rectangle(destX, destY, destW, destH));

                            currentPageIndex++;
                            ev.HasMorePages = currentPageIndex < bitmaps.Count;
                        }
                        else
                        {
                            ev.HasMorePages = false;
                        }
                    };

                    pd.Print();
                    MessageBox.Show("تم إرسال أمر الطباعة بنجاح إلى الطابعة المحددة.", "تمت الطباعة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"تعذر استكمال الطباعة عبر الطابعة المحددة:\n{ex.Message}\n\nيمكنك حفظ المستند بصيغة PDF أو فتحه بالقارئ الخارجي لطباعته.",
                    "تنبيه الطباعة",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void BtnSavePdf_Click(object? sender, EventArgs e)
        {
            if (_pdfBytes == null)
            {
                MessageBox.Show("المستند غير متوفر للحفظ.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "ملف PDF (*.pdf)|*.pdf",
                FileName = $"{_documentTitle.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.WriteAllBytes(sfd.FileName, _pdfBytes);
                    MessageBox.Show("تم حفظ ملف PDF بنجاح.", "نجاح الحفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"حدث خطأ أثناء حفظ الملف:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnOpenPdf_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_tempPdfPath) || !File.Exists(_tempPdfPath))
            {
                if (_pdfBytes != null)
                {
                    try
                    {
                        string tempDir = Path.Combine(Path.GetTempPath(), "LabManagementSystemReports");
                        Directory.CreateDirectory(tempDir);
                        _tempPdfPath = Path.Combine(tempDir, $"{Guid.NewGuid()}.pdf");
                        File.WriteAllBytes(_tempPdfPath, _pdfBytes);
                    }
                    catch { }
                }
            }

            if (!string.IsNullOrEmpty(_tempPdfPath) && File.Exists(_tempPdfPath))
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = _tempPdfPath,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"تعذر فتح الملف: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
