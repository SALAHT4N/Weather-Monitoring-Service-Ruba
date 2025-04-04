using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using WeatherMonitoringService.Models;

namespace WeatherMonitoringService.Observers;

public class SunBot : WeatherBot
{
    [JsonPropertyName("temperatureThreshold")]
    public decimal TemperatureThreshold { get; init; }
    
    private readonly ILogger<SunBot>? _logger;

    public SunBot(ILogger<SunBot> logger)
    {
        _logger = logger;
    }
   
    public override void Update(WeatherData weatherData)
    {
        if (Enabled && TemperatureThreshold < weatherData.Temperature)
        {
            _logger?.LogInformation("{Message}",Message);
        }
    }
}
