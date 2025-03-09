using System.Text.Json;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Parsers.Exceptions;

namespace WeatherMonitoringService.Parsers;

public class JsonWeatherDataParser(WeatherDataValidator validator) : IWeatherDataParser
{
    public WeatherData ParseWeatherInput(string input)
    {
        try
        {
            var weatherData = JsonSerializer.Deserialize<WeatherData>(input);
            if (weatherData is null)
                throw new InvalidJsonFormatException("Invalid JSON: Data is empty or null");

            var result = validator.Validate(weatherData);
            if (!result.IsValid)
            {
                throw new InvalidJsonFormatException($"Invalid weather data: {result.Errors.FirstOrDefault()?.ErrorMessage}");
            }
            return weatherData;
        }
        catch (JsonException ex)
        {
            throw new InvalidJsonFormatException($"Invalid JSON format: {ex.Message}");
        }
    }
}
