using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Domain.DTOs.Request.Auth
{
    public class RefreshTokenDto
    {
        public Guid UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string DeviceId { get; set; }
        public string? DeviceName { get; set; }
        public string? IPAddress { get; set; }
    }
}
