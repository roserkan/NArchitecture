using DataAccess.Common;
using Domain.Entities;

namespace DataAccess.Abstract;

public interface IRoleRepository : IRepository<Role>, IAsyncRepository<Role>
{
}