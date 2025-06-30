namespace SistemaOrganizacaoEstudantil.Middleware;

using SistemaOrganizacaoEstudantil.Exceptions;

using System.Text.Json;

public class ExceptionHandlerMiddleware
{
    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (HttpException exception)
        {
            context.Response.StatusCode = exception.Status;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(new {
                message = exception.Message
            });

            await context.Response.WriteAsync(json);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An unhandled exception occurred");

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var json = JsonSerializer.Serialize(new {
                message = "An internal server error occurred."
            });

            await context.Response.WriteAsync(json);
        }
    }

    private readonly RequestDelegate next;
    private readonly ILogger logger;
}
