using AutoFixture;
using Moq;
using WeatherMonitoringService.Models;
using WeatherMonitoringService.Observables;

namespace WeatherMonitoringService.Tests;

public class WeatherStationTest
{

    [Fact]
    public void WeatherStationSubscribeTest_ShouldAddObserverToList()
    {
        // Arrange
        var mockObserver = new Mock<IWeatherObserver>();
        var observers = new List<IWeatherObserver>();
        var weatherStation = new WeatherStation(observers);

        // Act
        weatherStation.Subscribe(mockObserver.Object);

        // Assert
        Assert.Contains(mockObserver.Object, observers);
    }
    
    [Fact]
    public void WeatherStationSubscribeTest_ShouldAddMultipleObserversToList()
    {
        // Arrange
        var obs1 = new Mock<IWeatherObserver>();
        var obs2 = new Mock<IWeatherObserver>();
        var observers = new List<IWeatherObserver>();
        var weatherStation = new WeatherStation(observers);

        // Act
        weatherStation.Subscribe(obs1.Object);
        weatherStation.Subscribe(obs2.Object);

        // Assert
        Assert.Equal(2, observers.Count);
    }
    
    [Fact]
    public void WeatherStationNotifyTest_ShouldCallUpdateOnAllObservers()
    {
        // Arrange
        var fixture = new Fixture();
        var obs1 = new Mock<IWeatherObserver>();
        var obs2 = new Mock<IWeatherObserver>();
        var observers = new List<IWeatherObserver> { obs1.Object, obs2.Object };
        var weatherStation = new WeatherStation(observers);
        var weatherData = fixture.Create<WeatherData>();

        // Act
        weatherStation.Notify(weatherData);

        // Assert
        obs1.Verify(o => o.Update(weatherData), Times.Once);
        obs2.Verify(o => o.Update(weatherData), Times.Once);
    }
}