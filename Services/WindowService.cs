using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace KeepAwake11.Services;

public class WindowService
{
    private readonly Window _window;
    private readonly IntPtr _hWnd;
    private readonly AppWindow _appWindow;

    public WindowService(Window window)
    {
        _window = window;

        _hWnd = WindowNative.GetWindowHandle(window);

        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(_hWnd);

        _appWindow = AppWindow.GetFromWindowId(windowId);
    }

    public IntPtr Handle => _hWnd;

    public void Show()
    {
        _appWindow.Show();
        _window.Activate();
    }

    public void Hide()
    {
        _appWindow.Hide();
    }
}
