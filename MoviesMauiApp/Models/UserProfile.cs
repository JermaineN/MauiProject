namespace MoviesMauiApp.Models;

/// <summary>
/// Represents a user profile in the application.
/// </summary>
public class UserProfile
{
    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the date and time when the profile was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
