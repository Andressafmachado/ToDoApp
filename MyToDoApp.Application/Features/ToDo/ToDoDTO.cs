namespace AIHR.LMS.Application.Features.Category;

public readonly record struct CategoryDTO(int Id, string ShortName, string FullName)
{
	public static CategoryDTO Map(CategoryEntity entity)
	{
		return new(entity.Id, entity.ShortName, entity.FullName);
	}
}