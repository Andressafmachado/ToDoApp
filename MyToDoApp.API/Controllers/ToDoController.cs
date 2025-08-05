using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyToDoApp.Application.Features.ToDo;
using MyToDoApp.Application.Features.ToDo.Commands;
using MyToDoApp.Application.Features.ToDo.Commands.Delete;
using MyToDoApp.Application.Features.ToDo.Commands.Update;
using MyToDoApp.Application.Features.ToDo.Queries;

namespace MyToDoApp.API.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public class ToDoController(IMediator mediator) : ControllerBase
{
    /// <summary>
    /// my comments here
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<ToDoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var toDos = await mediator.Send(new GetAllToDosQueryAsync(), cancellationToken);

        return Ok(toDos);
    }
    
    [HttpGet("{toDoId:int}")]
    [ProducesResponseType(typeof(ToDoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task<ToDoDto> GetAsync(int toDoId, CancellationToken cancellationToken)
    {
        return mediator.Send(new GetToDoByIdQuery(toDoId), cancellationToken);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(ToDoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
   public async Task<IActionResult> CreateAsync(CreateToDoCommand command, CancellationToken cancellationToken)
    {
        var toDo = await mediator.Send(command, cancellationToken);

        return Ok(toDo);
    }
   
   
   
    [HttpDelete("{toDoId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public Task DeleteAsync(int toDoId, CancellationToken cancellationToken)
    {
        return mediator.Send(new DeleteToDoCommand(toDoId), cancellationToken);
    }
    
    [HttpPut("{toDoId:int}")]
    [ProducesResponseType(typeof(ToDoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync(int toDoId, [FromBody] UpdateToDoDto updateRequestDto,
        CancellationToken cancellationToken)
    {
        var toDo = await mediator.Send(new UpdateToDoCommand(toDoId, updateRequestDto), cancellationToken);
    
        return Ok(toDo);
    }
    
    // delete - soft delete - global filters
    //.IgnoreQueryFilters() // when you want to see deleted items for example

    
    //validation - if todo pertence to a user before connecting with a subtodo
    
    // authentication - authorization //opt.AddOAuth2Authentication("oauth2", scheme =>
    // blazor webassembly
}