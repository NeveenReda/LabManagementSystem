using System;
using System.Collections.Generic;
using System.Linq;
using LabManagementSystem.Configuration;
using LabManagementSystem.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace LabManagementSystem.Printing
{
    public class DoctorVisitReportDocument : IDocument
    {
        private readonly List<DoctorVisitReportDto> _items;
        private readonly DateTime _fromDate;
        private readonly DateTime _toDate;
        private readonly string? _filterDoctorName;
        private readonly LabSettings _settings;

        public DoctorVisitReportDocument(List<DoctorVisitReportDto> items, DateTime fromDate, DateTime toDate, string? filterDoctorName = null)
        {
            _items = items ?? new List<DoctorVisitReportDto>();
            _fromDate = fromDate;
            _toDate = toDate;
            _filterDoctorName = filterDoctorName;
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
                        c.Item().Text("تقرير الزيارات الطبية حسب الطبيب المحول").Bold().FontSize(12).FontColor(Colors.Blue.Darken2);
                        c.Item().Text($"الفترة من: {_fromDate:yyyy/MM/dd}  إلى: {_toDate:yyyy/MM/dd}").FontSize(9.5f);
                        if (!string.IsNullOrWhiteSpace(_filterDoctorName))
                        {
                            c.Item().Text($"الطبيب المحدد: {_filterDoctorName}").Bold().FontSize(9.5f).FontColor(Colors.Blue.Darken3);
                        }
                    });

                    row.RelativeItem(2).AlignLeft().Column(c =>
                    {
                        c.Item().Text($"تاريخ الاستخراج: {DateTime.Now:yyyy/MM/dd HH:mm}").FontSize(8.5f).FontColor(Colors.Grey.Darken2);
                        c.Item().Text("نظام إدارة المعامل الطبية - أوليفيا").FontSize(8).FontColor(Colors.Grey.Darken1);
                    });
                });

                col.Item().PaddingVertical(5).LineHorizontal(1.5f).LineColor(Colors.Blue.Darken3);
            });
        }

        private void ComposeContent(IContainer container)
        {
            int totalVisits = _items.Sum(x => x.TotalVisits);
            int totalDoctors = _items.Count;
            var topDoctor = _items.OrderByDescending(x => x.TotalVisits).FirstOrDefault();

            container.Column(col =>
            {
                // Summary KPI Cards
                col.Item().PaddingBottom(10).Table(t =>
                {
                    t.ColumnsDefinition(c =>
                    {
                        c.RelativeColumn();
                        c.RelativeColumn();
                        c.RelativeColumn();
                    });

                    t.Cell().Padding(3).Border(1).BorderColor(Colors.Blue.Medium).Background(Colors.Blue.Lighten5).Padding(6).Column(c =>
                    {
                        c.Item().AlignCenter().Text("إجمالي الزيارات المحولة").FontSize(9);
                        c.Item().AlignCenter().Text($"{totalVisits} زيارة").Bold().FontSize(13).FontColor(Colors.Blue.Darken3);
                    });

                    t.Cell().Padding(3).Border(1).BorderColor(Colors.Blue.Medium).Background(Colors.Blue.Lighten5).Padding(6).Column(c =>
                    {
                        c.Item().AlignCenter().Text("عدد الأطباء في التقرير").FontSize(9);
                        c.Item().AlignCenter().Text($"{totalDoctors} طبيب").Bold().FontSize(13).FontColor(Colors.Blue.Darken3);
                    });

                    t.Cell().Padding(3).Border(1).BorderColor(Colors.Green.Medium).Background(Colors.Green.Lighten5).Padding(6).Column(c =>
                    {
                        c.Item().AlignCenter().Text("الأكثر تحويلاً للزيارات").FontSize(9);
                        string topText = topDoctor != null && topDoctor.TotalVisits > 0 
                            ? $"{topDoctor.DoctorName} ({topDoctor.TotalVisits})" 
                            : "-";
                        c.Item().AlignCenter().Text(topText).Bold().FontSize(11).FontColor(Colors.Green.Darken3);
                    });
                });

                // Detailed Table
                col.Item().Table(t =>
                {
                    t.ColumnsDefinition(c =>
                    {
                        c.ConstantColumn(35);  // #
                        c.RelativeColumn(3);   // Doctor Name
                        c.RelativeColumn(2);   // Specialty
                        c.RelativeColumn(2);   // Phone
                        c.ConstantColumn(75);  // Visits Count
                        c.ConstantColumn(65);  // Percentage
                    });

                    // Header
                    t.Header(h =>
                    {
                        static IContainer HeaderCell(IContainer c) =>
                            c.Background(Colors.Blue.Darken3).Padding(5).AlignCenter();

                        h.Cell().Element(HeaderCell).Text("#").Bold().FontColor(Colors.White).FontSize(9);
                        h.Cell().Element(HeaderCell).Text("اسم الطبيب").Bold().FontColor(Colors.White).FontSize(9);
                        h.Cell().Element(HeaderCell).Text("التخصص").Bold().FontColor(Colors.White).FontSize(9);
                        h.Cell().Element(HeaderCell).Text("رقم الهاتف").Bold().FontColor(Colors.White).FontSize(9);
                        h.Cell().Element(HeaderCell).Text("عدد الزيارات").Bold().FontColor(Colors.White).FontSize(9);
                        h.Cell().Element(HeaderCell).Text("النسبة").Bold().FontColor(Colors.White).FontSize(9);
                    });

                    if (_items.Count == 0)
                    {
                        t.Cell().ColumnSpan(6).Padding(16).AlignCenter().Text("لا توجد زيارات مسجلة للأطباء خلال هذه الفترة.").Italic().FontColor(Colors.Grey.Darken1);
                    }
                    else
                    {
                        int index = 1;
                        foreach (var item in _items)
                        {
                            var bg = (index % 2 == 0) ? Colors.Grey.Lighten4 : Colors.White;
                            double percent = totalVisits > 0 ? (item.TotalVisits * 100.0 / totalVisits) : 0;

                            t.Cell().Background(bg).Padding(4).AlignCenter().Text(index.ToString()).FontSize(8.5f);
                            t.Cell().Background(bg).Padding(4).AlignRight().Text(item.DoctorName).Bold().FontSize(9);
                            t.Cell().Background(bg).Padding(4).AlignCenter().Text(string.IsNullOrWhiteSpace(item.Specialty) ? "-" : item.Specialty).FontSize(8.5f);
                            t.Cell().Background(bg).Padding(4).AlignCenter().Text(string.IsNullOrWhiteSpace(item.PhoneNumber) ? "-" : item.PhoneNumber).FontSize(8.5f);
                            t.Cell().Background(bg).Padding(4).AlignCenter().Text(item.TotalVisits.ToString()).Bold().FontSize(9.5f).FontColor(Colors.Blue.Darken3);
                            t.Cell().Background(bg).Padding(4).AlignCenter().Text($"{percent:0.0}%").FontSize(8.5f);

                            index++;
                        }

                        // Summary Total Row
                        t.Cell().ColumnSpan(4).Background(Colors.Blue.Lighten4).Padding(6).AlignRight().Text("الإجمالي الكلي للزيارات:").Bold().FontSize(10).FontColor(Colors.Blue.Darken4);
                        t.Cell().Background(Colors.Blue.Lighten4).Padding(6).AlignCenter().Text(totalVisits.ToString()).Bold().FontSize(11).FontColor(Colors.Blue.Darken4);
                        t.Cell().Background(Colors.Blue.Lighten4).Padding(6).AlignCenter().Text(totalVisits > 0 ? "100%" : "0%").Bold().FontSize(9.5f).FontColor(Colors.Blue.Darken4);
                    }
                });
            });
        }

        private void ComposeFooter(IContainer container)
        {
            container.Column(col =>
            {
                col.Item().PaddingTop(10).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                col.Item().PaddingTop(3).Row(row =>
                {
                    row.RelativeItem().Text($"معمل أوليفيا للتحاليل الطبية - هاتف: {_settings.Phone}").FontSize(8).FontColor(Colors.Grey.Darken1);
                    row.RelativeItem().AlignLeft().Text(x =>
                    {
                        x.Span("صفحة ");
                        x.CurrentPageNumber();
                        x.Span(" من ");
                        x.TotalPages();
                    });
                });
            });
        }
    }
}
