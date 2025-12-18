using MoviesMauiApp.ViewModels;

namespace MoviesMauiApp.Views;

/// <summary>
/// The details page showing in-depth information about a specific movie.
/// </summary>
public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    /// <summary>
    /// Handles the click event for the favourite button, triggering a bounce animation.
    /// </summary>
    private async void OnFavouriteClicked(object sender, EventArgs e)
    {
        // Simple Bounce Animation
        await EmojiLabel.ScaleTo(1.5, 150);
        await EmojiLabel.ScaleTo(1.0, 150);
    }
}
