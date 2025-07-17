using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.DTOs.Request.Comments;
using BetThanYes.Infrastructure.Services.Comment;

namespace BetThanYes.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentService;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentService = commentRepository;
        }
        public async Task<Guid> AddAsync(CreateCommentDto createCommentDto)
        {
            return await _commentService.AddAsync(createCommentDto);
        }

    }
}
