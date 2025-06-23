namespace AIHR.LMS.API.Extensions.ServiceCollection;

internal static partial class ServiceCollectionExtensions
{
	public static IServiceCollection AddToDoApiVersioning(this IServiceCollection services)
	{
		services.AddApiVersioning(opt =>
		{
			opt.DefaultApiVersion = new(1, 0);
			opt.AssumeDefaultVersionWhenUnspecified = true;
			opt.ReportApiVersions = true;
		});

		services.AddVersionedApiExplorer(o =>
		{
			o.GroupNameFormat = "'v'VVV";
			o.SubstituteApiVersionInUrl = true;
		});

		return services;
	}
}