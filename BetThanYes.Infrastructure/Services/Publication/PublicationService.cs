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

        public async Task<Guid> AddAsync(CreatePublicationDto objPublication)
        {
            const string sql = @"
                INSERT INTO [Publication] (
                     Id, Title, Body, CreatedDate, UserId, CategoryId
                )
                VALUES (
                     @Id, @Title, @Body, @CreatedDate, @UserId, @CategoryId
                );
            ";

            using var connection = await _dbContext.CreateConnectionAsync();
            var newId = Guid.NewGuid();

            var parameters = new
            {
                Id = newId,
                Title = objPublication.Title,
                Body = objPublication.Body,
                CreatedDate = objPublication.CreatedDate,
                UserId = objPublication.UserId,
                CategoryId = objPublication.CategoryId
            };

            await connection.ExecuteAsync(sql, parameters);
            return newId;
        }



        public async Task<List<Domain.Models.Publication>> GetAsync()
        {
            const string sql = @"
                SELECT * FROM [Publication]
                    
            ";

            using var connection = await _dbContext.CreateConnectionAsync();


            /*var parameters = new
            {
                Id = newId,
                Ttitle = objPublication.Title,
                Body = objPublication.Body,
                CreatedDate = objPublication.CreatedDate,
                UserId = objPublication.UserId,
                CategoryId = objPublication.CategoryId
            };
*/

            var objResult = await connection.QueryAsync<Domain.Models.Publication>(sql);
            return objResult.ToList();
        }


        public async Task<Domain.Models.Publication?> GetByIdAsync(Guid id)
        {
            const string sql = @"SELECT * FROM [Publication] WHERE Id = @Id";
            using var connection = await _dbContext.CreateConnectionAsync();
            var result = await connection.QuerySingleOrDefaultAsync<Domain.Models.Publication>(sql, new { Id = id });
            return result;
        }
        // Este método actualiza una publicación existente en la base de datos
        // y devuelve un valor booleano que indica si la actualización fue exitosa.
        public async Task<bool> UpdateAsync(UpdatePublicationDto request)
        {
            const string sql = @"
                UPDATE [Publication]
                SET Title = @Title, Body = @Body, UpdatedDate = @UpdatedDate, CategoryId = @CategoryId
                WHERE Id = @Id
            ";

            using var connection = await _dbContext.CreateConnectionAsync();
            var parameters = new
            {
                Id = request.Id,
                Title = request.Title,
                Body = request.Body,
                UpdatedDate = request.UpdatedDate,
                CategoryId = request.CategoryId
            };

            var rowsAffected = await connection.ExecuteAsync(sql, parameters);
            return rowsAffected > 0;
        }
    }
}
