using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyToDoApp.Application.Data;
using MyToDoApp.Infrastructure.Data;
using MyToDoApp.Infrastructure.Options;

namespace MyToDoApp.Infrastructure.Extensions.ServiceCollection;

public static partial class ServiceCollectionExtensions
{
	private static IServiceCollection AddToDoEntityFramework(this IServiceCollection services, IConfiguration configuration)
	{
		services
			.AddOptions<DatabaseOptions>()
			.Bind(configuration.GetRequiredSection(nameof(DatabaseOptions)))
			.Validate(opt => !string.IsNullOrWhiteSpace(opt.ConnectionString),
				$"{nameof(DatabaseOptions.ConnectionString)} is required.")
			.ValidateOnStart();
		
		services.AddDbContextPool<IToDoDbContext, ToDoDbContext>((sp, opt) =>
		{
			var env = sp.GetRequiredService<IHostEnvironment>();
			
			var databaseOptions = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
			opt.UseNpgsql(databaseOptions.ConnectionString);
			
			// Register 2nd level cache
			//opt.AddInterceptors(sp.GetRequiredService<SecondLevelCacheInterceptor>());
			
			if (env.IsProduction() is false)
			{
				opt.EnableSensitiveDataLogging();
			}

			var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
			opt.UseLoggerFactory(loggerFactory);
			
			opt.EnableDetailedErrors();
			opt.EnableSensitiveDataLogging();
			//opt.UseExceptionProcessor();
		});

		return services;
	}
}