using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace TerasoiogyLauncher.ViewModels
{
    [ObservableObject]
    public partial class MainWindowViewModel
    {
        [RelayCommand]
        public void Close() => Task.Run(() => Environment.Exit(0));
        [RelayCommand]
#pragma warning disable CS8602 // 解引用可能出现空引用。
        public void Minimum(object window) => (window as MainWindow).WindowState = WindowState.Minimized;
    }
}
