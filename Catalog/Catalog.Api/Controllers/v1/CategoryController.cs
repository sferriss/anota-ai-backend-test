using Catalog.Application.Commands.Categories.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers.v1;

[ApiController]
[Route("category")]
public class CategoryController(ISender mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostCreateCategoryAsync([FromBody] CreateCategoryCommand request)
    {
        var result = await mediator.Send(request);
        
        return Created(string.Empty, result);
    }
}