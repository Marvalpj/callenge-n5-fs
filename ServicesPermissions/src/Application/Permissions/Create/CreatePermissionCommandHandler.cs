using Domain.DomainErrors;
using Domain.Permissions;
using Domain.PermissionTypes;
using Domain.Primitives;
using Domain.Services;
using ErrorOr;
using MediatR;

namespace Application.Permissions.Create
{
    public sealed class CreatePermissionCommandHandler : IRequestHandler<CreatePermissionCommand, ErrorOr<Unit>>
    {
        private readonly IPermissionRepository permissionRepository;
        private readonly IPermissionTypeRepository permissionTypeRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IKafkaProducer kafkaProducer;

        public CreatePermissionCommandHandler(
            IPermissionRepository permissionRepository, 
            IPermissionTypeRepository permissionTypeRepository,
            IUnitOfWork unitOfWork,
            IKafkaProducer kafkaProducer
        )
        {
            this.permissionRepository = permissionRepository;
            this.permissionTypeRepository = permissionTypeRepository;
            this.unitOfWork = unitOfWork;
            this.kafkaProducer = kafkaProducer;
        }

        public async Task<ErrorOr<Unit>> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await kafkaProducer.ProduceMessage("permission-topic", "request - permission");

                if (string.IsNullOrEmpty(request.NameEmployee))
                    return Errors.Permission.PermissionNameIsEmpty;


                if (string.IsNullOrEmpty(request.LastNameEmployee))
                    return Errors.Permission.PermissionLastNameIsEmpty;

                if (await permissionTypeRepository.GetByIdAsync(request.PermissionTypeId) is not PermissionType permissionType)
                    return Errors.Permission.PermissionTypeIdDoesNotExist;

                Permission permission = new Permission(
                    request.NameEmployee,
                    request.LastNameEmployee,
                    request.PermissionTypeId,
                    request.Date
                );

                await permissionRepository.Add(permission);

                //await unitOfWork.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                return Error.Failure("CreatePermissionType.Failure", ex.Message);
            }
        }
    }
}
