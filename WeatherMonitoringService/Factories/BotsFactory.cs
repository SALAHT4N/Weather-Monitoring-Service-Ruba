using Microsoft.Extensions.Configuration;
using WeatherMonitoringService.Observables;
using WeatherMonitoringService.Observers;

namespace WeatherMonitoringService.Factories;

public class BotsFactory
{
    public static List<IWeatherObserver> GetBots()
    {
        List<IWeatherObserver> bots = [];
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"D:\WeatherMonitoringService\WeatherMonitoringService\configuration.json", optional: false, reloadOnChange: true)
            .Build();
        
        var rainBot = configuration.GetSection("RainBot").Get<RainBot>();
        var sunBot = configuration.GetSection("SunBot").Get<SunBot>();
        var snowBot = configuration.GetSection("SnowBot").Get<SnowBot>();
        if (rainBot is null || sunBot is null || snowBot is null)
        {
            throw new NullReferenceException("Invalid configuration");
        }
        bots.Add(rainBot);
        bots.Add(sunBot);
        bots.Add(snowBot);
        return bots;
    }
}