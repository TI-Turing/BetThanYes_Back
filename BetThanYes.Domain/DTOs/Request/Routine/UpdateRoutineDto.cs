using System;

namespace BetThanYes.Domain.DTOs.Request.Routine
{
    public class UpdateRoutineResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
