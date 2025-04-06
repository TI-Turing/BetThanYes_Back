using BetThanYes.Application.Services;
using BetThanYes.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using BetThanYes.Domain.Interfaces;
using BetThanYes.Infrastructure.Repositories;
using BetThanYes.Infrastructure.Database;


var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(config =>
    {
        config.SetBasePath(Environment.CurrentDirectory)
              .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Inyectar configuración si la necesitas
        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton(new SqlDbContext(connectionString));

        // Agregar servicios
        services.AddScoped<IRoutineService, RoutineService>();
        services.AddScoped<IRoutineRepository, RoutineRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();


        // Aquí puedes seguir agregando más servicios si los necesitas
        // services.AddScoped<IUserService, UserService>();
    })
    .Build();

host.Run();
