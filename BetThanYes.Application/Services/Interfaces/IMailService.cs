using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services.Interfaces
{
    public interface IMailService
    {
        Task<bool> SendEmailAsync(string toEmail);
    }
}
