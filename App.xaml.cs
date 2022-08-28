using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TerasoiogyLauncher.Services;
using TerasoiogyLauncher.ViewModels;

namespace TerasoiogyLauncher
{
    public partial class App : Application
    {
        public App()
        {
            Ioc.Default.ConfigureServices(new ServiceCollection()
                .AddSingleton<MainWindow>()
                .AddTransient<INimLauncherCliService, NimLauncherCliService>()
                .AddSingleton<DevTestViewModel>()
                .BuildServiceProvider());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Ioc.Default.GetRequiredService<MainWindow>().Show();
            base.OnStartup(e);
        }
    }
}