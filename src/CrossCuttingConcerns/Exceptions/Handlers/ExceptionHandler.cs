using CrossCuttingConcerns.Exceptions.Types;

namespace CrossCuttingConcerns.Exceptions.Handlers;

public abstract class ExceptionHandler
{
    public Task HandleExceptionAsync(Exception exception) =>
        exception switch
        {
            AggregateException aggregateException => HandleAggregateExceptionHandler(aggregateException),
            BusinessException businessException => HandleException(businessException),
            ValidationException validationException => HandleException(validationException),
            AuthorizationException authorizationException => HandleException(authorizationException),
            NotFoundException notFoundException => HandleException(notFoundException),
            _ => HandleException(exception)
        };

    protected abstract Task HandleException(BusinessException businessException);
    protected abstract Task HandleException(ValidationException validationException);
    protected abstract Task HandleException(AuthorizationException authorizationException);
    protected abstract Task HandleException(NotFoundException notFoundException);
    protected abstract Task HandleException(Exception exception);

    private async Task HandleAggregateExceptionHandler(AggregateException aggregateException)
    {
        if (aggregateException.InnerException != null)
            await HandleExceptionAsync(aggregateException.InnerException);
    }
}
