using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BetThanYes.Domain.DTOs.Request.Publication
{
    public class UpdatePublicationDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime UpdatedDate { get; set; }
        public Guid CategoryId { get; set; }
    }
}