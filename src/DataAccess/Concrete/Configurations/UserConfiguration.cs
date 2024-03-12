using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users").HasKey(u => u.Id);

        builder.Property(u => u.Id).HasColumnName("id").IsRequired();
        
        builder.Property(u => u.EmailAddress).HasColumnName("email_address").IsRequired();
        builder.Property(u => u.PasswordSalt).HasColumnName("password_salt").IsRequired();
        builder.Property(u => u.PasswordHash).HasColumnName("password_hash").IsRequired();
        builder.Property(u => u.Status).HasColumnName("status").HasDefaultValue(true);
        
        builder.Property(u => u.CreatedDate).HasColumnName("created_date").IsRequired();
        builder.Property(u => u.UpdatedDate).HasColumnName("updated_date");
        builder.Property(u => u.DeletedDate).HasColumnName("deleted_date");

        builder.HasQueryFilter(u => !u.DeletedDate.HasValue);

        builder.HasMany(u => u.UserRoles);
    }
}