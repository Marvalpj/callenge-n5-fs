using Application.Permissions.Common;
using Domain.Permissions;
using Domain.Services;
using ErrorOr;
using MediatR;

namespace Application.Permissions.GetAll
{
    public class GetAllPermissionQueryHandler : IRequestHandler<GetAllPermissionQuery, ErrorOr<IEnumerable<PermissionResponse>>>
    {
        private readonly IPermissionRepository permissionRepository;
        private readonly IKafkaProducer kafkaProducer;

        public GetAllPermissionQueryHandler(IPermissionRepository permissionRepository, IKafkaProducer kafkaProducer)
        {
            this.permissionRepository = permissionRepository;
            this.kafkaProducer = kafkaProducer;
        }

        public async Task<ErrorOr<IEnumerable<PermissionResponse>>> Handle(GetAllPermissionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                await kafkaProducer.ProduceMessage("permission-topic", "get - permissions");
                
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
