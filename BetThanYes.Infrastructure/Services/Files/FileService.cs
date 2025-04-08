using BetThanYes.Domain.DTOs.Request.File;
using BetThanYes.Domain.Enums;
using BetThanYes.Domain.Models;
using BetThanYes.Infrastructure.Database;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BetThanYes.Infrastructure.Services.Files
{
    public class FileRepository : IFileRepository
    {
        private readonly SqlDbContext _dbContext;
        public FileRepository(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<string> ProcessFile(byte[] fileBytes, string userId, string fileType, string fileExtension, string tempFolder)
        {
            try
            {
                var fileName = $"{userId}_{fileType}{fileExtension}";
                var filePath = Path.Combine(tempFolder, fileName);

                await File.WriteAllBytesAsync(filePath, fileBytes);
                //// Conexión al blob
                //string connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
                //var blobClient = new BlobContainerClient(connectionString, containerName);
                //await blobClient.CreateIfNotExistsAsync();

                //var blob = blobClient.GetBlobClient(file.FileName);
                //using var stream = new MemoryStream(fileBytes);
                //await blob.UploadAsync(stream, overwrite: true);

                //return new OkObjectResult(new { Url = blob.Uri.ToString() });

                return filePath;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return "";
            }
        }

        public async Task<bool> SaveFile(UploadFileDto uploadFileDto)
        {
            try
            {
                const string sql = @"
                UPDATE [User]
                SET 
                    ProfilePictureUrl = @ProfilePictureUrl                    
                WHERE Id = @IdUser;
            ";

                using var connection = _dbContext.CreateConnection();
                await connection.ExecuteAsync(sql, uploadFileDto);
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                throw;
            }
        }
    }
}
