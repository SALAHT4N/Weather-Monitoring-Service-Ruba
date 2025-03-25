using System.Text.Json;
using FluentValidation;
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

            validator.ValidateAndThrow(weatherData);
            return weatherData;
        }
        catch (JsonException ex)
        {
            throw new InvalidJsonFormatException($"Invalid JSON format: {ex.Message}");
        }
    }

    public bool IsParserInputFormatValid(string input)
    {
        try
        {
            return JsonSerializer.Deserialize<WeatherData>(input) is not null;
        }
        catch
        {
            return false;
        }
    }
}
