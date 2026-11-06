namespace LabManagementSystem.Enums
{
    public enum TaskPriorityEnum
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Urgent = 4
    }

    public static class TaskPriorityExtensions
    {
        public static string ToArabic(this TaskPriorityEnum priority)
        {
            return priority switch
            {
                TaskPriorityEnum.Low => "منخفضة",
                TaskPriorityEnum.Medium => "متوسطة",
                TaskPriorityEnum.High => "هامة",
                TaskPriorityEnum.Urgent => "عاجلة جداً",
                _ => "عادية"
            };
        }
    }
}

