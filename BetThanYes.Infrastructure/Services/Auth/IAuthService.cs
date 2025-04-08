using BetThanYes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Infrastructure.Services.Auth
{
    public interface IAuthRepository
    {
        Task<User> GetByEmail(string email);
        //Task<bool> ValidatePhone(string phone);
        //Task<bool> ValidateUserName(string userName);
        //Task<bool> ValidatePassword(string password);
        //Task<bool> ValidateUser(string userName, string password);
        //Task<string> GenerateToken(string userName, string password);
        //Task<string> GenerateRefreshToken();
        //Task<bool> ValidateToken(string token);
        //Task<bool> RevokeToken(string token);
    }
}
