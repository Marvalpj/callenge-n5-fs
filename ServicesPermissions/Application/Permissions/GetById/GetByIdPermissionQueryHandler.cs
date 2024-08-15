using Application.Permissions.Common;
using Domain.Permissions;
using Domain.Services;
using ErrorOr;
using MediatR;

namespace Application.Permissions.GetById
{
    public sealed class GetByIdPermissionQueryHandler : IRequestHandler<GetByIdPermissionQuery, ErrorOr<PermissionResponse>>
    {
        private readonly IPermissionRepository permissionRepository;
        private readonly IKafkaProducer kafkaProducer;

        public GetByIdPermissionQueryHandler(IPermissionRepository permissionRepository, IKafkaProducer kafkaProducer)
        {
            this.permissionRepository = permissionRepository;
            this.kafkaProducer = kafkaProducer;
        }

        public async Task<ErrorOr<PermissionResponse>> Handle(GetByIdPermissionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                await kafkaProducer.ProduceMessage("permission-topic", "get - permission");
                
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
