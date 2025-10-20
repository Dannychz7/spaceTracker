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
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };
        private const string BaseUrl = "https://api.n2yo.com/rest/v1/satellite";

        public n2yoService(HttpClient httpClient, IConfiguration configuration, ILogger<n2yoService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _apiKey = configuration["N2YO:ApiKey"] ?? "null";
            _logger.LogInformation("n2yoService initialized with API Key: {ApiKey}", _apiKey != "null" ? "[REDACTED]" : "null");
        }

        // 1. Get satellite position (TLE) data for real-time tracking
        public async Task<SatellitePosition?> GetSatellitePosition(int satelliteId, double observerLat, double observerLng, double observerAlt, int seconds = 1)
        {
            var url = $"{BaseUrl}/positions/{satelliteId}/{observerLat}/{observerLng}/{observerAlt}/{seconds}/&apiKey={_apiKey}";
            _logger.LogDebug("Fetching satellite position with URL: {Url}", url);

            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                _logger.LogDebug("Satellite position response length: {Length}", content.Length);

                var data = JsonSerializer.Deserialize<SatellitePosition>(content, _jsonOptions);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching satellite position for ID {SatelliteId}", satelliteId);
                return null;
            }
        }

        // 2. Get visual passes (when satellite is visible from observer location)
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

                var data = JsonSerializer.Deserialize<VisualPassesResponse>(content, _jsonOptions);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching visual passes for satellite ID {SatelliteId}", satelliteId);
                return null;
            }
        }

        // 3. Get radio passes (when satellite is above horizon for radio communication)
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

                var data = JsonSerializer.Deserialize<RadioPassesResponse>(content, _jsonOptions);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching radio passes for satellite ID {SatelliteId}", satelliteId);
                return null;
            }
        }

        // 4. Get satellites above a specific location (what's overhead right now)
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

                var data = JsonSerializer.Deserialize<SatellitesAboveResponse>(content, _jsonOptions);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching satellites above location ({Lat}, {Lng})", observerLat, observerLng);
                return null;
            }
        }

        // 5. Get TLE (Two-Line Element) data for a satellite
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

                var data = JsonSerializer.Deserialize<TleResponse>(content, _jsonOptions);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching TLE for satellite ID {SatelliteId}", satelliteId);
                return null;
            }
        }

        // Bonus: Helper method to get ISS position (NORAD ID: 25544)
        public async Task<SatellitePosition?> GetISSPosition(double observerLat, double observerLng, double observerAlt = 0)
        {
            const int issNoradId = 25544;
            _logger.LogDebug("Fetching ISS position for observer at ({Lat}, {Lng})", observerLat, observerLng);
            return await GetSatellitePosition(issNoradId, observerLat, observerLng, observerAlt);
        }
    }
}
