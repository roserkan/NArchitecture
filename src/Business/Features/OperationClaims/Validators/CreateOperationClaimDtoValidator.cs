using Business.Features.OperationClaims.Dtos;
using FluentValidation;

namespace Business.Features.OperationClaims.Validators;

public class CreateOperationClaimDtoValidator : AbstractValidator<CreateOperationClaimDto>
{
    public CreateOperationClaimDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(2).MaximumLength(50);
        RuleFor(x => x.Alias).MinimumLength(2).MaximumLength(50);
        RuleFor(x => x.Description).MinimumLength(2).MaximumLength(50);
    }
}