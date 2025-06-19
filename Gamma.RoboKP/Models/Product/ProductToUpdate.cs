using System.ComponentModel.DataAnnotations;

namespace Gamma.RoboKP.Models.Product;

public record ProductToUpdate(
    [MaxLength(255)]
    string Name,
    [MaxLength(500)]
    string Description,
    decimal Price,
    long SubCategoryId
);
    
