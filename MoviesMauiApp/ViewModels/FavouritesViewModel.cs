using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MoviesMauiApp.Models;
using MoviesMauiApp.Services;
using MoviesMauiApp.Views;

namespace MoviesMauiApp.ViewModels;

/// <summary>
/// ViewModel for the favourites page, handling the display of favourite movies.
/// </summary>
public partial class FavouritesViewModel : BaseViewModel
{
    private readonly UserService _userService;
    private readonly MovieService _movieService;

    /// <summary>
    /// Collection of favourite movies.
    /// </summary>
    public ObservableCollection<Movie> FavouriteMovies { get; } = new();

    public FavouritesViewModel(UserService userService, MovieService movieService)
    {
        _userService = userService;
        _movieService = movieService;
        Title = "Favourites";
    }

    /// <summary>
    /// Initializes the ViewModel by loading favourite movies.
    /// </summary>
    public async Task InitializeAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        try
        {
            // Reload favourites from service in case they changed
            // In a real app we might use MessagingCenter or Observable events from Service
            // For now, refreshing on appearing is fine.
            
            var allMovies = await _movieService.GetMoviesAsync();
            
            FavouriteMovies.Clear();
            foreach(var fav in _userService.Favourites)
            {
                var movie = allMovies.FirstOrDefault(m => m.Title == fav.MovieTitle);
                if (movie != null)
                    FavouriteMovies.Add(movie);
            }
        }
        finally
        {
            IsBusy = false;
        }
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
