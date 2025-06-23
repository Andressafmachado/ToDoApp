using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json.Serialization;
using AIHR.LMS.API.Extensions.ServiceCollection;
using FluentValidation;
using MyToDoApp.Application.Features.ToDo.Commands;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace MyToDoApp.API.Extensions.ServiceCollection;

[ExcludeFromCodeCoverage]
internal static partial class ServiceCollectionExtensions
{
	private static readonly TimeSpan DefaultRequestTimeout = TimeSpan.FromSeconds(10);
	
	public static void AddApi(this IServiceCollection services, IWebHostEnvironment hostEnvironment,
		ConfigurationManager configuration)
	{
		services
			.AddControllers()
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
				options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
			});

		services.AddFluentValidationAutoValidation();
		// services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
		services.AddValidatorsFromAssembly(typeof(CreateToDoValidator).Assembly);

		services.AddRequestTimeouts(opt =>
		{
			opt.DefaultPolicy = new()
			{
				Timeout = configuration.GetValue<TimeSpan?>(nameof(DefaultRequestTimeout)) ?? DefaultRequestTimeout,
				TimeoutStatusCode = (int) HttpStatusCode.ServiceUnavailable,
				WriteTimeoutResponse = ctx => ctx.Response.WriteAsJsonAsync("Request timed out.", ctx.RequestAborted)
			};
		});

		services.AddHttpContextAccessor();
		services.AddProblemDetails();

		// Custom services
		services
			.AddToDoApiVersioning()
			.AddCustomGlobalExceptionHandler()
			.AddToDoSwagger(configuration);
		//
		// services.AddStartupTask<DatabaseMigratorStartupTask>();
		// services.AddStartupTask<DefaultCategoriesStartupTask>();
		// services.AddStartupTask<DefaultLabelsStartupTask>();
		// services.AddStartupTask<SearchSynchronizerStartupTask>();
		// services.AddStartupTask<DefaultBadgesStartupTask>();

		// if (hostEnvironment.IsLocal())
		// {
		// 	services.AddSingleton<IAuthorizationHandler, AllowAnonymousHandler>();
		// }
	}
	
	// public static IServiceCollection AddStartupTask<T>(this IServiceCollection services) where T : class, IStartupTask
	// 	=> services.AddTransient<IStartupTask, T>();
}