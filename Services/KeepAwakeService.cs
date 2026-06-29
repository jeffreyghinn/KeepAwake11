using System.Runtime.InteropServices;

namespace KeepAwake11.Services;

public class KeepAwakeService
{
    private enum EXECUTION_STATE : uint
    {
        ES_CONTINUOUS = 0x80000000,
        ES_SYSTEM_REQUIRED = 0x00000001,
        ES_DISPLAY_REQUIRED = 0x00000002
    }

    [DllImport("kernel32.dll")]
    private static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

    public bool IsEnabled { get; private set; }

    public bool IsKeepingDisplayAwake { get; private set; }

    public void EnableSystemAwake()
    {
        SetState(keepDisplayAwake: false);
    }

    public void EnableSystemAndDisplayAwake()
    {
        SetState(keepDisplayAwake: true);
    }

    public void Disable()
    {
        IsEnabled = false;
        IsKeepingDisplayAwake = false;

        SetThreadExecutionState(EXECUTION_STATE.ES_CONTINUOUS);
    }

    private void SetState(bool keepDisplayAwake)
    {
        IsEnabled = true;
        IsKeepingDisplayAwake = keepDisplayAwake;

        var flags =
            EXECUTION_STATE.ES_CONTINUOUS |
            EXECUTION_STATE.ES_SYSTEM_REQUIRED;

        if (keepDisplayAwake)
        {
            flags |= EXECUTION_STATE.ES_DISPLAY_REQUIRED;
        }

        SetThreadExecutionState(flags);
    }
}
