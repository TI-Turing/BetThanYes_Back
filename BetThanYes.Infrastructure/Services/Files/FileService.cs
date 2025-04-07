using BetThanYes.Domain.Enums;
using BetThanYes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BetThanYes.Infrastructure.Services.Files
{
    public class FileRepository : IFileRepository
    {
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
    }
}
