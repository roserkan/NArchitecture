using DataAccess.Common;
using Domain.Entities;

namespace DataAccess.Abstract;

public interface IUserRepository : IRepository<User>, IAsyncRepository<User>
{
}