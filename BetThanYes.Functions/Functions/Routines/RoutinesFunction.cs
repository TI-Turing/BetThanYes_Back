using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.DTOs.Request.Routine;
using BetThanYes.Domain.Models;
using BetThanYes.Domain.DTOs.Response.Routine;
using BetThanYes.Functions.Functions.Login;
using Microsoft.Extensions.Logging;

namespace BetThanYes.Functions.Functions.Routines
{
    public class RoutinesFunction
    {
        private readonly IRoutineService _routineService;
        private readonly ILogger<LoginFunction> _logger;

        public RoutinesFunction(ILogger<LoginFunction> logger, IRoutineService routineService)
        {
            _routineService = routineService;
            _logger = logger;
        }

        [Function("CreateRoutine")]
        public async Task<ApiResponse<CreateRoutineResponse>> CreateRoutine(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var response = new ApiResponse<CreateRoutineResponse>();
            _logger.LogInformation("C# HTTP trigger function processed a request.");
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
        
        

        [Function("UpdateRoutine")]
        public async Task<ApiResponse<UpdateRoutineResponse>> UpdateRoutine(
            [HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
        {
            var response = new ApiResponse<UpdateRoutineResponse>();
            _logger.LogInformation("C# HTTP trigger function processed an update request.");
            try
            {
                var requestBody = await req.ReadFromJsonAsync<UpdateRoutineDto>();

                if (requestBody == null)
                {
                    response.Success = false;
                    response.Message = "Solicitud inválida.";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }

                var routineResponse = await _routineService.UpdateAsync(requestBody);

                response.Data = routineResponse;
                response.Success = true;
                response.Message = "Rutina actualizada exitosamente.";
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
        
        
        [Function("DeleteRoutine")]
        public async Task<ApiResponse<DeleteRoutineResponse>> DeleteRoutine(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "routines/{id:guid}")] HttpRequestData req, Guid id)
        {
            var response = new ApiResponse<DeleteRoutineResponse>();
            _logger.LogInformation("C# HTTP trigger function processed a delete request.");
            try
            {
                // Llamar al servicio para eliminar la rutina
                await _routineService.DeleteAsync(id);
                DeleteRoutineResponse routineResponse = new DeleteRoutineResponse
                {
                    Success = true,
                };
                response.Data = routineResponse;
                response.Success = true;
                response.Message = "Rutina eliminada exitosamente.";
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
        
        
        [Function("GetAllRoutinesByUser")]
        public async Task<ApiResponse<IEnumerable<Routine>>> GetAllRoutinesByUser(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "routines")] HttpRequestData req)
        {
            var response = new ApiResponse<IEnumerable<Routine>>();
            _logger.LogInformation("C# HTTP trigger function processed a get all routines request.");
            try
            {
                var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
                Guid userId = new Guid(query["Id"] ?? "");
                // Llamar al servicio para obtener todas las rutinas
                var routines = await _routineService.GetAllByUserIdAsync(userId); // Cambia Guid.Empty si necesitas un filtro específico

                response.Data = routines;
                response.Success = true;
                response.Message = "Rutinas obtenidas exitosamente.";
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
