using CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;

namespace CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class ValidationProblemDetails : ValidationProblemDetailModel
{
    public IEnumerable<ValidationExceptionModel> Errors { get; init; }

    public ValidationProblemDetails(IEnumerable<ValidationExceptionModel> errors)
    {
        Title = "Validation error(s)";
        Detail = "One or more validation errors occurred.";
        Errors = errors;
        Status = StatusCodes.Status400BadRequest;
    }
}
