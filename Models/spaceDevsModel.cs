using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace spaceTracker.Models
{
    // -------------------------
    // Response Wrappers
    // -------------------------
    public class SpaceDevsResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("results")]
        public List<SpaceDevsLaunch>? Results { get; set; }
    }

    public class SpaceEventResponse
    {
        [JsonPropertyName("results")]
        public List<SpaceEvent>? Results { get; set; }
    }

    public class AstronautResponse
    {
        [JsonPropertyName("results")]
        public List<Astronaut>? Results { get; set; }
    }

    public class SpacewalkResponse
    {
        [JsonPropertyName("results")]
        public List<Spacewalk>? Results { get; set; }
    }

    public class SpaceStationResponse
    {
        [JsonPropertyName("results")]
        public List<SpaceStation>? Results { get; set; }
    }

    public class DockingEventResponse
    {
        [JsonPropertyName("results")]
        public List<DockingEvent>? Results { get; set; }
    }

    public class ExpeditionResponse
    {
        [JsonPropertyName("results")]
        public List<Expedition>? Results { get; set; }
    }

    public class AgencyResponse
    {
        [JsonPropertyName("results")]
        public List<Agency>? Results { get; set; }
    }

    public class ProgramResponse
    {
        [JsonPropertyName("results")]
        public List<SpaceProgram>? Results { get; set; }
    }

    public class SpacecraftResponse
    {
        [JsonPropertyName("results")]
        public List<Spacecraft>? Results { get; set; }
    }

    public class LauncherConfigurationResponse
    {
        [JsonPropertyName("results")]
        public List<LauncherConfiguration>? Results { get; set; }
    }

    public class LocationResponse
    {
        [JsonPropertyName("results")]
        public List<Location>? Results { get; set; }
    }

    public class PadResponse
    {
        [JsonPropertyName("results")]
        public List<Pad>? Results { get; set; }
    }

    // -------------------------
    // Core Models
    // -------------------------
    public class SpaceDevsLaunch
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("net")]
        public DateTime? Net { get; set; }

        [JsonPropertyName("status")]
        public LaunchStatus? Status { get; set; }

        [JsonPropertyName("rocket")]
        public Rocket? Rocket { get; set; }

        [JsonPropertyName("pad")]
        public Pad? Pad { get; set; }

        [JsonPropertyName("mission")]
        public Mission? Mission { get; set; }

        [JsonPropertyName("launch_service_provider")]
        public Agency? Provider { get; set; }

        [JsonPropertyName("window_start")]
        public DateTime? WindowStart { get; set; }

        [JsonPropertyName("window_end")]
        public DateTime? WindowEnd { get; set; }

        [JsonPropertyName("infographic")]
        public string? Infographic { get; set; }

        [JsonPropertyName("image")]
        public ProgramImage? Image { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

    public class LaunchStatus
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("abbrev")]
        public string? Abbrev { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    public class Rocket
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("configuration")]
        public LauncherConfiguration? Configuration { get; set; }

        [JsonPropertyName("spacecraft_stage")]
        public List<SpacecraftStage>? SpacecraftStage { get; set; }

        [JsonPropertyName("launcher_stage")]
        public List<LauncherStage>? LauncherStage { get; set; }
    }

    public class Pad
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("latitude")]
        public float? latitude { get; set; }

        [JsonPropertyName("longitude")]
        public float? longitude { get; set; }

        [JsonPropertyName("location")]
        public Location? Location { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("country_code")]
        public string? CountryCode { get; set; }

    }

    public class Mission
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public class Agency
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("abbrev")]
        public string? Abbrev { get; set; }

        [JsonPropertyName("type")]
        public SimpleTypeObject? Type { get; set; }

        [JsonPropertyName("country_code")]
        public string? CountryCode { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }

    public class Astronaut
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("status")]
        public SimpleTypeObject? Status { get; set; }  // Active, Retired, etc.

        [JsonPropertyName("type")]
        public SimpleTypeObject? Type { get; set; }  // Government, Commercial, etc.

        [JsonPropertyName("nationality")]
        public List<NationalityInfo>? Nationality { get; set; }

        [JsonPropertyName("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        [JsonPropertyName("date_of_death")]
        public DateTime? DateOfDeath { get; set; }

        [JsonPropertyName("image")]
        public ProgramImage? Image { get; set; }

        [JsonPropertyName("agency")]
        public Agency? Agency { get; set; }

        [JsonPropertyName("profile_image_thumbnail")]
        public string? ProfileImageThumbnail { get; set; }

        [JsonPropertyName("profile_image")]
        public string? ProfileImage { get; set; }

        [JsonPropertyName("bio")]
        public string? Bio { get; set; }

        [JsonPropertyName("twitter")]
        public string? Twitter { get; set; }

        [JsonPropertyName("instagram")]
        public string? Instagram { get; set; }

        [JsonPropertyName("wiki")]
        public string? Wiki { get; set; }

        [JsonPropertyName("first_flight")]
        public DateTime? FirstFlight { get; set; }

        [JsonPropertyName("last_flight")]
        public DateTime? LastFlight { get; set; }

        [JsonPropertyName("flights_count")]
        public int FlightsCount { get; set; }

        [JsonPropertyName("landings_count")]
        public int LandingsCount { get; set; }

        [JsonPropertyName("spacewalks_count")]
        public int SpacewalksCount { get; set; }

        [JsonPropertyName("time_in_space")]
        public string? TimeInSpace { get; set; }  // Duration string
    }

    public class SpaceEvent
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }

        [JsonPropertyName("feature_image")]
        public string? FeatureImage { get; set; }
    }

    public class SpaceStation
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("type")]
        public SimpleTypeObject? Type { get; set; }

        [JsonPropertyName("founded")]
        public DateTime? Founded { get; set; }
    }

    public class Spacecraft
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("serial_number")]
        public string? SerialNumber { get; set; }

        [JsonPropertyName("image")]
        public ProgramImage? Image { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("spacecraft_config")]
        public SpacecraftConfiguration? SpacecraftConfig { get; set; }
    }

    public class CraftFamily
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("maiden_flight")]
        public string? MaidenFlight { get; set; }

        [JsonPropertyName("spacecraft_flown")]
        public int? SpacecraftFlown { get; set; }

        [JsonPropertyName("total_launch_count")]
        public int? TotalLaunchCount { get; set; }

        [JsonPropertyName("successful_launches")]
        public int? SuccessfulLaunches { get; set; }

        [JsonPropertyName("failed_launches")]
        public int? FailedLaunches { get; set; }

        [JsonPropertyName("attempted_landings")]
        public int? AttemptedLandings { get; set; }

        [JsonPropertyName("successful_landings")]
        public int? SuccessfulLandings { get; set; }

        [JsonPropertyName("failed_landings")]
        public int? FailedLandings { get; set; }
    }

    public class SpacecraftConfiguration
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("type")]
        public SimpleTypeObject? Type { get; set; }
        
        [JsonPropertyName("family")]
        public List<CraftFamily>? Family { get; set; }

        [JsonPropertyName("maiden_flight")]
        public string? MaidenFlight { get; set; }
    }

    public class SpacecraftStage
    {
        [JsonPropertyName("destination")]
        public string? Destination { get; set; }

        [JsonPropertyName("spacecraft")]
        public Spacecraft? Spacecraft { get; set; }
    }

    public class LauncherStage
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public string? Type { get; set; }
    }

    public class Spacewalk
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("crew")]
        public List<SpacewalkCrew>? Crew { get; set; }

        [JsonPropertyName("start")]
        public DateTime? Start { get; set; }

        [JsonPropertyName("duration")]
        public string? Duration { get; set; }
    }

    public class Expedition
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("start")]
        public DateTime? Start { get; set; }

        [JsonPropertyName("end")]
        public DateTime? End { get; set; }

        [JsonPropertyName("crew")]
        public List<Astronaut>? Crew { get; set; }

        [JsonPropertyName("space_station")]
        public SpaceStation? SpaceStation { get; set; }
    }

    public class DockingEvent
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("docking")]
        public DateTime? Docking { get; set; }

        [JsonPropertyName("departure")]
        public DateTime? Departure { get; set; }

        [JsonPropertyName("docked")]
        public bool Docked { get; set; }

        [JsonPropertyName("space_station")]
        public SpaceStation? SpaceStation { get; set; }

        [JsonPropertyName("spacecraft")]
        public Spacecraft? Spacecraft { get; set; }
    }

    public class LauncherConfiguration
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("family")]
        public string? Family { get; set; }

        [JsonPropertyName("variant")]
        public string? Variant { get; set; }

        [JsonPropertyName("full_name")]
        public string? FullName { get; set; }
    }

    public class SpaceProgram
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("start_date")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTime? EndDate { get; set; }

        [JsonPropertyName("image")]
        public ProgramImage? Image { get; set; }

        [JsonPropertyName("agencies")]
        public List<Agency>? Agencies { get; set; }
    }

    public class ProgramImage
    {
        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }
    }

    public class StarshipDashboard
    {
        [JsonPropertyName("last_update")]
        public DateTime? LastUpdate { get; set; }

        [JsonPropertyName("next_launch")]
        public SpaceDevsLaunch? NextLaunch { get; set; }

        [JsonPropertyName("previous_launch")]
        public SpaceDevsLaunch? PreviousLaunch { get; set; }
    }

    // Add this class to handle the common "type" objects
    public class SimpleTypeObject
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    // Add these classes to handle the complex Spacewalk crew structure
    public class SpacewalkCrew
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("astronaut")]
        public Astronaut? Astronaut { get; set; }
    }

    public class NationalityInfo
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("nationality_name")]
        public string? NationalityName { get; set; }
    }

    public class Destination
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

}
