using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetThanYes.Domain.Enums;
using Microsoft.VisualBasic.FileIO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using BetThanYes.Domain.Enums;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Infrastructure.Services.Routines;
using BetThanYes.Infrastructure.Services.Files;
using BetThanYes.Domain.DTOs.Request.File;
using Azure.Storage.Blobs.Models;
//using BetThanYes.;

namespace BetThanYes.Application.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _repository;
        public FileService(IFileRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> ProcessFile(UploadFileDto file)
        {
            try
            {
                if (!Enum.TryParse<FileType>(file.FileType.ToString(), ignoreCase: true, out var fileType))
                {
                    return "";
                }

                if (file.Base64Content.StartsWith("data:"))
                {
                    int commaIndex = file.Base64Content.IndexOf(',');
                    if (commaIndex >= 0)
                    {
                        file.Base64Content = file.Base64Content.Substring(commaIndex + 1);
                    }
                }

                file.Base64Content = file.Base64Content.Replace("\n", "").Replace("\r", "").Replace(" ", "");

                // Decodifica el contenido base64
                byte[] fileBytes = Convert.FromBase64String(file.Base64Content);
                Guid userId = file.IdUser;

                var tempFolder = Path.Combine(Directory.GetCurrentDirectory(), "tempUploads");
                Directory.CreateDirectory(tempFolder); // Asegura que la carpeta exista

                // Determina la extensión del archivo según el tipo
                string fileExtension = fileType switch
                {
                    FileType.ProfileImage => ".jpg", // o .png, etc.
                    FileType.PostImage => ".jpg", // o .png, etc.
                    FileType.Document => ".pdf",
                    _ => ".dat" // extensión por defecto para otros tipos
                };

                return await _repository.ProcessFile(fileBytes, userId.ToString(), fileType.ToString(), fileExtension, tempFolder);

                // Determina el contenedor según el tipo de archivo
                //string containerName = file.FileType switch
                //{
                //    FileType.ProfileImage => "profile-images",
                //    FileType.PostImage => "post-images",
                //    FileType.Document => "documents",
                //    _ => "others"
                //};




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
                return await _repository.SaveFile(uploadFileDto);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return false;
            }
        }

        public async Task<BlobDownloadInfo> GetBlobFile(string containerName, string blobName, string storageAccountUrl)
        {
            try
            {
                return await _repository.GetBlobFile(containerName, blobName, storageAccountUrl);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                throw new InvalidOperationException("Error al obtener el blob.", ex);
            }
        }

        public async Task<byte[]> ConvertBlobToBytes(BlobDownloadInfo blobFile)
        {
            try
            {
                using var memoryStream = new MemoryStream();
                await blobFile.Content.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                throw new InvalidOperationException("Error al convertir el blob en bytes.", ex);
            }
        }

    }
}
