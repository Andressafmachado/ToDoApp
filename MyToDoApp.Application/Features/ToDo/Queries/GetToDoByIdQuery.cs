using EFCoreSecondLevelCacheInterceptor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyToDoApp.Application.Data;
using MyToDoApp.Domain.Exceptions;

namespace MyToDoApp.Application.Features.ToDo.Queries;

public readonly record struct GetToDoByIdQuery(int ToDoId) : IRequest<ToDoDto>;

internal sealed class GetToDoByIdQueryHandler(IToDoDbContext dbContext) : IRequestHandler<GetToDoByIdQuery, ToDoDto>
{
	public async Task<ToDoDto> Handle(GetToDoByIdQuery request, CancellationToken cancellationToken)
	{
		var toDo = await dbContext.ToDos
			.Include(toDo => toDo.SubTodos)
			.Cacheable(CacheExpirationMode.Absolute, TimeSpan.FromDays(365))
			.AsSplitQuery()
			.AsNoTracking()
			.SingleOrDefaultAsync(x => x.Id == request.ToDoId, cancellationToken);

		if (toDo is null)
		{
			throw new ToDoNotFoundException(request.ToDoId);
		}
		
		var result = ToDoDto.Map(toDo);
		return result;
	}
}