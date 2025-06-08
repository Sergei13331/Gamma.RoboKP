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
        var product = mapper.Map<ProductEntity>(productDto);
        
        var response = await productService.CreateProduct(product);
        
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetProduct(long id)
    {
        var product = await productService.GetProduct(id);
        if (product == null) return NotFound();
        var response = mapper.Map<ProductResponseDto>(product);
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductResponseDto>>> GetProducts()
    {
        var products = await productService.GetProducts();
        var response = mapper.Map<List<ProductResponseDto>>(products);
        return Ok(response);
    }

    [HttpPatch("{id}/change-data")]
    public async Task<ActionResult<long>> UpdateData(long id, string name, string description, decimal price)
    {
        var result = await productService.UpdateProductData(id, name, description, price);
        if (result == 0) return NotFound();
        return Ok(result);
    }

    [HttpPatch("{id}/change-image")]
    public async Task<ActionResult<long>> UpdateImage(long id, string imageUrl)
    {
        var result = await productService.UpdateProductImage(id, imageUrl);
        if (result == 0) return NotFound();
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProduct(long id)
    {
        var result = await productService.RemoveProduct(id);
        if (!result) return NotFound();
        return Ok();
    }
}