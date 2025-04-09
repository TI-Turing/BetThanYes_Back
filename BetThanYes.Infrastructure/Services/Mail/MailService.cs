using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Infrastructure.Services.Mail
{
    public class MailRepository : IMailRepository
    {
        private readonly string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            var client = new SendGridClient("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");
            var from = new EmailAddress("jlap.11@hotmail.com"); // Puedes usar uno temporal
            var to = new EmailAddress("jlapnnn@gmail.com");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);

            var response = await client.SendEmailAsync(msg);
            var message = response.Body.ReadAsStringAsync();
            return response.IsSuccessStatusCode;
        }
    }
}
