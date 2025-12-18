using MoviesMauiApp.ViewModels;

namespace MoviesMauiApp.Views;

/// <summary>
/// The settings page allows users to configure application preferences.
/// </summary>
public partial class SettingsPage : ContentPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsPage"/> class.
    /// </summary>
    /// <param name="viewModel">The view model for the settings page.</param>
    public SettingsPage(SettingsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
