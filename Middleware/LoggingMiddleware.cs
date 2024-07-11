using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RestfulWebApi.Middleware
{
  public class LoggingMiddleware
  {
    private readonly RequestDelegate _next;

  private readonly ILogger<LoggingMiddleware> _logger;

     public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
      _next = next;
      _logger = logger;
    }

     public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
        //You can log all requests processed at the middleware layer.
        _logger.LogInformation($"Incoming Request: {context.Request.Protocol} {context.Request.Method} {context.Request.Path}{context.Request.QueryString}");
      }
      catch (Exception ex)
      {
        //You can log all requests processed at the HandleException.
        await HandleExceptionAsync(context, ex);
      }
    }
    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      //Here you can log all exceptions at the middleware layer.
        _logger.LogInformation(exception.Message + "\n" + exception.StackTrace);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error from the custom middleware.",
            Detailed = exception.Message
        };

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }

  }
}