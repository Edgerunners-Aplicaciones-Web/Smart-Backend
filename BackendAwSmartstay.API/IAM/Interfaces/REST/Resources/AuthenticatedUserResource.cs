namespace BackendAwSmartstay.API.IAM.Interfaces.REST.Resources;

/// <summary>
/// Resource representing an authenticated user.
/// </summary>
/// <param name="Id">The unique identifier of the user.</param>
/// <param name="Username">The username of the user.</param>
/// <param name="Token">The JWT token for the authenticated user.</param>
public record AuthenticatedUserResource(int Id, string Username, string Token);