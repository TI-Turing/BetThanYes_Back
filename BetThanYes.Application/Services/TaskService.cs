using BetThanYes.Domain.DTOs.Request.Task;
using BetThanYes.Infrastructure.Services.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services
{
    public class TaskService:Interfaces.ITaskService

    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public Task<Guid> AddAsync(CreateTaskDto objTask)
        {
                return _taskRepository.AddAsync(objTask);
        }
        public Task<List<Domain.Models.Task>> GetAsync()
        {
            return _taskRepository.GetAsync();
        }
    }
}
