using BetThanYes.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Application.DTOs.Request.File
{
    public class UploadFileDto
    {
        public string FileName { get; set; } = string.Empty;
        public string Base64Content { get; set; } = string.Empty;
        public FileType FileType { get; set; }
        public Guid IdUser { get; set; }
    }
}
