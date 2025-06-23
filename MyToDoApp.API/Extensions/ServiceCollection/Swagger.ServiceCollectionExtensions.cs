using System.Reflection;
using Microsoft.OpenApi.Models;

namespace MyToDoApp.API.Extensions.ServiceCollection;

internal static partial class ServiceCollectionExtensions
{
	// ReSharper disable once UnusedMethodReturnValue.Local
	private static void AddToDoSwagger(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSwaggerGen(options =>
		{
			// Docs & Examples
			
			options.SwaggerDoc("v1", new()
			{
				Title = "TODO API",
				Version = "v1"
			});

			var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			//options.IncludeXmlComments(xmlPath);

			// Bearer token
			// options.AddSecurityDefinition("Bearer", new()
			// {
			// 	Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
			// 	Name = "Authorization",
			// 	In = ParameterLocation.Header,
			// 	Type = SecuritySchemeType.ApiKey,
			// 	Scheme = "Bearer"
			// });

			// options.AddSecurityRequirement(new()
			// {
			// 	{
			// 		new()
			// 		{
			// 			Reference = new()
			// 			{
			// 				Id = "Bearer",
			// 				Type = ReferenceType.SecurityScheme
			// 			}
			// 		},
			// 		Array.Empty<string>()
			// 	}
			// });
		
		});
	}
}