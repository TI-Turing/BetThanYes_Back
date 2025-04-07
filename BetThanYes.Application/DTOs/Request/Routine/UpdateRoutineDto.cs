using System;

namespace BetThanYes.Application.DTOs.Request.Routine
{
    public class UpdateRoutineResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
