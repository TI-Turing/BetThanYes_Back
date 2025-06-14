using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.DTOs.Request.Task;
using BetThanYes.Domain.Models;
using BetThanYes.Domain.DTOs.Response.Task;
using BetThanYes.Functions.Functions.Login;
using Microsoft.Extensions.Logging;

namespace BetThanYes.Functions.Functions.Peticiones
{
    public class PeticionesFunction
    {
        private readonly IPeticionService _routineService;
        private readonly ILogger<LoginFunction> _logger;

        public PeticionesFunction(ILogger<LoginFunction> logger, IRoutineService routineService)
        {
            _peticionService = routineService;
            _logger = logger;
        }

        [Function("CreatePeticion")]
        public async Task<ApiResponse<CreatePeticionResponse>> CreateRoutine(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var response = new ApiResponse<CreatePeticionResponse>();
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                var requestBody = await req.ReadFromJsonAsync<CreatePeticionDto>();

                if (requestBody == null)
                {
                    response.Success = false;
                    response.Message = "Solicitud inválida.";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }

                var result = await _peticionService.CreateAsync(requestBody);

                response.Data = new CreatePeticionResponse();
                response.Data.Id = result.Id;
                response.Success = true;
                response.Message = "Rutina creada exitosamente.";
                response.StatusCode = StatusCodes.Status200OK;

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = StatusCodes.Status500InternalServerError;
                return response;
            }
        }
        
    }
}
