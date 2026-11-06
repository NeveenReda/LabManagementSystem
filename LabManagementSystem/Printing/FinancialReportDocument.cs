using LabManagementSystem.Configuration;
using LabManagementSystem.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LabManagementSystem.Printing
{
    public class FinancialReportDocument : IDocument
    {
        private readonly FinancialSummaryDto _model;
        private readonly LabSettings _settings;

        public FinancialReportDocument(FinancialSummaryDto model)
        {
            _model = model;
            _settings = AppConfig.GetLabSettings();
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1.5f, Unit.Centimetre);
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
                col.Item().Row(row =>
                {
                    row.RelativeItem(3).Column(c =>
                    {
                        c.Item().Text(_settings.LabName).Bold().FontSize(15).FontColor(Colors.Blue.Darken4);
                        c.Item().Text("التقرير المالي والإحصائي للنشاط المعملي").Bold().FontSize(12).FontColor(Colors.Blue.Darken2);
                        c.Item().Text($"الفترة من: {_model.FromDate:yyyy/MM/dd}  إلى: {_model.ToDate:yyyy/MM/dd}").FontSize(9);
                    });

                    row.RelativeItem(2).AlignLeft().Column(c =>
                    {
                        c.Item().Text($"تاريخ استخراج التقرير: {DateTime.Now:yyyy/MM/dd HH:mm}").FontSize(8).FontColor(Colors.Grey.Darken2);
                    });
                });

                col.Item().PaddingVertical(4).LineHorizontal(1.5f).LineColor(Colors.Blue.Darken3);
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.Column(col =>
            {
                // KPI Cards
                col.Item().PaddingBottom(8).Table(t =>
                {
                    t.ColumnsDefinition(c =>
                    {
                        c.RelativeColumn();
                        c.RelativeColumn();
                        c.RelativeColumn();
                    });

                    t.Cell().Padding(3).Border(1).BorderColor(Colors.Blue.Medium).Background(Colors.Blue.Lighten5).Padding(6).Column(c =>
                    {
                        c.Item().AlignCenter().Text("إجمالي عدد الزيارات").FontSize(8.5f);
                        c.Item().AlignCenter().Text($"{_model.TotalVisits}").Bold().FontSize(14).FontColor(Colors.Blue.Darken3);
                    });

                    t.Cell().Padding(3).Border(1).BorderColor(Colors.Blue.Medium).Background(Colors.Blue.Lighten5).Padding(6).Column(c =>
                    {
                        c.Item().AlignCenter().Text("إجمالي التحاليل المنجزة").FontSize(8.5f);
                        c.Item().AlignCenter().Text($"{_model.TotalTests}").Bold().FontSize(14).FontColor(Colors.Blue.Darken3);
                    });

                    t.Cell().Padding(3).Border(1).BorderColor(Colors.Green.Medium).Background(Colors.Green.Lighten5).Padding(6).Column(c =>
                    {
                        c.Item().AlignCenter().Text("المحصل نقداً وإلكترونياً").FontSize(8.5f);
                        c.Item().AlignCenter().Text($"{_model.TotalCollectedPayments:0.00} ج.م").Bold().FontSize(14).FontColor(Colors.Green.Darken3);
                    });

                    t.Cell().Padding(3).Border(1).BorderColor(Colors.Grey.Medium).Background(Colors.Grey.Lighten4).Padding(6).Column(c =>
                    {
                        c.Item().AlignCenter().Text("إجمالي المبيعات (Gross)").FontSize(8.5f);
                        c.Item().AlignCenter().Text($"{_model.TotalGrossRevenue:0.00} ج.م").Bold().FontSize(12);
                    });

                    t.Cell().Padding(3).Border(1).BorderColor(Colors.Grey.Medium).Background(Colors.Grey.Lighten4).Padding(6).Column(c =>
                    {
                        c.Item().AlignCenter().Text("إجمالي الخصومات").FontSize(8.5f);
                        c.Item().AlignCenter().Text($"{_model.TotalDiscounts:0.00} ج.م").Bold().FontSize(12).FontColor(Colors.Red.Darken1);
                    });

                    t.Cell().Padding(3).Border(1).BorderColor(Colors.Red.Medium).Background(Colors.Red.Lighten5).Padding(6).Column(c =>
                    {
                        c.Item().AlignCenter().Text("الديون المتبقية (Outstanding)").FontSize(8.5f);
                        c.Item().AlignCenter().Text($"{_model.TotalOutstandingBalances:0.00} ج.م").Bold().FontSize(14).FontColor(Colors.Red.Darken3);
                    });
                });

                // Tests Breakdown Table
                col.Item().PaddingTop(6).Text("أولاً: إحصائية التحاليل الأكثر طلباً والإيراد الخاص بها").Bold().FontSize(10.5f).FontColor(Colors.Blue.Darken3);
                col.Item().Table(t =>
                {
                    t.ColumnsDefinition(c =>
                    {
                        c.ConstantColumn(30);
                        c.RelativeColumn(3);
                        c.RelativeColumn(2);
                        c.RelativeColumn(1.5f);
                        c.RelativeColumn(2);
                    });

                    t.Header(h =>
                    {
                        h.Cell().Background(Colors.Blue.Darken2).Padding(3).AlignCenter().Text("#").FontColor(Colors.White).Bold();
                        h.Cell().Background(Colors.Blue.Darken2).Padding(3).AlignRight().Text("اسم التحليل").FontColor(Colors.White).Bold();
                        h.Cell().Background(Colors.Blue.Darken2).Padding(3).AlignCenter().Text("القسم / التصنيف").FontColor(Colors.White).Bold();
                        h.Cell().Background(Colors.Blue.Darken2).Padding(3).AlignCenter().Text("العدد").FontColor(Colors.White).Bold();
                        h.Cell().Background(Colors.Blue.Darken2).Padding(3).AlignCenter().Text("الإيراد (ج.م)").FontColor(Colors.White).Bold();
                    });

                    int idx = 1;
                    foreach (var item in _model.TestsBreakdown)
                    {
                        var bg = idx % 2 == 0 ? Colors.Grey.Lighten5 : Colors.White;
                        t.Cell().Background(bg).Padding(3).AlignCenter().Text($"{idx}");
                        t.Cell().Background(bg).Padding(3).AlignRight().Text(item.TestName);
                        t.Cell().Background(bg).Padding(3).AlignCenter().Text(item.CategoryName);
                        t.Cell().Background(bg).Padding(3).AlignCenter().Text($"{item.Count}");
                        t.Cell().Background(bg).Padding(3).AlignCenter().Text($"{item.TotalRevenue:0.00}");
                        idx++;
                    }
                });

                // Payment Methods Breakdown
                if (_model.PaymentMethodsBreakdown != null && _model.PaymentMethodsBreakdown.Count > 0)
                {
                    col.Item().PaddingTop(10).Text("ثانياً: التوزيع حسب طريقة الدفع").Bold().FontSize(10.5f).FontColor(Colors.Blue.Darken3);
                    col.Item().Table(t =>
                    {
                        t.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn(3);
                            c.RelativeColumn(2);
                            c.RelativeColumn(2);
                        });

                        t.Header(h =>
                        {
                            h.Cell().Background(Colors.Blue.Darken2).Padding(3).AlignRight().Text("طريقة الدفع").FontColor(Colors.White).Bold();
                            h.Cell().Background(Colors.Blue.Darken2).Padding(3).AlignCenter().Text("عدد المعاملات").FontColor(Colors.White).Bold();
                            h.Cell().Background(Colors.Blue.Darken2).Padding(3).AlignCenter().Text("المبلغ المحصل").FontColor(Colors.White).Bold();
                        });

                        foreach (var m in _model.PaymentMethodsBreakdown)
                        {
                            t.Cell().Padding(3).AlignRight().Text(m.MethodName);
                            t.Cell().Padding(3).AlignCenter().Text($"{m.TransactionsCount}");
                            t.Cell().Padding(3).AlignCenter().Text($"{m.TotalAmount:0.00} ج.م").Bold();
                        }
                    });
                }
            });
        }

        private void ComposeFooter(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().LineHorizontal(0.5f).LineColor(Colors.Grey.Medium);
                col.Item().PaddingTop(3).Row(row =>
                {
                    row.RelativeItem().AlignRight().Text("نظام إدارة المعامل الطبية - تقرير مالي رسمي").FontSize(7.5f).Italic();
                    row.RelativeItem().AlignLeft().Text("توقيع المدير المالي: .........................").FontSize(8);
                });
            });
        }
    }
}

