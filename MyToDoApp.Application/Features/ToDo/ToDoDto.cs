namespace MyToDoApp.Application.Features.ToDo;

public readonly record struct ToDoDto(int Id, string Description, string Title, int Priority, int? ParentToDoId, string Auth0UserId, List<ToDoDto>? SubTodos)
{
	public static ToDoDto Map(ToDoEntity entity)
    {
        return new ToDoDto(entity.Id,
            entity.Description,
            entity.Title,
            entity.Priority,
            entity.ParentToDoId,
            entity.Auth0UserId,
            entity.SubTodos?.Select(Map).ToList());
    }
}