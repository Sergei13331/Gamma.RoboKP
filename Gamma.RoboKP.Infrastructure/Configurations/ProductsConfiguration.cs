using Gamma.RoboKP.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamma.RoboKP.Infrastructure.Configurations;

public class ProductsConfiguration : IEntityTypeConfiguration<Produсt>
{
    public void Configure(EntityTypeBuilder<Produсt> builder)
    {
        builder.HasKey(p => p.Id);
    }
}