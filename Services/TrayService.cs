using System;
using System.Runtime.InteropServices;
using Microsoft.UI.Xaml;

namespace KeepAwake11.Services;

public class TrayService
{
	private const int WM_USER = 0x0400;
	private const int WM_TRAYICON = WM_USER + 1;
	private const int NIM_ADD = 0x00000000;
	private const int NIM_DELETE = 0x00000002;
	private const int NIF_MESSAGE = 0x00000001;
	private const int NIF_ICON = 0x00000002;
	private const int NIF_TIP = 0x00000004;

	private const int WM_LBUTTONUP = 0x0202;
	private const int WM_RBUTTONUP = 0x0205;

	[StructLayout(LayoutKind.Sequential)]
	private struct NOTIFYICONDATA
	{
		public int cbSize;
		public IntPtr hWnd;
		public int uID;
		public int uFlags;
		public int uCallbackMessage;
		public IntPtr hIcon;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string szTip;
	}

	[DllImport("shell32.dll")]
	private static extern bool Shell_NotifyIcon(int dwMessage, ref NOTIFYICONDATA lpData);

	private NOTIFYICONDATA _data;
	private Window? _window;

	public event Action? OnShowWindow;
	public event Action? OnExit;
	public event Action? OnToggleKeepAwake;

	public void Initialize(Window window)
	{
		_window = window;

		var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

		_data = new NOTIFYICONDATA
		{
			cbSize = Marshal.SizeOf<NOTIFYICONDATA>(),
			hWnd = hWnd,
			uID = 1,
			uFlags = NIF_MESSAGE | NIF_ICON | NIF_TIP,
			uCallbackMessage = WM_TRAYICON,
			hIcon = IntPtr.Zero,
			szTip = "KeepAwake11"
		};

		Shell_NotifyIcon(NIM_ADD, ref _data);
	}

	public void HandleMessage(int msg)
	{
		if (msg == WM_LBUTTONUP)
		{
			OnShowWindow?.Invoke();
		}
		else if (msg == WM_RBUTTONUP)
		{
			// For now: simple toggle behavior placeholder
			OnToggleKeepAwake?.Invoke();
		}
	}

	public void Dispose()
	{
		Shell_NotifyIcon(NIM_DELETE, ref _data);
	}
}
