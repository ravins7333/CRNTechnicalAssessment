using System.Net;
using System.Text.Json;

namespace CRN.API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }


    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }


    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Something went wrong",
            Details = exception.Message
        };


        var json = JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}