using Gamma.RoboKP.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamma.RoboKP.Infrastructure.Configurations;
//TODO:маграцию создал но она не применилась, вылезла ошибка
public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
{
    public void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasMany(s => s.Products)
            .WithOne(p => p.SubCategory);
        
        builder.HasOne(s => s.ParentCategory)
            .WithMany(s => s.SubCategories);
    }
}