using FluentValidation.Results;
using Moq;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Parsers;
using WeatherMonitoringService.Parsers.Exceptions;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace WeatherMonitoringService.Tests;

public class JsonWeatherParserTest
{
    
    [Fact]
    public void ParseWeatherInput_ShouldReturnsWeatherData()
    {
        const string input = "{\"Location\":\"location\",\"Temperature\":25,\"Humidity\":60}";
        
        var validatorMock = new Mock<WeatherDataValidator> { CallBase = true };

        var parser = new JsonWeatherDataParser(validatorMock.Object);

        var weatherData = parser.ParseWeatherInput(input);

        Assert.Equal("location", weatherData.Location);
        Assert.Equal(25, weatherData.Temperature);
        Assert.Equal(60, weatherData.Humidity);
    }
    
    [Fact]
    public void ParseWeatherInput_ShouldThrowsInvalidJsonFormatException()
    { 
        const string input = "{\"Location\":\"\",\"Temperature\":25,\"Humidity\":50}";
        var validatorMock = new Mock<WeatherDataValidator> { CallBase = true };
        var validationResult = new ValidationResult();
        validationResult.Errors.Add(new ValidationFailure("Location", "Please specify a valid location"));

        var parser = new JsonWeatherDataParser(validatorMock.Object);

        var exception = Assert.Throws<InvalidJsonFormatException>(() => parser.ParseWeatherInput(input));
        Assert.Contains("Please specify a valid location", exception.Message);
    }
}
