using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.DTOs.Request.Schedule;
using BetThanYes.Domain.Models;
using BetThanYes.Domain.DTOs.Response.Schedule;

namespace BetThanYes.Functions.Functions.Schedule
{
    public class ScheduleFunction
    {
        private readonly IScheduleService _scheduleService;
        private readonly ILogger<ScheduleFunction> _logger; //Control de logs

        public ScheduleFunction(IScheduleService scheduleService, ILogger<ScheduleFunction> logger)
        {
            _scheduleService = scheduleService;
            _logger = logger;
        }

        [Function("CreateSchedule")]
        public async Task<ApiResponse<CreateScheduleResponse>> CreateSchedule(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {

            ApiResponse<CreateScheduleResponse> objResponse = new ApiResponse<CreateScheduleResponse>();

            try
            {
                var requestBody = await req.ReadFromJsonAsync<CreateScheduleDto>();

                // if (requestBody == null)
                // {
                //     objResponse.Success = false;
                //     objResponse.Message = "Solicitud invalida.";
                //     objResponse.StatusCode = StatusCodes.Status400BadRequest;
                //     return objResponse;
                // }

                
                var objResult = await _scheduleService.CreateAsync(requestBody); //Inserta en Base de datos

                objResponse.Data = new CreateScheduleResponse();
                objResponse.Data.Id = objResult.Id;
                objResponse.Success = true;
                objResponse.Message = "Horario creado exitosamente.";
                objResponse.StatusCode = StatusCodes.Status200OK;

                return objResponse;
            }
            catch (Exception ex)
            {
                objResponse.Success = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = StatusCodes.Status500InternalServerError;
                return objResponse;
            }
        }
    }
}
