using Gamma.RoboKP.Domain.Abstractions.Services;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Models.Product;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Gamma.RoboKP.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(
    IProductService productService, 
    [FromKeyedServices("ControllerMapper")] IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<long>> AddProduct(ProductToAddDto productDto)
    {
        var product = mapper.Map<ProductToAddDto, ProductEntity>(productDto);
        
        var response = await productService.CreateProduct(product);
        
        return Ok(response);
    }
}