// En tu carpeta Infrastructure/Database
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BetThanYes.Infrastructure.Database
{
    public class SqlDbContext
    {
        private readonly string _configuration;

        public SqlDbContext(string configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            
            return new SqlConnection(_configuration);

         


        }
    }
}
