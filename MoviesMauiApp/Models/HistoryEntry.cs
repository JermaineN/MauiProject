namespace MoviesMauiApp.Models;

/// <summary>
/// Represents an entry in the user's viewing history.
/// </summary>
public class HistoryEntry
{
    /// <summary>
    /// Gets or sets the title of the movie viewed.
    /// </summary>
    public string MovieTitle { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the emoji representing the movie.
    /// </summary>
    public string MovieEmoji { get; set; } = "🎬";
    /// <summary>
    /// Gets or sets the release year of the movie.
    /// </summary>
    public int MovieYear { get; set; }
    /// <summary>
    /// Gets or sets the genre of the movie.
    /// </summary>
    public string MovieGenre { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the date and time when the movie was viewed.
    /// </summary>
    public DateTime ViewedAt { get; set; } = DateTime.Now;
}
