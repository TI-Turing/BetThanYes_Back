using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace BetThanYes.Functions.Functions.Auth
{
    public class PasswordFunction
    {
        private readonly ILogger<PasswordFunction> _logger;

        public PasswordFunction(ILogger<PasswordFunction> logger)
        {
            _logger = logger;
        }

        [Function("PasswordFunction")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
