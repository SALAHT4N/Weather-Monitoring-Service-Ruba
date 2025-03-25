using System.Xml.Linq;
using FluentValidation;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Parsers.Exceptions;

namespace WeatherMonitoringService.Parsers;

public class XmlWeatherDataParser(WeatherDataValidator validator) : IWeatherDataParser
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

        validator.ValidateAndThrow(weatherData);
        return weatherData;
    }

    public bool IsParserInputFormatValid(string input)
    {
        try
        {
            return XDocument.Parse(input).Root is not null;
        }
        catch
        {
            return false;
        }
            
    }
}
