using System;

namespace BetThanYes.Application.DTOs.Request.Routine
{
    public class CreateRoutineDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
    }
}
