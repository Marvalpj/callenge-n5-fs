using Application.PermissionTypes.Common;
using Domain.PermissionTypes;
using Domain.Primitives;
using ErrorOr;
using MediatR;

namespace Application.PermissionTypes.Update
{
    public sealed class UpdatePermissionTypeCommandHandler : IRequestHandler<UpdatePermissionTypeCommand, ErrorOr<Unit>>
    {
        private readonly IPermissionTypeRepository permissionTypeRepository;
        private readonly IUnitOfWork unitOfWork;

        public UpdatePermissionTypeCommandHandler(IPermissionTypeRepository permissionTypeRepository, IUnitOfWork unitOfWork)
        {
            this.permissionTypeRepository = permissionTypeRepository;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ErrorOr<Unit>> Handle(UpdatePermissionTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if(string.IsNullOrEmpty(request.Description))
                    return Error.Validation("PermissionType.Description", "Debe enviar el nombre de la descripcion");

                if (await permissionTypeRepository.GetByIdAsync(request.Id) is not PermissionType permissionType)
                    return Error.NotFound("PermissionType.NotFound", "El tipo de permiso con el id proporcionado no existe");


                permissionType.Description = request.Description;

                permissionTypeRepository.Update(permissionType);

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
