using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.DTOs.Request.Peticion;
using BetThanYes.Domain.Models;
using BetThanYes.Domain.DTOs.Response.Peticion;
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
        
        

        [Function("UpdatePeticion")]
        public async Task<ApiResponse<UpdatePeticionResponse>> UpdateRoutine(
            [HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
        {
            var response = new ApiResponse<UpdatePeticionResponse>();
            _logger.LogInformation("C# HTTP trigger function processed an update request.");
            try
            {
                var requestBody = await req.ReadFromJsonAsync<UpdatePeticionDto>();

                if (requestBody == null)
                {
                    response.Success = false;
                    response.Message = "Solicitud inválida.";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }

                var routineResponse = await _peticionService.UpdateAsync(requestBody);

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
        
        
        [Function("DeletePeticion")]
        public async Task<ApiResponse<DeletePeticionResponse>> DeletePeticion(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "peticiones/{id:guid}")] HttpRequestData req, Guid id)
        {
            var response = new ApiResponse<DeletePeticionResponse>();
            _logger.LogInformation("C# HTTP trigger function processed a delete request.");
            try
            {
                // Llamar al servicio para eliminar la rutina
                await _routineService.DeleteAsync(id);
                DeletePeticionResponse peticionResponse = new DeletePeticionResponse
                {
                    Success = true,
                };
                response.Data = peticionResponse;
                response.Success = true;
                response.Message = "Peticion eliminada exitosamente.";
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
        
        
        [Function("GetAllPeticionesByUser")]
        public async Task<ApiResponse<IEnumerable<Peticion>>> GetAllPeticionesByUser(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "peticiones")] HttpRequestData req)
        {
            var response = new ApiPeticion<IEnumerable<Peticion>>();
            _logger.LogInformation("C# HTTP trigger function processed a get all peticiones request.");
            try
            {
                var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
                Guid userId = new Guid(query["Id"] ?? "");
                // Llamar al servicio para obtener todas las peticiones
                var routines = await _routineService.GetAllByUserIdAsync(userId); // Cambia Guid.Empty si necesitas un filtro específico

                response.Data = peticiones;
                response.Success = true;
                response.Message = "Peticiones obtenidas exitosamente.";
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
