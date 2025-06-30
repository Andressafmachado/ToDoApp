using MediatR;
using MyToDoApp.Application.Data;

namespace MyToDoApp.Application.Features.ToDo.Commands;

public readonly record struct CreateToDoCommand(
	string Title,
	string Description,
	int Priority,
	int? ParentToDoId,
	string Auth0UserId
	) : IRequest<ToDoDto>;

internal sealed class CreateLessonCommandHandler(IToDoDbContext dbContext) : IRequestHandler<CreateToDoCommand, ToDoDto>
{
	public async Task<ToDoDto> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
	{
		var item = new ToDoEntity(
			request.Title,
			request.Description,
			request.Priority,
			request.ParentToDoId,
			request.Auth0UserId
		);

		await dbContext.ToDos.AddAsync(item, cancellationToken);
		await dbContext.SaveChangesAsync(cancellationToken);

		return ToDoDto.Map(item);
	}
}