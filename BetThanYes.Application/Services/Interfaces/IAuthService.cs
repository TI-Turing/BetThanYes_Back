using BetThanYes.Domain.DTOs.Request.Auth;
using BetThanYes.Domain.DTOs.Response.Auth;
using BetThanYes.Domain.DTOs.Response.Login;
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
        Task SaveRefreshTokenAsync(RefreshTokenDto dto);
        Task<string> GenerateRefreshTokenAsync();
        Task<LoginResponse> GetNewToken(Guid id, string email, int tokenType);
    }
}
