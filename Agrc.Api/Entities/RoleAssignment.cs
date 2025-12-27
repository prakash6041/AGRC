namespace Agrc.Api.Entities
{
   

    public record RoleAssignment(
        string PrincipalId,
        string PrincipalDisplayName,
        string PrincipalType,
        string AppRoleId,
        DateTime CreatedDateTime
    );

}
