using BetThanYes.Domain.DTOs.Request.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services.Interfaces
{
    public interface ITaskService
    {
        Task<Guid> AddAsync(CreateTaskDto objTask);
        Task<List<Domain.Models.Task>> GetAsync();
    }
}
