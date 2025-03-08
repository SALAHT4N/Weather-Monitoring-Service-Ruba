using System.Text.Json.Serialization;

namespace WeatherMonitoringService.Models;

public class WeatherBot
{
    [JsonPropertyName("enabled")]
    public required bool Enabled { get; set; }
    
    [JsonPropertyName("message")]
    public required string Message { get; set; }
    
}