using Aspects.Autofac.Validation;
using AutoMapper;
using Business.Features.OperationClaims.Constants;
using Business.Features.OperationClaims.Dtos;
using Business.Features.OperationClaims.Validators;
using CrossCuttingConcerns.Exceptions.Types;
using DataAccess.Abstract;
using Domain.Entities;

namespace Business.Features.OperationClaims.Services;

public class OperationClaimService : IOperationClaimService
{
    private readonly IOperationClaimRepository _repository;
    private readonly IMapper _mapper;
    
    public OperationClaimService(IOperationClaimRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [ValidationAspect(typeof(CreateOperationClaimDtoValidator))]
    public async Task CreateAsync(CreateOperationClaimDto dto)
    {
        await EnsureOperationClaimNameIsUnique(dto.Name);
        var entity = _mapper.Map<OperationClaim>(dto);
        await _repository.AddAsync(entity);
    }

    #region Rules
    private Task EnsureOperationClaimNameIsUnique(string name)
    {
        var isExist = _repository.Any(x => x.Name == name);
        if (isExist)
            throw new BusinessException(OperationClaimMessages.OperationClaimAlreadyExist);
        return Task.CompletedTask;
    }
    #endregion
}