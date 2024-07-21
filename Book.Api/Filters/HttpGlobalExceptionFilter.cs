using System.Net;
using Book.Api.Middleware.Extensions;
using Book.Infrastructure.Shared.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Book.Api.Middleware;

public class HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger) : IAsyncExceptionFilter
{

    public Task OnExceptionAsync(ExceptionContext context)
    {
        logger.LogError(new EventId(context.Exception.HResult), context.Exception, $"Failed api call: {context.HttpContext.Request.Path}.");
        context.ExceptionHandled = true;
        var exception = context.Exception;
        var httpContext = context.HttpContext;

        return exception switch
        {
            ValidationException validationException => HandleValidationException(httpContext, validationException),
            BadRequestException badRequestException => HandleBadRequestException(httpContext, badRequestException),
            ResourceNotFoundException notFoundException => HandleNotFoundException(httpContext, notFoundException),
            _ => HandleError(httpContext, exception),
        };
    }

    private Task HandleValidationException(HttpContext context, ValidationException validationException)
    {
        return context.WriteErrorAsync(HttpStatusCode.BadRequest, 
        validationException.Errors.Select(x => x.ErrorMessage).ToArray());
    }

    private Task HandleNotFoundException(HttpContext context, ResourceNotFoundException notFoundException)
    {
        return context.WriteErrorAsync(HttpStatusCode.NotFound, notFoundException.Message);
    }

    private Task HandleBadRequestException(HttpContext context, BadRequestException badHttpRequestException)
    {
        return context.WriteErrorAsync(HttpStatusCode.BadRequest, badHttpRequestException.Message);
    }

    private Task HandleError(HttpContext context, Exception exception)
    {
        return context.WriteErrorAsync(HttpStatusCode.InternalServerError, exception.Message);
    }
}