using Microsoft.UI.Xaml;
using KeepAwake11.Controllers;

namespace KeepAwake11;

public sealed partial class MainWindow : Window
{
    private readonly AppController _controller = new();

    public MainWindow()
    {
        this.InitializeComponent();

        (this.Content as FrameworkElement).Loaded += MainWindow_Loaded;
    }

    private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await _controller.InitializeAsync(this);

        KeepAwakeToggle.IsOn = _controller.ViewModel.Settings.KeepComputerAwake;

        StatusText.Text = _controller.ViewModel.Settings.KeepComputerAwake
            ? "Keep Awake: ACTIVE"
            : "Keep Awake: INACTIVE";

        _controller.HideWindow();
    }

    private async void KeepAwakeToggle_Toggled(object sender, RoutedEventArgs e)
    {
        var vm = _controller.ViewModel;

        vm.Settings.KeepComputerAwake = KeepAwakeToggle.IsOn;

        vm.ApplySettings();

        StatusText.Text = KeepAwakeToggle.IsOn
            ? "Keep Awake: ACTIVE"
            : "Keep Awake: INACTIVE";

        await vm.SaveAsync();
    }
}
