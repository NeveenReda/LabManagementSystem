namespace LabManagementSystem.Enums
{
    public enum AbnormalFlagEnum
    {
        Normal = 0,
        Low = 1,
        High = 2,
        Abnormal = 3
    }

    public static class AbnormalFlagExtensions
    {
        public static string ToArabic(this AbnormalFlagEnum flag)
        {
            return flag switch
            {
                AbnormalFlagEnum.Low => "منخفض (Low)",
                AbnormalFlagEnum.High => "مرتفع (High)",
                AbnormalFlagEnum.Abnormal => "غير طبيعي (Abnormal)",
                _ => "طبيعي (Normal)"
            };
        }
    }
}

