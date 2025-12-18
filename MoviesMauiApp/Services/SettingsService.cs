using MoviesMauiApp.Models;

namespace MoviesMauiApp.Services;

/// <summary>
/// Service responsible for managing application settings.
/// </summary>
public class SettingsService
{
    private readonly FileService _fileService;
    private const string SettingsFile = "app_settings.json";
    
    /// <summary>
    /// Gets the current application settings.
    /// </summary>
    public AppSettings Settings { get; private set; } = new();

    public SettingsService(FileService fileService)
    {
        _fileService = fileService;
    }

    /// <summary>
    /// Initializes the service by loading settings from local storage.
    /// </summary>
    public async Task InitializeAsync()
    {
        var saved = await _fileService.ReadAsync<AppSettings>(SettingsFile);
        if (saved != null) 
            Settings = saved;
    }

    /// <summary>
    /// Saves the current settings to local storage.
    /// </summary>
    public async Task SaveSettingsAsync()
    {
        await _fileService.SaveAsync(SettingsFile, Settings);
    }
}
