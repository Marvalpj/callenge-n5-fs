
namespace Domain.Permissions
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();
        Task<Permission?> GetByIdAsync(long id);
        Task Add(Permission permission);
        Task UpdateAsync(long id, string? nameEmployee, string? lastNameEmployee, long? permissionTypeId, DateTime? date);
    }
}
