using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeatherMonitoringService.Factories;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Observables;

namespace WeatherMonitoringService;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton<BotsFactory>();
        services.AddLogging(configure => configure.AddConsole().SetMinimumLevel(LogLevel.Information));
        services.AddSingleton<WeatherDataValidator>();

        try
        {
            var weatherDataValidator = services.BuildServiceProvider().GetService<WeatherDataValidator>();
            var botFactory = services.BuildServiceProvider().GetService<BotsFactory>();
            var observers = botFactory.GetBots();
            
            Console.WriteLine("Weather monitoring service started.");
            Console.WriteLine("Enter the weather data:");
            var input = Console.ReadLine();
            IWeatherDataFactory weatherDataFactory = new WeatherDataFactory(weatherDataValidator);
            if (string.IsNullOrEmpty(input)) return;
            var weatherData = weatherDataFactory.CreateWeatherData(input);
      
            IWeatherStation weatherStation = new WeatherStation(observers);
            weatherStation.Notify(weatherData);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
