using MoviesMauiApp.ViewModels;

namespace MoviesMauiApp.Views;

/// <summary>
/// The page displaying the user's viewing history.
/// </summary>
public partial class HistoryPage : ContentPage
{
    private readonly HistoryViewModel _viewModel;

	public HistoryPage(HistoryViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
	}

    /// <summary>
    /// Method called when the page appears on screen. Initializes the ViewModel.
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.Initialize();
    }
}
