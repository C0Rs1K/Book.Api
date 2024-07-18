using System.Net;
using Book.Api.Middleware.Extensions;
using Book.Infrastructure.Shared.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Book.Api.Middleware;

public class HttpGlobalExceptionMiddleware(IHostEnvironment environment) : IExceptionHandler
{
    private readonly IHostEnvironment _environment = environment ?? throw new ArgumentNullException(nameof(environment));

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var task = exception switch
        {
            ValidationException validationException => HandleValidationException(httpContext, validationException),
            BadRequestException badRequestException => HandleBadRequestException(httpContext, badRequestException),
            ResourceNotFoundException notFoundException => HandleNotFoundException(httpContext, notFoundException),
            _ => HandleError(httpContext, exception),
        };

        await task;

        return true;
    }

    private Task HandleValidationException(HttpContext context, ValidationException validationException)
    {
        return context.WriteErrorAsync(HttpStatusCode.BadRequest,
        validationException.Errors.Select(x => x.ErrorMessage).ToArray());
    }

    private Task HandleNotFoundException(HttpContext context, ResourceNotFoundException notFoundException)
    {
        return context.WriteErrorAsync(HttpStatusCode.NotFound,
        _environment.IsDevelopment() ? notFoundException.Message : "Resource not found");
    }

    private Task HandleBadRequestException(HttpContext context, BadRequestException badHttpRequestException)
    {
        return context.WriteErrorAsync(HttpStatusCode.BadRequest,
            _environment.IsDevelopment() ? badHttpRequestException.Message : "Bad request");
    }

    private Task HandleError(HttpContext context, Exception exception)
    {
        return context.WriteErrorAsync(HttpStatusCode.InternalServerError,
            _environment.IsDevelopment() ? exception.Message : "An unexpected error occured");
    }
}