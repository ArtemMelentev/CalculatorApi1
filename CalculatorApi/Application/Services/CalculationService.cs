using CalculatorApi.Application.Interfaces;

namespace CalculatorApi.Application.Services;

public class CalculationService : ICalculationService
{
    public double PerformOperation(double operand1, double operand2, string operation)
    {
        return operation.ToLower() switch
        {
            "add" => operand1 + operand2,
            "subtract" => operand1 - operand2,
            "multiply" => operand1 * operand2,
            "divide" => operand2 != 0 ? operand1 / operand2 : throw new DivideByZeroException(),
            "power" => Math.Pow(operand1, operand2),
            "root" => Math.Pow(operand1, 1 / operand2),
            _ => throw new InvalidOperationException("Invalid operation.")
        };
    }
}
