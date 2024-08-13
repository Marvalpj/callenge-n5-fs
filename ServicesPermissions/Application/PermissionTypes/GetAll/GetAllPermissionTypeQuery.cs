using Application.PermissionTypes.Common;
using ErrorOr;
using MediatR;

namespace Application.PermissionTypes.GetAll
{
    public class GetAllPermissionTypeQuery : IRequest<ErrorOr<IEnumerable<PermissionTypeResponse>>>
    {
        public GetAllPermissionTypeQuery()
        {
            
        }
    }
}
