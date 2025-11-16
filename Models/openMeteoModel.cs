// Model class for Open Meteo API responses
// Author: Daniel Chavez 
// Last Updated: November 2025

namespace spaceTracker.Models;
using System.Text.Json.Serialization;

public class weatherResponse
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("current")]
    public CurrentWeather? Current { get; set; }
}

public class CurrentWeather
{
    [JsonPropertyName("temperature_2m")]
    public double Temperature_2m { get; set; }

    [JsonPropertyName("wind_speed_10m")]
    public double WindSpeed_10m { get; set; }

    [JsonPropertyName("time")]
    public DateTime Time { get; set; }
}
