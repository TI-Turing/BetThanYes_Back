using BetThanYes.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Application.DTOs.File
{
    public class UploadFileResponse
    {
        public string Uri { get; set; } = string.Empty;
        public bool Result { get; set; }

    }
}
