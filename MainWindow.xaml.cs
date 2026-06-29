using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using KeepAwake11.Services;
using KeepAwake11.ViewModels;

namespace KeepAwake11;

public sealed partial class MainWindow : Window
{

    private  readonly MainViewModel _viewModel = new();
    public MainWindow()
    {
        this.InitializeComponent();
    }

    private void KeepAwakeToggle_Toggled(object sender, RoutedEventArgs e)
    {
        _viewModel.Settings.KeepComputerAwake = KeepAwakeToggle.IsOn;

        _viewModel.ApplySettings();

        StatusText.Text = KeepAwakeToggle.IsOn
            ? "Keep Awake: ON"
            : "Keep Awake: OFF";
    }
}
