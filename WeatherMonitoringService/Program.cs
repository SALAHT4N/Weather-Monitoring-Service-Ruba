using WeatherMonitoringService.Factories;
using WeatherMonitoringService.Observables;

namespace WeatherMonitoringService;

class Program
{
    static void Main(string[] args)
    {
        
        var observers = BotsFactory.GetBots();
        Console.WriteLine("Weather monitoring service started.");
        var input = Console.ReadLine();
        IWeatherDataFactory weatherDataFactory = new WeatherDataFactory();
        if (string.IsNullOrEmpty(input)) return;
        var weatherData = weatherDataFactory.CreateWeatherData(input);
      
        IWeatherStation weatherStation = new WeatherStation(observers);
        weatherStation.Notify(weatherData);
    }
}