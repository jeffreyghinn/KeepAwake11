using KeepAwake11.Models;
using KeepAwake11.Services;

namespace KeepAwake11.ViewModels;

public class MainViewModel
{
    private readonly KeepAwakeService _keepAwakeService = new();
    private readonly SettingsService _settingsService = new();

    public AppSettings Settings { get; private set; } = new();

    public async Task InitializeAsync()
    {
        Settings = await _settingsService.LoadAsync();

        ApplySettings();
    }

    public void ApplySettings()
    {
        if (!Settings.KeepComputerAwake)
        {
            _keepAwakeService.Disable();
            return;
        }

        if (Settings.KeepDisplayAwake)
        {
            _keepAwakeService.EnableSystemAndDisplayAwake();
        }
        else
        {
            _keepAwakeService.EnableSystemAwake();
        }
    }

    public async Task SaveAsync()
    {
        await _settingsService.SaveAsync(Settings);
    }
}
