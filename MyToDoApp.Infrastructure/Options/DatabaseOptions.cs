using System.Diagnostics.CodeAnalysis;

namespace MyToDoApp.Infrastructure.Options;

[ExcludeFromCodeCoverage]
public class DatabaseOptions
{
    public required string ConnectionString { get; set; }
}