using System.ComponentModel.DataAnnotations;

namespace Gamma.RoboKP.Infrastructure.Models;

public class SubCategory
{
    public long Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    public List<Product> Products { get; set; } = [];
    
    public long ParentCategoryId { get; set; }
    public Category ParentCategory { get; set; }
}