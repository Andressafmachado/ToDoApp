using EFCoreSecondLevelCacheInterceptor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyToDoApp.Application.Data;

namespace MyToDoApp.Application.Features.ToDo.Queries;

public readonly struct GetAllToDosQueryAsync : IRequest<List<ToDoDto>>;

internal sealed class GetAllToDosQueryHandler(IToDoDbContext dbContext)
    : IRequestHandler<GetAllToDosQueryAsync, List<ToDoDto>>
{
    public async Task<List<ToDoDto>> Handle(GetAllToDosQueryAsync request, CancellationToken cancellationToken)
    {
        var toDos = await dbContext.ToDos
            .Include(toDo => toDo.SubTodos)
            .Cacheable(CacheExpirationMode.Absolute, TimeSpan.FromDays(365))
            .ToListAsync(cancellationToken);

        return toDos.Select(ToDoDto.Map).ToList();
    }
}

