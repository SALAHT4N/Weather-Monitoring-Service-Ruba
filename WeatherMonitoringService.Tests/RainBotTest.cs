using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Observers;

namespace WeatherMonitoringService.Tests;

public class RainBotTest
{
    private readonly Fixture _fixture = new();
    private readonly Mock<ILogger<RainBot>> _logger = new();
    private readonly WeatherData _weatherData;

    public RainBotTest()
    {
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
        var rainBot = new RainBot(_logger.Object)
        {
            Enabled = true,
            Message = message,
            HumidityThreshold = 50
        };

        rainBot.Update(_weatherData);

        _logger.Verify(
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
        var rainBot = new RainBot(_logger.Object)
        {
            Enabled = true,
            Message = message,
            HumidityThreshold = 70 
        };
        
        rainBot.Update(_weatherData);

        _logger.Verify(
            x => x.Log(It.IsAny<LogLevel>(),
                It.IsAny<EventId>(), 
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Never);
    }
}