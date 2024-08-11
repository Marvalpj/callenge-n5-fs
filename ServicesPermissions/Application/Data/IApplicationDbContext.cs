using Domain.Permissions;
using Domain.PermissionTypes;
using Microsoft.EntityFrameworkCore;

namespace Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Permission> Permissions { get; set; }
        DbSet<PermissionType> PermissionTypes { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellation = default);

    }
}
