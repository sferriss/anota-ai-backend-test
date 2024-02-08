using Catalog.Application.Commands.Categories.Create;
using Catalog.Application.Commands.Products.Create;
using Catalog.Application.Commands.Products.Delete;
using Catalog.Application.Commands.Products.Update;
using Catalog.Application.Queries.Products.Get;
using Catalog.Application.Queries.Products.GetAll;
using Catalog.Domain.Aggregations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers.v1;

[ApiController]
[Route("product")]
public class ProductController(ISender mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateCategoryCommandResult))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductCommand request)
    {
        var result = await mediator.Send(request);
        
        return Created(string.Empty, result);
    }
    
    [HttpPatch("{id:required}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateProductAsync([FromRoute] string id, [FromBody] UpdateProductCommand request)
    {
        await mediator.Send(request.WithId(id));
        
        return NoContent();
    }
    
    [HttpDelete("{id:required}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteProductAsync([FromRoute] string id)
    {
        await mediator.Send(new DeleteProductCommand(id));
        
        return NoContent();
    }
    
    [HttpGet("{id:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductWithCategoryResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetProductAsync([FromRoute] string id)
    {
        var result = await mediator.Send(new GetProductQuery(id));
        
        return Ok(result);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllProductsQueryResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllProductsAsync([FromQuery] string owner)
    {
        var result = await mediator.Send(new GetAllProductsQuery(owner));
        
        return Ok(result);
    }
}