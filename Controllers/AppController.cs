using System;
using KeepAwake11.Services;
using KeepAwake11.ViewModels;
using Microsoft.UI.Xaml;

namespace KeepAwake11.Controllers;

public class AppController
{
	private readonly MainViewModel _viewModel = new();

	private WindowService? _windowService;

	public MainViewModel ViewModel => _viewModel;

	public async Task InitializeAsync(Window window)
	{
		_windowService = new WindowService(window);

		await _viewModel.InitializeAsync();

		if (_viewModel.Settings.KeepComputerAwake)
		{
			_viewModel.ApplySettings();
		}
	}

	public void ShowWindow()
	{
		_windowService?.Show();
	}

	public void HideWindow()
	{
		_windowService?.Hide();
	}

	public IntPtr WindowHandle => _windowService?.Handle ?? IntPtr.Zero;
}
