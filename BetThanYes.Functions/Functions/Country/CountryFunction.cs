using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Domain.DTOs.Response.Country;
using BetThanYes.Domain.DTOs.Response.User;
using BetThanYes.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Azure;

namespace BetThanYes.Functions.Functions.Country
{
    public class CountryFunction
    {
        private readonly ILogger<CountryFunction> _logger;
        private readonly ICountryService _countryService;


        public CountryFunction(ILogger<CountryFunction> logger, ICountryService countryService)
        {
            _logger = logger;
            _countryService = countryService;
        }

        [Function("GetCountries")]
        public async Task<ApiResponse<List<GetCountriesResponse>>> Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
        {
            ApiResponse<List<GetCountriesResponse>> apiResponse = new ApiResponse<List<GetCountriesResponse>>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Domain.Models.Country, GetCountriesResponse>();
                });

                var mapper = new Mapper(config);

                _logger.LogInformation("C# HTTP trigger function processed a request.");
                
                var countries = await _countryService.GetCountries();

                
                var getCountriesResponse = mapper.Map<List<GetCountriesResponse>>(countries);

                apiResponse.Data = getCountriesResponse;
                apiResponse.Success = true;
                apiResponse.Message = "Paises consultados exitosamente";
                apiResponse.StatusCode = StatusCodes.Status200OK;

                return apiResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error: " + ex.Message);
                apiResponse.Success = false;
                apiResponse.Message = ex.Message;
                apiResponse.StatusCode = StatusCodes.Status500InternalServerError;
                return apiResponse;
            }
        }
    }
}
