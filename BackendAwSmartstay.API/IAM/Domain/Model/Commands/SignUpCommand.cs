namespace BackendAwSmartstay.API.IAM.Domain.Model.Commands;

/// <summary>
///     Command to register a new user.
/// </summary>
/// <param name="Username">The desired username.</param>
/// <param name="Password">The raw password.</param>
/// <param name="Role">The role to assign (optional, default 'guest').</param>
public record SignUpCommand(string Username, string Password, string Role = "guest");