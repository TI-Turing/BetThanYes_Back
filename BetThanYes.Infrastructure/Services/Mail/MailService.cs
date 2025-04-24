using System;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using Microsoft.Extensions.Logging;

namespace BetThanYes.Infrastructure.Services.Mail
{
    public class MailRepository : IMailRepository
    {
        private readonly RestClient _client;
        private readonly string _domain;
        private readonly ILogger<MailRepository> _logger;

        public MailRepository(ILogger<MailRepository> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // Obtén tu API key y dominio desde variables de entorno
            var apiKey = Environment.GetEnvironmentVariable("MAILGUN_API_KEY");
            _domain = Environment.GetEnvironmentVariable("MAILGUN_DOMAIN"); // "betthayes.com"

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(_domain))
            {
                _logger.LogError("MAILGUN_API_KEY o MAILGUN_DOMAIN no están configurados.");
                throw new InvalidOperationException("MAILGUN_API_KEY o MAILGUN_DOMAIN no están configurados.");
            }

            var options = new RestClientOptions("https://api.mailgun.net")
            {
                Authenticator = new HttpBasicAuthenticator("api", apiKey)
            };
            _client = new RestClient(options);
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            var request = new RestRequest($"/v3/{_domain}/messages", Method.Post);
            request.AlwaysMultipartFormData = true;

            request.AddParameter("from", $"Info <info@{_domain}>");
            request.AddParameter("to", toEmail);
            request.AddParameter("subject", subject);
            request.AddParameter("html", htmlContent);


            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                _logger.LogInformation($"Email enviado exitosamente a {toEmail}.");
            }
            else
            {
                _logger.LogError($"Error al enviar el email a {toEmail}. Status: {response.StatusCode}, Error: {response.ErrorMessage}");
            }

            return response.IsSuccessful;
        }
    }
}
