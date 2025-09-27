using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Daitan.BackgroundServices.Controllers
{
    //public class DeleteHistoryServiceController : BackgroundService
    //{
    //    private readonly ILogger<DeleteHistoryServiceController> _logger;
    //    public DeleteHistoryServiceController(ILogger<DeleteHistoryServiceController> logger)
    //    {
    //        _logger = logger;
    //    }
    //    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //    {
    //        _logger.LogInformation("DailyJobService is starting.");

    //        while (!stoppingToken.IsCancellationRequested)
    //        {
    //            var now = DateTime.Now;
    //            var nextRunTime = DateTime.Today.AddDays(1).AddHours(0); // Set to run at 12:00 AM
    //            var delay = nextRunTime - now;

    //            _logger.LogInformation($"Next run scheduled for {nextRunTime} (in {delay.TotalMinutes} minutes)");

    //            if (delay.TotalMilliseconds <= 0)
    //                delay = TimeSpan.FromDays(1);

    //            await Task.Delay(delay, stoppingToken); // Wait until next run

    //            if (!stoppingToken.IsCancellationRequested)
    //            {
    //                await DoDailyWork(stoppingToken);
    //            }
    //        }
    //    }
    //    private async Task DoDailyWork(CancellationToken cancellationToken)
    //    {
    //        _logger.LogInformation($"Daily work started at {DateTime.Now}");

    //        // Your daily logic here
    //        int k = 0;

    //        await Task.Delay(5000, cancellationToken); // Simulate work

    //        _logger.LogInformation("Daily work completed");
    //    }
    //}

}

