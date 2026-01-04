namespace FrasesApi.Shared.Domain.Constants;

public enum Roles
{
    Admin,
    User,
}

public static class RolesExtensions
{
    public static string ToString(this Roles role)
    {
        return role switch
        {
            Roles.Admin => "Admin",
            _ => "User"
        };
    }
}