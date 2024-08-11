using Domain.PermissionTypes;
using Domain.Primitives;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PermissionTypes.Create
{
    internal sealed class CreatePermissionTypeCommandHandler : IRequestHandler<CreatePermissionTypeCommand, Unit>
    {
        private readonly IPermissionTypeRepository permissionTypeRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreatePermissionTypeCommandHandler(IPermissionTypeRepository permissionTypeRepository, IUnitOfWork unitOfWork)
        {
            this.permissionTypeRepository = permissionTypeRepository ?? throw new ArgumentNullException(nameof(permissionTypeRepository));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork)); ;
        }

        public async Task<Unit> Handle(CreatePermissionTypeCommand request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.Description))
                throw new ArgumentNullException(nameof(request.Description));

            PermissionType permissionType = new PermissionType(request.Description);

            await permissionTypeRepository.Add(permissionType);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}
