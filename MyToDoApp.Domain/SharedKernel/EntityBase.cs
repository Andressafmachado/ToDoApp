using System.ComponentModel.DataAnnotations;

namespace MyToDoApp.Domain.SharedKernel;

public abstract class EntityBase<TKey> : EntityBase
	where TKey : struct
{
	[Key]
	public TKey Id { get; protected init; }
}

public abstract class EntityBase
{
	public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
	
	public DateTimeOffset? UpdatedAt { get; private set; }

	public DateTimeOffset? DeletedAt { get; private set; }

	public bool IsDeleted => DeletedAt is not null;
	
	public void MarkAsUpdated()
	{
		UpdatedAt = DateTimeOffset.UtcNow;
	}
	
	public void MarkAsDeleted()
	{
		DeletedAt = DateTimeOffset.UtcNow;
	}
}