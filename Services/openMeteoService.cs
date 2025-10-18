using spaceTracker.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace spaceTracker.Server.Services;

public class weatherService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<weatherService> _logger;
    private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };
    public weatherService(HttpClient httpClient, ILogger<weatherService> logger) // Does not need an API Key
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    
    public async Task<weatherResponse?> GetWeatherData(double latitude, double longitude)
    {
        _logger.LogInformation("[WeatherService] Fetching weather for lat: {Lat}, lon: {Lon}", latitude, longitude);
        try
        {
            // This call will get you current temperature and wind speed
            // This call will get you hourly data as well -> add &hourly=temperature_2m,relative_humidity_2m,wind_speed_10m
            var url = $"https://api.open-meteo.com/v1/forecast?" +
                      $"latitude={latitude}&longitude={longitude}" +
                      $"&current=temperature_2m,wind_speed_10m";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<weatherResponse>(content, _jsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError("[WeatherService] Error fetching weather: {Message}", ex.Message);
            return null;
        }
    }
}