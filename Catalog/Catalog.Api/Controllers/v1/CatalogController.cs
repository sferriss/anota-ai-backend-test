using Catalog.Application.Queries.Catalog.Get;
using Catalog.Domain.Files;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers.v1;

[ApiController]
[Route("catalog")]
public class CatalogController(ISender mediator) : ControllerBase
{
    [HttpGet("{ownerId:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCatalogAsync([FromRoute] string ownerId)
    {
        var result = await mediator.Send(new GetCatalogQuery(ownerId));
        
        return Ok(result.File);
    }
}