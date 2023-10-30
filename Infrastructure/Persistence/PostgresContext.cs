using Domain;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence;
public class PostgresContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public PostgresContext(DbContextOptions<PostgresContext> options, IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public DbSet<Todo> Todo { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings
        options.UseNpgsql(Configuration.GetConnectionString("Postgres"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
