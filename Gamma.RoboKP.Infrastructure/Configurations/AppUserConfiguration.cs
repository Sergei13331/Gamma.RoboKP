using Gamma.RoboKP.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamma.RoboKP.Infrastructure.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasKey(au => au.Id);

        builder.OwnsOne(au => au.Company, company =>
        {
            company.Property(c => c.CompanyName).IsRequired();
        });
    }
}