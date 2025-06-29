using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Domain.DTOs.Request.Task
{
    public class CreateTaskDto
    {
        
        public string Title { get; set; }
        public TimeSpan Hour { get; set; }
        public Guid RoutineId { get; set; }

    }

}

