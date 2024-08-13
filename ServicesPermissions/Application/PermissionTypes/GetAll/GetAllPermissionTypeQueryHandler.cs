using Application.PermissionTypes.Common;
using Domain.PermissionTypes;
using ErrorOr;
using MediatR;

namespace Application.PermissionTypes.GetAll
{
    public sealed class GetAllPermissionTypeQueryHandler : IRequestHandler<GetAllPermissionTypeQuery, ErrorOr<IEnumerable<PermissionTypeResponse>>>
    {
        private readonly IPermissionTypeRepository permissionTypeRepository;

        public GetAllPermissionTypeQueryHandler(IPermissionTypeRepository permissionTypeRepository)
        {
            this.permissionTypeRepository = permissionTypeRepository;
        }

        public async Task<ErrorOr<IEnumerable<PermissionTypeResponse>>> Handle(GetAllPermissionTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<PermissionType> permissionTypes = await permissionTypeRepository.GetAllAsync();

                return permissionTypes.Select(permission => new PermissionTypeResponse(permission.Id, permission.Description)).ToList();

            }
            catch (Exception ex)
            {
                return Error.Failure("CreatePermissionType.Failure", ex.Message);
            }
        }

       
    }
}
