using System.Text.Json.Serialization;
using MediatR.NotificationPublishers;
using Microsoft.AspNetCore.Mvc;

[assembly: ApiController]


var builder = WebApplication.CreateSlimBuilder(args);
    
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
    cfg.NotificationPublisherType = typeof(TaskWhenAllPublisher);
});

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();