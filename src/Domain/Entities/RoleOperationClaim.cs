using Domain.Common;

namespace Domain.Entities;

public class RoleOperationClaim : BaseEntity
{
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public int OperationClaimId { get; set; }
    public OperationClaim OperationClaim { get; set; }
}