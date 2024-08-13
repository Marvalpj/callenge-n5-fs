using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PermissionTypes.Common
{
    public class PermissionTypeResponse
    {

        public long Id { get; set; }
        public string Description { get; set; }
        public PermissionTypeResponse()
        {
        }
        public PermissionTypeResponse(long id, string description)
        {
            Id = id;
            Description = description;
        }
        
    }
}
