using System.Globalization;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyToDoApp.Domain.Exceptions;

namespace MyToDoApp.API.Framework;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
	private const string DefaultParameterName = "parameter";
	
	private static readonly JsonSerializerOptions JsonSerializerOptions = new()
	{
		PropertyNamingPolicy = JsonNamingPolicy.CamelCase
	};
	
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
		CancellationToken cancellationToken)
	{
		if (exception is BaseException {StatusCode: HttpStatusCode.NoContent})
		{
			httpContext.Response.StatusCode = StatusCodes.Status204NoContent;
			return true;
		}
		
		ProblemDetails problemDetails = exception switch
		{
			BaseException baseException => CreateBaseProblemDetailsResponse(httpContext, baseException),
			
			// ValidationException validationException => CreateValidationProblemDetailsResponse(httpContext,
			// 	validationException.Errors
			// 		.GroupBy(error => error.PropertyName, StringComparer.OrdinalIgnoreCase)
			// 		.ToDictionary(
			// 			errorGroup => errorGroup.Key,
			// 			errorGroup => errorGroup.Select(error => error.ErrorMessage).ToArray(),
			// 			StringComparer.OrdinalIgnoreCase
			// 		)),
			
			ArgumentException argumentException => CreateValidationProblemDetailsResponse(httpContext,
				new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
				{
					[argumentException.ParamName ?? DefaultParameterName] = [argumentException.Message]
				}),
			
			DbUpdateException dbUpdateException => CreateEFCoreProblemDetailsResponse(httpContext, dbUpdateException),
			
			_ => new()
			{
				Status = StatusCodes.Status500InternalServerError,
				Title = "Server error",
				Detail = "An unexpected error occurred.",
				Type = $"https://httpstatuses.io/{StatusCodes.Status500InternalServerError}",
				Instance = httpContext.Request.Path
			}
		};
		
		httpContext.Response.ContentType = "application/problem+json";
		httpContext.Response.StatusCode = problemDetails.Status!.Value;
		
		string response = JsonSerializer.Serialize(problemDetails, problemDetails.GetType(), JsonSerializerOptions);
		await httpContext.Response.WriteAsync(response, cancellationToken);
		
		return true;
	}
	
	private static ProblemDetails CreateBaseProblemDetailsResponse(HttpContext ctx, BaseException exception)
	{
		var statusCode = (int) exception.StatusCode;
		
		return new()
		{
			Status = statusCode,
			Title = exception.Message,
			Detail = exception.InnerException?.Message ?? string.Empty,
			Type = $"https://httpstatuses.io/{statusCode.ToString(CultureInfo.InvariantCulture)}",
			Instance = ctx.Request.Path
		};
	}
	
	private static ValidationProblemDetails CreateValidationProblemDetailsResponse(HttpContext ctx,
		IDictionary<string, string[]> errors)
	{
		const int badRequestStatusCode = (int) HttpStatusCode.BadRequest;
		
		return new(errors)
		{
			Status = badRequestStatusCode,
			Title = "One or more validation errors occurred.",
			Detail = "Please refer to the errors property for additional details.",
			Type = $"https://httpstatuses.io/{badRequestStatusCode}",
			Instance = ctx.Request.Path
		};
	}
	
	private static ValidationProblemDetails CreateEFCoreProblemDetailsResponse(HttpContext ctx, DbUpdateException exception)
	{
		var errors = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
		{
			[DefaultParameterName] = [exception.InnerException?.Message ?? exception.Message]
		};
		
		return CreateValidationProblemDetailsResponse(ctx, errors);
	}
}