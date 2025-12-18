using MoviesMauiApp.Models;

namespace MoviesMauiApp.Services;

/// <summary>
/// Service responsible for managing user-related data such as profile, favourites, and viewing history.
/// </summary>
public class UserService
{
    private readonly FileService _fileService;
    
    private const string ProfileFile = "user_profile.json";
    private const string FavouritesFile = "user_favourites.json";
    private const string HistoryFile = "user_history.json";

    /// <summary>
    /// Gets the current user's profile.
    /// </summary>
    public UserProfile? CurrentUser { get; private set; }
    /// <summary>
    /// Gets the list of favourite movies.
    /// </summary>
    public List<FavouriteMovie> Favourites { get; private set; } = new();
    /// <summary>
    /// Gets the list of viewing history.
    /// </summary>
    public List<HistoryEntry> History { get; private set; } = new();

    public UserService(FileService fileService)
    {
        _fileService = fileService;
    }

    /// <summary>
    /// Initializes the service by loading user data from local storage.
    /// </summary>
    public async Task InitializeAsync()
    {
        CurrentUser = await _fileService.ReadAsync<UserProfile>(ProfileFile);
        var favs = await _fileService.ReadAsync<List<FavouriteMovie>>(FavouritesFile);
        if (favs != null) Favourites = favs;

        var hist = await _fileService.ReadAsync<List<HistoryEntry>>(HistoryFile);
        if (hist != null) History = hist;
    }

    /// <summary>
    /// Creates or updates the user profile and saves it to local storage.
    /// </summary>
    /// <param name="name">The name of the user.</param>
    public async Task SaveProfileAsync(string name)
    {
        CurrentUser = new UserProfile { Name = name };
        await _fileService.SaveAsync(ProfileFile, CurrentUser);
    }

    /// <summary>
    /// Adds a movie to the favourites list if it's not already there.
    /// </summary>
    /// <param name="movie">The movie to add to favourites.</param>
    public async Task AddFavouriteAsync(Movie movie)
    {
        if (Favourites.Any(f => f.MovieTitle == movie.Title)) return;
        
        Favourites.Add(new FavouriteMovie { MovieTitle = movie.Title });
        await _fileService.SaveAsync(FavouritesFile, Favourites);
    }

    /// <summary>
    /// Removes a movie from the favourites list.
    /// </summary>
    /// <param name="movieTitle">The title of the movie to remove.</param>
    public async Task RemoveFavouriteAsync(string movieTitle)
    {
        var item = Favourites.FirstOrDefault(f => f.MovieTitle == movieTitle);
        if (item != null)
        {
            Favourites.Remove(item);
            await _fileService.SaveAsync(FavouritesFile, Favourites);
        }
    }

    /// <summary>
    /// Checks if a movie is currently in the favourites list.
    /// </summary>
    /// <param name="movieTitle">The title of the movie to check.</param>
    /// <returns>True if the movie is a favourite; otherwise, false.</returns>
    public bool IsFavourite(string movieTitle)
    {
        return Favourites.Any(f => f.MovieTitle == movieTitle);
    }

    /// <summary>
    /// Adds a movie to the viewing history.
    /// </summary>
    /// <param name="movie">The movie viewed.</param>
    public async Task AddHistoryAsync(Movie movie)
    {
        // Optional: Avoid duplicate entries if recently viewed? 
        // Or just log everything. Requirement says "Add entry", implying log.
        // Let's just add it.
        History.Add(new HistoryEntry 
        { 
            MovieTitle = movie.Title,
            MovieEmoji = movie.Emoji,
            MovieGenre = movie.GenreString,
            MovieYear = movie.Year
        });
        
        await _fileService.SaveAsync(HistoryFile, History);
    }
}
