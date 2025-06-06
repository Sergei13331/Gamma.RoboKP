using System.ComponentModel.DataAnnotations;

namespace Gamma.RoboKP.Models.Product;

public record ProductToAddDto(
    [Required]
    string Name,
    [Required]
    string Description,
    [Required]
    decimal Price,
    [Required]
    string ImageUrl
    );