using ErrorOr;

namespace Domain.DomainErrors
{
    public static class Errors
    {
        public static class Permission
        {
            public static Error PermissionTypeIdDoesNotExist =>
                Error.Validation("PermissionType.NotFound", "El tipo de permiso con el id proporcionado no existe");

        }
    }
}
