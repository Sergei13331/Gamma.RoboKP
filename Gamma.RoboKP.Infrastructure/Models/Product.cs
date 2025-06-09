using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Gamma.RoboKP.Infrastructure.Models;

public class Product
{
    public long Id { get; set; }
    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;
    [Required]
    public decimal? Price { get; set; }
    [Required]
    [MaxLength(500)]
    public string ImageUrl { get; set; } = string.Empty;
}