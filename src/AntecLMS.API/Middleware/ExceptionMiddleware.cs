using System.Net;
using System.Text.Json;
using AntecLMS.Application.Common.Exceptions;
using FluentValidation;

namespace AntecLMS.API.Middleware;

public class ExceptionMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionMiddleware> _logger;

  public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
  {
    _next = next;
    _logger = logger;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Unhandled exception");
      await HandleExceptionAsync(context, ex);
    }
  }

  private static Task HandleExceptionAsync(HttpContext context, Exception ex)
  {
    context.Response.ContentType = "application/json";

    object response;

    switch (ex)
    {
      case NotFoundException notFound:
        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        response = new { message = notFound.Message };
        break;

      case ForbiddenException:
        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
        response = new { message = ex.Message };
        break;

      case ValidationException validation:
        context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
        var errors = validation
          .Errors.GroupBy(e => e.PropertyName)
          .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());
        response = new { message = "Doğrulama xətası.", errors };
        break;

      case UnauthorizedAccessException:
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        response = new { message = "İcazə yoxdur." };
        break;

      default:
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        response = new { message = "Daxili server xətası." };
        break;
    }

    return context.Response.WriteAsync(
      JsonSerializer.Serialize(
        response,
        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
      )
    );
  }
}
