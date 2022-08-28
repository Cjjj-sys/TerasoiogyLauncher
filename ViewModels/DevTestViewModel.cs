using CliWrap;
using CliWrap.Buffered;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TerasoiogyLauncher.Services;

namespace TerasoiogyLauncher.ViewModels
{
    [ObservableObject]
    public partial class DevTestViewModel
    {
        public DevTestViewModel(INimLauncherCliService nimLauncherCliService)
        {
            this.nimLauncherCliService = nimLauncherCliService;
        }
        [ObservableProperty]
        private string _toDownloadVersion;
        [ObservableProperty]
        private double _progress = 0;
        [ObservableProperty]
        private string _outputResult;
        private readonly INimLauncherCliService nimLauncherCliService;

        [RelayCommand]
        private async void Excute()
        {
            try
            {
                /*var input = ToExcuteCommand.Split(' ').ToList();
                var command = input[0];
                input.RemoveAt(0);
                var args = input;
                var cmd = Cli.Wrap(command).WithArguments(args) | PipeTarget.ToDelegate(output => OutputResult += output + Environment.NewLine);
                await cmd.ExecuteBufferedAsync();*/
                await nimLauncherCliService.DownloadGameAsync(ToDownloadVersion, (info, progress, speed) =>
                {
                    OutputResult = $"下载进度: {Progress}, 下载速度: {speed}";
                    Progress = progress is null ? Progress : progress.Value;
                });
            }
            catch (Exception e)
            {
                MessageBox.Show($"草率地处理一下异常: \n{e.Message}", e.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        [RelayCommand]
        private void ShowOutputs()
        {
            MessageBox.Show($"Prop: {OutputResult}\nField: {_outputResult}");
        }
    }
}
