using Microsoft.IO;
using System.Diagnostics;

namespace Transflo.Assessment.Api.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            await LogRequest(context);
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using MemoryStream requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            _logger.LogDebug($"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Method:{context.Request.Method} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}");
            context.Request.Body.Position = 0;
        }

        private async Task LogResponse(HttpContext context)
        {
            Stream originalBodyStream = context.Response.Body;

            await using MemoryStream responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            long start = Stopwatch.GetTimestamp();

            await _next(context);
            double elapsedMs = GetElapsedMilliseconds(start, Stopwatch.GetTimestamp());

            try
            {
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                string text = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                _logger.LogDebug($"Http Response Information:{Environment.NewLine}" +
                                             $"Schema:{context.Request.Scheme} " +
                                             $"Method:{context.Request.Method} " +
                                             $"Host: {context.Request.Host} " +
                                             $"Path: {context.Request.Path} " +
                                             $"Status: {context.Response.StatusCode} " +
                                             $"Elapsed {elapsedMs:0.0000} ms " +
                                             $"QueryString: {context.Request.QueryString} " +
                                             $"Response Body: {text}");
            }
            catch { }


            await responseBody.CopyToAsync(originalBodyStream);
        }

        private static double GetElapsedMilliseconds(long start, long stop)
        {
            return (stop - start) * 1000 / (double)Stopwatch.Frequency;
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using StringWriter textWriter = new StringWriter();
            using StreamReader reader = new StreamReader(stream);

            char[] readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
    }
}