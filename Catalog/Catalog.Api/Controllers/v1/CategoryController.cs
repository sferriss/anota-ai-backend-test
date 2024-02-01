using Catalog.Application.Commands.Categories.Create;
using Catalog.Application.Commands.Categories.Delete;
using Catalog.Application.Commands.Categories.Update;
using Catalog.Application.Queries.Categories.Common;
using Catalog.Application.Queries.Categories.Get;
using Catalog.Application.Queries.Categories.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers.v1;

[ApiController]
[Route("category")]
public class CategoryController(ISender mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCategoryCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategoryCommand request)
    {
        var result = await mediator.Send(request);
        
        return Created(string.Empty, result);
    }
    
    [HttpPatch("{id:required}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCategoryAsync([FromRoute] string id, [FromBody] UpdateCategoryCommand request)
    {
        await mediator.Send(request.WithId(id));
        
        return NoContent();
    }
    
    [HttpDelete("{id:required}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCategoryAsync([FromRoute] string id)
    {
        await mediator.Send(new DeleteCategoryCommand(id));
        
        return NoContent();
    }
    
    [HttpGet("{id:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetCategoryQueryResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCategoryAsync([FromRoute] string id)
    {
        var result = await mediator.Send(new GetCategoryQuery(id));
        
        return Ok(result);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllCategoriesQueryResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllCategoriesAsync()
    {
        var result = await mediator.Send(new GetAllCategoriesQuery());
        
        return Ok(result);
    }
}