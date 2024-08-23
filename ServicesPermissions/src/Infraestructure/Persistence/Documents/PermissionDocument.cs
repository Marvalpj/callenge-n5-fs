using Domain.PermissionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Persistence.Documents
{
    public class PermissionDocument
    {
        public long Id { get; set; }
        public string NameEmployee { get; set; }
        public string LastNameEmployee { get; set; }
        public long PermissionTypeId { get; set; }
        public DateTime Date { get; set; }
    }
}
