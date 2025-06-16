using System.Security.Claims;
using Gamma.RoboKP.Domain.Abstractions.Services;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Domain.Enums;
using Gamma.RoboKP.Models.Product;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gamma.RoboKP.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(
    IProductService productService,
    ISubCategoryService subCategoryService,
    [FromKeyedServices("ControllerMapper")] IMapper mapper) : ControllerBase
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    [HttpPost]
    public async Task<ActionResult<long>> AddProduct([FromBody] ProductToAddDto productDto)
    {
        var product = mapper.Map<ProductEntity>(productDto);
        
        var subCategory = await subCategoryService.GetSubCategory(productDto.SubCategoryId);
        if (subCategory == null) return NotFound("Подкатегория не найдена");
        
        product.SetCategory(subCategory.ParentCategoryId);
        
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

    [Authorize]
    [HttpGet("{id}/discount")]
    public async Task<ActionResult<ProductResponseDto>> GetProductByDiscount(long id)
    {
        var userStatus = User.FindFirst(ClaimTypes.UserData)?.Value;
        
        if (userStatus == null) return Unauthorized();
        
        var productEntity = await productService.GetProductWithDiscount(id, userStatus);
        if (productEntity == null) return NotFound();
        
        var response = mapper.Map<ProductResponseDto>(productEntity);
        
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ProductResponseDto>>> GetProducts()
    {
        var products = await productService.GetProducts();
        var response = mapper.Map<List<ProductResponseDto>>(products);
        return Ok(response);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<List<ProductResponseDto>>> SearchByName([FromRoute] string name)
    {
        var products = await productService.GetProductsByName(name);
        if (products == null) return NotFound();
        
        var response = mapper.Map<List<ProductResponseDto>>(products);
        
        return Ok(response);
    }

    [HttpGet("price/{price}")]
    public async Task<ActionResult<List<ProductResponseDto>>> SearchByPrice([FromRoute] decimal price)
    {
        var products = await productService.GetProductByPrice(price);
        if (products == null) return NotFound();
        
        var response = mapper.Map<List<ProductResponseDto>>(products);
        
        return Ok(response);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<ProductResponseDto>>> SearchProducts(
        [FromQuery] string? name,
        [FromQuery] decimal? exactPrice,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var result = await productService.SearchAndFilter(name, exactPrice, minPrice, maxPrice, page, pageSize);
        if (result is null) return NotFound();
        
        var response = mapper.Map<List<ProductResponseDto>>(result);
        return Ok(response);
    }
    
    [HttpPatch("{id}")]
    public async Task<ActionResult<long>> UpdateData([FromRoute]long id, [FromBody] ProductToUpdate productDto)
    {
        var result = await productService.UpdateProductData(id, productDto.Name, productDto.Description, productDto.Price);
        if (result == 0) return NotFound();
        return Ok(result);
    }

    [HttpPatch("{id}/image")]
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