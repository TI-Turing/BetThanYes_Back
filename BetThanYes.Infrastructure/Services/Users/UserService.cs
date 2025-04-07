using BetThanYes.Domain.Models;
using BetThanYes.Infrastructure.Database;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BetThanYes.Infrastructure.Services.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlDbContext _dbContext;

        public UserRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            const string sql = @"
                INSERT INTO [User] (
                    Id, FullName, Email, PasswordHash, BirthDate, Gender,
                    TimeZone, ProfilePictureUrl, CustomMotivationalQuote,
                    RegistrationDate, IsActive, MotivationScore
                )
                VALUES (
                    @Id, @FullName, @Email, @PasswordHash, @BirthDate, @Gender,
                    @TimeZone, @ProfilePictureUrl, @CustomMotivationalQuote,
                    @RegistrationDate, @IsActive, @MotivationScore
                );
            ";

            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(sql, user);
        }

        public async Task DeleteAsync(Guid id)
        {
            const string sql = "DELETE FROM Users WHERE Id = @Id;";
            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<User>> GetAllByUserIdAsync(Guid userId)
        {
            const string sql = "SELECT * FROM Users WHERE Id = @UserId;";
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryAsync<User>(sql, new { UserId = userId });
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            const string sql = "SELECT * FROM Users WHERE Id = @Id;";
            using var connection = _dbContext.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task UpdateAsync(User user)
        {
            const string sql = @"
                UPDATE Users
                SET 
                    FullName = @FullName,
                    Email = @Email,
                    PasswordHash = @PasswordHash,
                    BirthDate = @BirthDate,
                    Gender = @Gender,
                    TimeZone = @TimeZone,
                    ProfilePictureUrl = @ProfilePictureUrl,
                    CustomMotivationalQuote = @CustomMotivationalQuote,
                    LastLogin = @LastLogin,
                    IsActive = @IsActive,
                    MotivationScore = @MotivationScore
                WHERE Id = @Id;
            ";

            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(sql, user);
        }
    }
}
