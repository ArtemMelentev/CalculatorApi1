using CalculatorApi.Application.Features.Commands;
using CalculatorApi.Application.Validators;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Регистрация сервисов
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
builder.Services.AddControllers().AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<CalculateCommandValidator>();
});

var app = builder.Build();
app.UseHttpsRedirection();

// Включите маршруты для контроллеров
app.UseCors("AllowAll");
app.MapControllers();
app.Run();
