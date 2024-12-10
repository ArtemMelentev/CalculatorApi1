using CalculatorApi.Application.Features.Commands;
using CalculatorApi.Infrastructure.DB;
using CalculatorApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorApi.WebApi.Controllers;

[ApiController]
public class CalculatorController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CalculatorController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("calculate")]
    public async Task<IActionResult> Calculate([FromBody] CalculateCommand command)
    {
        try
        {
            var result = PerformCalculation(command);
            
            var calculationRecord = new CalculationResultModels
            {
                FirstNumber = command.FirstNumber,
                SecondNumber = command.SecondNumber,
                Operation = command.Operation,
                Result = result
            };

            await _context.CalculationRecords.AddAsync(calculationRecord);
            await _context.SaveChangesAsync();
            
            return Ok(new { result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    private double PerformCalculation(CalculateCommand command)
    {
        return command.Operation switch
        {
            "+" => command.FirstNumber + command.SecondNumber,
            "-" => command.FirstNumber - command.SecondNumber,
            "*" => command.FirstNumber * command.SecondNumber,
            "/" when command.SecondNumber != 0 => command.FirstNumber / command.SecondNumber,
            "/" => throw new InvalidOperationException("Division by zero is not allowed."),
            _ => throw new InvalidOperationException("Invalid operation.")
        };
    }
}
