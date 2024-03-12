using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class RoleOperationClaimConfiguration : IEntityTypeConfiguration<RoleOperationClaim>
{
    public void Configure(EntityTypeBuilder<RoleOperationClaim> builder)
    {
        builder.ToTable("role_operation_claims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("id").IsRequired();
        
        builder.Property(oc => oc.RoleId).HasColumnName("role_id").IsRequired();
        builder.Property(oc => oc.OperationClaimId).HasColumnName("operation_claim_id").IsRequired();
        
        builder.Property(u => u.CreatedDate).HasColumnName("created_date").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("updated_date");
        builder.Property(u => u.DeletedDate).HasColumnName("deleted_date");

        builder.HasOne(uoc => uoc.OperationClaim);
        builder.HasOne(uoc => uoc.Role);
        
        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);
    }
}