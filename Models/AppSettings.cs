namespace KeepAwake11.Models;

public class AppSettings
{
	/// <summary>
	/// Prevent the computer from sleeping.
	/// </summary>
	public bool KeepComputerAwake { get; set; }

	/// <summary>
	/// Prevent the display from turning off.
	/// </summary>
	public bool KeepDisplayAwake { get; set; }

	/// <summary>
	/// Launch the app when Windows starts.
	/// </summary>
	public bool StartWithWindows { get; set; }

	/// <summary>
	/// Start the app minimized to the system tray.
	/// </summary>
	public bool StartMinimized { get; set; }
}
