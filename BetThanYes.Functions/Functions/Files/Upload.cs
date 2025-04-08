using BetThanYes.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BetThanYes.Application.Services.Interfaces;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using BetThanYes.Domain.Models;
using BetThanYes.Domain.DTOs.Request.File;
using BetThanYes.Domain.DTOs.File;


namespace BetThanYes.Functions.Functions.Files
{
    public class Upload
    {
        private readonly IFileService _fileService;        

        public Upload(IFileService fileService)
        {
            _fileService = fileService;
        }

        [Function("Upload")]
        public async Task<ApiResponse<UploadFileResponse>> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            ApiResponse<UploadFileResponse> objResponse = new ApiResponse<UploadFileResponse>();
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<UploadFileDto>(requestBody);

                if (string.IsNullOrWhiteSpace(data.Base64Content) || string.IsNullOrWhiteSpace(data.FileName) || data.IdUser == Guid.Empty || string.IsNullOrEmpty(data.FileType.ToString()))
                {
                    objResponse.Success = false;
                    objResponse.Message = "Faltan campos obligatorios.";
                    objResponse.StatusCode = StatusCodes.Status400BadRequest;
                    objResponse.Data = null;
                    return objResponse;
                }

                objResponse.Data = new UploadFileResponse();
                objResponse.Data.Uri = await _fileService.ProcessFile(data);
                data.ProfilePictureUrl = objResponse.Data.Uri;
                objResponse.Data.Result = await _fileService.SaveFile(data);
                objResponse.Success = true;
                objResponse.Message = "Archivo subido correctamente.";
                objResponse.StatusCode = StatusCodes.Status200OK;
                
                return objResponse;
            }
            catch(Exception ex)
            {
                objResponse.Success = false;
                objResponse.Message = ex.Message;
                objResponse.StatusCode = StatusCodes.Status500InternalServerError;
                objResponse.Data = null;
                return objResponse;
            }
        }
    }
}
