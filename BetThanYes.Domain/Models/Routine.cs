using System;

namespace BetThanYes.Domain.Models
{
    public class Routine
    {
        public Guid Id { get; set; }
        public long RoutineNumber { get; set; }
        public Guid UserId { get; set; }
        public string RoutineName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsDefault { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}
