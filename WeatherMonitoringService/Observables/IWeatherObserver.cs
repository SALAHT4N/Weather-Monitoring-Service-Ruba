using WeatherMonitoringService.Models;

namespace WeatherMonitoringService.Observables;

public interface IWeatherObserver
{
    void Update (WeatherData weatherData);
}