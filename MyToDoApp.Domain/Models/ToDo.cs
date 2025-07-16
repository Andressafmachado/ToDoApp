using MyToDoApp.Domain.SharedKernel;

namespace MyToDoApp.Domain.Models;

public class ToDo : EntityBase<int>
{
    
    public ToDo(string title, string description, int priority, int? parentToDoId, string auth0UserId)
    {
        Title = title;
        Description = description;
        Priority = priority;
        ParentToDoId = parentToDoId;
        Auth0UserId = auth0UserId;
    }
    
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int Priority { get; private set; }
    public int? ParentToDoId { get; private set; }
    public string Auth0UserId { get; private set; }
    public ToDoEntity? ParentToDo { get; }
    public List<ToDoEntity>? SubTodos { get; private set; } = [];
    public List<int>? SubTodosIds { get; private set; } = [];

    public void Update(
        string title,
        string description,
        int priority,
        int? parentToDoId,
        IEnumerable<ToDoEntity> subToDos
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        Title = title;
        Description = description;
        Priority = priority;
        ParentToDoId = parentToDoId;
        SubTodos = subToDos.ToList();
    }
}