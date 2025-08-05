using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IO;

namespace MyToDoApp.Infrastructure.Extensions.ServiceCollection;

public static partial class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
		IHostEnvironment environment)
	{
		services.AddSingleton<RecyclableMemoryStreamManager>();

		return services
			.AddToDoEntityFramework(configuration);
	}
}