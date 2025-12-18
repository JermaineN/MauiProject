using System.Text.Json;

namespace MoviesMauiApp.Services;

/// <summary>
/// Service responsible for file I/O operations, saving and reading data from the app's local storage.
/// </summary>
public class FileService
{
    /// <summary>
    /// Saves data to a file in JSON format.
    /// </summary>
    /// <typeparam name="T">The type of data to save.</typeparam>
    /// <param name="fileName">The name of the file.</param>
    /// <param name="data">The data object to serialize and save.</param>
    public async Task SaveAsync<T>(string fileName, T data)
    {
        string path = Path.Combine(FileSystem.AppDataDirectory, fileName);
        string json = JsonSerializer.Serialize(data);
        await File.WriteAllTextAsync(path, json);
    }

    /// <summary>
    /// Reads data from a JSON file.
    /// </summary>
    /// <typeparam name="T">The type of data to read.</typeparam>
    /// <param name="fileName">The name of the file to read from.</param>
    /// <returns>The deserialized data object, or default if file does not exist.</returns>
    public async Task<T?> ReadAsync<T>(string fileName)
    {
        string path = Path.Combine(FileSystem.AppDataDirectory, fileName);
        if (!File.Exists(path))
            return default;

        string json = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<T>(json);
    }
    
    /// <summary>
    /// Checks if a file exists in the app's local storage.
    /// </summary>
    /// <param name="fileName">The name of the file to check.</param>
    /// <returns>True if the file exists; otherwise, false.</returns>
    public bool Exists(string fileName)
    {
        string path = Path.Combine(FileSystem.AppDataDirectory, fileName);
        return File.Exists(path);
    }
}
