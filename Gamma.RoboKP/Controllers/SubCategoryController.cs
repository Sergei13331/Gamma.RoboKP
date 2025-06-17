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

    [HttpGet("{id}")]
    public async Task<ActionResult<(long, long)>> GetSubCategory(long id)
    {
        var subCategoryEntity = await subCategoryService.GetSubCategory(id);
        return subCategoryEntity != null ? Ok((subCategoryEntity.Name, subCategoryEntity.ParentCategoryId)) : NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<List<(long, long)>>> GetSubCategories()
    {
        var subCategories = await subCategoryService.GetSubCategories();
        return Ok(subCategories.Select(e => (e.Name, e.ParentCategoryId)));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<(long, long)>> UpdateSubCategory(long id, string name, long parentCategoryId)
    {
        var result = await subCategoryService.UpdateSubCategory(id, name, parentCategoryId);
        return result.Item1 != 0 &&  result.Item2 != 0 ? Ok((result.Item1, result.Item2)) : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSubCategory(long id)
    {
        var result = await subCategoryService.DeleteSubCategory(id);
        return result ? Ok() : NotFound();
    }
}