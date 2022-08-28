using CliWrap;
using CliWrap.Buffered;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace TerasoiogyLauncher.Services
{
    public class NimLauncherCliService : INimLauncherCliService
    {
        public async Task<bool> InitAsync()
        {
            try
            {
                await Cli.Wrap("Main.exe").WithArguments("init").ExecuteAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public async Task DownloadGameAsync(string version, Action<string, double?, string?> progressBarAction)
        {
            var cli = Cli.Wrap("Main.exe").WithArguments(new string[] { "download_game", version }) | PipeTarget.ToDelegate(output =>
            {
                double? progress = null;
                string? speed = null;
                if (output.StartsWith("Downloaded"))
                {
                    bool isDownloadBytes = double.TryParse(output.Split(' ')[1], out double downloadBytes);
                    bool isTotalBytes = double.TryParse(output.Split(' ')[3], out double totalBytes);
                    if (isDownloadBytes && isTotalBytes)
                    {
                        progress = (downloadBytes / totalBytes) * 100;
                    }
                    else
                    {
                        progress = null;
                    }
                }
                if (output.StartsWith("Current rate"))
                {
                    speed = output.Split(' ')[2];
                }
                if (output == "")
                {
                    progress = 100;
                    speed = "下载完成";
                }
                
                progressBarAction.Invoke(output, progress, speed);
            });
            await cli.ExecuteBufferedAsync();
        }
    }
}
