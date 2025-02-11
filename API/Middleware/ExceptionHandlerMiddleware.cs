using System.Net;
using System.Text.Json;
using API.Exceptions;

namespace API.Middleware;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (AlreadyExistException ex)
        {
            await HandleExceptionAsync(context, HttpStatusCode.Conflict, ex.Message);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    
    private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.Redirect($"/Home/Error?statusCode={(int)statusCode}&message={message}");
        return Task.CompletedTask;
    }
}