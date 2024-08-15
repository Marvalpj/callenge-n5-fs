using Application.PermissionTypes.Common;
using ErrorOr;
using MediatR;

namespace Application.PermissionTypes.GetById
{
    public class GetByIdPermissionTypeQuery : IRequest<ErrorOr<PermissionTypeResponse>>
    {
        public long Id { get; set; }
        public GetByIdPermissionTypeQuery(long id)
        {
            Id = id;
        }
    }
}
