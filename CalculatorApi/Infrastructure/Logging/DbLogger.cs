using CalculatorApi.Infrastructure.Persistence.DbContextes;

namespace CalculatorApi.Infrastructure.Logging;

public class DbLogger : ILogger
{
    private readonly AppDbContext _dbContext;

    public DbLogger(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Log(string message)
    {
        // Сохранение лога в базу данных
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        throw new NotImplementedException();
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        throw new NotImplementedException();
    }
}