using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.DTOs.Request.Routine;
using BetThanYes.Domain.DTOs.Request.Task;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace BetThanYes.Functions.Functions.Task
{
    public class TaskFunction
    {
        private readonly ITaskService _taskService;
        private readonly ILogger<TaskFunction> _logger;

        public TaskFunction(ILogger<TaskFunction> logger,ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        [Function("TaskFunction")]
        public async Task<IActionResult> AddAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var requestBody = await req.ReadFromJsonAsync<CreateTaskDto>();
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            Guid id = await _taskService.AddAsync(requestBody);
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
