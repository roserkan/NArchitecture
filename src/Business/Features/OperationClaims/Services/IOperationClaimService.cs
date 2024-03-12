using Business.Features.OperationClaims.Dtos;

namespace Business.Features.OperationClaims.Services;

public interface IOperationClaimService
{
    Task CreateAsync(CreateOperationClaimDto dto);
}