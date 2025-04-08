using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.DTOs.Request.Auth;
using BetThanYes.Domain.DTOs.Request.Login;
using BetThanYes.Domain.DTOs.Response.Login;
using BetThanYes.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace BetThanYes.Functions.Functions.Login
{
    public class LoginFunction
    {
        private readonly ILogger<LoginFunction> _logger;
        private readonly IAuthService _authService;
        private readonly IPasswordService _passwordService;
        private readonly ILoginService _loginService;

        public LoginFunction(ILogger<LoginFunction> logger, IAuthService authService, IPasswordService passwordService, ILoginService loginService)
        {
            _logger = logger;
            _authService = authService;
            _passwordService = passwordService;
            _loginService = loginService;
        }

        [Function("LoginFunction")]
        public async Task<ApiResponse<LoginResponse>> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var response = new ApiResponse<LoginResponse>();

            try
            {
                var requestBody = await req.ReadFromJsonAsync<LoginDto>();

                if (requestBody == null)
                {
                    response.Success = false;
                    response.Message = "Solicitud inválida.";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }
                User user = await _loginService.GetUserByEmail(requestBody.Email);
                string passwordHash = user.PasswordHash;

                bool isValid = _passwordService.VerifyPassword(requestBody.Password, passwordHash);
                if (isValid)
                {
                    var result = await _authService.GetNewToken(user.Id, requestBody.Email, 1);
                    var refreshToken = await _authService.GenerateRefreshTokenAsync();

                    req.Headers.TryGetValues("X-Device-Id", out var deviceIdValues);
                    var deviceId = deviceIdValues?.FirstOrDefault() ?? Guid.NewGuid().ToString();

                    req.Headers.TryGetValues("X-Device-Name", out var deviceNameValues);
                    var deviceName = deviceNameValues?.FirstOrDefault() ?? "Unknown device";

                    req.Headers.TryGetValues("X-Forwarded-For", out var ipAddressValue);
                    var ipAddress = ipAddressValue?.FirstOrDefault() ?? "Unknown Ip";


                    await _authService.SaveRefreshTokenAsync(new RefreshTokenDto
                    {
                        UserId = user.Id,
                        RefreshToken = refreshToken,
                        ExpirationDate = DateTime.UtcNow.AddDays(7),
                        DeviceId = deviceId,
                        DeviceName = deviceName,
                        IPAddress = ipAddress
                    });
                    result.RefreshToken = refreshToken;
                    response.Data = result;
                    response.Success = true;
                    response.Message = "Inicio de sesión exitoso.";
                    response.StatusCode = StatusCodes.Status200OK;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Credenciales incorrectas.";
                    response.StatusCode = StatusCodes.Status401Unauthorized;
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el login.");
                response.Success = false;
                response.Message = "Ha ocurrido un error inesperado.";
                response.StatusCode = StatusCodes.Status500InternalServerError;
                return response;
            }

        }
    }
}
