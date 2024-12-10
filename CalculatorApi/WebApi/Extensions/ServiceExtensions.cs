using CalculatorApi.Application.Features.Commands;
using CalculatorApi.Application.Interfaces;
using CalculatorApi.Application.Services;
using CalculatorApi.Application.Validators;
using FluentValidation;

namespace CalculatorApi.WebApi.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICalculationService, CalculationService>();
        services.AddValidatorsFromAssembly(typeof(CalculateCommandValidator).Assembly);
        return services;
    }
}
