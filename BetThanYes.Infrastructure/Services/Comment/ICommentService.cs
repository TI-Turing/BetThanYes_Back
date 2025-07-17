using BetThanYes.Domain.DTOs.Request.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Infrastructure.Services.Comment
{
    public interface ICommentRepository
    {
        public Task<Guid> AddAsync(CreateCommentDto createCommentDto);
    }
}
