using DataAccess.Abstract;
using DataAccess.Common;
using DataAccess.Concrete.Contexts;
using Domain.Entities;

namespace DataAccess.Concrete.EfCore;

public class OperationClaimRepository : EfRepositoryBase<OperationClaim, ProjectDbContext>, IOperationClaimRepository
{
    public OperationClaimRepository(ProjectDbContext context) : base(context)
    {
    }
}