using Microsoft.Extensions.Configuration;

namespace Services;

public class GlobalConfiguration
{
    public static IConfiguration GlobalConfigurationSettings = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
}