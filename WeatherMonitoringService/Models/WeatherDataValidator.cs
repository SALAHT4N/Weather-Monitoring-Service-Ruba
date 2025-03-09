using FluentValidation;

namespace WeatherMonitoringService.Models;

public class WeatherDataValidator : AbstractValidator<WeatherData>
{
    public WeatherDataValidator()
    {
        RuleFor(weatherData => weatherData.Temperature)
            .NotEmpty().WithMessage("Please specify a valid temperature");
        RuleFor(weatherData => weatherData.Location)
            .NotEmpty().WithMessage("Please specify a valid location");
        RuleFor(weatherData => weatherData.Humidity)
            .NotEmpty().WithMessage("Please specify a valid humidity");
    }
}
