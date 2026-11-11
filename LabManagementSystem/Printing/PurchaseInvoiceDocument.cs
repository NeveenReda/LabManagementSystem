using System;
using System.Linq;
using LabManagementSystem.Configuration;
using LabManagementSystem.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LabManagementSystem.Printing
{
    /// <summary>
    /// QuestPDF A4 Arabic RTL purchase invoice document built strictly from stored Order and OrderDetail records.
    /// </summary>
    public class PurchaseInvoiceDocument : IDocument
    {
        private readonly Order _order;
        private readonly LabSettings _settings;

        public PurchaseInvoiceDocument(Order order)
        {
            _order = order ?? throw new ArgumentNullException(nameof(order));
            _settings = AppConfig.GetLabSettings();
        }

        public DocumentMetadata GetMetadata() => new DocumentMetadata
        {
            Title = $"فاتورة مشتريات #{_order.Id}",
            Author = _settings.LabName,
            Creator = "Olivia Lab Management System"
        };

        public byte[] GeneratePdfBytes()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            return this.GeneratePdf();
        }

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(1.4f, QuestPDF.Infrastructure.Unit.Centimetre);
                page.ContentFromRightToLeft();
                page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(9.5f).FontColor(Colors.Grey.Darken4));

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });
        }

        private void ComposeHeader(IContainer container)
        {
            var details = _order.OrderDetails?.OrderBy(d => d.Id).ToList() ?? new();
            int itemsCount = details.Count;
            int totalUnits = details.Sum(d => d.Quantity);

            container.Column(col =>
            {
                // 1. Organization Banner & Document Title Badge
                col.Item().Row(row =>
                {
                    row.RelativeItem(3).AlignRight().Column(c =>
                    {
                        c.Item().AlignRight().Text(_settings.LabName).Bold().FontSize(16).FontColor(Colors.Blue.Darken4);
                        if (!string.IsNullOrWhiteSpace(_settings.LabSubtitle))
                            c.Item().AlignRight().Text(_settings.LabSubtitle).FontSize(9).FontColor(Colors.Grey.Darken2);

                        string contactLine = string.Join("  |  ", new[]
                        {
                            string.IsNullOrWhiteSpace(_settings.Address) ? null : $"العنوان: {_settings.Address}",
                            string.IsNullOrWhiteSpace(_settings.Phone) ? null : $"هاتف: {_settings.Phone}"
                        }.Where(s => s != null));

                        if (!string.IsNullOrWhiteSpace(contactLine))
                            c.Item().AlignRight().Text(contactLine).FontSize(8.5f).FontColor(Colors.Grey.Darken3);
                    });

                    row.RelativeItem(2).AlignLeft().Column(c =>
                    {
                        c.Item().Border(1.5f).BorderColor(Colors.Blue.Darken3).Background(Colors.Blue.Lighten5).Padding(8).Column(badge =>
                        {
                            badge.Item().AlignCenter().Text("فاتورة مشتريات").Bold().FontSize(14).FontColor(Colors.Blue.Darken4);
                            badge.Item().PaddingTop(2).AlignCenter().Text($"رقم الفاتورة: #{_order.Id}").Bold().FontSize(10).FontColor(Colors.Blue.Darken3);
                            badge.Item().AlignCenter().Text($"تاريخ الفاتورة: {_order.OrderDate:yyyy/MM/dd}").FontSize(9).FontColor(Colors.Grey.Darken4);
                        });
                    });
                });

                col.Item().PaddingVertical(6).LineHorizontal(1.5f).LineColor(Colors.Blue.Darken3);

                // 2. Stored Metadata Grid Box
                col.Item().Border(1).BorderColor(Colors.Grey.Lighten1).Background(Colors.Grey.Lighten4).Padding(8).Column(meta =>
                {
                    meta.Item().Row(r =>
                    {
                        r.RelativeItem().AlignRight().Text(t =>
                        {
                            t.Span("رقم الفاتورة: ").FontSize(9).FontColor(Colors.Grey.Darken2);
                            t.Span($"#{_order.Id}").Bold().FontSize(9.5f);
                        });

                        r.RelativeItem().AlignRight().Text(t =>
                        {
                            t.Span("تاريخ الفاتورة: ").FontSize(9).FontColor(Colors.Grey.Darken2);
                            t.Span($"{_order.OrderDate:yyyy/MM/dd}").Bold().FontSize(9.5f);
                        });

                        r.RelativeItem().AlignRight().Text(t =>
                        {
                            t.Span("تاريخ التسجيل: ").FontSize(9).FontColor(Colors.Grey.Darken2);
                            t.Span($"{_order.CreateDate:yyyy/MM/dd HH:mm}").Bold().FontSize(9.5f);
                        });
                    });

                    meta.Item().PaddingTop(4).Row(r =>
                    {
                        r.RelativeItem().AlignRight().Text(t =>
                        {
                            t.Span("عدد الأصناف: ").FontSize(9).FontColor(Colors.Grey.Darken2);
                            t.Span($"{itemsCount}").Bold().FontSize(9.5f);
                        });

                        r.RelativeItem().AlignRight().Text(t =>
                        {
                            t.Span("إجمالي الكميات: ").FontSize(9).FontColor(Colors.Grey.Darken2);
                            t.Span($"{totalUnits}").Bold().FontSize(9.5f);
                        });

                        r.RelativeItem().AlignRight().Text(t =>
                        {
                            if (_order.ExpectedDeliveryDate.HasValue)
                            {
                                t.Span("التوريد المتوقع: ").FontSize(9).FontColor(Colors.Grey.Darken2);
                                t.Span($"{_order.ExpectedDeliveryDate.Value:yyyy/MM/dd}").Bold().FontSize(9.5f);
                            }
                        });
                    });
                });

                col.Item().PaddingBottom(8);
            });
        }

        private void ComposeContent(IContainer container)
        {
            var details = _order.OrderDetails?.OrderBy(d => d.Id).ToList() ?? new();
            decimal calculatedSubtotal = details.Sum(d => d.Price * d.Quantity);
            decimal itemsTotal = _order.TotalPrice > 0 ? _order.TotalPrice : calculatedSubtotal;
            decimal finalTotal = _order.NetPrice > 0 || itemsTotal == 0 ? _order.NetPrice : itemsTotal;
            decimal discountDiff = Math.Max(0m, itemsTotal - finalTotal);

            container.Column(col =>
            {
                // Items Table (repeats header cleanly across pages)
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(28);    // #
                        columns.ConstantColumn(58);    // Product Code
                        columns.RelativeColumn(4.0f);  // Item Name
                        columns.RelativeColumn(1.4f);  // Unit
                        columns.RelativeColumn(1.1f);  // Qty
                        columns.RelativeColumn(1.6f);  // Unit Price
                        columns.RelativeColumn(1.8f);  // Line Total
                    });

                    table.Header(header =>
                    {
                        header.Cell().Background(Colors.Blue.Darken3).Padding(5).AlignCenter().Text("#").Bold().FontColor(Colors.White).FontSize(9);
                        header.Cell().Background(Colors.Blue.Darken3).Padding(5).AlignCenter().Text("الكود").Bold().FontColor(Colors.White).FontSize(9);
                        header.Cell().Background(Colors.Blue.Darken3).Padding(5).AlignRight().Text("اسم الصنف").Bold().FontColor(Colors.White).FontSize(9);
                        header.Cell().Background(Colors.Blue.Darken3).Padding(5).AlignCenter().Text("الوحدة").Bold().FontColor(Colors.White).FontSize(9);
                        header.Cell().Background(Colors.Blue.Darken3).Padding(5).AlignCenter().Text("الكمية").Bold().FontColor(Colors.White).FontSize(9);
                        header.Cell().Background(Colors.Blue.Darken3).Padding(5).AlignCenter().Text("سعر الوحدة").Bold().FontColor(Colors.White).FontSize(9);
                        header.Cell().Background(Colors.Blue.Darken3).Padding(5).AlignCenter().Text("الإجمالي").Bold().FontColor(Colors.White).FontSize(9);
                    });

                    if (details.Count == 0)
                    {
                        table.Cell().ColumnSpan(7).Border(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(14).AlignCenter()
                            .Text("لا توجد أصناف مسجلة في هذه الفاتورة.").Italic().FontColor(Colors.Grey.Darken1);
                    }
                    else
                    {
                        int rowIndex = 1;
                        foreach (var item in details)
                        {
                            var bg = rowIndex % 2 == 0 ? Colors.Grey.Lighten5 : Colors.White;
                            var borderCol = Colors.Grey.Lighten2;
                            string productCode = item.ProductId > 0 ? $"#{item.ProductId}" : "-";
                            string productName = item.Product?.Name ?? $"صنف #{item.ProductId}";
                            string unitName = !string.IsNullOrWhiteSpace(item.Unit?.Name)
                                ? item.Unit.Name
                                : (item.Product?.BaseUnit?.Name ?? "-");
                            decimal lineTotal = item.Price * item.Quantity;

                            table.Cell().Background(bg).BorderBottom(0.5f).BorderColor(borderCol).Padding(4).ShowEntire().AlignCenter().Text($"{rowIndex}").FontSize(9);
                            table.Cell().Background(bg).BorderBottom(0.5f).BorderColor(borderCol).Padding(4).ShowEntire().AlignCenter().Text(productCode).FontSize(8.5f).FontColor(Colors.Grey.Darken2);
                            table.Cell().Background(bg).BorderBottom(0.5f).BorderColor(borderCol).Padding(4).ShowEntire().AlignRight().Text(productName).Bold().FontSize(9);
                            table.Cell().Background(bg).BorderBottom(0.5f).BorderColor(borderCol).Padding(4).ShowEntire().AlignCenter().Text(unitName).FontSize(9);
                            table.Cell().Background(bg).BorderBottom(0.5f).BorderColor(borderCol).Padding(4).ShowEntire().AlignCenter().Text($"{item.Quantity}").Bold().FontSize(9);
                            table.Cell().Background(bg).BorderBottom(0.5f).BorderColor(borderCol).Padding(4).ShowEntire().AlignCenter().Text($"{item.Price:0.00}").FontSize(9);
                            table.Cell().Background(bg).BorderBottom(0.5f).BorderColor(borderCol).Padding(4).ShowEntire().AlignCenter().Text($"{lineTotal:0.00}").Bold().FontSize(9.5f);

                            rowIndex++;
                        }
                    }
                });

                // Financial Summary & Notes (kept intact on one page, never split)
                col.Item().PaddingTop(14).ShowEntire().Column(summary =>
                {
                    summary.Item().Row(r =>
                    {
                        // Notes box on Right (if present)
                        r.RelativeItem(3).PaddingLeft(12).Column(nc =>
                        {
                            if (!string.IsNullOrWhiteSpace(_order.Notes))
                            {
                                nc.Item().Border(1).BorderColor(Colors.Grey.Lighten1).Background(Colors.Grey.Lighten5).Padding(8).Column(box =>
                                {
                                    box.Item().AlignRight().Text("ملاحظات الفاتورة:").Bold().FontSize(9.5f).FontColor(Colors.Blue.Darken3);
                                    box.Item().PaddingTop(3).AlignRight().Text(_order.Notes).FontSize(9).FontColor(Colors.Grey.Darken4);
                                });
                            }
                        });

                        // Financial Totals Box on Left
                        r.RelativeItem(2).Border(1).BorderColor(Colors.Blue.Darken3).Background(Colors.White).Column(fc =>
                        {
                            fc.Item().Background(Colors.Grey.Lighten4).Padding(6).Row(fr =>
                            {
                                fr.RelativeItem().AlignRight().Text("إجمالي الأصناف:").Bold().FontSize(9.5f);
                                fr.RelativeItem().AlignLeft().Text($"{itemsTotal:0.00} ج.م").Bold().FontSize(9.5f);
                            });

                            if (discountDiff > 0)
                            {
                                fc.Item().BorderTop(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(6).Row(fr =>
                                {
                                    fr.RelativeItem().AlignRight().Text("الخصم:").FontSize(9.5f).FontColor(Colors.Grey.Darken2);
                                    fr.RelativeItem().AlignLeft().Text($"{discountDiff:0.00} ج.م").FontSize(9.5f).FontColor(Colors.Red.Darken1);
                                });
                            }

                            fc.Item().Background(Colors.Blue.Darken3).Padding(8).Row(fr =>
                            {
                                fr.RelativeItem().AlignRight().Text("الإجمالي النهائي:").Bold().FontSize(11).FontColor(Colors.White);
                                fr.RelativeItem().AlignLeft().Text($"{finalTotal:0.00} ج.م").Bold().FontSize(11.5f).FontColor(Colors.White);
                            });
                        });
                    });
                });
            });
        }

        private void ComposeFooter(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().LineHorizontal(1).LineColor(Colors.Blue.Darken3);
                col.Item().PaddingTop(4).Row(row =>
                {
                    row.RelativeItem(2).AlignRight().Text($"تاريخ الطباعة: {DateTime.Now:yyyy/MM/dd HH:mm}").FontSize(8).FontColor(Colors.Grey.Darken1);

                    row.RelativeItem(2).AlignCenter().Text(text =>
                    {
                        text.Span("صفحة ").FontSize(8).FontColor(Colors.Grey.Darken1);
                        text.CurrentPageNumber().FontSize(8).Bold().FontColor(Colors.Blue.Darken3);
                        text.Span(" من ").FontSize(8).FontColor(Colors.Grey.Darken1);
                        text.TotalPages().FontSize(8).Bold().FontColor(Colors.Blue.Darken3);
                    });

                    row.RelativeItem(2).AlignLeft().Text(_settings.LabName).FontSize(8).Bold().FontColor(Colors.Blue.Darken3);
                });
            });
        }
    }
}

