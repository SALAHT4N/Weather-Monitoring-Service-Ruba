namespace WeatherMonitoringService.Models;

public class WeatherData
{
    public required string Location { get; set; }
    public required decimal Temperature { get; set; }
    public required decimal Humidity { get; set; }

    public override string ToString()
    {
        return $"Location: {Location}, Temperature: {Temperature}, Humidity: {Humidity}";
    }
}