using ErrorOr;
using MediatR;

namespace Application.Permissions.Create
{
    public class CreatePermissionCommand : IRequest<ErrorOr<Unit>>
    {
        public string NameEmployee { get; private set; }
        public string LastNameEmployee { get; private set; }
        public long PermissionTypeId { get; private set; }
        public DateTime Date { get; private set; }
        
        public CreatePermissionCommand(string nameEmployee, string lastNameEmployee, long permissionTypeId, DateTime date)
        {
            NameEmployee = nameEmployee;
            LastNameEmployee = lastNameEmployee;
            PermissionTypeId = permissionTypeId;
            Date = date;
        }

    }
}
