using BetThanYes.Domain.DTOs.Request.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> ProcessFile(UploadFileDto file);
        Task<bool> SaveFile(UploadFileDto uploadFileDto);
    }
}
