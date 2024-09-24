using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
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

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        if (ex is ValidationException validationException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var validationResponse = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Ошибка валидации данных",
                Errors = validationException.Errors.Select(e => new
                {
                    Field = e.PropertyName,
                    Error = e.ErrorMessage
                })
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(validationResponse));
        }


        if(ex is KeyNotFoundException keyNotFoundException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            var errorResponse = new { StatusCode = context.Response.StatusCode };
            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }

        if(ex is UnauthorizedAccessException unauthorized)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            var errorResponse = new { StatusCode = context.Response.StatusCode };
            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }

        if (ex is ArgumentException argumentException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errorResponse = new
            {
                StatusCode = context.Response.StatusCode,
                Message = argumentException.Message
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }

        // Обработка по умолчанию
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var defaultErrorResponse = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Произошла ошибка в обработке запроса",
            Details = ex.Message
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(defaultErrorResponse));

    }
}
