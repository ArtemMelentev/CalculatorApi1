using CalculatorApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CalculatorApi.Infrastructure.Persistence.DbContextes;

public class AppDbContext : DbContext
{
    public DbSet<CalculationLog> CalculationLogs { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CalculationLog>().HasKey(x => x.Id);
    }
}