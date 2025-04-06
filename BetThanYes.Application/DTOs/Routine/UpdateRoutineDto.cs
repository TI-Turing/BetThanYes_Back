using System;

namespace BetThanYes.Application.DTOs.Routine
{
    public class UpdateRoutineDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
