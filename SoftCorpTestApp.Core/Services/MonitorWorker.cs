using Microsoft.Extensions.Hosting;

namespace SoftCorpTestApp.Core.Services
{
    public class MonitorWorker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"Worker running at: {DateTimeOffset.Now}");
                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}
