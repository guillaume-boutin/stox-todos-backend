using System.Text.Json;
using Application.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ProblemDetailsFactory _problemDetailsFactory;

    public ErrorHandlingMiddleware(RequestDelegate next, ProblemDetailsFactory problemDetailsFactory)
    {
        _next = next;
        _problemDetailsFactory = problemDetailsFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (exception is ValidationError)
            return OnValidationException(context, (ValidationError)exception);

        return OnException(context, exception);
    }

    private Task OnException(HttpContext context, Exception exception)
    {
        if (exception is ValidationError)
            return OnValidationException(context, (ValidationError)exception);

        var statusCode = exception switch
        {
            NotFoundError => 404,
            _ => 500
        };

        var problemDetails = _problemDetailsFactory.CreateProblemDetails(
            context,
            title: exception.Message,
            statusCode: statusCode
        );

        var code = problemDetails.Status ?? 500;
        var result = JsonSerializer.Serialize(problemDetails);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;

        return context.Response.WriteAsync(result);
    }

    private Task OnValidationException(HttpContext context, ValidationError exception)
    {
        var errors = new ModelStateDictionary();
        foreach (var failure in exception.Failures)
        {
            errors.AddModelError(failure.PropertyName, failure.ErrorMessage);
        }
        var problemDetails = _problemDetailsFactory.CreateValidationProblemDetails(
            context,
            errors,
            title: exception.Message,
            statusCode: 400
        );

        var code = problemDetails.Status ?? 500;
        var result = JsonSerializer.Serialize(problemDetails);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;

        return context.Response.WriteAsync(result);
    }
}
