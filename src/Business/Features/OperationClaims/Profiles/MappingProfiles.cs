using AutoMapper;
using Business.Features.OperationClaims.Dtos;
using Domain.Entities;

namespace Business.Features.OperationClaims.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<OperationClaim, CreateOperationClaimDto>().ReverseMap();
    }
}