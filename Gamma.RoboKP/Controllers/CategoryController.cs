using Gamma.RoboKP.Domain.Abstractions.Services;
using Gamma.RoboKP.Domain.Entities;
using Gamma.RoboKP.Models.Category;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Gamma.RoboKP.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController([FromKeyedServices("ControllerMapper")] IMapper mapper, ICategoryService categoryService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<long>> CreateCategory(string name)
    {
        var categoryEntity = CategoryEntity.Create(name);
        
        var response = await categoryService.CreateCategory(categoryEntity);

        return Ok(response);
    }

    [HttpGet("{id}/with_subcategories")]
    public async Task<ActionResult<CategoryWithSubcategoriesResponseDto>> GetCategoryWithSubcategories(long id)
    {
        var category = await categoryService.GetSubCategoriesByCategory(id);
        
        if (category == null) return NotFound();
        
        var response = mapper.Map<CategoryWithSubcategoriesResponseDto>(category);
        
        return Ok(response);
    }

    [HttpGet("with_subcategories")]
    public async Task<ActionResult<List<CategoryWithSubcategoriesResponseDto>>> GetCategoriesWithSubCategories()
    {   
        var categories = await categoryService.GetCategoriesWithSubCategories();
        if (!categories.Any()) return NotFound();
        
        var response = mapper.Map<List<CategoryWithSubcategoriesResponseDto>>(categories);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryResponseDto?>> GetCategory(long id)
    {
        var entity = await categoryService.GetCategory(id);
        return entity != null ? Ok(mapper.Map<CategoryResponseDto>(entity)) : NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryResponseDto>>> GetCategories()
    {
        var entities = await categoryService.GetCategories();
        return Ok(mapper.Map<List<CategoryResponseDto>>(entities));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<long>> UpdateCategory(long id, string name)
    {
        var result = await categoryService.UpdateCategory(id, name);
        return result != 0 ? Ok(result) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(long id)
    {
        var result = await categoryService.DeleteCategory(id);
        return result ? Ok() : NotFound();
    }
} 