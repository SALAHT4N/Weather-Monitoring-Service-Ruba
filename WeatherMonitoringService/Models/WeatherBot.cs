using System.Text.Json.Serialization;
using WeatherMonitoringService.Observables;

namespace WeatherMonitoringService.Models;

public abstract class WeatherBot : IWeatherObserver
{
    [JsonPropertyName("enabled")]
    public required bool Enabled { get; set; }
    
    [JsonPropertyName("message")]
    public required string Message { get; set; }

    public abstract void Update(WeatherData weatherData);
}
