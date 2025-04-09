using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetThanYes.Domain.Models;
using Newtonsoft.Json.Linq;
using BetThanYes.Infrastructure.Services.Mail;
using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.DTOs.Request.Auth;
using System.Net;

namespace BetThanYes.Application.Services
{
    public class MailService : IMailService
    {
        private readonly IMailRepository _mailRepository;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public MailService(IMailRepository mailRepository, IUserService userService, IAuthService authService)
        {
            _mailRepository = mailRepository;
            _userService = userService;
            _authService = authService;
        }

        public async Task<bool> SendEmailAsync(string toEmail)
        {
            var token = Guid.NewGuid().ToString();
            User user = await _userService.GetUserByEmail(toEmail);
            await _authService.SaveRefreshTokenAsync(new RefreshTokenDto
            {
                UserId = user.Id,
                RefreshToken = token,
                ExpirationDate = DateTime.UtcNow.AddDays(7),
                DeviceId = "",
                DeviceName = "",
                IPAddress = ""
            });
            var resetLink = $"https://betthanyes.com/reset-password?token={token}";
            var emailBody = $"<p>Hola,</p><p>Haz clic en el siguiente enlace para restablecer tu contraseña:</p><a href='{resetLink}'>{resetLink}</a>";

            
            return await _mailRepository.SendEmailAsync(toEmail, "Recupera tu contraseña", emailBody);
        }

    }
}
