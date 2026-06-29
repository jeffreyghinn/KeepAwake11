using System;
using System.Runtime.InteropServices;

namespace KeepAwake11.Services;

public class TrayService
{
	private const int NIM_ADD = 0x00000000;
	private const int NIM_DELETE = 0x00000002;
	private const int NIF_ICON = 0x00000002;
	private const int NIF_TIP = 0x00000004;

	[StructLayout(LayoutKind.Sequential)]
	private struct NOTIFYICONDATA
	{
		public int cbSize;
		public IntPtr hWnd;
		public uint uID;
		public uint uFlags;
		public uint uCallbackMessage;
		public IntPtr hIcon;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string szTip;
	}

	[DllImport("shell32.dll")]
	private static extern bool Shell_NotifyIcon(int dwMessage, ref NOTIFYICONDATA lpData);

	private NOTIFYICONDATA _data;

	public void Initialize(IntPtr windowHandle)
	{
		_data = new NOTIFYICONDATA
		{
			cbSize = Marshal.SizeOf<NOTIFYICONDATA>(),
			hWnd = windowHandle,
			uID = 1,
			uFlags = NIF_ICON | NIF_TIP,
			hIcon = IntPtr.Zero,
			szTip = "KeepAwake11"
		};

		Shell_NotifyIcon(NIM_ADD, ref _data);
	}

	public void Dispose()
	{
		Shell_NotifyIcon(NIM_DELETE, ref _data);
	}
}