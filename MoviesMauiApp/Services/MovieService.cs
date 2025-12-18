using System.Net.Http.Json;
using MoviesMauiApp.Models;

namespace MoviesMauiApp.Services;

/// <summary>
/// Service responsible for fetching movie data from a remote source or local cache.
/// </summary>
public class MovieService
{
    private readonly FileService _fileService;
    private const string CacheFileName = "movies_cache.json";
    private const string RemoteUrl = "https://raw.githubusercontent.com/DonH-ITS/jsonfiles/refs/heads/main/moviesemoji.json";
    private readonly HttpClient _httpClient; 

    public MovieService(FileService fileService)
    {
        _fileService = fileService;
        _httpClient = new HttpClient();
    }

    /// <summary>
    /// Retrieves a list of movies, preferring local cache over remote fetches.
    /// </summary>
    /// <returns>A list of movies.</returns>
    public async Task<List<Movie>> GetMoviesAsync()
    {
        // 1. Try Load from Cache
        if (_fileService.Exists(CacheFileName))
        {
            var cached = await _fileService.ReadAsync<List<Movie>>(CacheFileName);
            if (cached != null && cached.Count > 0)
                return cached;
        }

        // 2. Fetch from Web
        try
        {
            var movies = await _httpClient.GetFromJsonAsync<List<Movie>>(RemoteUrl);
            if (movies != null)
            {
                await _fileService.SaveAsync(CacheFileName, movies);
                return movies;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error fetching movies: {ex.Message}");
        }

        return new List<Movie>();
    }
}
