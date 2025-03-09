using WeatherMonitoringService.Factories;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Observables;

namespace WeatherMonitoringService;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var weatherDataValidator = new WeatherDataValidator();
            var observers = BotsFactory.GetBots();
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
