using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using WeatherMonitoringService.Models;

namespace WeatherMonitoringService.Observers;

public class SnowBot : WeatherBot
{
    [JsonPropertyName("temperatureThreshold")]
    public decimal TemperatureThreshold { get; init; }

    private readonly ILogger<SnowBot>? _logger;

    public SnowBot(ILogger<SnowBot> logger)
    {
        _logger = logger;
    }

    public override void Update(WeatherData weatherData)
    {
        if (Enabled && TemperatureThreshold > weatherData.Temperature)
        {
            _logger?.LogInformation("{Message}", Message);
        }
    }
}
