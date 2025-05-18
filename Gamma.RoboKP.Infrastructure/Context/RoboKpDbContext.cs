using Gamma.RoboKP.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Gamma.RoboKP.Infrastructure.Context;

public class RoboKpDbContext : DbContext
{
    public RoboKpDbContext(DbContextOptions<RoboKpDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
    
    public DbSet<UserEntity> Users { get; set; }
}