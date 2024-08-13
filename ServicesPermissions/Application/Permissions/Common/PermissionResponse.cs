using Domain.PermissionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Permissions.Common
{
    public class PermissionResponse
    {

        public long Id { get; private set; }
        public string NameEmployee { get; private set; }
        public string LastNameEmployee { get; private set; }
        public long PermissionTypeId { get; private set; }
        public DateTime Date { get; private set; }
        public string FullName => $"{NameEmployee} {LastNameEmployee}";
        public PermissionResponse()
        {

        }
        public PermissionResponse(long id, string nameEmployee, string lastNameEmployee, long permissionTypeId, DateTime date)
        {
            Id = id;
            NameEmployee = nameEmployee;
            LastNameEmployee = lastNameEmployee;
            PermissionTypeId = permissionTypeId;
            Date = date;
        }
    }
}
