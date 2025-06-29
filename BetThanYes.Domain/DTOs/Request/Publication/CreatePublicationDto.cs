using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Domain.DTOs.Request.Publication
{
    public class CreatePublicationDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserId { get; set; } 
        public int CategoryId { get; set; }
    }
}
    