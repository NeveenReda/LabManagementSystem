using LabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Services
{
    public class TaskService : ITaskService
    {
        private readonly Func<LabContext> _contextFactory;

        public TaskService(Func<LabContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<Need> GetTasks(TaskFilterDto filter)
        {
            using var db = _contextFactory();
            return BuildTasksQuery(db, filter).ToList();
        }

        public async Task<List<Need>> GetTasksAsync(TaskFilterDto filter, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            return await BuildTasksQuery(db, filter).ToListAsync(ct);
        }

        private static IQueryable<Need> BuildTasksQuery(LabContext db, TaskFilterDto filter)
        {
            var query = db.Needs.AsNoTracking().Where(n => !n.IsDeleted).AsQueryable();

            if (filter.IsCompleted.HasValue)
            {
                query = query.Where(n => n.IsCompleted == filter.IsCompleted.Value);
            }

            if (filter.Priority.HasValue && filter.Priority.Value > 0)
            {
                query = query.Where(n => n.Priority == filter.Priority.Value);
            }

            if (filter.OnlyOverdue)
            {
                var now = DateTime.Now;
                query = query.Where(n => !n.IsCompleted && n.DueDate.HasValue && n.DueDate.Value < now);
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                string term = filter.SearchTerm.Trim();
                query = query.Where(n => (n.Title != null && n.Title.Contains(term)) || n.Notes.Contains(term));
            }

            return query
                .OrderBy(n => n.IsCompleted)
                .ThenByDescending(n => n.Priority)
                .ThenBy(n => n.DueDate ?? DateTime.MaxValue);
        }

        public Need? GetTaskById(int id)
        {
            using var db = _contextFactory();
            return db.Needs.Find(id);
        }

        public async Task<Need?> GetTaskByIdAsync(int id, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            return await db.Needs.FindAsync(new object[] { id }, ct);
        }

        public void SaveTask(Need task)
        {
            using var db = _contextFactory();
            SaveTaskInternal(db, task);
            db.SaveChanges();
        }

        public async Task SaveTaskAsync(Need task, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            SaveTaskInternal(db, task);
            await db.SaveChangesAsync(ct);
        }

        private static void SaveTaskInternal(LabContext db, Need task)
        {
            if (string.IsNullOrWhiteSpace(task.Notes))
                throw new InvalidOperationException("نص المهمة / الطلب مطلوب ولا يمكن تركه فارغاً.");

            if (task.Id == 0)
            {
                task.CreatedAt ??= DateTime.Now;
                db.Needs.Add(task);
            }
            else
            {
                var existing = db.Needs.Find(task.Id);
                if (existing != null)
                {
                    existing.Title = task.Title;
                    existing.Notes = task.Notes;
                    existing.Priority = task.Priority;
                    existing.DueDate = task.DueDate;
                    existing.IsCompleted = task.IsCompleted;
                }
            }
        }

        public void ToggleTaskStatus(int taskId)
        {
            using var db = _contextFactory();
            var task = db.Needs.Find(taskId);
            if (task == null) return;

            task.IsCompleted = !task.IsCompleted;
            db.SaveChanges();
        }

        public async Task ToggleTaskStatusAsync(int taskId, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            var task = await db.Needs.FindAsync(new object[] { taskId }, ct);
            if (task == null) return;

            task.IsCompleted = !task.IsCompleted;
            await db.SaveChangesAsync(ct);
        }

        public void DeleteTask(int taskId)
        {
            using var db = _contextFactory();
            var task = db.Needs.Find(taskId);
            if (task == null) return;

            task.IsDeleted = true;
            db.SaveChanges();
        }

        public async Task DeleteTaskAsync(int taskId, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            var task = await db.Needs.FindAsync(new object[] { taskId }, ct);
            if (task == null) return;

            task.IsDeleted = true;
            await db.SaveChangesAsync(ct);
        }
    }
}
