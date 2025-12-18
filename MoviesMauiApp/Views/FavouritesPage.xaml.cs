using MoviesMauiApp.ViewModels;

namespace MoviesMauiApp.Views;

/// <summary>
/// The page displaying the user's favourite movies.
/// </summary>
public partial class FavouritesPage : ContentPage
{
    private readonly FavouritesViewModel _viewModel;

	public FavouritesPage(FavouritesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
	}

    /// <summary>
    /// Method called when the page appears on screen. Initializes the ViewModel.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.InitializeAsync();
    }
}
