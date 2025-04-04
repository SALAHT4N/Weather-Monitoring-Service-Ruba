using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WeatherMonitoringService.Observables;
using WeatherMonitoringService.Observers;

namespace WeatherMonitoringService.Factories;

public class BotsFactory (ILoggerFactory loggerFactory, IConfigurationRoot configuration)
{
    public List<IWeatherObserver> GetBots()
    {
        List<IWeatherObserver> bots = [];

        var rainBotConfig = configuration.GetSection("rainBot").Get<RainBot>();
        var sunBotConfig = configuration.GetSection("SunBot").Get<SunBot>();
        var snowBotConfig = configuration.GetSection("SnowBot").Get<SnowBot>();

        if (rainBotConfig is null || sunBotConfig is null || snowBotConfig is null)
        {
            throw new NullReferenceException("Invalid configuration");
        }

        var rainBot = new RainBot(loggerFactory.CreateLogger<RainBot>())
        {
            Enabled = rainBotConfig.Enabled,
            Message = rainBotConfig.Message,
            HumidityThreshold = rainBotConfig.HumidityThreshold
        };

        var sunBot = new SunBot(loggerFactory.CreateLogger<SunBot>())
        {
            Enabled = sunBotConfig.Enabled,
            Message = sunBotConfig.Message,
            TemperatureThreshold = sunBotConfig.TemperatureThreshold
        };

        var snowBot = new SnowBot(loggerFactory.CreateLogger<SnowBot>())
        {
            Enabled = snowBotConfig.Enabled,
            Message = snowBotConfig.Message,
            TemperatureThreshold = snowBotConfig.TemperatureThreshold
        };
        
        bots.Add(rainBot);
        bots.Add(sunBot);
        bots.Add(snowBot);
        return bots;
    }
}
