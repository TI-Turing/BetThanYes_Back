using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Application.DTOs.Request.User;
using BetThanYes.Domain.Models;
using BetThanYes.Application.DTOs.Response.User;

namespace BetThanYes.Functions.Functions.Users
{
    public class UsersFunction
    {
        private readonly IUserService _userService;

        public UsersFunction(IUserService userService)
        {
            _userService = userService;
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
    }
}
