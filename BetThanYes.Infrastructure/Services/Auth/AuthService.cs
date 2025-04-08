using BetThanYes.Domain.DTOs.Request.Auth;
using BetThanYes.Domain.DTOs.Request.Login;
using BetThanYes.Domain.Models;
using BetThanYes.Infrastructure.Database;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;

namespace BetThanYes.Infrastructure.Services.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SqlDbContext _dbContext;
        public AuthRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByEmail(string email)
        {
            const string sql = "SELECT * FROM [User] WHERE Email = @Email;";
            using var connection = _dbContext.CreateConnection();
            
            User result = await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
            return result;
        }

        public async Task<bool> SaveRefreshTokenAsync(RefreshTokenDto dto)
        {
            try
            {
                var query = @"INSERT INTO RefreshTokens (UserId, RefreshToken, ExpirationDate, DeviceId, DeviceName, IPAddress)
                  VALUES (@UserId, @RefreshToken, @ExpirationDate, @DeviceId, @DeviceName, @IPAddress)";
                using var connection = _dbContext.CreateConnection();

                await connection.ExecuteAsync(query, dto);
                return true;
            }
            catch (SqlException ex)
            {                
                throw;
            }
        }
    }
}
