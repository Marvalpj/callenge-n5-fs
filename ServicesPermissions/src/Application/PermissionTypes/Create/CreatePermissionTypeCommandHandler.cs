using Domain.PermissionTypes;
using Domain.Primitives;
using Domain.Services;
using ErrorOr;
using MediatR;

namespace Application.PermissionTypes.Create
{
    public sealed class CreatePermissionTypeCommandHandler : IRequestHandler<CreatePermissionTypeCommand, ErrorOr<Unit>>
    {
        private readonly IPermissionTypeRepository permissionTypeRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IKafkaProducer kafkaProducer;

        public CreatePermissionTypeCommandHandler(
            IPermissionTypeRepository permissionTypeRepository, 
            IUnitOfWork unitOfWork,
            IKafkaProducer kafkaProducer)
        {
            this.permissionTypeRepository = permissionTypeRepository ?? throw new ArgumentNullException(nameof(permissionTypeRepository));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork)); 
            this.kafkaProducer = kafkaProducer ?? throw new ArgumentNullException(nameof(kafkaProducer));
        }

        public async Task<ErrorOr<Unit>> Handle(CreatePermissionTypeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await kafkaProducer.ProduceMessage("permission-topic", "request - permissionType");

                if (string.IsNullOrEmpty(request.Description))
                    return Error.Validation("PermissionType.Description", "Debe enviar el nombre de la descripcion");

                PermissionType permissionType = new PermissionType(request.Description);

                await permissionTypeRepository.Add(permissionType);

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
