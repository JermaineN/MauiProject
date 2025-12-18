using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoviesMauiApp.Models;
using MoviesMauiApp.Services;
using MoviesMauiApp.Views;

namespace MoviesMauiApp.ViewModels;

/// <summary>
/// ViewModel for the main page, handling movie listing, filtering, and searching.
/// </summary>
public partial class MainViewModel : BaseViewModel
{
    private readonly MovieService _movieService;
    private List<Movie> _allMovies = new();

    /// <summary>
    /// Collection of movies displayed on the UI.
    /// </summary>
    public ObservableCollection<Movie> Movies { get; } = new();
    
    /// <summary>
    /// Collection of available genres for filtering.
    /// </summary>
    public ObservableCollection<string> Genres { get; } = new();
    /// <summary>
    /// Collection of available directors for filtering.
    /// </summary>
    public ObservableCollection<string> Directors { get; } = new();

    /// <summary>
    /// Gets or sets the search text entered by the user.
    /// </summary>
    [ObservableProperty]
    private string searchText = string.Empty;

    /// <summary>
    /// Gets or sets the selected genre for filtering.
    /// </summary>
    [ObservableProperty]
    private string selectedGenre = "All";

    /// <summary>
    /// Gets or sets the selected director for filtering.
    /// </summary>
    [ObservableProperty]
    private string selectedDirector = "All";

    public MainViewModel(MovieService movieService)
    {
        _movieService = movieService;
        Title = "Movie Explorer";
    }

    /// <summary>
    /// Initializes the ViewModel by loading movies and populating filter lists.
    /// </summary>
    public async Task InitializeAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        
        try 
        {
            _allMovies = await _movieService.GetMoviesAsync();
            
            // Populate Genres
            var genres = _allMovies
                .SelectMany(m => m.Genres)
                .Distinct()
                .OrderBy(g => g)
                .ToList();
            
            Genres.Clear();
            Genres.Add("All");
            foreach(var g in genres) Genres.Add(g);

            // Populate Directors
            var directors = _allMovies
                .Select(m => m.Director)
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            Directors.Clear();
            Directors.Add("All");
            foreach(var d in directors) Directors.Add(d);
            
            FilterMovies();
        }
        finally
        {
            IsBusy = false;
        }
    }

    /// <summary>
    /// Filters the movies whenever search text or selected filters change.
    /// </summary>
    partial void OnSearchTextChanged(string value) => FilterMovies();
    partial void OnSelectedGenreChanged(string value) => FilterMovies();
    partial void OnSelectedDirectorChanged(string value) => FilterMovies();

    private void FilterMovies()
    {
        var filtered = _allMovies.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filtered = filtered.Where(m => m.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase));
        }

        if (SelectedGenre != "All" && !string.IsNullOrWhiteSpace(SelectedGenre))
        {
            filtered = filtered.Where(m => m.Genres.Contains(SelectedGenre));
        }

        if (SelectedDirector != "All" && !string.IsNullOrWhiteSpace(SelectedDirector))
        {
            filtered = filtered.Where(m => m.Director.Equals(SelectedDirector, StringComparison.OrdinalIgnoreCase));
        }

        Movies.Clear();
        foreach (var m in filtered) Movies.Add(m);
    }

    /// <summary>
    /// Navigates to the details page of the selected movie.
    /// </summary>
    /// <param name="movie">The movie to display details for.</param>
    [RelayCommand]
    private async Task GoToDetails(Movie movie)
    {
        if (movie == null) return;

        await Shell.Current.GoToAsync(nameof(DetailsPage), new Dictionary<string, object>
        {
            ["Movie"] = movie
        });
    }
}
