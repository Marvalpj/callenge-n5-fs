using Domain.Permissions;
using Domain.PermissionTypes;
using Domain.Primitives;
using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Permissions.Create
{
    internal class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, ErrorOr<Unit>>
    {
        private readonly IPermissionRepository permissionRepository;
        private readonly IPermissionTypeRepository permissionTypeRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreatePermissionCommandHandler(IPermissionRepository permissionRepository, IPermissionTypeRepository permissionTypeRepository, IUnitOfWork unitOfWork)
        {
            this.permissionRepository = permissionRepository;
            this.permissionTypeRepository = permissionTypeRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Unit>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.NameEmployee))
                    return Error.Validation("Permission.NameEmployee", "Debe enviar el nombre de la descripcion");
                
                if (string.IsNullOrEmpty(request.LastNameEmployee))
                    return Error.Validation("Permission.LastNameEmployee", "Debe enviar el nombre de la descripcion");
                
                if (await permissionTypeRepository.GetByIdAsync(request.PermissionTypeId) is not PermissionType permissionType)
                    return Error.NotFound("PermissionType.NotFound", "El tipo de permiso con el id proporcionado no existe");



                Permission permission = new Permission(
                    request.NameEmployee,
                    request.LastNameEmployee,
                    request.PermissionTypeId,
                    request.Date
                );

                await permissionRepository.Add(permission);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                return Error.Failure("CreatePermissionType.Failure", ex.Message);
            }
        }
    }
}
