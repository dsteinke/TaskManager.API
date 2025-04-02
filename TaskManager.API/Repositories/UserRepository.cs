using Dapper;
using System.Data;
using TaskManager.API.Interfaces.Repositories;
using TaskManager.API.Models;

namespace TaskManager.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _db;

        public UserRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<int> CreateUser(User user)
        {
            var sql = @"INSERT INTO User (Id, Username, Email, PasswordHash)
                        VALUES(@Id, @Username, @Email, @PasswordHash)";

            var effectedRows = await _db.ExecuteAsync(sql, new
            {
                user.Id,
                user.Username,
                user.Email,
                user.PasswordHash
            });

            return effectedRows;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var sql = @"SELECT * FROM User";
            
            var users = await _db.QueryAsync<User>(sql);

            return users.ToList();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var sql = @$"SELECT * FROM User
                        WHERE Email = @Email";

            var user = await _db.QueryFirstOrDefaultAsync<User>(sql, new {Email = email});

            return user;
        }

        public async Task<User> GetUserById(Guid userId)
        {
            var sql = @$"SELECT * FROM User
                        WHERE Id = @Id";

            var user = await _db.QueryFirstOrDefaultAsync<User>(sql, new { Id = userId });

            return user;
        }
    }
}
