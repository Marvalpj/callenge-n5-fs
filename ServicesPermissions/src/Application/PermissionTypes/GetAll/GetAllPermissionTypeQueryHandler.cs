using Application.PermissionTypes.Common;
using Domain.PermissionTypes;
using Domain.Services;
using ErrorOr;
using MediatR;

namespace Application.PermissionTypes.GetAll
{
    public sealed class GetAllPermissionTypeQueryHandler : IRequestHandler<GetAllPermissionTypeQuery, ErrorOr<IEnumerable<PermissionTypeResponse>>>
    {
        private readonly IPermissionTypeRepository permissionTypeRepository;
        private readonly IKafkaProducer kafkaProducer;

        public GetAllPermissionTypeQueryHandler(IPermissionTypeRepository permissionTypeRepository, IKafkaProducer kafkaProducer)
        {
            this.permissionTypeRepository = permissionTypeRepository;
            this.kafkaProducer = kafkaProducer;
        }

        public async Task<ErrorOr<IEnumerable<PermissionTypeResponse>>> Handle(GetAllPermissionTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                await kafkaProducer.ProduceMessage("permission-topic", "get - permissionsTypes");

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
