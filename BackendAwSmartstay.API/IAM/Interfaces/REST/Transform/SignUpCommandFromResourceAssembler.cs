using BackendAwSmartstay.API.IAM.Domain.Model.Commands;
using BackendAwSmartstay.API.IAM.Interfaces.REST.Resources;

namespace BackendAwSmartstay.API.IAM.Interfaces.REST.Transform;

/// <summary>
/// Assembler to create a SignUpCommand from a SignUpResource.
/// </summary>
public static class SignUpCommandFromResourceAssembler
{
    /// <summary>
    /// Converts a SignUpResource to a SignUpCommand.
    /// </summary>
    /// <param name="resource">The sign-up resource.</param>
    /// <returns>The sign-up command.</returns>
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password);
    }
}