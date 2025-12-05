using BackendAwSmartstay.API.IAM.Application.OutboundServices;
using BackendAwSmartstay.API.IAM.Domain.Model.Aggregates;
using BackendAwSmartstay.API.IAM.Domain.Model.Commands;
using BackendAwSmartstay.API.IAM.Domain.Repositories;
using BackendAwSmartstay.API.IAM.Domain.Services;
using BackendAwSmartstay.API.Shared.Domain.Repositories;

namespace BackendAwSmartstay.API.IAM.Application.Internal.CommandServices;

/// <summary>
///     Service responsible for handling user-related commands (Write operations).
/// </summary>
public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork)
    : IUserCommandService
{
    /// <summary>
    ///     Processes a sign-in request.
    /// </summary>
    /// <param name="command">The command containing credentials.</param>
    /// <returns>A tuple containing the authenticated User entity and the generated JWT token.</returns>
    /// <exception cref="Exception">Thrown if user is not found or password is invalid.</exception>
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);

        if (user == null)
            throw new Exception($"User with username '{command.Username}' not found");

        if (!hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid password");

        var token = tokenService.GenerateToken(user);

        return (user, token);
    }

    /// <summary>
    ///     Processes a sign-up request.
    /// </summary>
    /// <param name="command">The command containing new user details.</param>
    /// <exception cref="Exception">Thrown if the username is already taken.</exception>
    public async Task Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} is already taken");

        var hashedPassword = hashingService.HashPassword(command.Password);
        
        // Ahora pasamos el ROL al crear el usuario
        var user = new User(command.Username, hashedPassword, command.Role); 
        
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating user: {e.Message}");
        }
    }
}