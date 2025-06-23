using System.Diagnostics.CodeAnalysis;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyToDoApp.Application.Extensions.ServiceCollection;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
            cfg.NotificationPublisherType = typeof(TaskWhenAllPublisher);
        });
        
        // services ===> CORS SHOULD BE API LAYER
	       //  .AddOptions<CorsOptions>()
	       //  .Bind(configuration.GetRequiredSection("Cors"))
	       //  .Validate(opt => opt.Origins is not {Count: 0}, $"{nameof(CorsOptions.Origins)} is required.")
	       //  .ValidateOnStart();

        return services;
    }
}