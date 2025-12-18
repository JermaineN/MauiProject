using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using MoviesMauiApp.Models;
using MoviesMauiApp.Services;

namespace MoviesMauiApp.ViewModels;

/// <summary>
/// ViewModel for the history page, displaying viewing history and genre statistics.
/// </summary>
public partial class HistoryViewModel : BaseViewModel
{
    private readonly UserService _userService;
    
    /// <summary>
    /// Collection of history entries.
    /// </summary>
    public ObservableCollection<HistoryEntry> History { get; } = new();
    
    /// <summary>
    /// Collection of genre statistics.
    /// </summary>
    public ObservableCollection<GenreStat> Stats { get; } = new();

    public HistoryViewModel(UserService userService)
    {
        _userService = userService;
        Title = "History";
    }

    /// <summary>
    /// Initializes the ViewModel by loading history and calculating statistics.
    /// </summary>
    public void Initialize()
    {
        History.Clear();
        // Show latest first
        var sorted = _userService.History.OrderByDescending(h => h.ViewedAt).ToList();
        foreach(var item in sorted)
        {
            History.Add(item);
        }

        // Calculate Stats
        Stats.Clear();
        var grouped = sorted
            .SelectMany(h => h.MovieGenre.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries))
            .GroupBy(g => g)
            .OrderByDescending(g => g.Count())
            .Take(5); // Top 5 genres

        foreach(var g in grouped)
        {
            // Create emoji bar (max 10 to avoid overflow)
            int count = g.Count();
            string bar = string.Concat(Enumerable.Repeat("🎬", Math.Min(count, 10)));
            
            Stats.Add(new GenreStat 
            { 
                Genre = g.Key, 
                Count = count, 
                EmojiBar = bar 
            });
        }
    }
}

/// <summary>
/// Represents statistics for a specific genre.
/// </summary>
public class GenreStat
{
    /// <summary>
    /// Gets or sets the genre name.
    /// </summary>
    public string Genre { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the count of movies viewed in this genre.
    /// </summary>
    public int Count { get; set; }
    /// <summary>
    /// Gets or sets the visual representation (emoji bar) of the count.
    /// </summary>
    public string EmojiBar { get; set; } = string.Empty;
}
