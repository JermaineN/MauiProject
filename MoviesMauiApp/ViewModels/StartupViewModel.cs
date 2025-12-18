using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoviesMauiApp.Services;

namespace MoviesMauiApp.ViewModels;

/// <summary>
/// ViewModel for the startup page, handling user profile creation.
/// </summary>
public partial class StartupViewModel : BaseViewModel
{
    private readonly UserService _userService;

    /// <summary>
    /// Gets or sets the user name entered by the user.
    /// </summary>
    [ObservableProperty]
    private string userName = string.Empty;

    public StartupViewModel(UserService userService)
    {
        _userService = userService;
        Title = "Welcome";
    }

    /// <summary>
    /// Saves the user profile and navigates to the main page.
    /// </summary>
    [RelayCommand]
    private async Task SaveAndContinue()
    {
        if (string.IsNullOrWhiteSpace(UserName))
            return; 

        await _userService.SaveProfileAsync(UserName);
        // Navigate to Main Page (absolute route to reset stack)
        await Shell.Current.GoToAsync("//MainPage");
    }
}
