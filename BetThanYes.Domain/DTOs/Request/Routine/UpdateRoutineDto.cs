using System;

namespace BetThanYes.Domain.DTOs.Request.Routine
{
    public class UpdateRoutineDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? IsDefault { get; set; }
    }
}
