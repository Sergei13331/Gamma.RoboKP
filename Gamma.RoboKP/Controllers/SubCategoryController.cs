using Gamma.RoboKP.Domain.Abstractions.Services;
using Gamma.RoboKP.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Gamma.RoboKP.Controllers;

[ApiController]
[Route("api/subcategories")]
public class SubCategoryController(ISubCategoryService subCategoryService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<(long, long)>> CreateSubCategory(string name, long parentCategoryId)
    {
        var subCategoryEntity = SubCategoryEntity.Create(name, parentCategoryId);
        
        var response = await subCategoryService.CreateCategory(subCategoryEntity);
        
        return Ok(response);
    }
}