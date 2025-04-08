using BetThanYes.Domain.Models;
using BetThanYes.Infrastructure.Database;
using Dapper;
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
    }
}
