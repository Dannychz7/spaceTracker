using spaceTracker.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace spaceTracker.Services
{
    public class RocketLaunchLiveService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RocketLaunchLiveService> _logger;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public RocketLaunchLiveService(HttpClient httpClient, ILogger<RocketLaunchLiveService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<RocketLaunchItem>?> GetUpcomingLaunches()
        {
            _logger.LogInformation("[RocketLaunchLiveService] Fetching upcoming rocket launches");
            try
            {
                var url = "https://fdo.rocketlaunch.live/json/launches/next/5";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var launchData = JsonSerializer.Deserialize<RocketLaunchResponse>(content, _jsonOptions);
                return launchData?.Results;
            }
            catch (Exception ex)
            {
                _logger.LogError("[RocketLaunchLiveService] Error fetching launches: {Message}", ex.Message);
                return null;
            }
        }
    }
}
