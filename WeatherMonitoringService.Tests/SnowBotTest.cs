namespace WeatherMonitoringService.Tests;

using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using Models;
using Observers;



public class SnowBotTest
{
    private readonly IFixture _fixture;
    private readonly Mock<ILogger<SnowBot>> _loggerMock = new();
    private readonly WeatherData _weatherData;

    public SnowBotTest()
    {
        _fixture = new Fixture();

        _weatherData = new WeatherData
        {
            Location = _fixture.Create<string>(),
            Temperature = 20,
            Humidity = 60 
        };
    }


    [Fact]
    public void RainObserverTest_ShouldLogMessage_WhenHumidityExceedsThreshold()
    {
        var message = _fixture.Create<string>();
        var snowBot = new SnowBot(_loggerMock.Object)
        {
            Enabled = true,
            Message = message,
            TemperatureThreshold = 30
        };

        snowBot.Update(_weatherData);

        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(message)),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public void RainObserverTest_ShouldNotLog_WhenHumidityBelowThreshold()
    {
        var message = _fixture.Create<string>();
        var snowBot = new SnowBot(_loggerMock.Object)
        {
            Enabled = true,
            Message = message,
            TemperatureThreshold = 10 
        };
        
        snowBot.Update(_weatherData);
    
        _loggerMock.Verify(
            x => x.Log(It.IsAny<LogLevel>(),
                It.IsAny<EventId>(), 
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Never);
    }
}