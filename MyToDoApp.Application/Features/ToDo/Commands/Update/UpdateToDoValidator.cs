using FluentValidation;

namespace MyToDoApp.Application.Features.ToDo.Commands.Update;

public class UpdateToDoValidator : AbstractValidator<ToDoEntity>
{
	public UpdateToDoValidator()
	{
// 		RuleFor(x => x.Title)
// 			.NotEmpty()
// 			.MaximumLength(255);
//
// 		RuleFor(x => x.SKU)
// 			.NotEmpty()
// 			.MaximumLength(255);
//
// 		RuleFor(x => x.Duration)
// 			.GreaterThan(TimeSpan.Zero);
// 		
// 		RuleFor(x => x.LessonCategories)
// 			.NotEmpty();
// 		
// 		RuleFor(x => x.Contents)
// 			.NotEmpty();
//
// 		RuleForEach(x => x.Contents)
// #pragma warning disable MA0045
// 			.Must(x => new ContentValidator().Validate(ContentDTO.Map(x)).IsValid);
// #pragma warning restore MA0045
//
// 		RuleFor(x => x.Contents)
// 			.Must(x => x.GroupBy(g => g.Order).All(f => f.Take(2).Count() == 1))
// 			.WithMessage(x => $"{nameof(x.Contents)} order must be unique");
	}
}