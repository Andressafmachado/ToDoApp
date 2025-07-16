using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyToDoApp.Application.Data;
using MyToDoApp.Domain.Exceptions;

namespace MyToDoApp.Application.Features.ToDo.Commands.Update;

public record UpdateToDoDto(
	string Title,
	string Description,
	int Priority,
	int? ParentToDoId,
	string Auth0UserId,
	List<ToDoDto>? SubTodos
);


public record UpdateToDoCommand(int ToDoId, UpdateToDoDto ToDoDto) : IRequest<ToDoDto>;

internal sealed class UpdateToDoCommandHandler(IToDoDbContext dbContext) : IRequestHandler<UpdateToDoCommand, ToDoDto>
{
	public async Task<ToDoDto> Handle(UpdateToDoCommand request, CancellationToken cancellationToken)
	{
		var toDo = await dbContext.ToDos
			.Include(toDo => toDo.SubTodos)
			.SingleOrDefaultAsync(toDo => toDo.Id == request.ToDoId, cancellationToken);
		
		if (toDo is null)
		{
			throw new ToDoNotFoundException(request.ToDoId, HttpStatusCode.BadRequest);
		}
		
		ValidateToDo(request, toDo);
		
		// await ApplyToDoChangesAsync(toDo, request.ToDoDto, cancellationToken);
		
		//await mediator.Publish(new LessonUpdatedNotification(toDo), cancellationToken);
		
		return ToDoDto.Map(toDo);
	}
	
	private static void ValidateToDo(UpdateToDoCommand request, ToDoEntity toDo)
	{
		
		// validate that the user is the owner of the ToDo
		
	}
	
	// private async Task ApplyToDoChangesAsync(ToDoEntity toDoEntity, UpdateToDoDto toDoDto,
	// 	CancellationToken cancellationToken)
	// {
	// 	// var toDo = await _dbContext.ToDos
	// 	// 	.Include(t => t.SubTodos)
	// 	// 	.FirstOrDefaultAsync(t => t.Id == id);
	// 	// if (toDo == null) throw new NotFoundException();
	// 	// var subTodos = await _dbContext.ToDos
	// 	// 	.Where(t => dto.SubTodosIds.Contains(t.Id))
	// 	// 	.ToListAsync();
	// 	// toDo.Update(dto.Title, dto.Description, dto.Priority, dto.ParentToDoId, subTodos);
	// 	// await _dbContext.SaveChangesAsync();
	// 	
	// 	
	// 	
	// 	
	// 	// var toDo = await dbContext.ToDos
	// 	// 	.Include(t => t.SubTodos)
	// 	// 	.FirstOrDefaultAsync(t => t.Id == toDoDto);
	// 	// //if (toDo == null) throw new NotFoundException();
	// 	// var subTodos = await dbContext.ToDos
	// 	// 	.Where(t => toDoDto.SubTodosIds.Contains(t.Id))
	// 	// 	.ToListAsync();
	// 	// toDo.Update(dto.Title, dto.Description, dto.Priority, dto.ParentToDoId, subTodos);
	// 	// await _dbContext.SaveChangesAsync();
	// }
}