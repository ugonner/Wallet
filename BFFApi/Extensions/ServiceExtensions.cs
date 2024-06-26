namespace BFFApi.Extensions;

using Microsoft.EntityFrameworkCore;
using Contracts.ServiceContracts;
using Repository;
using Contracts.RepositoryContracts;
using Services;
using Presentation;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Configuration;

public static class ServiceExtensions
{
// public static void ConfigureGlobalConfiguration( this IServiceCollection service)
// {
//    service.AddSingleton<IConfiguration, Configuration>();

// }

    public static IServiceCollection ConfigureDbContext(this IServiceCollection service, IConfigurationRoot config)
    {
        return service.AddDbContext<RepositoryContext>((DbContextOptionsBuilder opt) => opt.UseMySQL($"{config.GetConnectionString("sqlConnection")}") );
    }

    public static IServiceCollection ConfigureRepositoryManager(this IServiceCollection service) => service.AddScoped<IRepositoryManager, RepositoryManager>();
    public static IServiceCollection ConfigureServiceManager(this IServiceCollection service) => service.AddScoped<IServiceManager, ServiceManager>();
    public static IServiceCollection ConfigureLogger(this IServiceCollection service) => service.AddSingleton<ILoggerManager, LoggerManager>();

    public static IMvcBuilder ConfigureController(this IServiceCollection service) => 
    service.AddControllers()
    .AddApplicationPart(typeof(AssemblyRef).Assembly);

    public static IServiceCollection ConfigureAutoMapper(this IServiceCollection service) => service.AddAutoMapper(typeof(ServiceExtensions));

}