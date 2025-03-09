using System.Text.Json.Serialization;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Observables;

namespace WeatherMonitoringService.Observers;

public class SnowBot : WeatherBot, IWeatherObserver
{
    [JsonPropertyName("temperatureThreshold")]
    public decimal TemperatureThreshold { get; set; }

    public void Update(WeatherData weatherData)
    {
        if (Enabled && TemperatureThreshold > weatherData.Temperature)
        {
            Console.WriteLine(Message);
        }
    }
}
