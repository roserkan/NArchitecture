using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("operation_claims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("id").IsRequired();
        
        builder.Property(oc => oc.Name).HasColumnName("name").IsRequired();
        builder.Property(oc => oc.Alias).HasColumnName("alias");
        builder.Property(oc => oc.Description).HasColumnName("description");
        builder.Property(oc => oc.Status).HasColumnName("status");
        
        builder.Property(u => u.CreatedDate).HasColumnName("created_date").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("updated_date");
        builder.Property(u => u.DeletedDate).HasColumnName("deleted_date");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasMany(oc => oc.RoleOperationClaims);
    }
}