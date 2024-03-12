using DataAccess.Common;
using Domain.Entities;

namespace DataAccess.Abstract;

public interface IOperationClaimRepository : IRepository<OperationClaim>, IAsyncRepository<OperationClaim>
{
}