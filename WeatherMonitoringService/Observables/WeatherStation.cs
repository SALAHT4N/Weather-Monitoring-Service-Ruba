using WeatherMonitoringService.Models;

namespace WeatherMonitoringService.Observables;

public class WeatherStation (List<IWeatherObserver> observers) : IWeatherStation
{
    public void Subscribe(IWeatherObserver observer)
    {
        observers.Add(observer);
    }

    public void Unsubscribe(IWeatherObserver observer)
    {
        observers.Remove(observer);
    }

    public void Notify(WeatherData weatherData)
    {
        observers.ForEach(o => o.Update(weatherData));
    }
}
