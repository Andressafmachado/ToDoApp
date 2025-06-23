using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyToDoApp.Infrastructure.Data.Configuration;

internal sealed class ToDoConfiguration : IEntityTypeConfiguration<ToDoEntity>
{
	public void Configure(EntityTypeBuilder<ToDoEntity> builder)
	{
		builder.HasKey(toDo => toDo.Id);
		builder.Property(toDo => toDo.Title).IsRequired();
		builder.Property(toDo => toDo.Description).IsRequired();
		builder.Property(toDo => toDo.Description).IsRequired();
		builder.Property(toDo => toDo.Priority).IsRequired();
		builder.Property(toDo => toDo.Auth0UserId).IsRequired();

        builder.HasIndex(toDo => new { toDo.Priority, toDo.Auth0UserId })
            .IsUnique()
            .HasFilter("\"ParentToDoId\" IS NULL");

        builder.HasIndex(toDo => new { toDo.ParentToDoId, toDo.Priority, toDo.Auth0UserId })
            .IsUnique()
            .HasFilter("\"ParentToDoId\" IS NOT NULL");

        builder
			.HasMany(toDo => toDo.SubTodos)
			.WithOne(toDo => toDo.ParentToDo)
			.HasForeignKey(t => t.ParentToDoId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}