using FluentValidation;

namespace WeatherMonitoringService.Models;

public class WeatherDataValidator : AbstractValidator<WeatherData>
{
    public WeatherDataValidator()
    {
        RuleFor(weatherData => weatherData.Temperature)
            .NotEmpty()
            .WithMessage("The limit is -100")
            .LessThan(100)
            .WithMessage("The limit is 100")
            .GreaterThan(-100)
            .WithMessage("Please specify a valid temperature");
        
        RuleFor(weatherData => weatherData.Location)
            .NotEmpty()
            .MaximumLength(80)
            .WithMessage("Please specify a valid location");
        
        RuleFor(weatherData => weatherData.Temperature)
            .NotEmpty()
            .WithMessage("The limit is -100")
            .LessThan(100)
            .WithMessage("The limit is 100")
            .GreaterThan(-100)
            .WithMessage("Please specify a valid Humidity");
    }
}
