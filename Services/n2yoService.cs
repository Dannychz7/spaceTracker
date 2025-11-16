// -------------------------------------------------------------
// n2yoService: A service that communicates with the N2YO API to
// retrieve real-time satellite tracking data, including positions,
// passes, and Two-Line Element (TLE) orbital information. This class
// provides helper methods for visual, radio, and overhead satellite
// lookups, as well as ISS tracking.
// Author: Daniel Chavez 
// Last Updated: November 2025
// -------------------------------------------------------------

using spaceTracker.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace spaceTracker.Services
{
    public class n2yoService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<n2yoService> _logger;
        private readonly string _apiKey;

        // JSON options to allow case-insensitive deserialization from the API
        private readonly JsonSerializerOptions _jsonOptions = new() 
        { 
            PropertyNameCaseInsensitive = true 
        };

        // Base API URL for all N2YO endpoints
        private const string BaseUrl = "https://api.n2yo.com/rest/v1/satellite";

        public n2yoService(HttpClient httpClient, IConfiguration configuration, ILogger<n2yoService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            // Retrieve API key from configuration
            _apiKey = configuration["N2YO:ApiKey"] ?? "null";

            _logger.LogInformation("n2yoService initialized with API Key: {ApiKey}",
                _apiKey != "null" ? "[REDACTED]" : "null");
        }

        // -------------------------------------------------------------
        // Get satellite position and TLE-based real-time tracking data.
        // This returns coordinates for the next X seconds (default: 1 sec).
        // -------------------------------------------------------------
        public async Task<SatellitePosition?> GetSatellitePosition(int satelliteId, double observerLat, double observerLng, double observerAlt, int seconds = 1)
        {
            var url = $"{BaseUrl}/positions/{satelliteId}/{observerLat}/{observerLng}/{observerAlt}/{seconds}/&apiKey={_apiKey}";
            _logger.LogDebug("Fetching satellite position with URL: {Url}", url);

            try
            {
                // Issue GET request to the API
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Read JSON response as a string
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("Satellite position response length: {Length}", content.Length);

                // Deserialize JSON into C# model
                return JsonSerializer.Deserialize<SatellitePosition>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching satellite position for ID {SatelliteId}", satelliteId);
                return null;
            }
        }

        // -------------------------------------------------------------
        // Get visual passes — times when the satellite is visible from
        // the observer's location due to sunlight and angle.
        // -------------------------------------------------------------
        public async Task<VisualPassesResponse?> GetVisualPasses(int satelliteId, double observerLat, double observerLng, double observerAlt, int days = 10, int minVisibility = 300)
        {
            var url = $"{BaseUrl}/visualpasses/{satelliteId}/{observerLat}/{observerLng}/{observerAlt}/{days}/{minVisibility}/&apiKey={_apiKey}";
            _logger.LogDebug("Fetching visual passes with URL: {Url}", url);

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("Visual passes response length: {Length}", content.Length);

                return JsonSerializer.Deserialize<VisualPassesResponse>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching visual passes for satellite ID {SatelliteId}", satelliteId);
                return null;
            }
        }

        // -------------------------------------------------------------
        // Get radio passes — satellite windows where it is above the
        // radio horizon for communication purposes.
        // -------------------------------------------------------------
        public async Task<RadioPassesResponse?> GetRadioPasses(int satelliteId, double observerLat, double observerLng, double observerAlt, int days = 10, int minElevation = 0)
        {
            var url = $"{BaseUrl}/radiopasses/{satelliteId}/{observerLat}/{observerLng}/{observerAlt}/{days}/{minElevation}/&apiKey={_apiKey}";
            _logger.LogDebug("Fetching radio passes with URL: {Url}", url);

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("Radio passes response length: {Length}", content.Length);

                return JsonSerializer.Deserialize<RadioPassesResponse>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching radio passes for satellite ID {SatelliteId}", satelliteId);
                return null;
            }
        }

        // -------------------------------------------------------------
        // Get list of all satellites currently above the observer's
        // location within a given radius and category.
        // -------------------------------------------------------------
        public async Task<SatellitesAboveResponse?> GetSatellitesAbove(double observerLat, double observerLng, double observerAlt, int searchRadius = 70, int categoryId = 0)
        {
            var url = $"{BaseUrl}/above/{observerLat}/{observerLng}/{observerAlt}/{searchRadius}/{categoryId}/&apiKey={_apiKey}";
            _logger.LogDebug("Fetching satellites above location with URL: {Url}", url);

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("Satellites above response length: {Length}", content.Length);

                return JsonSerializer.Deserialize<SatellitesAboveResponse>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error fetching satellites above location ({Lat}, {Lng})",
                    observerLat, observerLng);

                return null;
            }
        }

        // -------------------------------------------------------------
        // Get TLE (Two-Line Element) orbital data for a satellite.
        // TLE is used to calculate precise orbital positions.
        // -------------------------------------------------------------
        public async Task<TleResponse?> GetSatelliteTLE(int satelliteId)
        {
            var url = $"{BaseUrl}/tle/{satelliteId}&apiKey={_apiKey}";
            _logger.LogDebug("Fetching TLE with URL: {Url}", url);

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("TLE response length: {Length}", content.Length);

                return JsonSerializer.Deserialize<TleResponse>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching TLE for satellite ID {SatelliteId}", satelliteId);
                return null;
            }
        }

        // -------------------------------------------------------------
        // Convenience method for fetching the ISS position.
        // The ISS has NORAD ID 25544 — this just wraps the main method.
        // -------------------------------------------------------------
        public async Task<SatellitePosition?> GetISSPosition(double observerLat, double observerLng, double observerAlt = 0)
        {
            const int issNoradId = 25544;

            _logger.LogDebug("Fetching ISS position for observer at ({Lat}, {Lng})",
                observerLat, observerLng);

            return await GetSatellitePosition(issNoradId, observerLat, observerLng, observerAlt);
        }
    }
}
