using System;
using System.IO;
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

	[DllImport("user32.dll", CharSet = CharSet.Auto)]
	private static extern IntPtr LoadImage(
	IntPtr hInst,
	string name,
	uint type,
	int cx,
	int cy,
	uint fuLoad);

	private const uint IMAGE_ICON = 1;
	private const uint LR_LOADFROMFILE = 0x0010;

	private IntPtr LoadTrayIcon()
	{
		string path = Path.Combine(AppContext.BaseDirectory, "Assets", "tray.ico");

		return LoadImage(
			IntPtr.Zero,
			path,
			IMAGE_ICON,
			16,
			16,
			LR_LOADFROMFILE);
	}

	public void Initialize(IntPtr windowHandle)
	{
		_data = new NOTIFYICONDATA
		{
			cbSize = Marshal.SizeOf<NOTIFYICONDATA>(),
			hWnd = windowHandle,
			uID = 1,
			uFlags = NIF_ICON | NIF_TIP,
			hIcon = LoadTrayIcon(),
			szTip = "KeepAwake11"
		};

		Shell_NotifyIcon(NIM_ADD, ref _data);
	}

	public void Dispose()
	{
		Shell_NotifyIcon(NIM_DELETE, ref _data);
	}
}