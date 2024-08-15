using Application.Permissions.Common;
using ErrorOr;
using MediatR;

namespace Application.Permissions.GetAll
{
    public class GetAllPermissionQuery : IRequest<ErrorOr<IEnumerable<PermissionResponse>>>
    {
        public GetAllPermissionQuery()
        {

        }
    }
}
