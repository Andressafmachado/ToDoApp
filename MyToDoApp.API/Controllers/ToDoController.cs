using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyToDoApp.Application.Features.ToDo;
using MyToDoApp.Application.Features.ToDo.Queries;

namespace MyToDoApp.API.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CategoriesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<ToDoDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var toDos = await mediator.Send(new GetAllToDosQuery(), cancellationToken);

        return Ok(toDos);
    }
}