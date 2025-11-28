using System.Text.Json.Serialization;

namespace BackendAwSmartstay.API.IAM.Domain.Model.Aggregates;

/// <summary>
///     The user aggregate
/// </summary>
/// <remarks>
///     This class is used to represent a user
/// </remarks>
/// <param name="username">The username of the user</param>
/// <param name="passwordHash">The hashed password of the user</param>
public class User(string username, string passwordHash)
{
    /// <summary>
    ///     Default constructor for the user aggregate
    /// </summary>
    public User() : this(string.Empty, string.Empty)
    {
    }

    /// <summary>
    ///     Gets the unique identifier of the user
    /// </summary>
    public int Id { get; }
    
    /// <summary>
    ///     Gets the username of the user
    /// </summary>
    public string Username { get; private set; } = username;

    /// <summary>
    ///     Gets the password hash of the user
    /// </summary>
    [JsonIgnore] public string PasswordHash { get; private set; } = passwordHash;

    /// <summary>
    ///     Update the username
    /// </summary>
    /// <param name="username">The new username</param>
    /// <returns>The updated user</returns>
    public User UpdateUsername(string username)
    {
        Username = username;
        return this;
    }

    /// <summary>
    ///     Update the password hash
    /// </summary>
    /// <param name="passwordHash">The new password hash</param>
    /// <returns>The updated user</returns>
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
}