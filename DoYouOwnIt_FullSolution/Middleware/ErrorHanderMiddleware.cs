using Microsoft.Data.SqlClient;

namespace DoYouOwnIt.Api.Middleware
{
    public class ErrorHanderMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHanderMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DbUpdateException ex) when (IsDuplicateKeyException(ex))
            {
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                await context.Response.WriteAsJsonAsync(new
                {
                    Error = "Duplicate entry",
                    Message = "A record with this value already exists."
                });
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var result = new
            {
                error = "An unexpected error occurred.",
                details = exception.Message
            };
            return context.Response.WriteAsJsonAsync(result);
        }
        private static bool IsDuplicateKeyException(Exception ex)
        {
            // Check if the exception is a DbUpdateException and contains a unique constraint violation
            return ex.InnerException is SqlException sqlEx &&
                   (sqlEx.Number == 2601 || sqlEx.Number == 2627); // SQL Server error codes for unique constraint violation and duplicates
        }
    }
}
