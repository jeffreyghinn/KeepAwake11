using System.Text.Json;
using KeepAwake11.Models;
using Windows.Storage;

namespace KeepAwake11.Services;

public class SettingsService
{
    private const string FileName = "settings.json";

    public async Task<AppSettings> LoadAsync()
    {
        try
        {
            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(FileName);

            string json = await FileIO.ReadTextAsync(file);

            return JsonSerializer.Deserialize<AppSettings>(json)
                   ?? new AppSettings();
        }
        catch
        {
            // First run or unreadable file
            return new AppSettings();
        }
    }

    public async Task SaveAsync(AppSettings settings)
    {
        StorageFile file = await ApplicationData.Current.LocalFolder
            .CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);

        string json = JsonSerializer.Serialize(
            settings,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        await FileIO.WriteTextAsync(file, json);
    }
}
