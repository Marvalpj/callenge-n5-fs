using Domain.Permissions;
using Microsoft.EntityFrameworkCore;
using System.Security;

namespace Infraestructure.Persistence.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext context;

        public PermissionRepository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task Add(Permission permission) => await this.context.Permissions.AddAsync(permission);

        public async Task<IEnumerable<Permission>> GetAllAsync() => await this.context.Permissions.ToListAsync();

        public async Task<Permission?> GetByIdAsync(long id) => await this.context.Permissions.FindAsync(id);

        public void Update(Permission permission) => this.context.Permissions.Update(permission);
    }
}
