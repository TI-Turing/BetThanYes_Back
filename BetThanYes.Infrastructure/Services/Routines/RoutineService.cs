//using BetThanYes.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using BetThanYes.Domain.Models;

namespace BetThanYes.Infrastructure.Services.Routines
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }

        public async Task<IEnumerable<Routine>> GetAllAsync(Guid userId)
        {
            var sql = "SELECT * FROM Routines WHERE UserId = @UserId";
            using var connection = CreateConnection();
            return await connection.QueryAsync<Routine>(sql, new { UserId = userId });
        }

        public async Task<Routine?> GetByIdAsync(Guid id)
        {
            var sql = "SELECT * FROM Routines WHERE Id = @Id";
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Routine>(sql, new { Id = id });
        }

        public async Task CreateAsync(Routine routine)
        {
            var sql = @"INSERT INTO Routines (Id, UserId, RoutineName, Description, IsDefault)
                        VALUES (@Id, @UserId, @RoutineName, @Description, @IsDefault)";
            routine.Id = Guid.NewGuid();
            using var connection = CreateConnection();
            await connection.ExecuteAsync(sql, routine);
        }

        public async Task UpdateAsync(Routine routine)
        {
            var sql = @"UPDATE Routines
                        SET RoutineName = @RoutineName,
                            Description = @Description,
                            IsDefault = @IsDefault
                        WHERE Id = @Id";
            using var connection = CreateConnection();
            await connection.ExecuteAsync(sql, routine);
        }

        public async Task DeleteAsync(Guid id)
        {
            var sql = "DELETE FROM Routines WHERE Id = @Id";
            using var connection = CreateConnection();
            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
