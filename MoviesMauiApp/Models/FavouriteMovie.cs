namespace MoviesMauiApp.Models;

/// <summary>
/// Represents a movie marked as favourite by the user.
/// </summary>
public class FavouriteMovie
{
    /// <summary>
    /// Gets or sets the title of the favourite movie.
    /// </summary>
    public string MovieTitle { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the date and time when the movie was added to favourites.
    /// </summary>
    public DateTime AddedAt { get; set; } = DateTime.Now;
}
