using System;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace BetThanYes.Infrastructure.Services.Mail
{
    public class MailRepository : IMailRepository
    {
        private readonly RestClient _client;
        private readonly string _domain;

        public MailRepository()
        {
            // Obtén tu API key y dominio desde variables de entorno
            var apiKey = Environment.GetEnvironmentVariable("MAILGUN_API_KEY");
            _domain = Environment.GetEnvironmentVariable("MAILGUN_DOMAIN"); // "betthayes.com"

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
            request.AddParameter("text", htmlContent);

            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful;
        }
    }
}
