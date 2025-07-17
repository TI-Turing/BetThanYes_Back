using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Domain.DTOs.Request.Comments
{
    public class CreateCommentDto
    {
        public Guid PublicationId { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
