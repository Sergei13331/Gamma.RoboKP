using System.ComponentModel.DataAnnotations;

namespace Gamma.RoboKP.Infrastructure.Models;

public class Category
{
    public long Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    public List<SubCategory> SubCategories { get; set; } = [];
    public List<Product> Products { get; set; } = [];
}