using spaceTracker.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace spaceTracker.Services;
public class SpaceDevsService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<SpaceDevsService> _logger;
    private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };
    private const string BASE_URL = "https://lldev.thespacedevs.com/2.3.0";

    public SpaceDevsService(HttpClient httpClient, ILogger<SpaceDevsService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    #region Launches
    public async Task<List<SpaceDevsLaunch>> GetUpcomingLaunchesAsync(int limit = 5, int offset = 0)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/launches/upcoming/?limit={limit}&offset={offset}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<SpaceDevsResponse>(content, _jsonOptions);

            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching upcoming launches: {ex.Message}");
            return [];
        }
    }

    public async Task<List<SpaceDevsLaunch>> GetPreviousLaunchesAsync(int limit = 5, int offset = 0)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/launches/previous/?limit={limit}&offset={offset}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<SpaceDevsResponse>(content, _jsonOptions);

            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching previous launches: {ex.Message}");
            return [];
        }
    }

    public async Task<SpaceDevsLaunch?> GetLaunchByIdAsync(string id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/launches/{id}/");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SpaceDevsLaunch>(content, _jsonOptions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching launch {id}: {ex.Message}");
            return null;
        }
    }
    #endregion

    #region Events
    public async Task<List<SpaceEvent>> GetUpcomingEventsAsync(int limit = 10, int offset = 0)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/events/upcoming/?limit={limit}&offset={offset}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<SpaceEventResponse>(content, _jsonOptions);

            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching events: {ex.Message}");
            return [];
        }
    }

    public async Task<List<SpaceEvent>> GetPreviousEventsAsync(int limit = 10, int offset = 0)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/events/previous/?limit={limit}&offset={offset}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<SpaceEventResponse>(content, _jsonOptions);

            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching previous events: {ex.Message}");
            return [];
        }
    }
    #endregion

    #region Astronauts
    public async Task<List<Astronaut>> GetAstronautsAsync(int limit = 10, int offset = 0, string? search = null)
    {
        try
        {
            var url = $"{BASE_URL}/astronauts/?limit={limit}&offset={offset}";
            if (!string.IsNullOrEmpty(search))
                url += $"&search={Uri.EscapeDataString(search)}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AstronautResponse>(content, _jsonOptions);

            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching astronauts: {ex.Message}");
            return [];
        }
    }

    public async Task<Astronaut?> GetAstronautByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/astronauts/{id}/");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Astronaut>(content, _jsonOptions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching astronaut {id}: {ex.Message}");
            return null;
        }
    }
    #endregion

    #region Spacewalks
    public async Task<List<Spacewalk>> GetSpacewalksAsync(int limit = 10, int offset = 0)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/spacewalks/?limit={limit}&offset={offset}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<SpacewalkResponse>(content, _jsonOptions);

            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching spacewalks: {ex.Message}");
            return [];
        }
    }
    #endregion

    #region Space Stations
    public async Task<List<SpaceStation>> GetSpaceStationsAsync(int limit = 10, int offset = 0)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/space_stations/?limit={limit}&offset={offset}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<SpaceStationResponse>(content, _jsonOptions);

            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching space stations: {ex.Message}");
            return [];
        }
    }

    public async Task<SpaceStation?> GetSpaceStationByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/space_stations/{id}/");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SpaceStation>(content, _jsonOptions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching space station {id}: {ex.Message}");
            return null;
        }
    }
    #endregion

    #region Docking Events
    public async Task<List<DockingEvent>> GetDockingEventsAsync(int limit = 10, int offset = 0)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/docking_events/?limit={limit}&offset={offset}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<DockingEventResponse>(content, _jsonOptions);

            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching docking events: {ex.Message}");
            return [];
        }
    }
    #endregion

    #region Expeditions
    public async Task<List<Expedition>> GetExpeditionsAsync(int limit = 10, int offset = 0)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/expeditions/?limit={limit}&offset={offset}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ExpeditionResponse>(content, _jsonOptions);

            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching expeditions: {ex.Message}");
            return [];
        }
    }
    #endregion

    #region Agencies
    public async Task<List<Agency>> GetAgenciesAsync(int limit = 10, int offset = 0, string? search = null)
    {
        try
        {
            var url = $"{BASE_URL}/agencies/?limit={limit}&offset={offset}";
            if (!string.IsNullOrEmpty(search))
                url += $"&search={Uri.EscapeDataString(search)}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AgencyResponse>(content, _jsonOptions);

            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching agencies: {ex.Message}");
            return [];
        }
    }

    public async Task<Agency?> GetAgencyByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/agencies/{id}/");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Agency>(content, _jsonOptions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching agency {id}: {ex.Message}");
            return null;
        }
    }
    #endregion

    #region Programs
    public async Task<List<spaceTracker.Models.SpaceProgram>> SpaceProg(int limit = 10, int offset = 0)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/programs/?limit={limit}&offset={offset}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ProgramResponse>(content, _jsonOptions);

            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching programs: {ex.Message}");
            return [];
        }
    }
    #endregion

    #region Spacecraft
    public async Task<List<Spacecraft>> GetSpacecraftAsync(int limit = 10, int offset = 0)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/spacecraft/?limit={limit}&offset={offset}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<SpacecraftResponse>(content, _jsonOptions);

            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching spacecraft: {ex.Message}");
            return [];
        }
    }
    #endregion

    #region Starship Dashboard
    public async Task<StarshipDashboard?> GetStarshipDashboardAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/dashboard/starship/");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<StarshipDashboard>(content, _jsonOptions);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching Starship dashboard: {ex.Message}");
            return null;
        }
    }
    #endregion

    #region Launcher Configurations
    public async Task<List<LauncherConfiguration>> GetLauncherConfigurationsAsync(int limit = 10, int offset = 0)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/launcher_configurations/?limit={limit}&offset={offset}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LauncherConfigurationResponse>(content, _jsonOptions);

            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching launcher configurations: {ex.Message}");
            return [];
        }
    }
    #endregion

    #region Locations & Pads
    public async Task<List<Location>> GetLocationsAsync(int limit = 10, int offset = 0)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/locations/?limit={limit}&offset={offset}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<LocationResponse>(content, _jsonOptions);

            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching locations: {ex.Message}");
            return [];
        }
    }

    public async Task<List<Pad>> GetPadsAsync(int limit = 10, int offset = 0)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/pads/?limit={limit}&offset={offset}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<PadResponse>(content, _jsonOptions);

            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SpaceDevsService] Error fetching pads: {ex.Message}");
            return [];
        }
    }
    #endregion
}