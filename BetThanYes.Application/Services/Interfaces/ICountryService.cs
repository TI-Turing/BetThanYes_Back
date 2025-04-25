using BetThanYes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetThanYes.Application.Services.Interfaces
{
    public interface ICountryService
    {
        Task<List<Country>> GetCountries();
    }
}
