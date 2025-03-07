namespace WeatherMonitoringService.Models;

public class WeatherBotConfiguration
{
    public required bool Enabled { get; init; }
    public required decimal? HumidityThreshold { get; init; }
    public required decimal? TemperatureThreshold { get; init; }
    public required string Message { get; init; }

    public override string ToString()
    {
        return $"Enabled: {Enabled}, Threshold: {TemperatureThreshold}, humidityThreshold {HumidityThreshold}, Message: {Message}";
    }
}