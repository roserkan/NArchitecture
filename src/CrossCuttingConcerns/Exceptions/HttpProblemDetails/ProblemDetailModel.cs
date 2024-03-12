using CrossCuttingConcerns.Exceptions.Types;

namespace CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class ProblemDetailModel
{
    public string? Title { get; set; }
    public string? Detail { get; set; }
    public int? Status { get; set; }
}


public class ValidationProblemDetailModel : ProblemDetailModel
{
    public IEnumerable<ValidationExceptionModel>? Errors { get; set; }
}