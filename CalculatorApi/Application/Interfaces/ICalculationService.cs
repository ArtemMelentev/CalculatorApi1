namespace CalculatorApi.Application.Interfaces;

public interface ICalculationService
{
    double PerformOperation(double operand1, double operand2, string operation);
}