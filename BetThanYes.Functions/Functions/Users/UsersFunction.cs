using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Application.DTOs.User;
using Microsoft.Azure.Functions.Worker.Http;


namespace BetThanYes.Functions
{
    public class UsersFunction
    {
        private readonly IUserService _UserService;

        public UsersFunction(IUserService UserService)
        {
            _UserService = UserService;
        }

        [Function("CreateUser")]
        public async Task<IActionResult> CreateUser(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req, FunctionContext executionContext)
        {
            try
            {
                var requestBody = await req.ReadFromJsonAsync<CreateUserDto>();

                if (requestBody == null)
                    return new BadRequestObjectResult("Invalid request.");

                var result = await _UserService.CreateAsync(requestBody);
                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }

        // Puedes agregar aquí GetAll, GetById, Update, Delete si quieres que avancemos con eso.
    }
}
