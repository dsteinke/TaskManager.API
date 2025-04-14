using Dapper;
using System.Data;
using TaskManager.API.DTOs.Task;
using TaskManager.API.Interfaces.Repositories;
using Task = TaskManager.API.Models.Task;


namespace TaskManager.API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDbConnection _db;

        public TaskRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<int> CreateTask(Task task)
        {
            var sql = @"INSERT INTO Task
                        (Id, UserId, Title, Description, DueDate, Priority, IsCompleted)
                        VALUES (@Id, @UserId, @Title, @Description, @DueDate, @Priority, @IsCompleted)";

            var effectedRows = await _db.ExecuteAsync(sql, task);

            return effectedRows;
        }

        public async Task<List<Task>> GetAllTasksForUser(Guid userId)
        {
            var sql = @"SELECT * FROM Task 
                        WHERE Task.UserId = @UserId;";

            var result = await _db.QueryAsync<Task>(sql, new { UserId = userId.ToString() });

            return result.ToList();
        }

        public async Task<Task?> GetTaskById(Guid taskId)
        {
            var sql = @"SELECT * FROM Task
                        WHERE Id = @Id;";

            var result = await _db.QueryFirstOrDefaultAsync<Task>(sql, new { Id = taskId.ToString() });

            return result;
        }

        public async Task<List<Task>> SearchTask(Guid userId, TaskSearchDTO searchDTO)
        {
            var sql = @"SELECT * FROM Task 
                        WHERE UserId = @UserId";

            var parameters = new DynamicParameters();
            parameters.Add("UserId", userId.ToString());

            if (!string.IsNullOrEmpty(searchDTO.Title))
            {
                sql += " AND Title LIKE @Title";
                parameters.Add("Title", $"%{searchDTO.Title}%");
            }

            if (searchDTO.DueDateFrom.HasValue)
            {
                sql += " AND DueDate >= @DueDateFrom";
                parameters.Add("DueDateFrom", searchDTO.DueDateFrom.Value);
            }

            if (searchDTO.DueDateTo.HasValue)
            {
                sql += " AND DueDate <= @DueDateTo";
                parameters.Add("DueDateTo", searchDTO.DueDateTo.Value);
            }

            if (searchDTO.Priority != null)
            {
                sql += " AND Priority = @Priority";
                parameters.Add("Priority", searchDTO.Priority);
            }

            if (searchDTO.IsCompleted.HasValue)
            {
                sql += " AND IsCompleted = @IsCompleted";
                parameters.Add("IsCompleted", searchDTO.IsCompleted);
            }

            var allowedSortFields = new[] { "DueDate", "Title", "Priority" ,"CreatedAt", "UpdatedAt" };

            if (!string.IsNullOrWhiteSpace(searchDTO.SortBy) && allowedSortFields.Contains(searchDTO.SortBy))
            {
                var sortDirection = searchDTO.SortDescending == true ? "DESC" : "ASC";
                sql += $" ORDER BY Task.{searchDTO.SortBy} {sortDirection}";
            }

            var result = await _db.QueryAsync<Task>(sql, parameters);

            return result.ToList();
        }

        public async Task<int> UpdateTask(Guid taskId, Guid userId, TaskUpdateDTO taskUpdateDTO)
        {
            var sqlSnippets = new List<string>();
            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(taskUpdateDTO.Title))
            {
                sqlSnippets.Add("Title = @Title");
                parameters.Add("Title", taskUpdateDTO.Title);
            }

            if (!string.IsNullOrWhiteSpace(taskUpdateDTO.Description))
            {
                sqlSnippets.Add("Description = @Description");
                parameters.Add("Description", taskUpdateDTO.Description);
            }

            if (taskUpdateDTO.DueDate.HasValue)
            {
                sqlSnippets.Add("DueDate = @DueDate");
                parameters.Add("DueDate", taskUpdateDTO.DueDate);
            }

            if (taskUpdateDTO.Priority != null)
            {
                sqlSnippets.Add("Priority = @Priority");
                parameters.Add("Priority", taskUpdateDTO.Priority);
            }

            if (taskUpdateDTO.IsCompleted.HasValue)
            {
                sqlSnippets.Add("IsCompleted = @IsCompleted");
                parameters.Add("IsCompleted", taskUpdateDTO.IsCompleted);
            }

            var sql = $@"UPDATE Task SET {string.Join(", ", sqlSnippets)}
                        WHERE Id = @TaskId AND UserId = @UserId";

            parameters.Add("TaskId", taskId.ToString());
            parameters.Add("UserId", userId.ToString());

            var effectedRows = await _db.ExecuteAsync(sql, parameters);

            return effectedRows;
        }

        public async Task<int> DeleteTask(Guid taskId, Guid userId)
        {
            var sql = @"DELETE FROM Task WHERE Id = @TaskId AND UserId = @UserId;";

            var effectedRows = await _db.ExecuteAsync(sql, new
            {
                TaskId = taskId.ToString(),
                UserId = userId.ToString()
            });

            return effectedRows;
        }
    }
}
