using CliWrap;
using CliWrap.Buffered;
using System;
using System.Threading.Tasks;

namespace TerasoiogyLauncher.Services;

/// <summary>
/// 对Nim启动器各种命令的封装服务
/// </summary>
public class NimLauncherCliService : INimLauncherCliService
{
    /// <summary>
    /// Nim启动器的文件名
    /// </summary>
    private const string NIMLAUNCHER_FILENAME = "Main.exe";

    /// <summary>
    /// 初始化命令的封装
    /// </summary>
    /// <returns></returns>
    public async Task<bool> InitAsync()
    {
        try
        {
            await Cli.Wrap(NIMLAUNCHER_FILENAME).WithArguments("init").ExecuteAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// 下载游戏命令的封装
    /// </summary>
    /// <param name="version"></param>
    /// <param name="progressBarAction"></param>
    /// <returns></returns>
    public async Task DownloadGameAsync(string version, Action<string, double?, string?> progressBarAction)
    {
        await DownloadAsync(version, progressBarAction);
    }

    /// <summary>
    /// 安装游戏命令的封装
    /// </summary>
    /// <param name="version"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public async Task InstallGameAsync(string version, Action<string> action)
    {
        var cli = Cli.Wrap(NIMLAUNCHER_FILENAME).WithArguments("install_game") | PipeTarget.ToDelegate(output =>
        {
            action.Invoke(output);
        });
        await cli.ExecuteBufferedAsync();
    }

    /// <summary>
    /// 下载JRE命令的封装
    /// </summary>
    /// <param name="version"></param>
    /// <param name="progressBarAction"></param>
    /// <returns></returns>
    public async Task DownloadJREAsync(string version, Action<string, double?, string?> progressBarAction)
    {
        await DownloadAsync(version, progressBarAction);
    }

    /// <summary>
    /// 提取可复用的下载方法
    /// </summary>
    /// <param name="version"></param>
    /// <param name="progressBarAction"></param>
    /// <returns></returns>
    private static async Task DownloadAsync(string version, Action<string, double?, string?> progressBarAction)
    {
        var cli = Cli.Wrap(NIMLAUNCHER_FILENAME).WithArguments(new string[] { "download_jre", version }) | PipeTarget.ToDelegate(output =>
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
            else if (output.StartsWith("Current rate"))
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