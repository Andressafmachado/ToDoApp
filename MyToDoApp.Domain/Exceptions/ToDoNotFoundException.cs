using System.Globalization;
using System.Net;

namespace MyToDoApp.Domain.Exceptions;

public class ToDoNotFoundException(int lessonId, HttpStatusCode statusCode = HttpStatusCode.NoContent) 
	: BaseException($"To Do with id {lessonId.ToString(CultureInfo.InvariantCulture)} not found.", statusCode);