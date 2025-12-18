using System.Text.Json.Serialization;

namespace MoviesMauiApp.Models;

/// <summary>
/// Represents a movie with details such as title, year, genre, director, rating, and emoji.
/// </summary>
public class Movie
{
    /// <summary>
    /// Gets or sets the title of the movie.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the release year of the movie.
    /// </summary>
    [JsonPropertyName("year")]
    public int Year { get; set; }
    
    /// <summary>
    /// Gets or sets the list of genres associated with the movie.
    /// </summary>
    [JsonPropertyName("genre")]
    public List<string> Genres { get; set; } = new();

    /// <summary>
    /// Gets or sets the director of the movie.
    /// </summary>
    [JsonPropertyName("director")]
    public string Director { get; set; } = string.Empty;
    
    /// <summary>
    /// Gets or sets the IMDB rating of the movie.
    /// </summary>
    [JsonPropertyName("rating")]
    public double ImdbRating { get; set; } 
    
    /// <summary>
    /// Gets or sets the emoji representing the movie.
    /// </summary>
    [JsonPropertyName("emoji")]
    public string Emoji { get; set; } = "🎬";

    /// <summary>
    /// Gets a comma-separated string of genres.
    /// This property is ignored during JSON serialization.
    /// </summary>
    [JsonIgnore]
    public string GenreString => string.Join(", ", Genres);
}
