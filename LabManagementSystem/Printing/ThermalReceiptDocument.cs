using LabManagementSystem.Configuration;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Unit = QuestPDF.Infrastructure.Unit;

namespace LabManagementSystem.Printing
{
    public class ThermalReceiptModel
    {
        public int VisitId { get; set; }
        public DateTime VisitDate { get; set; }
        public string PatientName { get; set; } = "";
        public string MedicalCode { get; set; } = "";
        public string DoctorName { get; set; } = "";
        public List<Tuple<string, decimal>> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RemainingBalance { get; set; }
        public string PaymentMethodName { get; set; } = "نقدي";
        public DateTime ReceiptDate { get; set; } = DateTime.Now;
    }

    public class ThermalReceiptDocument : IDocument
    {
        private readonly ThermalReceiptModel _model;
        private readonly LabSettings _settings;

        public ThermalReceiptDocument(ThermalReceiptModel model)
        {
            _model = model;
            _settings = AppConfig.GetLabSettings();
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.ContinuousSize(80, Unit.Millimetre);
                page.Margin(3, Unit.Millimetre);
                page.DefaultTextStyle(x => x.FontFamily("Arial").FontSize(8).FontColor(Colors.Black));

                page.Content().Column(col =>
                {
                    col.Spacing(2);

                    // Header
                    col.Item().AlignCenter().Text(_settings.LabName).Bold().FontSize(10);
                    col.Item().AlignCenter().Text(_settings.LabSubtitle).FontSize(6.5f).FontColor(Colors.Grey.Darken2);
                    col.Item().AlignCenter().Text($"هاتف: {_settings.Phone}").FontSize(7);
                    col.Item().LineHorizontal(0.5f).LineColor(Colors.Black);

                    // Title
                    col.Item().AlignCenter().Text("إيصال سداد خدمات طبية").Bold().FontSize(8.5f);

                    // Patient Details
                    col.Item().Row(r =>
                    {
                        r.RelativeItem().AlignRight().Text($"رقم الزيارة: {_model.VisitId}").Bold();
                        r.RelativeItem().AlignLeft().Text($"التاريخ: {_model.ReceiptDate:yyyy/MM/dd HH:mm}").FontSize(6.5f);
                    });
                    col.Item().AlignRight().Text($"المريض: {_model.PatientName}").Bold();
                    col.Item().AlignRight().Text($"الكود الطبي: {_model.MedicalCode}");
                    if (!string.IsNullOrWhiteSpace(_model.DoctorName))
                        col.Item().AlignRight().Text($"الطبيب المعالج: د. {_model.DoctorName}");

                    col.Item().LineHorizontal(0.5f).LineColor(Colors.Black);

                    // Tests Table
                    col.Item().Table(t =>
                    {
                        t.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn(3);
                            c.RelativeColumn(1.5f);
                        });

                        t.Header(h =>
                        {
                            h.Cell().AlignRight().Text("التحليل المطلوب").Bold();
                            h.Cell().AlignLeft().Text("السعر").Bold();
                        });

                        foreach (var item in _model.Items)
                        {
                            t.Cell().AlignRight().Text(item.Item1);
                            t.Cell().AlignLeft().Text($"{item.Item2:0.00}");
                        }
                    });

                    col.Item().LineHorizontal(0.5f).LineColor(Colors.Black);

                    // Financials
                    col.Item().Row(r =>
                    {
                        r.RelativeItem().AlignRight().Text("الإجمالي:");
                        r.RelativeItem().AlignLeft().Text($"{_model.TotalAmount:0.00} ج.م").Bold();
                    });

                    if (_model.DiscountAmount > 0)
                    {
                        col.Item().Row(r =>
                        {
                            r.RelativeItem().AlignRight().Text("الخصم:");
                            r.RelativeItem().AlignLeft().Text($"-{_model.DiscountAmount:0.00} ج.م");
                        });
                    }

                    col.Item().Row(r =>
                    {
                        r.RelativeItem().AlignRight().Text("الصافي المطلوب:").Bold();
                        r.RelativeItem().AlignLeft().Text($"{_model.NetAmount:0.00} ج.م").Bold();
                    });

                    col.Item().Row(r =>
                    {
                        r.RelativeItem().AlignRight().Text($"المدفوع ({_model.PaymentMethodName}):").Bold();
                        r.RelativeItem().AlignLeft().Text($"{_model.PaidAmount:0.00} ج.م").Bold();
                    });

                    col.Item().Row(r =>
                    {
                        r.RelativeItem().AlignRight().Text("المتبقي:").Bold();
                        r.RelativeItem().AlignLeft().Text($"{_model.RemainingBalance:0.00} ج.م").Bold();
                    });

                    col.Item().LineHorizontal(0.5f).LineColor(Colors.Black);

                    // Footer
                    col.Item().AlignCenter().Text(_settings.ReportFooterNote).FontSize(6).Italic();
                    col.Item().AlignCenter().Text("*** شكراً لزيارتكم ***").FontSize(7).Bold();
                });
            });
        }
    }
}
