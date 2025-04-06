using Dapper;
using Microsoft.Extensions.Configuration;
using BetThanYes.Domain.Models;
using BetThanYes.Infrastructure.Database;
using System.Data;

namespace BetThanYes.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly SqlDbContext _context;

        public UserService(SqlDbContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<User>> GetAllAsync()
        //{
        //    using var connection = _context.CreateConnection();
        //    var sql = "SELECT * FROM Users";
        //    return await connection.QueryAsync<User>(sql, new { });
        //}

        public async Task<User?> GetByIdAsync(Guid id)
        {
            using var connection = _context.CreateConnection();
            var sql = "SELECT * FROM Users WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task CreateAsync(User user)
        {
            using var connection = _context.CreateConnection();
            var sql = @"INSERT INTO Users (Id, UserId, UserName, Description, IsDefault)
                        VALUES (@Id, @UserId, @UserName, @Description, @IsDefault)";
            user.Id = Guid.NewGuid();
            await connection.ExecuteAsync(sql, user);
        }

        public async Task UpdateAsync(User user)
        {
            using var connection = _context.CreateConnection();
            var sql = @"UPDATE Users
                        SET UserName = @UserName,
                            Description = @Description,
                            IsDefault = @IsDefault
                        WHERE Id = @Id";
            await connection.ExecuteAsync(sql, user);
        }

        public async Task DeleteAsync(Guid id)
        {
            using var connection = _context.CreateConnection();
            var sql = "DELETE FROM Users WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
