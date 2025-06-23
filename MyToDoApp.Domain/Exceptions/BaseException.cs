using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace MyToDoApp.Domain.Exceptions;

[ExcludeFromCodeCoverage]
public abstract class BaseException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest,
	Exception? exception = null) : Exception(message, exception)
{
	public HttpStatusCode StatusCode { get; } = statusCode;
}