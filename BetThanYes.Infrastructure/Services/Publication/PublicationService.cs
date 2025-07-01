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
        // Este método agrega una nueva publicación a la base de datos
        // y devuelve el ID de la nueva publicación.

        public async Task<Guid> AddAsync(CreatePublicationDto objPublication)
        {
            const string sql = @"
                INSERT INTO [Publication] (
                     Id, Title, Body, CreatedDate, UserId, CategoryId
                )
                VALUES (
                     @Id, @Ttitle, @Body, @CreatedDate, @UserId, @CategoryId
                );
            ";

            using var connection = await _dbContext.CreateConnectionAsync();
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

        // Este método obtiene todas las publicaciones
        // y devuelve una lista de objetos Publication.

        public async Task<List<Domain.Models.Publication>> GetAsync()
        {
            const string sql = @"
                SELECT * FROM [Publication]
                    
            ";
        
            using var connection = await _dbContext.CreateConnectionAsync();


            var objResult = await connection.QueryAsync<Domain.Models.Publication>(sql);
            return objResult.ToList();
        }

        // Este método obtiene una publicación por su ID
        // y devuelve un objeto Publication.

        public async Task<Domain.Models.Publication?> GetByIdAsync(Guid id)
        {
            const string sql = @"SELECT * FROM [Publication] WHERE Id = @Id"; 
            using var connection = await _dbContext.CreateConnectionAsync();
            var result = await connection.QuerySingleOrDefaultAsync<Domain.Models.Publication>(sql, new { Id = id });
            return result;
        }
    }
}
