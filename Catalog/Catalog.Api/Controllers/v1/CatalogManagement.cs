using Catalog.Api.Models.Requests;
using Catalog.Api.Models.Responses;
using Catalog.Domain.Categories.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers.v1;

[ApiController]
[Route("catalog-management")]
public class CatalogManagement : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostCreateCategoryAsync([FromBody] CreateCategoryRequest request)
    {
        // repository.Add(new()
        // {
        //     Description = request.Description,
        //     Owner = request.Owner,
        //     Title = request.Tile
        // });
        
        return Created(string.Empty, new CreateCategoryResponse(Guid.NewGuid().ToString()));
    }
}