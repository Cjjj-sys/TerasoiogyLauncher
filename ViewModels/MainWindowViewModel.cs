using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace TerasoiogyLauncher.ViewModels
{
    [ObservableObject]
    public partial class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            _currentViewModel = _homeViewModel;
        }

        private HomeViewModel _homeViewModel = new();
        private DownloadGameViewModel _downloadGameViewModel = new();
        private DevTestViewModel _devTestViewModel = Ioc.Default.GetRequiredService<DevTestViewModel>();
        private DevControlsViewModel _devControlsViewModel = new();

        [ObservableProperty]
        private INotifyPropertyChanged _currentViewModel;

        [RelayCommand]
        public void Close() => Task.Run(() => Environment.Exit(0));

        [RelayCommand]
        public void Minimum(object window) => (window as MainWindow).WindowState = WindowState.Minimized;

        [RelayCommand]
        public void NavigateTo(string page)
        {
            /* if (CurrentViewModel is HomeViewModel)
             {
                 _homeViewModel = (HomeViewModel)CurrentViewModel;
             }
             else if (CurrentViewModel is DownloadGameViewModel)
             {
                 _downloadGameViewModel = (DownloadGameViewModel)CurrentViewModel;
             }
             else if  (CurrentViewModel is DevTestViewModel)
             {
                 _devTestViewModel = (DevTestViewModel)CurrentViewModel;
             }*/

            CurrentViewModel = page switch
            {
                "Home" => _homeViewModel,
                "DownloadGame" => _downloadGameViewModel,
                "DevTest" => _devTestViewModel,
                "DevControls" => _devControlsViewModel
            };
        }
    }
}