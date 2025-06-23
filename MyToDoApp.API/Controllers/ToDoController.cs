using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyToDoApp.Application.Features.ToDo;
using MyToDoApp.Application.Features.ToDo.Commands;
using MyToDoApp.Application.Features.ToDo.Queries;

namespace MyToDoApp.API.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ToDoController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<ToDoDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var toDos = await mediator.Send(new GetAllToDosQueryAsync(), cancellationToken);

        return Ok(toDos);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(ToDoDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> CreateAsync(CreateToDoCommand command, CancellationToken cancellationToken)
    {
        var toDo = await mediator.Send(command, cancellationToken);

        return Ok(toDo);
    }
   
    // [HttpPut("{todoId:int}")]
    // [ProducesResponseType(typeof(ToDoDTO), StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public async Task<IActionResult> UpdateAsync(int toDoId, [FromBody] UpdateToDoDTO updateRequestDTO,
    //     CancellationToken cancellationToken)
    // {
    //     var toDo = await mediator.Send(new UpdateToDoCommand(toDoId, updateRequestDTO), cancellationToken);
    //
    //     return Ok(toDo);
    // }
}