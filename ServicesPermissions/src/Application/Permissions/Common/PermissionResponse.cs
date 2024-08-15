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

        public long Id { get; set; }
        public string NameEmployee { get; set; }
        public string LastNameEmployee { get; set; }
        public long PermissionTypeId { get; set; }
        public string PermissionType { get; set; }
        public DateTime Date { get; set; }
        public string FullName => $"{NameEmployee} {LastNameEmployee}";
        public PermissionResponse()
        {

        }
        public PermissionResponse(long id, string nameEmployee, string lastNameEmployee, long permissionTypeId, string permissionType, DateTime date)
        {
            Id = id;
            NameEmployee = nameEmployee;
            LastNameEmployee = lastNameEmployee;
            PermissionTypeId = permissionTypeId;
            PermissionType = permissionType;
            Date = date;
        }
    }
}
