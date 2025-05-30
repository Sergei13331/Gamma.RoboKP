using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Infrastructure.Configurations;
using Gamma.RoboKP.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gamma.RoboKP.Infrastructure.Context;

public class RoboKpDbContext : IdentityDbContext<AppUser, IdentityRoleEntity, long>
{
    public RoboKpDbContext(DbContextOptions<RoboKpDbContext> options) : base(options)
    {
    }
    
    public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
    public DbSet<AppUser> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AppUserConfiguration());

        modelBuilder.Entity<AppUser>()
            .Property(u => u.Status)
            .HasConversion<string>();
    }
}