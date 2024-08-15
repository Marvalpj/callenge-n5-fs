using Application.Permissions.Common;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Permissions.GetById
{
    public class GetByIdPermissionQuery : IRequest<ErrorOr<PermissionResponse>>
    {
        public long Id { get; set; }
        public GetByIdPermissionQuery(long id)
        {
            Id = id;
        }
    }
}
