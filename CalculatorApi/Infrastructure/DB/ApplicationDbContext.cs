using System.Reflection;
using CalculatorApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CalculatorApi.Infrastructure.DB;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<CalculationResultModels> CalculationRecords { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}