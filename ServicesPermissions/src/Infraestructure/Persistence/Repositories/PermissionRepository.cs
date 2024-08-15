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

        public async Task<IEnumerable<Permission>> GetAllAsync() => await this.context.Permissions.Include(p => p.PermissionType).ToListAsync();

        public async Task<Permission?> GetByIdAsync(long id) => await this.context.Permissions.Include(p => p.PermissionType).FirstOrDefaultAsync(p => p.Id == id);

        public void Update(Permission permission) => this.context.Permissions.Update(permission);
    }
}
