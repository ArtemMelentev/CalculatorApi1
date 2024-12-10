using CalculatorApi.Application.Features.Commands;
using FluentValidation;

namespace CalculatorApi.Application.Validators;

public class CalculateCommandValidator : AbstractValidator<CalculateCommand>
{
    public CalculateCommandValidator()
    {
        RuleFor(x => x.SecondNumber)
            .NotEqual(0).When(x => x.Operation == "/")
            .WithMessage("Division by zero is not allowed.");
    }
    
    
}