using System;
namespace KeepAwake11.Services;

public sealed class WindowMessageEventArgs : EventArgs
{
    public uint Message { get; }

    public IntPtr WParam { get; }

    public IntPtr LParam { get; }

    public WindowMessageEventArgs(
        uint message,
        IntPtr wParam,
        IntPtr lParam)
    {
        Message = message;
        WParam = wParam;
        LParam = lParam;
    }
}