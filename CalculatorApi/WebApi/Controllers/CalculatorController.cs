using CalculatorApi.Application.Features.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorApi.WebApi.Controllers;

[ApiController]
public class CalculatorController : ControllerBase
{
    [HttpPost("calculate")]
    public IActionResult Calculate([FromBody] CalculateCommand command)
    {
        double result = command.Operation switch
        {
            "+" => command.FirstNumber + command.SecondNumber,
            "-" => command.FirstNumber - command.SecondNumber,
            "*" => command.FirstNumber * command.SecondNumber,
            "/" => command.FirstNumber / command.SecondNumber,
        _ => throw new InvalidOperationException("Invalid operation1")
        };

        return Ok(new { Result = result });
    }
}
