using System;
using System.Threading.Tasks;

namespace TerasoiogyLauncher.Services
{
    public interface INimLauncherCliService
    {
        Task DownloadGameAsync(string version, Action<string, double?, string?> progressBarAction);

        Task<bool> InitAsync();
    }
}