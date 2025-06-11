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

        public async Task CreateAsync(CreateScheduleDto objSchedule)
        {
            const string sql = @"
                INSERT INTO [Post] (
                    Id, Name, Days
                )
                VALUES (
                    @Id, @Name, @Days
                );
            ";

            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(sql, objSchedule);
        }        
    }
} 
