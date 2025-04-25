using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.Models;
using BetThanYes.Infrastructure.Services.Country;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly ILogger<CountryService> _logger;
        private readonly ICountryRepository _countryRepository;
        public CountryService(ILogger<CountryService> logger, ICountryRepository countryRepository)
        {
            _logger = logger;
            _countryRepository = countryRepository;
        }
        public async Task<List<Country>> GetCountries()
        {
            return await _countryRepository.GetCountries();
        }
    }
}
