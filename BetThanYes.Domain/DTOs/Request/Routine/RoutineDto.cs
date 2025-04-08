using System;

namespace BetThanYes.Domain.DTOs.Request.Routine
{
    public class  RoutineResponse
    {
        public Guid Id { get; set; }
        public long RoutineNumber { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
