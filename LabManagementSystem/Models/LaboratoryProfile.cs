using System;

namespace LabManagementSystem.Models
{
    public class LaboratoryProfile
    {
        public int Id { get; set; }
        public string LabName { get; set; } = "معمل الأمل للتحاليل الطبية";
        public string? LabNameEn { get; set; } = "Al-Amal Clinical Laboratory";
        public string? Subtitle { get; set; } = "Al-Amal Clinical Laboratory & Diagnostic Center";
        public string? Address { get; set; } = "شارع الجمهورية - برج الأطباء - الدور الثاني";
        public string? Phone { get; set; } = "01000000000";
        public string? AdditionalPhone { get; set; }
        public string? Email { get; set; } = "info@alamallab.com";
        public string? DirectorName { get; set; } = "د. أخصائي التحاليل الطبية";
        public string? ReportFooterNote { get; set; } = "النتائج معتمدة إلكترونياً. تمنياتنا لكم بالشفاء العاجل.";
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

