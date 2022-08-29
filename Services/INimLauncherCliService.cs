using System;
using System.Threading.Tasks;

namespace TerasoiogyLauncher.Services
{
    public interface INimLauncherCliService
    {
        Task DownloadGameAsync(string version, Action<string, double?, string?> progressBarAction);
        Task DownloadJREAsync(string version, Action<string, double?, string?> progressBarAction);
        Task<bool> InitAsync();
        Task InstallGameAsync(string version, Action<string> action);
    }
}