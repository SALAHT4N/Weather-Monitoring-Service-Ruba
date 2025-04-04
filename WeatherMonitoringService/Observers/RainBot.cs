using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using WeatherMonitoringService.Models;

namespace WeatherMonitoringService.Observers;

public class RainBot : WeatherBot
{
    [JsonPropertyName("humidityThreshold")]
    public decimal HumidityThreshold { get; init; }
    
    private readonly ILogger<RainBot>? _logger;

    public RainBot(ILogger<RainBot> logger)
    {
        _logger = logger;
    }

    public override void Update(WeatherData weatherData)
    {
        if (Enabled && HumidityThreshold < weatherData.Humidity)
        {
            _logger?.LogInformation("{Message}", Message);
        }
    }
}
