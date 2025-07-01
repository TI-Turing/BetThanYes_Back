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
            await connection.OpenAsync(); // Aquí usas await para abrir la conexión asíncronamente
            return connection;
            // return new SqlConnection(_configuration);

        }
    }
}
