using Domain.PermissionTypes;
using Domain.Primitives;

namespace Domain.Permissions
{
    public sealed class Permission : AggregateRoot
    {
        public long Id { get; private set; }
        public string NameEmployee { get; private set; }
        public string LastNameEmployee { get; private set; }
        public long PermissionTypeId { get; private set; } // Clave foránea
        public PermissionType PermissionType { get; private set; } // Navegación a PermissionType
        public DateTime Date { get; private set; }
        public string FullName => $"{NameEmployee} {LastNameEmployee}";

        public Permission()
        {
            
        }

        public Permission(long id, string nameEmployee, string lastNameEmployee, long permissionTypeId, DateTime date)
        {
            Id = id;
            NameEmployee = nameEmployee;
            LastNameEmployee = lastNameEmployee;
            PermissionTypeId = permissionTypeId;
            Date = date;
        }
    }
}
