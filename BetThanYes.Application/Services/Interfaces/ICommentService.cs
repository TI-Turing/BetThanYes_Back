using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetThanYes.Domain.DTOs.Request.Comments;

namespace BetThanYes.Application.Services.Interfaces
{
    public interface ICommentService
    {

        Task<Guid> AddAsync(CreateCommentDto createCommentDto);
    }
}
