using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using sk.Core.Exceptions;
using System.Net;
using System.Text.Json;

namespace sk.Core.Middlewares
{
    public class ExceptionHandlingMiddleware: IExceptionHandler
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Произошла ошибка: {Message}", exception.Message);

            int statusCode = exception switch
            {
                ParticipationException => StatusCodes.Status403Forbidden,
                UserNotFoundException => StatusCodes.Status404NotFound,
                RegLogException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            httpContext.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = "Ошибка сервера",
                Detail = exception.Message
            };

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
