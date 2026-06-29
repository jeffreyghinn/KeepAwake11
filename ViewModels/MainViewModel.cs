using KeepAwake11.Models;
using KeepAwake11.Services;

namespace KeepAwake11.ViewModels;

public class MainViewModel
{
	private readonly KeepAwakeService _keepAwakeService;
	private readonly SettingsService _settingsService;

	public AppSettings Settings { get; }

	public MainViewModel()
	{
		_keepAwakeService = new KeepAwakeService();
		_settingsService = new SettingsService();

		Settings = new AppSettings();
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
}
