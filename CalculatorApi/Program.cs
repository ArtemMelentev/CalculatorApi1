using CalculatorApi.Application.Features.Commands;
using CalculatorApi.Application.Validators;
using CalculatorApi.Infrastructure.DB;
using CalculatorApi.WebApi.Middleware;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CalculateCommand).Assembly);
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<CalculateCommandValidator>();
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseMiddleware<ErrorHandlingMiddleware>();
app.MapControllers();
app.Run();
