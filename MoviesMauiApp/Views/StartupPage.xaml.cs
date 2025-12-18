using MoviesMauiApp.ViewModels;

namespace MoviesMauiApp.Views;

/// <summary>
/// The startup page shown to new users to create their profile.
/// </summary>
public partial class StartupPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StartupPage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model for the startup page.</param>
    public StartupPage(StartupViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
