using Newtonsoft.Json;
using Serilog.Events;
using System.Net;
using WebApiTemplate.SharedKernel.Enums;
using WebApiTemplate.SharedKernel.Exceptions;
using WebApiTemplate.SharedKernel.Interfaces;
using WebApiTemplate.SharedKernel.Models;

namespace WebApiTemplate.Api.Middlewares
{
    /// <summary>
    /// Middleware for handling exceptions and returning appropriate error responses in a web application.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="next"/> is null.</exception>
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// Invokes the middleware to handle exceptions and return error responses.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Invoke(HttpContext context)
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

        /// <summary>
        /// Handles an exception and returns an error response.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="exception">The exception to be handled.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errorResponse = CreateErrorResponse(context, exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = errorResponse.StatusCode;
            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
        }

        /// <summary>
        /// Creates an error response based on the given exception.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="exception">The exception for which to create an error response.</param>
        /// <returns>An error response containing error details.</returns>
        private static ErrorResponse CreateErrorResponse(HttpContext context, Exception exception)
        {
            var errors = new List<ErrorDetail>();
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var logger = context.RequestServices.GetRequiredService<ICustomLogger>();

            switch (exception)
            {
                case ValidationException ex:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    errors = new List<ErrorDetail>(ex.Errors);
                    break;
                case ResourceNotFoundException ex:
                    statusCode = (int)HttpStatusCode.NotFound;
                    errors.Add(new ErrorDetail(ErrorCode.ResourceNotFound.ToString(), ex.Message));
                    break;
                case UnauthorizedException ex:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    errors.Add(new ErrorDetail(ErrorCode.UnauthorizedAccess.ToString(), ex.Message));
                    break;
                case AuthenticationException ex:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    errors.Add(new ErrorDetail(ErrorCode.AuthenticationFailed.ToString(), ex.Message));
                    break;
                case DatabaseException ex:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    errors.Add(new ErrorDetail(ErrorCode.DatabaseError.ToString(), ex.Message));
                    break;
                case ConcurrencyException ex:
                    statusCode = (int)HttpStatusCode.Conflict;
                    errors.Add(new ErrorDetail(ErrorCode.ConcurrencyConflict.ToString(), ex.Message));
                    break;
                case RateLimitExceededException ex:
                    statusCode = (int)HttpStatusCode.TooManyRequests;
                    errors.Add(new ErrorDetail(ErrorCode.RateLimitExceeded.ToString(), ex.Message));
                    break;
                case InvalidInputException ex:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    errors = new List<ErrorDetail>(ex.Errors);
                    break;
                case ServiceUnavailableException ex:
                    statusCode = (int)HttpStatusCode.ServiceUnavailable;
                    errors.Add(new ErrorDetail(ErrorCode.ServiceUnavailable.ToString(), ex.Message));
                    break;
                case TimeoutException ex:
                    statusCode = (int)HttpStatusCode.RequestTimeout;
                    errors.Add(new ErrorDetail(ErrorCode.Timeout.ToString(), ex.Message));
                    break;
                case ConfigurationException ex:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    errors.Add(new ErrorDetail(ErrorCode.Configuration.ToString(), ex.Message));
                    break;
                case UnexpectedException ex:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    errors.Add(new ErrorDetail(ErrorCode.UnexpectedError.ToString(), ex.Message));
                    break;
                default:
                    var request = context.Request;
                    logger.LogMessage("An unexpected error occurred.", LogEventLevel.Fatal, exception);
                    errors.Add(new ErrorDetail(ErrorCode.UnexpectedError.ToString(),
                        "An unexpected error occurred.", $"Path: {request.Path} | Query: {JsonConvert.SerializeObject(request.Query)}"));
                    break;
            }

            return new ErrorResponse(statusCode, errors);
        }
    }
}
