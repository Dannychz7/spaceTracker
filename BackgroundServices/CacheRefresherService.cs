using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using spaceTracker.Data;
using spaceTracker.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace spaceTracker.BackgroundServices
{
    public class CacheRefresherService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CacheRefresherService> _logger;

        public CacheRefresherService(IServiceProvider serviceProvider, ILogger<CacheRefresherService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();

                    var dbContext = scope.ServiceProvider.GetRequiredService<SpaceTrackerDbContext>();
                    var spaceDevs = scope.ServiceProvider.GetRequiredService<SpaceDevsService>();
                    var n2yo = scope.ServiceProvider.GetRequiredService<n2yoService>();
                    var weather = scope.ServiceProvider.GetRequiredService<weatherService>();

                    _logger.LogInformation("CacheRefresherService: Refresh cycle started at {Time}", DateTime.UtcNow);

                    await RefreshUpcomingLaunchesAsync(spaceDevs, dbContext);
                    await RefreshISSPositionAsync(n2yo);
                    await RefreshWeatherDataAsync(spaceDevs, weather);

                    _logger.LogInformation("CacheRefresherService: Refresh cycle completed at {Time}", DateTime.UtcNow);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during cache refresh");
                }

                // Wait 10 minutes before next refresh
                await Task.Delay(TimeSpan.FromMinutes(.5), stoppingToken);
            }
        }

        private async Task RefreshUpcomingLaunchesAsync(SpaceDevsService spaceDevs, SpaceTrackerDbContext db)
        {
            try
            {
                var launches = await spaceDevs.GetUpcomingLaunchesAsync(limit: 40);
                if (launches == null || launches.Count == 0)
                {
                    _logger.LogWarning("No launches returned during refresh");
                    return;
                }

                _logger.LogInformation("Refreshed {Count} upcoming launches from SpaceDevs", launches.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing upcoming launches");
            }
        }

        private async Task RefreshISSPositionAsync(n2yoService n2yo)
        {
            try
            {
                var iss = await n2yo.GetISSPosition(42.2626, -71.8023, 0);
                if (iss?.Positions?.Count > 0)
                {
                    var pos = iss.Positions[0];
                    _logger.LogInformation("ISS position updated: lat={Lat}, lon={Lon}, alt={Alt}km",
                        pos.Satlatitude, pos.Satlongitude, pos.Sataltitude);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing ISS position");
            }
        }

        private async Task RefreshWeatherDataAsync(SpaceDevsService spaceDevs, weatherService weather)
        {
            try
            {
                var launches = await spaceDevs.GetUpcomingLaunchesAsync(limit: 5);
                int updated = 0;

                foreach (var launch in launches)
                {
                    if (launch.Pad?.latitude != null && launch.Pad?.longitude != null)
                    {
                        await weather.GetWeatherData(launch.Pad.latitude.Value, launch.Pad.longitude.Value);
                        updated++;
                    }
                }

                _logger.LogInformation("Refreshed weather data for {Count} launch pads", updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error refreshing weather data");
            }
        }
    }
}

