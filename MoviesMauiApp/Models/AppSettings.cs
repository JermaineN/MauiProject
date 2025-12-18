namespace MoviesMauiApp.Models;

/// <summary>
/// Represents the application settings.
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Gets or sets a value indicating whether dark mode is enabled.
    /// </summary>
    public bool IsDarkMode { get; set; } = false;
    /// <summary>
    /// Gets or sets the font size preference.
    /// </summary>
    public double FontSize { get; set; } = 14.0;
    // Difficulty/TimeLimit or AccentColor as required
    /// <summary>
    /// Gets or sets the accent color preference.
    /// </summary>
    public string AccentColor { get; set; } = "#512BD4"; 
}
