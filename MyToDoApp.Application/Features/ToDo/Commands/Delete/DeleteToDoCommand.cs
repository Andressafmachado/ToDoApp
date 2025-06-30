using Microsoft.EntityFrameworkCore;
using MediatR;
using MyToDoApp.Application.Data;
using MyToDoApp.Domain.Exceptions;

namespace MyToDoApp.Application.Features.ToDo.Commands.Delete;

public readonly record struct DeleteToDoCommand(int Id) : IRequest;

internal sealed class DeleteLessonCommandHandler(IToDoDbContext dbContext) : IRequestHandler<DeleteToDoCommand>
{
	public async Task Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
	{
		var toDo = await dbContext.ToDos
			.Where(toDo => toDo.Id == request.Id)
			.SingleOrDefaultAsync(cancellationToken);

		if (toDo is null)
		{
			throw new ToDoNotFoundException(request.Id);
		}

		toDo.MarkAsDeleted();

		// to clean the cache?
		//await mediator.Publish(new LessonDeletedNotification(toDo), cancellationToken);

		await dbContext.SaveChangesAsync(cancellationToken);
	}
}