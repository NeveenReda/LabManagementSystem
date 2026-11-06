namespace LabManagementSystem.Enums
{
    public enum PaymentMethodEnum
    {
        Cash = 1,
        Visa = 2,
        BankTransfer = 3,
        Insurance = 4
    }

    public static class PaymentMethodExtensions
    {
        public static string ToArabic(this PaymentMethodEnum method)
        {
            return method switch
            {
                PaymentMethodEnum.Cash => "نقدي (كاش)",
                PaymentMethodEnum.Visa => "فيزا / بطاقة إلكترونية",
                PaymentMethodEnum.BankTransfer => "تحويل بنكي / محفظة",
                PaymentMethodEnum.Insurance => "تأمين طبي",
                _ => "نقدي"
            };
        }
    }
}

