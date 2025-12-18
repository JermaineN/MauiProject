using Microsoft.Extensions.Logging;
using MoviesMauiApp.Services;
using MoviesMauiApp.ViewModels;
using MoviesMauiApp.Views;

namespace MoviesMauiApp
{
    /// <summary>
    /// The entry point for the MAUI application, configuring services, fonts, and the app shell.
    /// </summary>
    public static class MauiProgram
    {
        /// <summary>
        /// Creates and configures the MauiApp.
        /// </summary>
        /// <returns>The configured MauiApp.</returns>
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Services
            builder.Services.AddSingleton<FileService>();
            builder.Services.AddSingleton<MovieService>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddSingleton<SettingsService>();

            // ViewModels
            builder.Services.AddTransient<StartupViewModel>();
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<DetailsViewModel>();
            builder.Services.AddTransient<FavouritesViewModel>();
            builder.Services.AddTransient<HistoryViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();

            // Views
            builder.Services.AddTransient<StartupPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<DetailsPage>();
            builder.Services.AddTransient<FavouritesPage>();
            builder.Services.AddTransient<HistoryPage>();
            builder.Services.AddTransient<SettingsPage>();

            return builder.Build();
        }
    }
}
