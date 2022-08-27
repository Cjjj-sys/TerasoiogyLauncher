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

namespace TerasoiogyLauncher.ViewModels
{
    [ObservableObject]
    public partial class DevTestViewModel
    {
        [ObservableProperty]
        private string? _toExcuteCommand;
        [ObservableProperty]
        private string _outputResult;

        [RelayCommand]
        private async void Excute()
        {
            try
            {
                var input = ToExcuteCommand.Split(' ').ToList();
                var command = input[0];
                input.RemoveAt(0);
                var args = input;
                var cmd = Cli.Wrap(command).WithArguments(args) | PipeTarget.ToDelegate(output => OutputResult += output + Environment.NewLine);
                await cmd.ExecuteBufferedAsync();
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
