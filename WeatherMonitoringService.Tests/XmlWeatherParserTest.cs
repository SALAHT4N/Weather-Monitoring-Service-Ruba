using FluentValidation.Results;
using Moq;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Parsers;
using WeatherMonitoringService.Parsers.Exceptions;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace WeatherMonitoringService.Tests;

public class XmlWeatherParserTest
{
    
    [Fact]
    public void ParseWeatherInput_ShouldReturnsWeatherData()
    {
        const string input = "<WeatherData><Location>location</Location><Temperature>25</Temperature><Humidity>60</Humidity></WeatherData>";
        
        var validatorMock = new Mock<WeatherDataValidator> { CallBase = true };

        var parser = new XmlWeatherDataParser(validatorMock.Object);

        var weatherData = parser.ParseWeatherInput(input);

        Assert.Equal("location", weatherData.Location);
        Assert.Equal(25, weatherData.Temperature);
        Assert.Equal(60, weatherData.Humidity);
    }
    
    [Fact]
    public void ParseWeatherInput_ShouldThrowsInvalidXmlFormatException()
    { 
        const string input = "<WeatherData><Location>location</Location><Temperature>25</Temperature></WeatherData>";
        var validatorMock = new Mock<WeatherDataValidator> { CallBase = true };
        var validationResult = new ValidationResult();
        validationResult.Errors.Add(new ValidationFailure("Humidity", "Missing Humidity element"));

        var parser = new XmlWeatherDataParser(validatorMock.Object);

        var exception = Assert.Throws<InvalidXmlFormatException>(() => parser.ParseWeatherInput(input));
        Assert.Contains("Missing Humidity element", exception.Message);
    }
}