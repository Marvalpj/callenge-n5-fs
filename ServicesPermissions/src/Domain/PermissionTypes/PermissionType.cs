using Domain.Permissions;
using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
