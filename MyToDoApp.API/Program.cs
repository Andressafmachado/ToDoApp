using Microsoft.AspNetCore.Mvc;
using MyToDoApp.API.Extensions;
using MyToDoApp.API.Extensions.ServiceCollection;
using MyToDoApp.Application.Extensions.ServiceCollection;
using MyToDoApp.Infrastructure.Extensions.ServiceCollection;

[assembly: ApiController]

var builder = WebApplication.CreateSlimBuilder(args);
builder.Host.UseDefaultServiceProvider(opt => opt.ValidateScopes = true);

// -> The order of adding Options is important. Don't move it. <-

builder.Services
	.AddApplication(builder.Configuration)
	.AddInfrastructure(builder.Configuration, builder.Environment)
	.AddApi(builder.Environment, builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();
app.UseRequestTimeouts();

if (!app.Environment.IsProduction())
{
	app.UseToDoSwagger(app.Environment);
}

// app.UseCors("AIHR");
//app.UseAuthorization();

app.MapControllers();

// app.MapHealthChecks("/health/ready", new()
// {
// 	Predicate = healthCheck => healthCheck.Tags.Contains("ready")
// });

// app.MapHealthChecks("/health/self", new()
// {
// 	Predicate = healthCheck => healthCheck.Tags.Contains("self")
// });


await app.RunAsync();

namespace MyToDoApp.API
{
	public abstract class Program;
}