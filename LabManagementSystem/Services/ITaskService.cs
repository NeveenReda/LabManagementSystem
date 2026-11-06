using LabManagementSystem.Models;

namespace LabManagementSystem.Services
{
    public class TaskFilterDto
    {
        public bool? IsCompleted { get; set; }
        public int? Priority { get; set; }
        public bool OnlyOverdue { get; set; }
        public string? SearchTerm { get; set; }
    }

    public interface ITaskService
    {
        List<Need> GetTasks(TaskFilterDto filter);
        Task<List<Need>> GetTasksAsync(TaskFilterDto filter, CancellationToken ct = default);

        Need? GetTaskById(int id);
        Task<Need?> GetTaskByIdAsync(int id, CancellationToken ct = default);

        void SaveTask(Need task);
        Task SaveTaskAsync(Need task, CancellationToken ct = default);

        void ToggleTaskStatus(int taskId);
        Task ToggleTaskStatusAsync(int taskId, CancellationToken ct = default);

        void DeleteTask(int taskId);
        Task DeleteTaskAsync(int taskId, CancellationToken ct = default);
    }
}
