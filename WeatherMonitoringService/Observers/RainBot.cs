using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Observables;

namespace WeatherMonitoringService.Observers;

public class RainBot : WeatherBot, IWeatherObserver
{
    private readonly ILogger<RainBot> _logger;
    public RainBot()
    {
        
    }

    public RainBot(ILogger<RainBot> logger)
    {
        _logger = logger;
    }
    [JsonPropertyName("humidityThreshold")]
    public decimal HumidityThreshold { get; init; }

    public void Update(WeatherData weatherData)
    {
        if (Enabled && HumidityThreshold < weatherData.Humidity)
        {
            _logger.LogInformation(Message);
        }
    }
}
