using BetThanYes.Domain.DTOs.Request.Comments;
using BetThanYes.Infrastructure.Database;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BetThanYes.Infrastructure.Services.Comment
{
    public class CommentRepository : ICommentRepository
    {
        private readonly SqlDbContext _dbContext;

        public CommentRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public async Task<Guid> AddAsync(CreateCommentDto createCommentDto)
        {
            const string sql = @"
                INSERT INTO [Comment] (
                     Id,Body,UserId,CreatedAt, PublicationId 
)
                VALUES (
                    @Id, @Body, @UserId, @CreatedAt, @PublicationId
                );
            ";

            using var connection = await _dbContext.CreateConnectionAsync();
            var newId = Guid.NewGuid();
            var parameters = new
            {
                Id = newId,
                Body = createCommentDto.Body,
                UserId = createCommentDto.UserId,
                CreatedAT = createCommentDto.CreatedAt,
                PublicationId = createCommentDto.PublicationId,
            };



            await connection.ExecuteAsync(sql, parameters);
            return newId;
        }
    }

}
