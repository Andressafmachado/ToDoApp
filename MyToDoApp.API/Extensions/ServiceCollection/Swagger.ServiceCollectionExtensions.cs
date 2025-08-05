using System.Reflection;
using Microsoft.OpenApi.Models;

namespace MyToDoApp.API.Extensions.ServiceCollection;

internal static partial class ServiceCollectionExtensions
{
    private static void AddToDoSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            // Define Swagger doc
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TODO API",
                Version = "v1"
            });

            // Optional: Include XML comments (for method/documentation tooltips)
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; // this uses the summary from controller 
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            // Uncomment this line if XML comments are enabled in your .csproj
            options.IncludeXmlComments(xmlPath);

            // Add JWT Bearer support
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", // must be lowercase
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}