using Microsoft.EntityFrameworkCore;

namespace MyToDoApp.Application.Data;

public interface IToDoDbContext 
{
    DbSet<ToDoEntity> ToDos { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}