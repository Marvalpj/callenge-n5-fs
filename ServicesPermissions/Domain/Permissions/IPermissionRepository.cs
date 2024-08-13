
namespace Domain.Permissions
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<Permission?> GetByIdAsync(long id);
        Task Add(Permission permission);
        void Update(Permission permission);
    }
}
