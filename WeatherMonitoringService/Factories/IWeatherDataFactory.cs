using WeatherMonitoringService.Models;

namespace WeatherMonitoringService.Factories;

public interface IWeatherDataFactory
{
    WeatherData CreateWeatherData(string input);
}
