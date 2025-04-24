using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.DTOs.Response.Auth;
using BetThanYes.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace BetThanYes.Functions.Functions.Auth
{
    public class PasswordFunction
    {
        private readonly ILogger<PasswordFunction> _logger;
        private readonly IMailService _mailService;

        public PasswordFunction(ILogger<PasswordFunction> logger, IMailService mailService)
        {
            _logger = logger;
            _mailService = mailService;
        }

        [Function("ResetPassword")]
        public async Task<ApiResponse<ResetPasswordResponse>> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            try
            {
                _logger.LogInformation("C# HTTP trigger function processed a request.");
                var response = new ApiResponse<ResetPasswordResponse>();
                var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
                var email = query["Email"];

                response.Data = new ResetPasswordResponse();
                response.Data.Result = await _mailService.SendEmailAsync(email);

                _logger.LogInformation("Resultado del envio de correo: " + response.Data.Result);

                if (response.Data.Result == null || response.Data.Result == false)
                {
                    _logger.LogError("El correo no fue enviado: " + response.Data.Result);
                    response.Success = false;
                    response.Message = "Error al enviar el correo.";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }

                response.Success = true;
                response.Message = "Envio de correo exitoso.";
                response.StatusCode = StatusCodes.Status200OK;
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Ejecucion fallida. " + ex.Message);
                var response = new ApiResponse<ResetPasswordResponse>();
                response.Success = false;
                response.Message = ex.Message;
                response.StatusCode = StatusCodes.Status500InternalServerError;
                return response;
            }
        }
    }
}
