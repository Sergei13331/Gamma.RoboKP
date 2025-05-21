using Gamma.RoboKP.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gamma.RoboKP.Infrastructure.Context;

public class RoboKpDbContext : IdentityDbContext<UserEntity, IdentityRoleEntity, long>
{
    public DbSet<RefreshTokenEntity> RefreshToken { get; set; }
    public RoboKpDbContext(DbContextOptions<RoboKpDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<UserEntity>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}