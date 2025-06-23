using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Hosting;

namespace MyToDoApp.API.Extensions.ServiceCollection;

internal static class ApplicationBuilderExtensions
{
    [ExcludeFromCodeCoverage]
    public static WebApplication UseCustomSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(opt =>
        {
           opt.SwaggerEndpoint($"swagger/swagger.json", "MyToDoApp API");
           opt.RoutePrefix = string.Empty;
        });

        return app; 
    }
}