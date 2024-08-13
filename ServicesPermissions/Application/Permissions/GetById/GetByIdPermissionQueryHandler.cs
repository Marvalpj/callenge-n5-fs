using Application.Permissions.Common;
using Domain.Permissions;
using ErrorOr;
using MediatR;

namespace Application.Permissions.GetById
{
    internal class GetByIdPermissionQueryHandler : IRequestHandler<GetByIdPermissionQuery, ErrorOr<PermissionResponse>>
    {
        private readonly IPermissionRepository permissionRepository;

        public GetByIdPermissionQueryHandler(IPermissionRepository permissionRepository)
        {
            this.permissionRepository = permissionRepository;
        }

        public async Task<ErrorOr<PermissionResponse>> Handle(GetByIdPermissionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (await permissionRepository.GetByIdAsync(request.Id) is not Permission p)
                    return Error.NotFound("PermissionType.NotFound", "El tipo de permiso con el id proporcionado no existe");

                return new PermissionResponse(
                    p.Id,
                    p.NameEmployee,
                    p.LastNameEmployee,
                    p.PermissionTypeId,
                    p.Date
                );

            }
            catch (Exception ex)
            {
                return Error.Failure("CreatePermissionType.Failure", ex.Message);
            }
        }
    }
}
