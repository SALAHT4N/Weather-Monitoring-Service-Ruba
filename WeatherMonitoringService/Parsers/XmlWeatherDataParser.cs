using System.Xml.Linq;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Parsers.Exceptions;

namespace WeatherMonitoringService.Parsers;

public class XmlWeatherDataParser : IWeatherDataParser
{
    public WeatherData ParseWeatherInput(string input)
    {
        var xDoc = XDocument.Parse(input);
        var root = xDoc.Root;
        if (root == null)
            throw new InvalidXmlFormatException("Invalid XML: Missing root element.");

        var weatherData = new WeatherData
        {
            Location = root.Element("Location")?.Value ?? throw new InvalidXmlFormatException("Missing Location element."),
            Temperature = Convert.ToDecimal(root.Element("Temperature")?.Value ?? throw new InvalidXmlFormatException("Missing Temperature element.")),
            Humidity = Convert.ToDecimal(root.Element("Humidity")?.Value ?? throw new InvalidXmlFormatException("Missing Humidity element."))
        };

        var validator = new WeatherDataValidator();
        var result = validator.Validate(weatherData);
        if (!result.IsValid)
        {
            throw new InvalidXmlFormatException($"Invalid weather data: {result.Errors.FirstOrDefault()?.ErrorMessage}");
        }
        return weatherData;
    }
}