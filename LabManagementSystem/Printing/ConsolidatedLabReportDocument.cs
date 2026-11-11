using LabManagementSystem.Configuration;
using LabManagementSystem.Enums;
using LabManagementSystem.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LabManagementSystem.Printing
{
    public class ConsolidatedLabReportDocument : IDocument
    {
        private readonly ConsolidatedVisitReportDto _report;
        private readonly LabSettings _settings;

        public ConsolidatedLabReportDocument(ConsolidatedVisitReportDto report)
        {
            _report = report;
            _settings = AppConfig.GetLabSettings();
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1.2f, Unit.Centimetre);
                page.ContentFromRightToLeft();
                page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(9.5f).FontColor(Colors.Grey.Darken4));

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });
        }

        private void ComposeHeader(IContainer container)
        {
            container.Column(col =>
            {
                // Top Lab Branding Banner (RTL: Right = Lab Info, Left = Report Badge)
                col.Item().Row(row =>
                {
                    row.RelativeItem(3).AlignRight().Column(c =>
                    {
                        c.Item().AlignRight().Text(_settings.LabName).Bold().FontSize(16).FontColor(Colors.Blue.Darken4);
                        c.Item().AlignRight().Text(_settings.LabSubtitle).FontSize(9.5f).FontColor(Colors.Grey.Darken2);
                        c.Item().AlignRight().Text($"العنوان: {_settings.Address} | ت: {_settings.Phone}").FontSize(8.5f);
                    });

                    row.RelativeItem(2).AlignLeft().Column(c =>
                    {
                        c.Item().Border(1.5f).BorderColor(Colors.Blue.Darken3).Background(Colors.Blue.Lighten5).Padding(8).Column(ic =>
                        {
                            ic.Item().AlignCenter().Text("تقرير نتائج التحاليل الطبية").Bold().FontSize(12).FontColor(Colors.Blue.Darken3);
                            ic.Item().AlignCenter().Text($"رقم الزيارة: #{_report.VisitId}").Bold().FontSize(9.5f);
                        });
                    });
                });

                col.Item().PaddingVertical(6).LineHorizontal(1.5f).LineColor(Colors.Blue.Darken3);

                // Patient Information Grid Box (Arabic labels only, RTL aligned)
                col.Item().Border(1).BorderColor(Colors.Grey.Medium).Background(Colors.Grey.Lighten4).Padding(8).Column(pc =>
                {
                    pc.Item().Row(r =>
                    {
                        r.RelativeItem(3).AlignRight().Text(t =>
                        {
                            t.Span("اسم المريض: ").FontSize(9f).FontColor(Colors.Grey.Darken2);
                            t.Span(_report.PatientName).Bold().FontSize(10.5f);
                        });

                        r.RelativeItem(2).AlignRight().Text(t =>
                        {
                            t.Span("الكود الطبي: ").FontSize(9f).FontColor(Colors.Grey.Darken2);
                            t.Span(_report.MedicalCode).Bold().FontSize(10f);
                        });

                        r.RelativeItem(2).AlignRight().Text(t =>
                        {
                            t.Span("التاريخ: ").FontSize(9f).FontColor(Colors.Grey.Darken2);
                            t.Span($"{_report.VisitDate:yyyy/MM/dd}").Bold().FontSize(9.5f);
                        });
                    });

                    pc.Item().PaddingTop(5).Row(r =>
                    {
                        r.RelativeItem(2).AlignRight().Text(t =>
                        {
                            t.Span("السن: ").FontSize(9f).FontColor(Colors.Grey.Darken2);
                            t.Span($"{_report.Age} سنة").Bold().FontSize(9.5f);
                        });

                        r.RelativeItem(2).AlignRight().Text(t =>
                        {
                            t.Span("النوع: ").FontSize(9f).FontColor(Colors.Grey.Darken2);
                            t.Span(string.IsNullOrWhiteSpace(_report.Gender) ? "غير محدد" : _report.Gender).Bold().FontSize(9.5f);
                        });

                        r.RelativeItem(3).AlignRight().Text(t =>
                        {
                            t.Span("الطبيب المعالج: ").FontSize(9f).FontColor(Colors.Grey.Darken2);
                            t.Span(string.IsNullOrWhiteSpace(_report.DoctorName) ? "غير محدد" : $"د. {_report.DoctorName}").Bold().FontSize(9.5f);
                        });
                    });
                });

                col.Item().PaddingBottom(8);
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.Column(col =>
            {
                if (_report.Tests == null || _report.Tests.Count == 0)
                {
                    col.Item().Padding(20).AlignCenter().Text("لا توجد تحاليل مسجلة لهذه الزيارة.").Italic();
                    return;
                }

                foreach (var test in _report.Tests)
                {
                    col.Item().PaddingTop(6).Column(tc =>
                    {
                        // Test Header Title Banner
                        tc.Item().Background(Colors.Blue.Darken2).Padding(4).Row(hr =>
                        {
                            hr.RelativeItem().AlignRight().Text(test.TestName).Bold().FontSize(11).FontColor(Colors.White);
                            hr.RelativeItem().AlignLeft().Text(test.CategoryName).FontSize(8.5f).FontColor(Colors.Grey.Lighten3);
                        });

                        // Parameters Table
                        tc.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3.5f); // Parameter
                                columns.RelativeColumn(2.0f); // Result
                                columns.RelativeColumn(1.5f); // Flag
                                columns.RelativeColumn(1.5f); // Unit
                                columns.RelativeColumn(3.0f); // Normal Range
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background(Colors.Grey.Lighten3).Padding(4).AlignRight().Text("اسم الباراميتر").Bold().FontSize(8.5f);
                                header.Cell().Background(Colors.Grey.Lighten3).Padding(4).AlignCenter().Text("النتيجة").Bold().FontSize(8.5f);
                                header.Cell().Background(Colors.Grey.Lighten3).Padding(4).AlignCenter().Text("الحالة").Bold().FontSize(8.5f);
                                header.Cell().Background(Colors.Grey.Lighten3).Padding(4).AlignCenter().Text("الوحدة").Bold().FontSize(8.5f);
                                header.Cell().Background(Colors.Grey.Lighten3).Padding(4).AlignCenter().Text("المعدل الطبيعي").Bold().FontSize(8.5f);
                            });

                            int rowIdx = 0;
                            foreach (var param in test.Parameters)
                            {
                                var bg = rowIdx % 2 == 0 ? Colors.White : Colors.Grey.Lighten5;

                                table.Cell().Background(bg).Padding(4).AlignRight().Text(param.ParameterName).Bold();

                                // Result Value
                                var resCell = table.Cell().Background(bg).Padding(4).AlignCenter();
                                bool isAbnormal = param.Flag == AbnormalFlagEnum.High || param.Flag == AbnormalFlagEnum.Low || param.Flag == AbnormalFlagEnum.Abnormal;
                                if (isAbnormal)
                                {
                                    resCell.Text(param.ResultValue).Bold().FontSize(10.5f).FontColor(Colors.Red.Darken2);
                                }
                                else
                                {
                                    resCell.Text(param.ResultValue).Bold().FontSize(10f).FontColor(Colors.Black);
                                }

                                // Flag Indicator
                                var flagCell = table.Cell().Background(bg).Padding(4).AlignCenter();
                                if (param.Flag == AbnormalFlagEnum.High)
                                {
                                    flagCell.Text("مرتفع").Bold().FontSize(8.5f).FontColor(Colors.Red.Darken2);
                                }
                                else if (param.Flag == AbnormalFlagEnum.Low)
                                {
                                    flagCell.Text("منخفض").Bold().FontSize(8.5f).FontColor(Colors.Red.Darken2);
                                }
                                else if (param.Flag == AbnormalFlagEnum.Abnormal)
                                {
                                    flagCell.Text("غير طبيعي").Bold().FontSize(8.5f).FontColor(Colors.Red.Darken2);
                                }
                                else
                                {
                                    flagCell.Text("طبيعي").FontSize(8.5f).FontColor(Colors.Green.Darken2);
                                }

                                table.Cell().Background(bg).Padding(4).AlignCenter().Text(string.IsNullOrWhiteSpace(param.Unit) ? "-" : param.Unit).FontSize(8.5f);
                                table.Cell().Background(bg).Padding(4).AlignCenter().Text(param.FormattedNormalRange).FontSize(8.5f);

                                rowIdx++;
                            }
                        });
                    });
                }
            });
        }

        private void ComposeFooter(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().LineHorizontal(1).LineColor(Colors.Blue.Darken3);

                col.Item().PaddingTop(4).Row(row =>
                {
                    row.RelativeItem(3).Column(c =>
                    {
                        c.Item().Text(_settings.ReportFooterNote).FontSize(7.5f).Italic();
                        c.Item().Text($"تاريخ الطباعة: {DateTime.Now:yyyy/MM/dd HH:mm}").FontSize(7.5f).FontColor(Colors.Grey.Medium);
                    });

                    row.RelativeItem(2).AlignLeft().Column(c =>
                    {
                        c.Item().AlignCenter().Text("مدير المعمل / التحاليل الطبية").Bold().FontSize(8.5f);
                        c.Item().PaddingTop(12).AlignCenter().Text("......................................").FontSize(8);
                        c.Item().AlignCenter().Text(_settings.DirectorName).FontSize(8).Bold();
                    });
                });
            });
        }
    }
}

