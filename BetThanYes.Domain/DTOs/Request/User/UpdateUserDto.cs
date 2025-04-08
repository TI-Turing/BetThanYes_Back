using System;

namespace BetThanYes.Domain.DTOs.Request.User
{
    public class UpdateUserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime? BirthDate { get; set; } 
        public string? Gender { get; set; }
    }
}
