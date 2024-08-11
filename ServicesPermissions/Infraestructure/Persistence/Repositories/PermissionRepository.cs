using Domain.Permissions;

namespace Infraestructure.Persistence.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        public PermissionRepository()
        {
            
        }
        public Task Add(Permission permission)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Permission>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Permission?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
