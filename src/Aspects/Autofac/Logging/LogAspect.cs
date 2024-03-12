using Aspects.Autofac.Interceptors;
using Castle.DynamicProxy;
using CrossCuttingConcerns.IoC;
using CrossCuttingConcerns.Logging;
using CrossCuttingConcerns.Logging.Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Aspects.Autofac.Logging;

public class LogAspect : MethodInterception
{
    private readonly LoggerServiceBase _loggerServiceBase;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LogAspect(Type loggerService)
    {
        if (loggerService.BaseType != typeof(LoggerServiceBase))
        {
            throw new ArgumentException("AspectMessages.WrongLoggerType");
        }

        _loggerServiceBase = ServiceTool.ServiceProvider.GetService<LoggerServiceBase>();
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
    }

    protected override void OnAfter(IInvocation invocation)
    {
        _loggerServiceBase?.Info(GetLogDetail(invocation));
    }

    private string GetLogDetail(IInvocation invocation)
    {
        var logDetail = new LogDetail
        {
            TraceId = _httpContextAccessor.HttpContext?.TraceIdentifier,
            LogType = "Info",
            RequestIpAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString(),
            RequestPath = _httpContextAccessor.HttpContext?.Request.Path.Value,
            RequestQuery = _httpContextAccessor.HttpContext?.Request.QueryString.Value,
            RequestHeader = _httpContextAccessor.HttpContext?.Request.Headers,
            RequestBody = invocation.Arguments[0],
            HttpMethod = _httpContextAccessor.HttpContext?.Request.Method,
            StatusCode = 200,
            //ResponseBody = invocation.ReturnValue,
            User = (_httpContextAccessor.HttpContext == null ||
                     _httpContextAccessor.HttpContext.User.Identity.Name == null)
                 ? "?"
                 : _httpContextAccessor.HttpContext.User.Identity.ToString(),
            LogDate = DateTime.UtcNow
        };

        return JsonSerializer.Serialize(logDetail);
    }
}
