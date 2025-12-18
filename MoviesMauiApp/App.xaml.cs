using MoviesMauiApp.Services;

namespace MoviesMauiApp
{
    /// <summary>
    /// The main Application class, handling application lifecycle events and initialization.
    /// </summary>
    public partial class App : Application
    {
        private readonly UserService _userService;
        private readonly SettingsService _settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="settingsService">The settings service.</param>
        public App(UserService userService, SettingsService settingsService)
        {
            InitializeComponent();
            _userService = userService;
            _settingsService = settingsService;
        }

        /// <summary>
        /// Event handler called when the application starts. Initializes services and handles navigation logic.
        /// </summary>
        protected override async void OnStart()
        {
            try
            {
                await _userService.InitializeAsync();
                if (_userService.CurrentUser == null)
                {
                    // Initial navigation to Startup Page if no user
                    // We use Dispatcher to ensure it runs on UI thread after Shell is ready
                    Dispatcher.Dispatch(async () => 
                    {
                        try 
                        {
                            await Shell.Current.GoToAsync(nameof(Views.StartupPage));
                        }
                        catch (Exception navEx)
                        {
                            System.Diagnostics.Debug.WriteLine($"Navigation Error: {navEx}");
                        }
                    });
                }
                
                // Initialize Settings (Theme & Font)
                await _settingsService.InitializeAsync();
                
                // Apply Theme
                Application.Current.UserAppTheme = _settingsService.Settings.IsDarkMode ? AppTheme.Dark : AppTheme.Light;
                
                // Apply Font Size
                if (Application.Current.Resources.ContainsKey("GlobalFontSize"))
                {
                    Application.Current.Resources["GlobalFontSize"] = _settingsService.Settings.FontSize;
                }
                else
                {
                    Application.Current.Resources.Add("GlobalFontSize", _settingsService.Settings.FontSize);
                }
            }
            catch (Exception ex)
            {
                 System.Diagnostics.Debug.WriteLine($"Startup Error: {ex}");
            }
        }

        /// <summary>
        /// Creates the main window of the application.
        /// </summary>
        /// <param name="activationState">The activation state.</param>
        /// <returns>The main window.</returns>
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}