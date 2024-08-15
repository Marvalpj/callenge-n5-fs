using Domain.PermissionTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configuration
{
    public class PermissionTypeConfiguration : IEntityTypeConfiguration<PermissionType>
    {
        public void Configure(EntityTypeBuilder<PermissionType> builder)
        {
            builder.HasKey(x => x.Id);
            // id identity
            builder.Property(x => x.Id)
               .ValueGeneratedOnAdd();

            builder.Property(x => x.Description)
                   .IsRequired();
        }
    }
}
