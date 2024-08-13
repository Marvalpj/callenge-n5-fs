using Application.Permissions.Common;
using Domain.Permissions;
using ErrorOr;
using MediatR;

namespace Application.Permissions.GetAll
{
    public class GetAllPermissionQueryHandler : IRequestHandler<GetAllPermissionQuery, ErrorOr<IEnumerable<PermissionResponse>>>
    {
        private readonly IPermissionRepository permissionRepository;

        public GetAllPermissionQueryHandler(IPermissionRepository permissionRepository)
        {
            this.permissionRepository = permissionRepository;
        }

        public async Task<ErrorOr<IEnumerable<PermissionResponse>>> Handle(GetAllPermissionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<Permission> permissionTypes = await permissionRepository.GetAllAsync();

                return permissionTypes.Select(p => new PermissionResponse(
                    p.Id,
                    p.NameEmployee,
                    p.LastNameEmployee,
                    p.PermissionTypeId,
                    p.Date
                )).ToList();

            }
            catch (Exception ex)
            {
                return Error.Failure("CreatePermissionType.Failure", ex.Message);
            }
        }
    }
}
