using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Infrastructure.Services.Files
{
    public interface IFileRepository
    {
        Task<string> ProcessFile(byte[] fileBytes, string userId, string fileType, string fileExtension, string tempFolder);
    }
}
