using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("user_roles").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("id").IsRequired();
        
        builder.Property(oc => oc.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(oc => oc.RoleId).HasColumnName("role_id").IsRequired();
        
        builder.Property(u => u.CreatedDate).HasColumnName("created_date").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("updated_date");
        builder.Property(u => u.DeletedDate).HasColumnName("deleted_date");

        builder.HasOne(uoc => uoc.User);
        builder.HasOne(uoc => uoc.Role);
        
        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);
    }
}