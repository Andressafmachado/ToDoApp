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

internal sealed class UpdateToDoCommandHandler(IToDoDbContext dbContext, IValidator<ToDoEntity> validator) : IRequestHandler<UpdateToDoCommand, ToDoDto>
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
		
		await ApplyLessonChangesAsync(toDo, request.ToDoDto, cancellationToken);
		
		//await mediator.Publish(new LessonUpdatedNotification(toDo), cancellationToken);
		
		return ToDoDto.Map(toDo);
	}
	
	private static void ValidateToDo(UpdateToDoCommand request, ToDoEntity toDo)
	{
		
		// validate that the user is the owner of the ToDo
		
	}
	
	private async Task ApplyLessonChangesAsync(ToDoEntity toDoEntity, UpdateToDoDto toDoDto,
		CancellationToken cancellationToken)
	{
		toDoEntity.Update(
			title: toDoDto.Title,
			description: toDoDto.Description,
			priority: toDoDto.Priority,
			parentToDoId: toDoDto.ParentToDoId,
			subToDos: [] //how to map? can i use the same DTO.map
		);
		
		await validator.ValidateAndThrowAsync(toDoEntity, cancellationToken);
		dbContext.ToDos.Update(toDoEntity);

		await dbContext.SaveChangesAsync(cancellationToken);
	}
}