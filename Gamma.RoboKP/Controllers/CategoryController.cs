using Gamma.RoboKP.Domain.Abstractions.Services;
using Gamma.RoboKP.Domain.Entities;
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

    [HttpGet("{id}")]
    public async Task<ActionResult<string?>> GetCategory(long id)
    {
        var entity = await categoryService.GetCategory(id);
        return entity != null ? Ok(entity.Id) : NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<List<string>>> GetCategories()
    {
        var entities = await categoryService.GetCategories();
        return Ok(entities.Select(entity => entity.Name));
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