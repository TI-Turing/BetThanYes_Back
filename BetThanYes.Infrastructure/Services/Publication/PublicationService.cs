using BetThanYes.Domain.Models;
using BetThanYes.Infrastructure.Database;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BetThanYes.Domain.DTOs.Request.Publication;
namespace BetThanYes.Infrastructure.Services.Publication
{
    public class PublicationRepository : IPublicationRepository
    {
        private readonly SqlDbContext _dbContext;

        public PublicationRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAsync(CreatePublicationDto objPublication)
        {
            const string sql = @"
                INSERT INTO [User] (
                    Id, NamePublication, DaysPublication
                )
                VALUES (
                    @Id, @Name, @Days 
                );
            ";

            using var connection = _dbContext.CreateConnection();
            await connection.ExecuteAsync(sql, objPublication);
        }

        
    }
}
