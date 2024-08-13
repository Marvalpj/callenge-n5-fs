using ErrorOr;
using MediatR;

namespace Application.PermissionTypes.Update
{
    public class UpdatePermissionTypeCommand : IRequest<ErrorOr<Unit>>
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public UpdatePermissionTypeCommand(long id, string descripcion)
        {
            Id = id;
            Description = descripcion;
        }
    }
}
