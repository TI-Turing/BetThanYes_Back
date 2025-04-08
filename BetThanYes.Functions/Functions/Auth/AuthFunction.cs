using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.DTOs.Request.User;
using BetThanYes.Domain.DTOs.Response.Auth;
using BetThanYes.Domain.DTOs.Response.User;
using BetThanYes.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace BetThanYes.Functions.Functions.Auth
{
    public class AuthFunction
    {
        private readonly ILogger<AuthFunction> _logger;
        private readonly IAuthService _authService;

        public AuthFunction(ILogger<AuthFunction> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [Function("AuthFunction")]
        public async Task<ApiResponse<ValidateEmailResponse>> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var response = new ApiResponse<ValidateEmailResponse>();

            try
            {
                var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
                var email = query["email"];                

                if (string.IsNullOrEmpty(email))
                {
                    response.Success = false;
                    response.Message = "Email no puede estar vacío.";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }
                

                var result = await _authService.ValidateEmail(email);

                response.Data = new ValidateEmailResponse();
                response.Data.Result = result.Result;
                response.Success = true;
                response.Message = "Consulta realizada de manera exitosa.";
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
