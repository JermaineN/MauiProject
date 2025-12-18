using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoviesMauiApp.Models;
using MoviesMauiApp.Services;

namespace MoviesMauiApp.ViewModels;

/// <summary>
/// ViewModel for the details page, managing movie details, favourite status, and history tracking.
/// </summary>
[QueryProperty(nameof(Movie), "Movie")]
public partial class DetailsViewModel : BaseViewModel
{
    private readonly UserService _userService;

    /// <summary>
    /// Gets or sets the movie to display details for.
    /// </summary>
    [ObservableProperty]
    private Movie movie;

    /// <summary>
    /// Gets or sets a value indicating whether the movie is a favourite.
    /// </summary>
    [ObservableProperty]
    private bool isFavourite;

    public DetailsViewModel(UserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Updates properties and checks favourite status when the movie property changes.
    /// Also records the movie in the viewing history.
    /// </summary>
    partial void OnMovieChanged(Movie value)
    {
        if (value != null)
        {
            Title = value.Title;
            CheckFavouriteStatus();
            
            // Add to history essentially "fire and forget" but safer to await if possible, 
            // but void OnChanged is synchronous. We can launch a task.
            MainThread.BeginInvokeOnMainThread(async () => 
            {
                await _userService.AddHistoryAsync(value);
            });
        }
    }

    private void CheckFavouriteStatus()
    {
        if (Movie != null)
        {
            IsFavourite = _userService.IsFavourite(Movie.Title);
        }
    }

    /// <summary>
    /// Toggles the favourite status of the current movie.
    /// </summary>
    [RelayCommand]
    private async Task ToggleFavourite()
    {
        if (Movie == null) return;

        if (IsFavourite)
        {
            await _userService.RemoveFavouriteAsync(Movie.Title);
            IsFavourite = false;
        }
        else
        {
            await _userService.AddFavouriteAsync(Movie);
            IsFavourite = true;
        }
    }
}
