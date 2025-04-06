using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using BetThanYes.Application.Services.Interfaces;
using Microsoft.Azure.Functions.Worker.Http;
using BetThanYes.Application.DTOs.Routine;


namespace BetThanYes.Functions
{
    public class RoutinesFunction
    {
        private readonly IRoutineService _routineService;

        public RoutinesFunction(IRoutineService routineService)
        {
            _routineService = routineService;
        }

        [Function("CreateRoutine")]
        public async Task<IActionResult> CreateRoutine(
            [HttpTrigger(AuthorizationLevel.Function,  "post")] HttpRequestData req, FunctionContext executionContext)
        {
            try
            {
                var requestBody = await req.ReadFromJsonAsync<CreateRoutineDto>();

                if (requestBody == null)
                    return new BadRequestObjectResult("Invalid request.");

                var result = await _routineService.CreateAsync(requestBody);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }

        // Puedes agregar aquí GetAll, GetById, Update, Delete si quieres que avancemos con eso.
    }
}
