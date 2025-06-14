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

        public async Task<Guid> CreateAsync(CreatePublicationDto objPublication)
        {
            const string sql = @"
                INSERT INTO [Publication] (
                     Id, Ttitle, Body, CreatedDate, UserId, CategoryId
                )
                VALUES (
                     @Id, @Ttitle, @Body, @CreatedDate, @UserId, @CategoryId
                );
            ";

            using var connection = _dbContext.CreateConnection();
            var newId = Guid.NewGuid();

            var parameters = new
            {
                Id = newId,
                Ttitle = objPublication.Title,
                Body = objPublication.Body,
                CreatedDate = objPublication.CreatedDate,
                UserId = objPublication.UserId,
                CategoryId = objPublication.CategoryId
            };

            await connection.ExecuteAsync(sql, parameters);
            return newId;
        }

        
    }
}
