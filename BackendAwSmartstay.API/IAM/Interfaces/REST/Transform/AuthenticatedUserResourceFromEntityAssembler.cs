using BackendAwSmartstay.API.IAM.Domain.Model.Aggregates;
using BackendAwSmartstay.API.IAM.Interfaces.REST.Resources;

namespace BackendAwSmartstay.API.IAM.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create an AuthenticatedUserResource from a User entity and token.
/// </summary>
public static class AuthenticatedUserResourceFromEntityAssembler
{
    /// <summary>
    /// Converts a User entity and token to an AuthenticatedUserResource.
    /// </summary>
    /// <param name="user">The user entity.</param>
    /// <param name="token">The JWT token.</param>
    /// <returns>The authenticated user resource.</returns>
    public static AuthenticatedUserResource ToResourceFromEntity(
        User user, string token)
    {
        return new AuthenticatedUserResource(user.Id, user.Username, token);
    }
}