using Domain.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configuration
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {

            builder.HasKey(x => x.Id);
            
            // id identity
            builder.Property(x => x.Id)
               .ValueGeneratedOnAdd();
            
            builder.Property(x => x.NameEmployee)
                   .IsRequired();
            
            builder.Property(x => x.LastNameEmployee)
                   .IsRequired();
            
            builder.Property(x => x.PermissionTypeId)
                   .IsRequired(); 
            
            builder.Property(x => x.Date)
                   .IsRequired();

            builder.Ignore(x => x.FullName);

            // Configura la relación con PermissionType
            builder.HasOne(x => x.PermissionType) // Relación de uno a muchos
                   .WithMany() 
                   .HasForeignKey(x => x.PermissionTypeId); // Clave foránea


        }
    }
}
