namespace BackendAwSmartstay.API.IAM.Interfaces.REST.Resources;

/// <summary>
/// Resource for signing up a new user.
/// </summary>
/// <param name="Username">The username of the new user.</param>
/// <param name="Password">The password of the new user.</param>
public record SignUpResource(string Username, string Password);