using BetThanYes.Domain.Models;
using BetThanYes.Infrastructure.Database;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BetThanYes.Infrastructure.Services.Routines
{
    public class RoutineRepository : IRoutineRepository
    {
        private readonly SqlDbContext _dbContext;

        public RoutineRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Routine routine)
        {
            const string sql = @"
                INSERT INTO Routine (Id, UserId, RoutineName, IsDefault, CreatedAt)
                VALUES (@Id, @UserId, @RoutineName, @IsDefault, @CreatedAt);
            ";

            using var connection = await _dbContext.CreateConnectionAsync();
            await connection.ExecuteAsync(sql, routine);
        }

        public async Task DeleteAsync(Guid id)
        {
            const string sql = "DELETE FROM Routine WHERE Id = @Id;";
            using var connection = await _dbContext.CreateConnectionAsync();
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<Routine>> GetAllByUserIdAsync(Guid userId)
        {
            const string sql = "SELECT * FROM Routine WHERE UserId = @UserId;";
            using var connection = await _dbContext.CreateConnectionAsync();
            return await connection.QueryAsync<Routine>(sql, new { UserId = userId });
        }

        public async Task<Routine?> GetByIdAsync(Guid id)
        {
            const string sql = "SELECT * FROM Routine WHERE Id = @Id;";
            using var connection = await _dbContext.CreateConnectionAsync();
            return await connection.QueryFirstOrDefaultAsync<Routine>(sql, new { Id = id });
        }

        public async Task<bool> UpdateAsync(Routine routine)
        {
            try
            {
                const string sql = @"
                UPDATE Routine 
                SET RoutineName = @RoutineName,
                    IsDefault = @IsDefault
                WHERE Id = @Id;
            ";

                using var connection = await _dbContext.CreateConnectionAsync();
                await connection.ExecuteAsync(sql, routine);
                return true;
            }
            catch (Exception ex)
            {
                
                return false;
            }  

        }
    }
}
