using DataAccess.Abstract;
using DataAccess.Common;
using DataAccess.Concrete.Contexts;
using Domain.Entities;

namespace DataAccess.Concrete.EfCore;

public class RoleRepository : EfRepositoryBase<Role, ProjectDbContext>, IRoleRepository
{
    public RoleRepository(ProjectDbContext context) : base(context)
    {
    }
}