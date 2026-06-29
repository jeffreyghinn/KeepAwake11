using System;
using Microsoft.UI.Xaml;

namespace KeepAwake11.Services;

public class WindowMessageHook
{
    private readonly WindowService _windowService;

    public WindowMessageHook(WindowService windowService)
    {
        _windowService = windowService;
    }

    public IntPtr WindowHandle => _windowService.Handle;

    public event EventHandler<WindowMessageEventArgs>? MessageReceived;

    internal void RaiseMessage(
        uint message,
        IntPtr wParam,
        IntPtr lParam)
    {
        MessageReceived?.Invoke(
            this,
            new WindowMessageEventArgs(message, wParam, lParam));
    }
}