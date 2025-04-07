using Dapper;
using System.Data;
using TaskManager.API.Interfaces.Repositories;
using TaskManager.API.Models;
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
                        (Id, UserId, Title, Description, DueDate, CategoryId, PriorityId, IsCompleted)
                        VALUES (@Id, @UserId, @Title, @Description, @DueDate, @CategoryId, @PriorityId, @IsCompleted)";

            var effectedRows = await _db.ExecuteAsync(sql, task);

            return effectedRows;
        }

        public async Task<List<Task>> GetAllTasksForUser(Guid userId)
        {
            var sql = @"SELECT * FROM Task 
                        LEFT JOIN Priority
                        ON Task.PriorityId = Priority.Id
                        LEFT JOIN Category
                        ON Task.CategoryId = Category.Id
                        WHERE Task.UserId = @UserId;";

            var result = await _db.QueryAsync<Task, Priority, Category, Task>
                (sql, (task, priority, category) =>
                {
                    task.Priority = priority;
                    task.Category = category;
                    return task;
                },
                new { UserId = userId.ToString() },
                splitOn: "Id"
                );

            return result.ToList();
        }

        public Task<Task> GetTaskById(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Task>> SearchTask(string title)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateTask(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteTask(Guid taskId)
        {
            throw new NotImplementedException();
        }
    }
}
