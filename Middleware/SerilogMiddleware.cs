using Serilog;
using Serilog.Events;
using System.Diagnostics;

namespace ContactManagementV2.Middleware;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class SerilogMiddleware
{
    const string MessageTemplate =
        "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";

    static readonly Serilog.ILogger Log = Serilog.Log.ForContext<SerilogMiddleware>();
    private readonly RequestDelegate _next;

    public SerilogMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(HttpContext httpContext)
    {
        //Log.Warning("Unable to load some services");
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        var sw = Stopwatch.StartNew();
        try
        {
            await _next(httpContext);
            sw.Stop();

            var statusCode = httpContext.Response?.StatusCode;
            var level = statusCode > 499 ? LogEventLevel.Error : LogEventLevel.Information;

            var log = level == LogEventLevel.Error ? LogForErrorContext(httpContext) : Log;
            log.Write(level, MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, statusCode, sw.Elapsed.TotalMilliseconds);
        }
        // Never caught, because `LogException()` returns false.
        catch (Exception ex) when (LogException(httpContext, sw, ex)) { }


    }
    static bool LogException(HttpContext httpContext, Stopwatch sw, Exception ex)
    {
        sw.Stop();

        LogForErrorContext(httpContext)
            .Error(ex, MessageTemplate, httpContext.Request.Method, httpContext.Request.Path, 500, sw.Elapsed.TotalMilliseconds);

        return false;
    }
    static Serilog.ILogger LogForErrorContext(HttpContext httpContext)
    {
        var request = httpContext.Request;

        var result = Log
            .ForContext("RequestHeaders", request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
            .ForContext("RequestHost", request.Host)
            .ForContext("RequestProtocol", request.Protocol);

        if (request.HasFormContentType)
            result = result.ForContext("RequestForm", request.Form.ToDictionary(v => v.Key, v => v.Value.ToString()));
        
        return result;
    }

    }

// Extension method used to add the middleware to the HTTP request pipeline.
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SerilogMiddleware>();
    }
}
