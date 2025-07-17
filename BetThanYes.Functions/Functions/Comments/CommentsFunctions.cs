using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using BetThanYes.Domain.DTOs.Request.Comments;
using BetThanYes.Domain.Models;
using Newtonsoft.Json;
using BetThanYes.Application.Services.Interfaces;
using Microsoft.Azure.Functions.Worker.Http;

namespace BetThanYes.Functions.Functions.Comments;

public class CommentsFunctions
{
    private readonly ICommentService _commentService;
    private readonly ILogger<CommentsFunctions> _logger;

    public CommentsFunctions(ILogger<CommentsFunctions> logger, ICommentService commentService)
    {
        _logger = logger;
        _commentService = commentService;
    }

    [Function("AddCommentsFunctions")]
    public async Task<IActionResult> AddAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        ApiResponse<Guid> ObjResponse = new ApiResponse<Guid>();
        try
        {
            //
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            CreateCommentDto createCommentDto = JsonConvert.DeserializeObject<CreateCommentDto>(requestBody);

            var result = await _commentService.AddAsync(createCommentDto);

            ObjResponse.Data = result;
            return new OkObjectResult(ObjResponse);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request.");
            ObjResponse.Success = false;
            ObjResponse.Message = "An error occurred while processing the request.";
            ObjResponse.StatusCode = StatusCodes.Status500InternalServerError;
            return new BadRequestObjectResult(ObjResponse);
        }



        return new OkObjectResult("Welcome to Azure Functions!");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
    [Function("ReadCommentsFunctions")]
    public IActionResult Read([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
    [Function("UpdateCommentsFunctions")]
    public IActionResult Update([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
    [Function("DeleteCommentsFunctions")]
    public IActionResult Delete([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}