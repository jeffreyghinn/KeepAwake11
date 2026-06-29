using KeepAwake11.ViewModels;
using Microsoft.UI.Xaml;

namespace KeepAwake11.Controllers;

public class AppController
{
    private readonly MainViewModel _viewModel = new();
    private Window? _window;

    public MainViewModel ViewModel => _viewModel;

    public async Task InitializeAsync(Window window)
    {
        _window = window;

        await _viewModel.InitializeAsync();

        ApplyStartupState();
    }

    private void ApplyStartupState()
    {
        if (!_viewModel.Settings.KeepComputerAwake)
            return;

        _viewModel.ApplySettings();
    }

    public void ShowWindow()
    {
        if (_window is null) return;

        _window.Activate();
    }

    public void HideWindow()
    {
        if (_window is null) return;

        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(_window);
        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
        var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

        appWindow.Hide();
    }
}
