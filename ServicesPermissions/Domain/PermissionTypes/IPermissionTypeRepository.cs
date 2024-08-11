using Domain.PermissionTypes;

namespace Domain.PermissionTypes
{
    public interface IPermissionTypeRepository
    {
        Task<IEnumerable<PermissionType>> GetAllAsync();
        Task<PermissionType?> GetByIdAsync(long id);
        Task Add(PermissionType permissionType);
    }
}
