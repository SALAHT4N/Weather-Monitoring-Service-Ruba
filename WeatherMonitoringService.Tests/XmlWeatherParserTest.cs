using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Parsers;
using WeatherMonitoringService.Parsers.Exceptions;

namespace WeatherMonitoringService.Tests;

public class XmlWeatherParserTest
{
    
    private readonly XmlWeatherDataParser _parser;
    
    public XmlWeatherParserTest()
    {
        var fixture = new Fixture().Customize(new AutoMoqCustomization());
        fixture.Freeze<Mock<WeatherDataValidator>>();
        _parser = fixture.Create<XmlWeatherDataParser>();
    }
    
    [Fact]
    public void ParseWeatherInput_ShouldReturnsWeatherData()
    {
        // Arrange
        const string location = "New York";
        const int temperature = 27;
        const int humidity = 60;
        var expectedWeatherData = new WeatherData
        {
            Location = location,
            Temperature = temperature,
            Humidity = humidity
        };
        var input = $"""
                      <WeatherData>
                          <Location>{location}</Location>
                          <Temperature>{temperature}</Temperature>
                          <Humidity>{humidity}</Humidity>
                      </WeatherData>
                      """;


        // Act
        var weatherData = _parser.ParseWeatherInput(input);

        // Assert
        Assert.Equal(expectedWeatherData.Location, weatherData.Location);
    }
    
    [Fact]
    public void ParseWeatherInput_ShouldThrowsInvalidXmlFormatException()
    { 
        // Arrange
        const string location = "New York";
        const int temperature = 27;
        
        var input = $"""
                     <WeatherData>
                         <Location>{location}</Location>
                         <Temperature>{temperature}</Temperature>
                     </WeatherData>
                     """;

        
        Assert.Throws<InvalidXmlFormatException>(() => _parser.ParseWeatherInput(input));
    }
}