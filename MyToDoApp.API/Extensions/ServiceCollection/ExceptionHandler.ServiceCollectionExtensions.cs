

using MyToDoApp.API.Framework;

namespace MyToDoApp.API.Extensions.ServiceCollection;

internal static partial class ServiceCollectionExtensions
{
	private static IServiceCollection AddCustomGlobalExceptionHandler(this IServiceCollection services)
	{
		services.AddExceptionHandler<GlobalExceptionHandler>();
		services.AddProblemDetails();

		return services;
	}
}