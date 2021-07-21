using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

public class DependencyInjection
{
    private static IConfiguration BuildConfiguration()
    {
        var configuration = new ConfigurationBuilder()
                                       .AddJsonFile("appsettings.json", true, true)
                                       .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
                                       .AddEnvironmentVariables()
                                       .Build();

        return configuration;

    }
    public static void ConfigureServices(ServiceCollection services)
    {
        var configuration = BuildConfiguration();

        //services.Configure<XXXXXX>(configuration.GetSection("XXXXX"));

        services.AddLogging(configure => {
            configure
            .AddConfiguration(configuration.GetSection("Logging"))
            //.AddConsole()
            .AddJsonConsole();
            
        });

    }

    public static IServiceProvider BuildServiceProvider()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }
}