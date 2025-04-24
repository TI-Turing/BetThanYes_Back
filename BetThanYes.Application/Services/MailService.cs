
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetThanYes.Domain.Models;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<MailService> _logger;
        private readonly IFileService _fileService;
        

        public MailService(ILogger<MailService> logger, IMailRepository mailRepository, IUserService userService, IAuthService authService, IFileService fileService)
        {
            _mailRepository = mailRepository;
            _userService = userService;
            _authService = authService;
            _logger = logger;
            _fileService = fileService;
        }

        public async Task<bool> SendEmailAsync(string toEmail)
        {
            
            var token = Guid.NewGuid().ToString();
            User user = await _userService.GetUserByEmail(toEmail);

            string logoUrl = $"https://sabetthanyespublic.blob.core.windows.net/images/favicon.png";

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
            var emailBody = $"<!DOCTYPE html>\r\n<html lang=\"es\">\r\n<head>\r\n  <meta charset=\"UTF-8\">\r\n  <title>Recuperación de contraseña</title>\r\n  <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n</head>\r\n<body style=\"margin:0; padding:0; background-color:#f2f4f6; font-family:Arial, sans-serif; color:#51545E;\">\r\n  <table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">\r\n    <tr>\r\n      <td align=\"center\" style=\"padding:20px;\">\r\n        <table width=\"600\" cellpadding=\"0\" cellspacing=\"0\" style=\"background-color:#FFFFFF; border-radius:8px; overflow:hidden;\">\r\n          \r\n          <!-- Logo -->\r\n          <tr>\r\n            <td align=\"center\" style=\"padding:40px 0; background-color:#3B82F6;\">\r\n              <img src=\"{logoUrl}\" alt=\"BetThanYes\" width=\"120\" style=\"display:block;\">\r\n            </td>\r\n          </tr>\r\n          \r\n          <!-- Header -->\r\n          <tr>\r\n            <td style=\"padding:30px;\">\r\n              <h1 style=\"margin:0; font-size:24px; color:#333333; text-align:center;\">Recupera tu contraseña</h1>\r\n            </td>\r\n          </tr>\r\n          \r\n          <!-- Body -->\r\n          <tr>\r\n            <td style=\"padding:0 30px 30px;\">\r\n              <p style=\"font-size:16px; line-height:1.5;\">\r\n                Hola {user.FullName},<br><br>\r\n                Hemos recibido una solicitud para restablecer la contraseña de tu cuenta en BetThanYes. Haz clic en el siguiente botón para continuar:\r\n              </p>\r\n              \r\n              <p style=\"text-align:center; margin:30px 0;\">\r\n                <a href=\"{resetLink}\" style=\"background-color:#3B82F6; color:#FFFFFF; text-decoration:none; padding:12px 24px; border-radius:4px; font-size:16px; display:inline-block;\">\r\n                  Restablecer contraseña\r\n                </a>\r\n              </p>\r\n              \r\n              <p style=\"font-size:14px; line-height:1.5; color:#6B7280;\">\r\n                Si no solicitaste este cambio, puedes ignorar este correo y tu contraseña permanecerá igual.\r\n              </p>\r\n            </td>\r\n          </tr>\r\n          \r\n          <!-- Footer -->\r\n          <tr>\r\n            <td style=\"padding:20px 30px; background-color:#F9FAFB; text-align:center; font-size:12px; color:#A1A1AA;\">\r\n              <p style=\"margin:0;\">© 2025 BetThanYes. Todos los derechos reservados.</p>\r\n              <p style=\"margin:5px 0 0;\">\r\n                <a href=\"\" style=\"color:#3B82F6; text-decoration:none;\">Soporte</a> \r\n              </p>\r\n            </td>\r\n          </tr>\r\n          \r\n        </table>\r\n      </td>\r\n    </tr>\r\n  </table>\r\n</body>\r\n</html>\r\n";

            _logger.LogInformation("Listo para enviar el correo.");
            return await _mailRepository.SendEmailAsync(toEmail, "Recupera tu contraseña", emailBody);
        }

    }
}
