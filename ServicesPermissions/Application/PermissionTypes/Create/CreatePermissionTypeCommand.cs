using ErrorOr;
using MediatR;

namespace Application.PermissionTypes.Create
{
    public class CreatePermissionTypeCommand : IRequest<ErrorOr<Unit>>
    {
        public string Description { get; set; }
        
        public CreatePermissionTypeCommand(string description)
        {
            Description = description;
        }
    }
}
