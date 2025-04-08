using BetThanYes.Domain.DTOs.Response.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ValidateEmailResponse> ValidateEmail(string email);
    }
}
