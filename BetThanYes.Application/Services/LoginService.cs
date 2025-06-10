using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.Models;
using BetThanYes.Infrastructure.Services.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAuthRepository _authRepository;
        public LoginService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _authRepository.GetByEmail(email);
            
            return user;
        }
    }
}
 |