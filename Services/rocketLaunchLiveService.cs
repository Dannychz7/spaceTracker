// ---------------------------------------------------------------------------
// RocketLaunchLiveService: A service for retrieving real-time rocket launch
// schedule data from the RocketLaunch.Live API. This class fetches upcoming
// launches, including launch windows, vehicles, locations, and mission details.
// No API key is required for basic launch schedule retrieval.
// Author: Daniel Chavez 
// Last Updated: November 2025
// ---------------------------------------------------------------------------

using spaceTracker.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace spaceTracker.Services
{
    public class RocketLaunchLiveService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RocketLaunchLiveService> _logger;

        // Allows case-insensitive matching of JSON properties when mapping to models16
        private readonly JsonSerializerOptions _jsonOptions = new() 
        { 
            PropertyNameCaseInsensitive = true 
        };

        public RocketLaunchLiveService(HttpClient httpClient, ILogger<RocketLaunchLiveService> logger)
        {
            // Inject HttpClient + logging; RocketLaunch.Live requires no auth token
            _httpClient = httpClient;
            _logger = logger;
        }

        // ---------------------------------------------------------------------------
        // Retrieves the next 5 scheduled rocket launches from RocketLaunch.Live.
        // Returns a list of launch items containing mission, vehicle, pad, and time info.
        // ---------------------------------------------------------------------------
        public async Task<List<RocketLaunchItem>?> GetUpcomingLaunches()
        {
            _logger.LogInformation("[RocketLaunchLiveService] Fetching upcoming rocket launches");

            try
            {
                // RocketLaunch.Live public endpoint returning the next 5 launches
                var url = "https://fdo.rocketlaunch.live/json/launches/next/5";

                // Issue GET request
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Get JSON content from API
                var content = await response.Content.ReadAsStringAsync();

                // Map into RocketLaunchResponse model
                var launchData = JsonSerializer.Deserialize<RocketLaunchResponse>(content, _jsonOptions);

                // Return the list of launch items
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
