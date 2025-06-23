using EFCoreSecondLevelCacheInterceptor;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyToDoApp.Application.Data;

namespace MyToDoApp.Application.Features.ToDo.Queries;

public readonly struct GetAllToDosQueryAsync : IRequest<List<ToDoDTO>>;

internal sealed class GetAllToDosQueryHandler(IToDoDbContext dbContext)
    : IRequestHandler<GetAllToDosQueryAsync, List<ToDoDTO>>
{
    public async Task<List<ToDoDTO>> Handle(GetAllToDosQueryAsync request, CancellationToken cancellationToken)
    {
        var toDos = await dbContext.ToDos
            .Include(t => t.SubTodos)
            .Cacheable(CacheExpirationMode.Absolute, TimeSpan.FromDays(365))
            .ToListAsync(cancellationToken);

        return toDos.Select(ToDoDTO.Map).ToList();
    }
}

