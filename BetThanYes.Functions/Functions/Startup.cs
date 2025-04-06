using BetThanYes.Application.Services.Interfaces;
using BetThanYes.Application.Services;
//using BetThanYes.Infrastructure.Data;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

[assembly: FunctionsStartup(typeof(BetThanYes.Functions.Startup))]

namespace BetThanYes.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Configuración de IConfiguration
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddSingleton<IConfiguration>(config);

            // Configuración de logging
            builder.Services.AddLogging();

            // Inyección de servicios
            builder.Services.AddScoped<IRoutineService, RoutineService>();

            // Aquí puedes inyectar otros servicios o helpers
            // builder.Services.AddScoped<IUserService, UserService>();

            // Agregar un log para verificar la configuración
            var serviceProvider = builder.Services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<Startup>>();
            logger.LogInformation("Startup configuration completed.");
        }
    }
}
