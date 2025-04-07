using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Application.DTOs.Request.Routine;
using BetThanYes.Domain.Models;
using BetThanYes.Application.DTOs.Response.Routine;

namespace BetThanYes.Functions.Functions.Routines
{
    public class RoutinesFunction
    {
        private readonly IRoutineService _routineService;

        public RoutinesFunction(IRoutineService routineService)
        {
            _routineService = routineService;
        }

        [Function("CreateRoutine")]
        public async Task<ApiResponse<CreateRoutineResponse>> CreateRoutine(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var response = new ApiResponse<CreateRoutineResponse>();

            try
            {
                var requestBody = await req.ReadFromJsonAsync<CreateRoutineDto>();

                if (requestBody == null)
                {
                    response.Success = false;
                    response.Message = "Solicitud inválida.";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }

                var result = await _routineService.CreateAsync(requestBody);

                response.Data = new CreateRoutineResponse();
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
