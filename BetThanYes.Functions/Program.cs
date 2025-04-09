using BetThanYes.Application.Services;
using BetThanYes.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Http;
using BetThanYes.Infrastructure.Database;
using BetThanYes.Infrastructure.Services.Files;
using BetThanYes.Infrastructure.Services.Users;
using BetThanYes.Infrastructure.Services.Routines;
using BetThanYes.Infrastructure.Services.Auth;
using BetThanYes.Infrastructure.Services.Mail;


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
        services.AddScoped<IRoutineService, RoutineService>();//App
        services.AddScoped<IRoutineRepository, RoutineRepository>();//Infraestructure

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IFileService, FileService>();
        services.AddScoped<IFileRepository, FileRepository>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthRepository, AuthRepository>();

        services.AddScoped<IMailService, MailService>();
        services.AddScoped<IMailRepository, MailRepository>();

        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IPasswordService, PasswordService>();

        

        // Aquí puedes seguir agregando más servicios si los necesitas
        // services.AddScoped<IUserService, UserService>();
    })
    .Build();

host.Run();
