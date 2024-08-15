using Application.PermissionTypes.Common;
using Domain.PermissionTypes;
using Domain.Services;
using ErrorOr;
using MediatR;

namespace Application.PermissionTypes.GetById
{
    public sealed class GetByIdPermissionTypeQueryHandler : IRequestHandler<GetByIdPermissionTypeQuery, ErrorOr<PermissionTypeResponse>>
    {
        private readonly IPermissionTypeRepository permissionTypeRepository;
        private readonly IKafkaProducer kafkaProducer;

        public GetByIdPermissionTypeQueryHandler(IPermissionTypeRepository permissionTypeRepository, IKafkaProducer kafkaProducer)
        {
            this.permissionTypeRepository = permissionTypeRepository;
            this.kafkaProducer = kafkaProducer;
        }
        public async Task<ErrorOr<PermissionTypeResponse>> Handle(GetByIdPermissionTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                await kafkaProducer.ProduceMessage("permission-topic", "get - permissionType");

                if (await permissionTypeRepository.GetByIdAsync(request.Id) is not PermissionType permissionType)
                    return Error.NotFound("PermissionType.NotFound" , "El tipo de permiso con el id proporcionado no existe");

                return new PermissionTypeResponse(permissionType.Id, permissionType.Description);

            }
            catch (Exception ex)
            {
                return Error.Failure("CreatePermissionType.Failure", ex.Message);
            }
        }
    }
}
