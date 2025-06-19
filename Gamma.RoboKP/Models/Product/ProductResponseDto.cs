using System.ComponentModel.DataAnnotations;

namespace Gamma.RoboKP.Models.Product;

public record ProductResponseDto(
    [Required]
    long Id,
    [Required]
    string Name,
    [Required]
    string Description,
    [Required]
    decimal Price,
    [Required]
    string ImageUrl,
    [Required]
    long SubCategoryId
    );