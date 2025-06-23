using Microsoft.EntityFrameworkCore;
using MyToDoApp.Application.Data;
using MyToDoApp.Domain.Models;

namespace MyToDoApp.Infrastructure.Data;

public class ToDoDbContext : DbContext, IToDoDbContext
{
    public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
        : base(options) { }

    public DbSet<ToDo> ToDos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToDo>(builder =>
        {
            builder.HasKey(toDo => new { toDo.ParentToDoId, toDo.Priority, toDo.Auth0UserId });
        });
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoDbContext).Assembly);
    }
}
