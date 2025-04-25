using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetThanYes.Domain.Models;
using BetThanYes.Infrastructure.Database;
using Dapper;

namespace BetThanYes.Infrastructure.Services.Country
{
    public class CountryRepository : ICountryRepository
    {
        private readonly SqlDbContext _dbContext;


        public CountryRepository(SqlDbContext sqlDbContext)
        {
            _dbContext = sqlDbContext;
        }

        public async Task<List<Domain.Models.Country>> GetCountries()
        {
            const string sql = "SELECT * FROM Country";
            using var connection = _dbContext.CreateConnection();
            var countries = await connection.QueryAsync<Domain.Models.Country>(sql);
            return countries.ToList(); // Convertir el IEnumerable a List
        }
    }
}
