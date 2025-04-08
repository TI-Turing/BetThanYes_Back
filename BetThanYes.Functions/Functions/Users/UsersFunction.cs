using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.DTOs.Request.User;
using BetThanYes.Domain.Models;
using BetThanYes.Domain.DTOs.Response.User;

namespace BetThanYes.Functions.Functions.Users
{
    public class UsersFunction
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersFunction> _logger;
        private readonly IAuthService _authService;
        private readonly IPasswordService _passwordService;

        public UsersFunction(IUserService userService, ILogger<UsersFunction> logger, IAuthService authService, IPasswordService passwordService)
        {
            _userService = userService;
            _logger = logger;
            _authService = authService;
            _passwordService = passwordService;
        }

        [Function("CreateUser")]
        public async Task<ApiResponse<CreateUserResponse>> CreateUser(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var response = new ApiResponse<CreateUserResponse>();

            try
            {
                var requestBody = await req.ReadFromJsonAsync<CreateUserDto>();

                if (requestBody == null)
                {
                    response.Success = false;
                    response.Message = "Solicitud inválida.";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }
                requestBody.Password = _passwordService.HashPassword(requestBody.Password);
                var result = await _userService.CreateAsync(requestBody);

                response.Data = new CreateUserResponse();
                response.Data.Id = result.Id;
                response.Success = true;
                response.Message = "Usuario creado exitosamente.";
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

        [Function("UpdateUser")]
        public async Task<ApiResponse<UpdateUserResponse>> UpdateUser(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            var response = new ApiResponse<UpdateUserResponse>();

            try
            {
                var requestBody = await req.ReadFromJsonAsync<UpdateUserDto>();

                if (requestBody == null)
                {
                    response.Success = false;
                    response.Message = "Solicitud inválida.";
                    response.StatusCode = StatusCodes.Status400BadRequest;
                    return response;
                }

                var result = await _userService.UpdateAsync(requestBody);

                response.Data = new UpdateUserResponse();
                response.Data.Result = result.Result;
                response.Success = true;
                response.Message = "Usuario actualizado exitosamente.";
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
