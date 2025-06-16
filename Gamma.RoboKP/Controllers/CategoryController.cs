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
} 