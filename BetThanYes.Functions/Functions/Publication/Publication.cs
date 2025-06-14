using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.DTOs.Request.Publication;
using BetThanYes.Domain.Models;
using BetThanYes.Domain.DTOs.Response.Publication;

namespace BetThanYes.Functions.Functions.Publication
{
    public class PublicationFunction
    {
        private readonly IPublicationService _publicationService;
        private readonly ILogger<PublicationFunction> _logger; //Control de logs

        public PublicationFunction(IPublicationService publicationService, ILogger<PublicationFunction> logger)
        {
            _publicationService = publicationService;
            _logger = logger;

        }

        [Function("CreatePublication")]

        public async Task<ApiResponse<PublicationResponse>> CreatePublication(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var objResponse = new ApiResponse<PublicationResponse>();

            try
            {
                var requestBody = await req.ReadFromJsonAsync<PublicationDto>();

                // if (requestBody == null)
                // {
                //     objResponse.Success = false;
                //     objResponse.Message = "Solicitud inv�lida.";
                //     objResponse.StatusCode = StatusCodes.Status400BadRequest;
                //     return objResponse;
                // }

                var objResult = await _publicationService.CreateAsync(requestBody);//Inserta en Base de Datos

                objResponse.Data = new PublicationResponse();
                objResponse.Data.Id = objResult.Id;
                objResponse.Success = true;
                objResponse.Message = "Publicación creada exitosamente.";
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
