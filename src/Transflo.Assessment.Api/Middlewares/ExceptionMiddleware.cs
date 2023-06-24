using System.Net;
using System.Text.Json;
using Transflo.Assessment.Api.Responses;
using Transflo.Assessment.Shared.Models;

namespace Transflo.Assessment.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }            
            catch (Exception ex)
            {

                _logger.LogError(ex,"Technicak Error while excute the request");
                await HandleExceptionAsync(context);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(JsonSerializer.Serialize(
                new FailedWithTechnicalErrorApiResponse<object>(new List<Error> {
                    new Error() { Message= "Something went wrong, please try again later..." , Code=(int)HttpStatusCode.InternalServerError, } })));
        }
    }
}
