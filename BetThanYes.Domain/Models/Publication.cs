using System;
using BetThanYes.Domain.DTOs.Request.Publication;

namespace BetThanYes.Domain.Models
{
    public class Publication
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }  // Clave foránea
        public int CategoryId { get; set; }  // Clave foránea


    }
}
 