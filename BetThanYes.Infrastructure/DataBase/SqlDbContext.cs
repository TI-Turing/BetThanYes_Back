// En tu carpeta Infrastructure/Database
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Azure.Identity;
using Azure.Core;

namespace BetThanYes.Infrastructure.Database
{
    public class SqlDbContext
    {
        private readonly string _configuration;

        public SqlDbContext(string configuration)
        {
            _configuration = configuration;
        }

        public async Task<IDbConnection> CreateConnectionAsync()
        {
            var connection = new SqlConnection(_configuration);

            // Obtener token con Azure.Identity
            var credential = new DefaultAzureCredential();
            var token = await credential.GetTokenAsync(
                new TokenRequestContext(new[] { "https://database.windows.net/.default" })
            );

            connection.AccessToken = token.Token;
            await connection.OpenAsync();

            return connection;
        }
    }
}
