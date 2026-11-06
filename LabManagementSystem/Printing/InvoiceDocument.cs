using LabManagementSystem.Configuration;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LabManagementSystem.Printing
{
    public class InvoiceDocument : IDocument
    {
        private readonly ThermalReceiptModel _model;
        private readonly LabSettings _settings;

        public InvoiceDocument(ThermalReceiptModel model)
        {
            _model = model;
            _settings = AppConfig.GetLabSettings();
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A5.Landscape());
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
                        c.Item().Text(_settings.LabName).Bold().FontSize(14).FontColor(Colors.Blue.Darken3);
                        c.Item().Text(_settings.LabSubtitle).FontSize(8.5f).FontColor(Colors.Grey.Darken2);
                        c.Item().Text($"العنوان: {_settings.Address} | ت: {_settings.Phone}").FontSize(8);
                    });

                    row.RelativeItem(2).AlignRight().Column(c =>
                    {
                        c.Item().Border(1).BorderColor(Colors.Blue.Medium).Background(Colors.Blue.Lighten5).Padding(6).Column(ic =>
                        {
                            ic.Item().AlignCenter().Text("فاتورة خدمات معملية").Bold().FontSize(11).FontColor(Colors.Blue.Darken3);
                            ic.Item().AlignCenter().Text($"رقم الفاتورة: #{_model.VisitId}").Bold().FontSize(9);
                            ic.Item().AlignCenter().Text($"التاريخ: {_model.ReceiptDate:yyyy/MM/dd}").FontSize(8.5f);
                        });
                    });
                });

                col.Item().PaddingVertical(4).LineHorizontal(1).LineColor(Colors.Blue.Darken2);

                // Patient Info Box
                col.Item().Background(Colors.Grey.Lighten4).Padding(6).Row(r =>
                {
                    r.RelativeItem().AlignRight().Text($"اسم المريض: {_model.PatientName}").Bold();
                    r.RelativeItem().AlignCenter().Text($"الكود الطبي: {_model.MedicalCode}").Bold();
                    if (!string.IsNullOrWhiteSpace(_model.DoctorName))
                        r.RelativeItem().AlignLeft().Text($"الطبيب: د. {_model.DoctorName}");
                });

                col.Item().PaddingBottom(4);
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(30);
                        columns.RelativeColumn(4);
                        columns.RelativeColumn(2);
                    });

                    table.Header(header =>
                    {
                        header.Cell().Background(Colors.Blue.Darken2).Padding(4).AlignCenter().Text("#").FontColor(Colors.White).Bold();
                        header.Cell().Background(Colors.Blue.Darken2).Padding(4).AlignRight().Text("التحليل المطلوب").FontColor(Colors.White).Bold();
                        header.Cell().Background(Colors.Blue.Darken2).Padding(4).AlignCenter().Text("السعر (ج.م)").FontColor(Colors.White).Bold();
                    });

                    int idx = 1;
                    foreach (var item in _model.Items)
                    {
                        var bg = idx % 2 == 0 ? Colors.Grey.Lighten4 : Colors.White;
                        table.Cell().Background(bg).Padding(4).AlignCenter().Text($"{idx}");
                        table.Cell().Background(bg).Padding(4).AlignRight().Text(item.Item1);
                        table.Cell().Background(bg).Padding(4).AlignCenter().Text($"{item.Item2:0.00}");
                        idx++;
                    }
                });

                col.Item().PaddingTop(8).AlignLeft().Width(220).Table(t =>
                {
                    t.ColumnsDefinition(c =>
                    {
                        c.RelativeColumn(2);
                        c.RelativeColumn(2);
                    });

                    t.Cell().Padding(2).AlignRight().Text("الإجمالي:");
                    t.Cell().Padding(2).AlignCenter().Text($"{_model.TotalAmount:0.00} ج.م").Bold();

                    if (_model.DiscountAmount > 0)
                    {
                        t.Cell().Padding(2).AlignRight().Text("الخصم:");
                        t.Cell().Padding(2).AlignCenter().Text($"-{_model.DiscountAmount:0.00} ج.م").FontColor(Colors.Red.Darken1);
                    }

                    t.Cell().Background(Colors.Grey.Lighten3).Padding(2).AlignRight().Text("الصافي المطلوب:").Bold();
                    t.Cell().Background(Colors.Grey.Lighten3).Padding(2).AlignCenter().Text($"{_model.NetAmount:0.00} ج.م").Bold();

                    t.Cell().Padding(2).AlignRight().Text($"المدفوع ({_model.PaymentMethodName}):");
                    t.Cell().Padding(2).AlignCenter().Text($"{_model.PaidAmount:0.00} ج.م").FontColor(Colors.Green.Darken2).Bold();

                    t.Cell().Background(Colors.Blue.Lighten5).Padding(2).AlignRight().Text("المتبقي:").Bold();
                    t.Cell().Background(Colors.Blue.Lighten5).Padding(2).AlignCenter().Text($"{_model.RemainingBalance:0.00} ج.م").Bold().FontColor(_model.RemainingBalance > 0 ? Colors.Red.Darken2 : Colors.Black);
                });
            });
        }

        private void ComposeFooter(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().LineHorizontal(0.5f).LineColor(Colors.Grey.Medium);
                col.Item().PaddingTop(3).Row(row =>
                {
                    row.RelativeItem().AlignRight().Text(_settings.ReportFooterNote).FontSize(7.5f).Italic();
                    row.RelativeItem().AlignLeft().Text("توقيع المحاسب / الاستقبال: .........................").FontSize(8);
                });
            });
        }
    }
}

