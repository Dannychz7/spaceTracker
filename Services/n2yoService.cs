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
        }

        // 1. Get satellite position (TLE) data for real-time tracking
        public async Task<SatellitePosition?> GetSatellitePosition(int satelliteId, double observerLat, double observerLng, double observerAlt, int seconds = 1)
        {
            try
            {
                var url = $"{BaseUrl}/positions/{satelliteId}/{observerLat}/{observerLng}/{observerAlt}/{seconds}/&apiKey={_apiKey}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<SatellitePosition>(content, _jsonOptions);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching satellite position for ID {SatelliteId}: {Message}", satelliteId, ex.Message);
                return null;
            }
        }

        // 2. Get visual passes (when satellite is visible from observer location)
        public async Task<VisualPassesResponse?> GetVisualPasses(int satelliteId, double observerLat, double observerLng, double observerAlt, int days = 10, int minVisibility = 300)
        {
            try
            {
                var url = $"{BaseUrl}/visualpasses/{satelliteId}/{observerLat}/{observerLng}/{observerAlt}/{days}/{minVisibility}/&apiKey={_apiKey}";
                Console.WriteLine(url);
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<VisualPassesResponse>(content, _jsonOptions);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching visual passes for satellite ID {SatelliteId}: {Message}", satelliteId, ex.Message);
                return null;
            }
        }

        // 3. Get radio passes (when satellite is above horizon for radio communication)
        public async Task<RadioPassesResponse?> GetRadioPasses(int satelliteId, double observerLat, double observerLng, double observerAlt, int days = 10, int minElevation = 0)
        {
            try
            {
                var url = $"{BaseUrl}/radiopasses/{satelliteId}/{observerLat}/{observerLng}/{observerAlt}/{days}/{minElevation}/&apiKey={_apiKey}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<RadioPassesResponse>(content, _jsonOptions);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching radio passes for satellite ID {SatelliteId}: {Message}", satelliteId, ex.Message);
                return null;
            }
        }

        // 4. Get satellites above a specific location (what's overhead right now)
        public async Task<SatellitesAboveResponse?> GetSatellitesAbove(double observerLat, double observerLng, double observerAlt, int searchRadius = 70, int categoryId = 0)
        {
            try
            {
                var url = $"{BaseUrl}/above/{observerLat}/{observerLng}/{observerAlt}/{searchRadius}/{categoryId}/&apiKey={_apiKey}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<SatellitesAboveResponse>(content, _jsonOptions);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching satellites above location ({Lat}, {Lng}): {Message}", observerLat, observerLng, ex.Message);
                return null;
            }
        }

        // 5. Get TLE (Two-Line Element) data for a satellite
        public async Task<TleResponse?> GetSatelliteTLE(int satelliteId)
        {
            try
            {
                var url = $"{BaseUrl}/tle/{satelliteId}&apiKey={_apiKey}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<TleResponse>(content, _jsonOptions);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error fetching TLE for satellite ID {SatelliteId}: {Message}", satelliteId, ex.Message);
                return null;
            }
        }

        // Bonus: Helper method to get ISS position (NORAD ID: 25544)
        public async Task<SatellitePosition?> GetISSPosition(double observerLat, double observerLng, double observerAlt = 0)
        {
            const int issNoradId = 25544;
            return await GetSatellitePosition(issNoradId, observerLat, observerLng, observerAlt);
        }
    }
}