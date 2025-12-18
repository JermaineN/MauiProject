using CommunityToolkit.Mvvm.ComponentModel;

namespace MoviesMauiApp.ViewModels;

/// <summary>
/// A base class for ViewModels implementing INotifyPropertyChanged via ObservableObject.
/// </summary>
public partial class BaseViewModel : ObservableObject
{
    /// <summary>
    /// Gets or sets a value indicating whether the ViewModel is busy performing an operation.
    /// </summary>
    [ObservableProperty]
    private bool isBusy;

    /// <summary>
    /// Gets or sets the title of the page associated with this ViewModel.
    /// </summary>
    [ObservableProperty]
    private string title = string.Empty;
}
