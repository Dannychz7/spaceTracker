using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace spaceTracker.Models
{
    public class RocketLaunchResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("result")]
        public List<RocketLaunchItem> Results { get; set; } = new();
    }

    public class RocketLaunchItem
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("win_open")]
        public DateTime? Win_open { get; set; }

        [JsonPropertyName("t0")]
        public DateTime? T0 { get; set; }

        [JsonPropertyName("provider")]
        public ProviderInfo? Provider { get; set; }

        [JsonPropertyName("pad")]
        public LaunchPad? Pad { get; set; }

        [JsonPropertyName("vehicle")]
        public VehicleInfo? Vehicle { get; set; }

        [JsonPropertyName("launch_description")]
        public string? LaunchDescription { get; set; }
    }

    public class ProviderInfo
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class VehicleInfo
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class LaunchPad
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("location")]
        public LaunchPadLocation? Location { get; set; }
    }

    public class LaunchPadLocation
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("statename")]
        public string? StateName { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }
    }
}



