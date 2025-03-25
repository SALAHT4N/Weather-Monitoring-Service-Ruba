using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Parsers;
using WeatherMonitoringService.Parsers.Exceptions;

namespace WeatherMonitoringService.Tests;

public class JsonWeatherParserTest
{
    private readonly JsonWeatherDataParser _parser;
    
    public JsonWeatherParserTest()
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Freeze<Mock<WeatherDataValidator>>();
        _parser = fixture.Create<JsonWeatherDataParser>();
    }
    
    [Fact]
    public void ParseWeatherInput_ShouldReturnsWeatherData()
    {
        //Arrange
        const string location = "New York";
        const int temperature = 27;
        const int humidity = 60;
        var expectedWeatherData = new WeatherData
        {
            Location = location,
            Temperature = temperature,
            Humidity = humidity
        };
        
        var input = $$"""
                      {
                          "Location":"{{location}}",
                          "Temperature":{{temperature}},
                          "Humidity":{{humidity}}
                      }
                      """;

        // Act
        var weatherData = _parser.ParseWeatherInput(input);

        // Assert
        Assert.Equal(expectedWeatherData.Location, weatherData.Location);
    }
    
    [Fact]
    public void ParseWeatherInput_ShouldThrowsInvalidJsonFormatException()
    { 
        //Arrange
        const string location = "New York";
        const int temperature = 27;
        
        var input = $$"""
                      {
                          "Location":"{{location}}",
                          "Temperature":{{temperature}}
                      }
                      """;
        
        //ACT && Assert

        Assert.Throws<InvalidJsonFormatException>(() => _parser.ParseWeatherInput(input));
    }
}
