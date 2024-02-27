using clutch_identity.Exceptions;
using System.Net;

namespace clutch_identity.Infrastructure.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (HttpRequestException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpRequestException ex)
        {
            // Customize the error response based on the exception
            var statusCode = HttpStatusCode.InternalServerError;
            var exception = ex.InnerException.ToString();
            var message = ex.InnerException.Message;

            // Set the status code and error message based on the exception type
            if (ex.InnerException is DuplicateException)
            {
                statusCode = HttpStatusCode.Conflict;
                exception = ex.InnerException.ToString();
                message = ex.InnerException.Message;
            }
            if (ex.StatusCode != null)
            {
                statusCode = (HttpStatusCode)ex.StatusCode;
                exception = ex.InnerException.ToString();
                message = ex.InnerException.Message;
            }

            // Log the exception if needed

            // Set the response status code and content
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsJsonAsync(new
            {
                exception = exception,
                message = message,
                status = statusCode
            });
        }
    }
}
