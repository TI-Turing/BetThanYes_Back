using System;

namespace BetThanYes.Domain.DTOs.Request.Task
{
    public class CreateTaskRequest
    {
        public string Tittle { get; set; }
        public TimeOnly Hour { get; set; }
        public int IdRoutine { get; set; }

    }
}
