using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows;

namespace TerasoiogyLauncher.ViewModels
{
    [ObservableObject]
    public partial class MainWindowViewModel
    {
        [RelayCommand]
        public void Close()
        {
            Environment.Exit(0);
        }
        [RelayCommand]
        public void Minimum()
        {
            
        }
    }
}
