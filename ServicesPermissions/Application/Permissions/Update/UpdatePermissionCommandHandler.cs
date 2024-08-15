using Domain.Permissions;
using Domain.PermissionTypes;
using Domain.Primitives;
using Domain.Services;
using ErrorOr;
using MediatR;

namespace Application.Permissions.Update
{
    public sealed class UpdatePermissionCommandHandler : IRequestHandler<UpdatePermissionCommand, ErrorOr<Unit>>
    {
        private readonly IPermissionRepository permissionRepository;
        private readonly IPermissionTypeRepository permissionTypeRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IKafkaProducer kafkaProducer;

        public UpdatePermissionCommandHandler(
            IPermissionRepository permissionRepository,
            IPermissionTypeRepository permissionTypeRepository,
            IUnitOfWork unitOfWork,
            IKafkaProducer kafkaProducer)
        {
            this.permissionRepository = permissionRepository;
            this.permissionTypeRepository = permissionTypeRepository;
            this.unitOfWork = unitOfWork;
            this.kafkaProducer = kafkaProducer;
        }

        public async Task<ErrorOr<Unit>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {

            await kafkaProducer.ProduceMessage("permission-topic", "modify - permission");


            if (await permissionRepository.GetByIdAsync(request.Id) is not Permission permission)
                return Error.NotFound("Permission.NotFound", "El permiso con el id proporcionado no existe");

            if(request.PermissionTypeId != null )
                if (await permissionTypeRepository.GetByIdAsync(request.PermissionTypeId.Value) is not PermissionType permissionType)
                    return Error.NotFound("PermissionType.NotFound", "El tipo de permiso con el id proporcionado no existe");

            permission.UpdatePermission(request.NameEmployee, request.LastNameEmployee, request.PermissionTypeId, request.Date);

            permissionRepository.Update(permission);

            await unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
