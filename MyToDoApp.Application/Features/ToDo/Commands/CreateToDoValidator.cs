using FluentValidation;

namespace MyToDoApp.Application.Features.ToDo.Commands;

// ReSharper disable once UnusedType.Global
public class CreateToDoValidator : AbstractValidator<CreateToDoCommand>
{
	public CreateToDoValidator()
	{
		RuleFor(x => x.Title)
			.NotEmpty()
			.MaximumLength(255);

        //missing more logic here
        RuleFor(x => x.Priority)
            .NotEmpty();

		RuleFor(x => x.Auth0UserId)
			.NotEmpty();
        
        
	}
}