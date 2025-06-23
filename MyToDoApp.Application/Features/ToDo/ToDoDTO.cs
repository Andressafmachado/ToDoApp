namespace MyToDoApp.Application.Features.ToDo;

public readonly record struct ToDoDTO(int Id, string Description, string Title, int Priority, int? ParentToDoId, string Auth0UserId, List<ToDoDTO>? SubTodos)
{
	public static ToDoDTO Map(ToDoEntity entity)
    {
        return new(entity.Id,
            entity.Description,
            entity.Title,
            entity.Priority,
            entity.ParentToDoId,
            entity.Auth0UserId,
            entity.SubTodos?.Select(Map).ToList());
    }
}