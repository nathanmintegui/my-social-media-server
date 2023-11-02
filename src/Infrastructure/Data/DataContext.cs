using Domain.Models;
using Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : DbContext
{
#pragma warning disable CS8618
    public DataContext()
#pragma warning restore CS8618
    {
    }

#pragma warning disable CS8618
    public DataContext(DbContextOptions<DataContext> options) : base(options)
#pragma warning restore CS8618
    {
    }

    public DbSet<Post> Post { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(@"Host=localhost;Database=postgres;Username=postgres;Password=postgres");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PostMap());
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}