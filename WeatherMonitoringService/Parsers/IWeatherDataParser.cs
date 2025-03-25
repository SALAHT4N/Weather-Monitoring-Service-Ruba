using WeatherMonitoringService.Models;

namespace WeatherMonitoringService.Parsers;

public interface IWeatherDataParser
{
    WeatherData ParseWeatherInput(string input);
    
    bool IsParserInputFormatValid(string input);
    
}
