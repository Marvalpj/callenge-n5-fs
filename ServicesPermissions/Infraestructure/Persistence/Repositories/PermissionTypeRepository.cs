using Domain.PermissionTypes;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Repositories
{
    public class PermissionTypeRepository : IPermissionTypeRepository
    {
        private readonly ApplicationDbContext context;

        public PermissionTypeRepository(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(PermissionType permissionType) => await this.context.PermissionTypes.AddAsync(permissionType);

        public async Task<IEnumerable<PermissionType>> GetAllAsync() => await this.context.PermissionTypes.ToListAsync();

        public async Task<PermissionType?> GetByIdAsync(long id) => await this.context.PermissionTypes.FindAsync(id);
    }
}
