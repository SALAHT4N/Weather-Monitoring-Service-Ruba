using System.Text.Json.Serialization;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Observables;

namespace WeatherMonitoringService.Observers;

public class RainBot : WeatherBot, IWeatherObserver
{
    [JsonPropertyName("humidityThreshold")]
    public decimal HumidityThreshold { get; set; }

    public void Update(WeatherData weatherData)
    {
        if (Enabled && HumidityThreshold < weatherData.Humidity)
        {
            Console.WriteLine(Message);
        }
    }
}
