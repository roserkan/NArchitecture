using Domain.Common;

namespace Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; }
    public string? Alias { get; set; }
    public string? Description { get; set; }
    public bool Status { get; set; }
    
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<RoleOperationClaim> RoleOperationClaims { get; set; }
}