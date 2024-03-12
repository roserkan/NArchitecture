using CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using CrossCuttingConcerns.Exceptions.Types;
using System.Text.Json;

namespace Aspects.Autofac.Logging.Handlers;

public class LogHttpExceptionHandler : LogExceptionHandler
{

    public ProblemDetailModel? Response;


    protected override Task HandleException(BusinessException businessException)
    {
        Response = new BusinessProblemDetails(businessException.Message);
        return Task.CompletedTask;
    }

    protected override Task HandleException(ValidationException validationException)
    {
        Response = new CrossCuttingConcerns.Exceptions.HttpProblemDetails.ValidationProblemDetails(validationException.Errors);
        return Task.CompletedTask;
    }

    protected override Task HandleException(AuthorizationException authorizationException)
    {
        Response = new AuthorizationProblemDetails(authorizationException.Message);
        return Task.CompletedTask;
    }

    protected override Task HandleException(NotFoundException notFoundException)
    {
        Response = new NotFoundProblemDetails(notFoundException.Message);
        return Task.CompletedTask;
    }


    protected override Task HandleException(Exception exception)
    {
        Response = new InternalServerErrorProblemDetails(exception.Message);
        return Task.CompletedTask;
    }
}
