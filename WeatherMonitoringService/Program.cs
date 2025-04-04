using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeatherMonitoringService.Factories;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Observables;
using WeatherMonitoringService.Parsers;

namespace WeatherMonitoringService;

class Program
{
    static void Main(string[] args)
    {
        var serviceProvider = ConfigureServices();

        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        var botFactory = serviceProvider.GetRequiredService<BotsFactory>();
        var weatherDataFactory = serviceProvider.GetRequiredService<IWeatherDataFactory>();

        var observers = botFactory.GetBots();

        try
        {
            Console.WriteLine("Weather monitoring service started.");
            Console.WriteLine("Enter the weather data:");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                logger.LogWarning("No input provided.");
                return;
            }

            var weatherData = weatherDataFactory.CreateWeatherData(input);
            var weatherStation = new WeatherStation(observers);
            weatherStation.Notify(weatherData);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred in the weather monitoring service.");
        }
    }

    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddLogging(configure => configure.AddConsole().SetMinimumLevel(LogLevel.Information));

        services.AddSingleton<WeatherDataValidator>();
        services.AddSingleton<ILoggerFactory, LoggerFactory>();
        services.AddSingleton(sp =>
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("./configurations.json", optional: false, reloadOnChange: true)
                .Build();

            return new BotsFactory(sp.GetRequiredService<ILoggerFactory>(), configuration);
        });

        services.AddSingleton<IWeatherDataParser, JsonWeatherDataParser>();
        services.AddSingleton<IWeatherDataParser, XmlWeatherDataParser>();
        services.AddSingleton<IWeatherDataFactory, WeatherDataFactory>(sp =>
        {
            var parsers = sp.GetServices<IWeatherDataParser>().ToList();
            return new WeatherDataFactory(parsers);
        });

        return services.BuildServiceProvider();
    }
}
