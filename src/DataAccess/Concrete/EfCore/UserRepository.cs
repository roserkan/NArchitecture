using DataAccess.Abstract;
using DataAccess.Common;
using DataAccess.Concrete.Contexts;
using Domain.Entities;

namespace DataAccess.Concrete.EfCore;

public class UserRepository : EfRepositoryBase<User, ProjectDbContext>, IUserRepository
{
    public UserRepository(ProjectDbContext context) : base(context)
    {
    }
}