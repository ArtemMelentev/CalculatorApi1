namespace CalculatorApi.Application.Features.Commands;

public class CalculateCommand
{
    public double FirstNumber { get; set; }
    public double SecondNumber { get; set; }
    public string Operation { get; set; } = string.Empty;
}