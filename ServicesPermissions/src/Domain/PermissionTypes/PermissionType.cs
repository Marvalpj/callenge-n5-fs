using Domain.Permissions;
using Domain.Primitives;

namespace Domain.PermissionTypes
{
    public sealed class PermissionType : AggregateRoot
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public ICollection<Permission>? Permissions { get; set; } // Colección de permisos
        
        public PermissionType()
        {
            Permissions = new List<Permission>();
        }

        public PermissionType(string description)
        {
            Description = description;
        }
        public PermissionType(long id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
