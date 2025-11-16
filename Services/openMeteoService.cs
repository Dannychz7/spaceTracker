// ---------------------------------------------------------------------------
// weatherService: A lightweight service for retrieving real-time weather data
// using the Open-Meteo API (no API key required). This service fetches 
// temperature, wind speed, and other optional weather metrics for any 
// latitude/longitude on Earth.
// Author: Daniel Chavez 
// Last Updated: November 2025
// ---------------------------------------------------------------------------

using spaceTracker.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace spaceTracker.Services;

public class weatherService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<weatherService> _logger;

    // Allow JSON properties in the API response to be matched case-insensitively
    private readonly JsonSerializerOptions _jsonOptions = new() 
    { 
        PropertyNameCaseInsensitive = true 
    };

    public weatherService(HttpClient httpClient, ILogger<weatherService> logger)
    {
        // Open-Meteo API requires no auth tokens, so only HttpClient + Logger are injected
        _httpClient = httpClient;
        _logger = logger;
    }
    
    // ---------------------------------------------------------------------------
    // Fetches real-time weather data for a given latitude/longitude coordinate.
    // Returns current temperature and wind speed, and can be expanded to include
    // hourly conditions such as humidity and additional metrics.
    // ---------------------------------------------------------------------------
    public async Task<weatherResponse?> GetWeatherData(double latitude, double longitude)
    {
        _logger.LogInformation("[WeatherService] Fetching weather for lat: {Lat}, lon: {Lon}", latitude, longitude);

        try
        {
            // Base weather API request.
            // To add hourly forecasts, append:
            //   &hourly=temperature_2m,relative_humidity_2m,wind_speed_10m
            var url = $"https://api.open-meteo.com/v1/forecast?" +
                      $"latitude={latitude}&longitude={longitude}" +
                      $"&current=temperature_2m,wind_speed_10m";

            // Make the HTTP GET request
            var response = await _httpClient.GetAsync(url);
            
            // Throw an exception if status code is not successful (4xx/5xx)
            response.EnsureSuccessStatusCode();

            // Read raw JSON
            var content = await response.Content.ReadAsStringAsync();

            // Deserialize JSON into weatherResponse model
            return JsonSerializer.Deserialize<weatherResponse>(content, _jsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError("[WeatherService] Error fetching weather: {Message}", ex.Message);
            return null;
        }
    }
}
