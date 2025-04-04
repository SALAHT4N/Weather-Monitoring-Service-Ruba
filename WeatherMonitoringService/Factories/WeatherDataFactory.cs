using WeatherMonitoringService.Factories.Exceptions;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Parsers;

namespace WeatherMonitoringService.Factories;

public class WeatherDataFactory (IEnumerable<IWeatherDataParser> parsers) : IWeatherDataFactory
{
    public WeatherData CreateWeatherData(string input)
    {
        var parser = parsers.FirstOrDefault(p => p.IsParserInputFormatValid(input))
            ?? throw new UnsupportedFormatException("Input format is not supported");
        
        return parser.ParseWeatherInput(input);
    }
}
