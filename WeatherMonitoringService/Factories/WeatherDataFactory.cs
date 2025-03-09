using WeatherMonitoringService.Factories.Exceptions;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Parsers;

namespace WeatherMonitoringService.Factories;

public class WeatherDataFactory (WeatherDataValidator validator) : IWeatherDataFactory
{
    private IWeatherDataParser? _parser;

    public WeatherData CreateWeatherData(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new InvalidOperationException("Input cannot be null or empty.");
        }

        if (input.Trim().StartsWith('{'))
        {
            _parser = new JsonWeatherDataParser(validator);
        }
        else if (input.Trim().StartsWith('<'))
        {
            _parser = new XmlWeatherDataParser(validator);
        }
        else
        {
            throw new UnsupportedFormatException("Unsupported weather data format.");
        }
        return _parser.ParseWeatherInput(input);
    }
}
