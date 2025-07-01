using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.DTOs.Request.Publication;
using BetThanYes.Domain.Models;
using BetThanYes.Domain.DTOs.Response.Publication;
using Newtonsoft.Json;

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

            try //Intenta
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var objRequest = JsonConvert.DeserializeObject<CreatePublicationDto>(requestBody);

                // if (requestBody == null)
                // {
                //     objResponse.Success = false;
                //     objResponse.Message = "Solicitud inv�lida.";
                //     objResponse.StatusCode = StatusCodes.Status400BadRequest;
                //     return objResponse;
                // }

                var objResult = await _publicationService.AddAsync(objRequest);//Inserta en Base de Datos

                objResponse.Data = new PublicationResponse();
                objResponse.Data.Id = objResult;
                objResponse.Success = true;
                objResponse.Message = "Publicación creada exitosamente.";
                objResponse.StatusCode = StatusCodes.Status200OK;

                return objResponse;
            }
            catch (Exception ex) //Capturar
            {
                objResponse.Success = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = StatusCodes.Status500InternalServerError;
                return objResponse;
            }
        }



        [Function("GetAllPublication")]
        public async Task<ApiResponse<List<Domain.Models.Publication>>> GetPublication(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            var objResponse = new ApiResponse<List<Domain.Models.Publication>>();

            try //Intenta
            {
                //    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                //var objRequest = JsonConvert.DeserializeObject<GetPublicationDto>(requestBody);

                // if (requestBody == null)
                // {
                //     objResponse.Success = false;
                //     objResponse.Message = "Solicitud inv�lida.";
                //     objResponse.StatusCode = StatusCodes.Status400BadRequest;
                //     return objResponse;
                // }

                var objResult = await _publicationService.GetAsync();//Lee en Base de Datos

                objResponse.Data = objResult;
                //  objResponse.Data.Id = objResult;
                objResponse.Success = true;
                objResponse.Message = "OK";
                objResponse.StatusCode = StatusCodes.Status200OK;

                return objResponse;
            }
            catch (Exception ex) //Capturar
            {
                objResponse.Success = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = StatusCodes.Status500InternalServerError;
                return objResponse;
            }
        }

        // Lee en Base de Datos por ID
        [Function("GetPublicationById")]
        public async Task<ApiResponse<Domain.Models.Publication>> GetPublicationById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "publication/{id:guid}")] HttpRequestData req,
            Guid id)
        {
            var objResponse = new ApiResponse<Domain.Models.Publication>();

            try
            {
                var publication = await _publicationService.GetByIdAsync(id);

                if (publication == null)
                {
                    objResponse.Success = false;
                    objResponse.Message = "Publicación no encontrada.";
                    objResponse.StatusCode = StatusCodes.Status404NotFound;
                    return objResponse;
                }

                objResponse.Data = publication;
                objResponse.Success = true;
                objResponse.Message = "OK";
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

