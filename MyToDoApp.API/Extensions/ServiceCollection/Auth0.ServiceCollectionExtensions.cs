using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MyToDoApp.API.Extensions.ServiceCollection;

internal static partial class ServiceCollectionExtensions
{
	private static IServiceCollection AddAuth0Authorization(this IServiceCollection services)
	{
		services.AddAuthentication().AddJwtBearer();
		return services;
	}
}