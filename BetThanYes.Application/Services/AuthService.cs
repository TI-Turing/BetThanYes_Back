using BetThanYes.Domain.DTOs.Response.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Infrastructure.Services.Auth;

namespace BetThanYes.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _AuthRepository;
        public AuthService(IAuthRepository AuthRepository)
        {
            _AuthRepository = AuthRepository;
        }

        public async Task<ValidateEmailResponse> ValidateEmail(string email)
        {
            var user = await _AuthRepository.GetByEmail(email);
            var response = new ValidateEmailResponse();
            if (user != null)
            {
                response.Result = true;
                return response;
            }
            response.Result = false;
            return response;
        }
    }
}
