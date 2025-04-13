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

            var effectedRows = await _db.ExecuteAsync(sql, user);

            return effectedRows;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var sql = @"SELECT * FROM User";
            
            var users = await _db.QueryAsync<User>(sql);

            return users.ToList();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var sql = @"SELECT * FROM User
                        WHERE Email = @Email";

            var user = await _db.QueryFirstOrDefaultAsync<User>(sql, new {Email = email});

            return user;
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            var sql = @"SELECT * FROM User
                        WHERE Username = @Username";

            var user = await _db.QueryFirstOrDefaultAsync<User>(sql, new { Username = username });

            return user;
        }

        public async Task<User?> GetUserById(Guid userId)
        {
            var sql = @"SELECT * FROM User
                        WHERE Id = @Id";

            var user = await _db.QueryFirstOrDefaultAsync<User>(sql, new { Id = userId.ToString() });

            return user;
        }

        public async Task<int> UpdateUser(Guid userId, string username, string email)
        {
            var sql = new List<string>();
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(username))
            {
                sql.Add("Username = @Username");
                parameters.Add("Username", username);
            }
            if (!string.IsNullOrEmpty(email))
            {
                sql.Add("Email = @Email");
                parameters.Add("Email", email);
            }

            if (sql.Count == 0)
                return 0;

            var updateQuery = $"UPDATE User SET {string.Join(", ", sql)} WHERE Id = @Id";
            parameters.Add("Id", userId.ToString());

            return await _db.ExecuteAsync(updateQuery, parameters);
        }

        public async Task<int> DeleteUser(Guid userId)
        {
            var sql = @"DELETE FROM User WHERE Id = @Id";

            var effectedRows = await _db.ExecuteAsync(sql, new {Id = userId.ToString()});

            return effectedRows;
        }
    }
}
