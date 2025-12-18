using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoviesMauiApp.Services;

namespace MoviesMauiApp.ViewModels;

/// <summary>
/// ViewModel for the settings page, managing application preferences like dark mode and font size.
/// </summary>
public partial class SettingsViewModel : BaseViewModel
{
    private readonly SettingsService _settingsService;

    /// <summary>
    /// Gets or sets a value indicating whether dark mode is enabled.
    /// </summary>
    [ObservableProperty]
    private bool isDarkMode;

    /// <summary>
    /// Gets or sets the font size preference.
    /// </summary>
    [ObservableProperty]
    private double fontSize;

    public SettingsViewModel(SettingsService settingsService)
    {
        _settingsService = settingsService;
        Title = "Settings";
        
        IsDarkMode = _settingsService.Settings.IsDarkMode;
        FontSize = _settingsService.Settings.FontSize;
    }

    /// <summary>
    /// Handles changes to the IsDarkMode property, updating the theme and saving settings.
    /// </summary>
    partial void OnIsDarkModeChanged(bool value)
    {
        _settingsService.Settings.IsDarkMode = value;
        UpdateTheme(value);
        _ = _settingsService.SaveSettingsAsync();
    }

    /// <summary>
    /// Handles changes to the FontSize property, updating resources and saving settings.
    /// </summary>
    partial void OnFontSizeChanged(double value)
    {
        _settingsService.Settings.FontSize = value;
        
        // Update global resource for dynamic font scaling
        if (Application.Current?.Resources != null)
        {
             Application.Current.Resources["GlobalFontSize"] = value;
        }

        _ = _settingsService.SaveSettingsAsync();
    }

    private void UpdateTheme(bool isDark)
    {
        if (Application.Current != null)
        {
            Application.Current.UserAppTheme = isDark ? AppTheme.Dark : AppTheme.Light;
        }
    }

    /// <summary>
    /// Resets application data (placeholder implementation).
    /// </summary>
    [RelayCommand]
    private async Task ResetData()
    {
        // Stretch goal: Reset data
        // Implementation: Delete files
        // Here we just clear local state for demo
        // For full implementation we'd need methods in Services to ClearAll()
    }
}
