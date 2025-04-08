using System;

namespace BetThanYes.Domain.DTOs.Request.Routine
{
    public class CreateRoutineDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}
