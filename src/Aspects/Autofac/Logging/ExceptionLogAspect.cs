using Aspects.Autofac.Interceptors;
using Aspects.Autofac.Logging.Handlers;
using Castle.DynamicProxy;
using CrossCuttingConcerns.Exceptions.Types;
using CrossCuttingConcerns.IoC;
using CrossCuttingConcerns.Logging;
using CrossCuttingConcerns.Logging.Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Aspects.Autofac.Logging;

public class ExceptionLogAspect : MethodInterception
{
    private readonly LoggerServiceBase _loggerServiceBase;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ExceptionLogAspect(Type loggerService)
    {
        if (loggerService.BaseType != typeof(LoggerServiceBase))
        {
            throw new ArgumentException("AspectMessages.WrongLoggerType");
        }

        _loggerServiceBase = ServiceTool.ServiceProvider.GetService<LoggerServiceBase>();
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    }

    protected override void OnException(IInvocation invocation, System.Exception e)
    {
        _loggerServiceBase?.Error(GetLogDetail(invocation, e));
    }

    private string GetLogDetail(IInvocation invocation, System.Exception e)
    {
        var logHandler = new LogHttpExceptionHandler();
        logHandler.HandleExceptionAsync(e).Wait();

        var logDetail = new LogDetail
        {
            TraceId = _httpContextAccessor.HttpContext?.TraceIdentifier,
            LogType = "Error",
            RequestIpAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString(),
            RequestPath = _httpContextAccessor.HttpContext?.Request.Path.Value,
            RequestQuery = _httpContextAccessor.HttpContext?.Request.QueryString.Value,
            RequestHeader = _httpContextAccessor.HttpContext?.Request.Headers,
            RequestBody = invocation.Arguments[0],
            HttpMethod = _httpContextAccessor.HttpContext?.Request.Method,
            StatusCode = logHandler.Response!.Status,
            ResponseBody = logHandler.Response,
            StackTrace = JsonSerializer.Serialize(e.StackTrace),
            User = (_httpContextAccessor.HttpContext == null ||
                     _httpContextAccessor.HttpContext.User.Identity.Name == null)
                 ? "?"
                 : _httpContextAccessor.HttpContext.User.Identity.ToString(),
            LogDate = DateTime.UtcNow
        };
        
        return JsonSerializer.Serialize(logDetail);
    }
}
