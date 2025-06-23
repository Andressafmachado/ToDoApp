using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace MyToDoApp.API.Extensions;

internal static class ApplicationBuilderExtensions
{
	[ExcludeFromCodeCoverage]
	public static void UseToDoSwagger(this WebApplication app, IHostEnvironment env)
	{
		app.UseSwagger();

		app.UseSwaggerUI(opt =>
		{
			var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
			foreach (string groupName in provider.ApiVersionDescriptions.Select(x => x.GroupName))
			{
				opt.SwaggerEndpoint($"swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
			}

			opt.RoutePrefix = string.Empty;
		});
	}
}