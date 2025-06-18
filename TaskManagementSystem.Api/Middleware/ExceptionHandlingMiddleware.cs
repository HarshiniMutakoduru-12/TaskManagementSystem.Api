using System.Net;
using System.Text;
using System.Text.Json;
using TaskManagementSystem.Common.Constants;
using TaskManagementSystem.Common.Exceptions;
using TaskManagementSystem.Common.ResponseModel;

namespace TaskManagementSystem.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogInformation(await FormatRequest(context.Request));
                await _next(context);
                _logger.LogInformation($"Status Code : {context.Response.StatusCode}");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }

        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<ExceptionHandlingMiddleware> _logger)
        {

            context.Response.ContentType = "application/json";
            GenericResponses<string> response = new GenericResponses<string>();

            if (exception is AppException)
            {
                _logger.LogError($"Status Code : {context.Response.StatusCode}. AppException handled at {DateTime.UtcNow.TimeOfDay} With the Error Message '{exception.Message}'.");
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response = new GenericResponses<string>()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    ErrorMsg = exception.Message,
                    Response = null,
                    StatusMsg = ApiErrorCodeMessages.FAILURE
                };
            }
            else
            {
                _logger.LogError($"Status Code : {context.Response.StatusCode}.  Unknown Exception handled at {DateTime.UtcNow.TimeOfDay} With the Error Message '{exception.Message}' StackTrace '{exception.StackTrace}'.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response = new GenericResponses<string>()
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMsg = $"{exception.Message} | StackTrace : {exception.StackTrace}",
                    Response = null,
                    StatusMsg = ApiErrorCodeMessages.FAILURE
                };
            }

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        /// <summary>
        /// Returns the destination path in the Http request, the query if any, and the first 100 characters in the body if any, as a string to log.
        /// </summary>
        /// <param name="request">Http request instance</param>
        /// <returns></returns>
        private static async Task<string> FormatRequest(HttpRequest request)
        {
            try
            {
                request.EnableBuffering();
                var body = request.Body;

                var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                await request.Body.ReadAsync(buffer, 0, buffer.Length);
                var bodyAsText = Encoding.UTF8.GetString(buffer);
                body.Seek(0, SeekOrigin.Begin);
                request.Body = body;

                return $"{request.Scheme}://{request.Host}{request.Path}" +
                   (!string.IsNullOrEmpty(request.QueryString.ToString()) ? $" - QUERY: {request.QueryString}" : string.Empty) +
                    //(!string.IsNullOrEmpty(bodyAsText) ? (bodyAsText.Length < 100 ? $" - BODY: {bodyAsText}" : $" - BODY: {bodyAsText[..98]}..") : string.Empty);
                    (!string.IsNullOrEmpty(bodyAsText) ? $" - BODY: {bodyAsText}" : string.Empty);

            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message} - METHOD: RequestResponseLoggingMiddleware.FormatRequest()";
            }
        }

        /// <summary>
        /// Returns the status code from the http response and the first 100 characters in the body if any, as a string to log.
        /// </summary>
        /// <param name="response">Outgoing side of Http request instance</param>
        /// <returns></returns>
        private static async Task<string> FormatResponse(HttpResponse response)
        {
            try
            {
                response.Body.Seek(0, SeekOrigin.Begin);
                string text = await new StreamReader(response.Body).ReadToEndAsync();
                response.Body.Seek(0, SeekOrigin.Begin);

                //return $"Response {response.StatusCode}" +
                //  (!string.IsNullOrEmpty(text) ? (text.Length < 100 ? $" - BODY: {text}" : $" - BODY: {text[..98]}..") : string.Empty);
                return $"Response {response.StatusCode}" +
                  (!string.IsNullOrEmpty(text) ? $"- BODY : {text} " : string.Empty);

            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message} - METHOD: RequestResponseLoggingMiddleware.FormatResponse()";
            }
        }
    }
}
