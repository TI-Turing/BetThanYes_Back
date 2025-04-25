using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetThanYes.Domain.Models;

namespace BetThanYes.Infrastructure.Services.Country
{
    public interface ICountryRepository
    {
        Task<List<Domain.Models.Country>> GetCountries();
    }
}
