using ErrorOr;

namespace Domain.DomainErrors
{
    public static class Errors
    {
        public static class Permission
        {
            public static Error PermissionIdDoesNotExist =>
                Error.Validation("Permission.NotFound", "El permiso con el id proporcionado no existe");
            public static Error PermissionTypeIdDoesNotExist =>
                Error.Validation("PermissionType.NotFound", "El tipo de permiso con el id proporcionado no existe");
            public static Error PermissionNameIsEmpty =>
                Error.Validation("Permission.NameEmployee", "Debe enviar el nombre de la descripcion");
            public static Error PermissionLastNameIsEmpty =>
                Error.Validation("Permission.LastNameEmployee", "Debe enviar el nombre de la descripcion");

        }
    }
}
