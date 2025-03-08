using WeatherMonitoringService.Models;

namespace WeatherMonitoringService.Observables;

public interface IWeatherStation
{
    void Subscribe(IWeatherObserver observer);
    void Unsubscribe(IWeatherObserver observer);
    void Notify(WeatherData weatherData);
}