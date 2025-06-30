namespace SistemaOrganizacaoEstudantil.Infrastructure;

using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(configuration.GetConnectionString("SOEDatabase"));
    }

    protected readonly IConfiguration configuration;

    public required DbSet<Entities.User> Users { get; set ; }
}
