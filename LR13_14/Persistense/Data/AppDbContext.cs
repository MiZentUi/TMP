using LR13_14.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LR13_14.Persistense.Data;

public class AppDbContext : DbContext
{
    public DbSet<RoomCategory> RoomCategories { get; set; }
    public DbSet<Service> Services { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Service>().OwnsOne(t => t.Data);
    }
}