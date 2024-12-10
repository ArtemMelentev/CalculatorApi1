namespace CalculatorApi.Models;

public class CalculationResult
{
    public int Id { get; set; } // Первичный ключ
    public double FirstNumber { get; set; }
    public double SecondNumber { get; set; }
    public string Operation { get; set; } = string.Empty;
    public double Result { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}