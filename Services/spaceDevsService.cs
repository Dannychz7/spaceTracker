using spaceTracker.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using spaceTracker.Data;
namespace spaceTracker.Services;
using Microsoft.EntityFrameworkCore;
using spaceTracker.Data.Entities;
public class SpaceDevsService
{
private readonly HttpClient _httpClient;
private readonly ILogger<SpaceDevsService> _logger;
private readonly SpaceTrackerDbContext _context;
private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };
private const string BASE_URL = "https://lldev.thespacedevs.com/2.3.0";

public SpaceDevsService(HttpClient httpClient, ILogger<SpaceDevsService> logger, SpaceTrackerDbContext context)
{
    _httpClient = httpClient;
    _logger = logger;
    _context = context;
    _logger.LogInformation("SpaceDevsService initialized");
}

    #region Launches
    public async Task<List<SpaceDevsLaunch>> GetUpcomingLaunchesAsync(int limit = 5, int offset = 0)
    {
        var url = $"{BASE_URL}/launches/upcoming/?limit={limit}&offset={offset}";
        _logger.LogDebug("Fetching upcoming launches with URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Upcoming launches response length: {Length}", content.Length);

            var result = JsonSerializer.Deserialize<SpaceDevsResponse>(content, _jsonOptions);
            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching upcoming launches");
            return [];
        }
    }

    public async Task<List<SpaceDevsLaunch>> GetPreviousLaunchesAsync(int limit = 5, int offset = 0)
    {
        var url = $"{BASE_URL}/launches/previous/?limit={limit}&offset={offset}";
        _logger.LogDebug("Fetching previous launches with URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Previous launches response length: {Length}", content.Length);

            var result = JsonSerializer.Deserialize<SpaceDevsResponse>(content, _jsonOptions);
            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching previous launches");
            return [];
        }
    }

    public async Task<SpaceDevsLaunch?> GetLaunchByIdAsync(string id)
    {
        var url = $"{BASE_URL}/launches/{id}/";
        _logger.LogDebug("Fetching launch by ID {Id} with URL: {Url}", id, url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SpaceDevsLaunch>(content, _jsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching launch {Id}", id);
            return null;
        }
    }
    #endregion

    #region Events
    public async Task<List<SpaceEvent>> GetUpcomingEventsAsync(int limit = 10, int offset = 0)
    {
        var url = $"{BASE_URL}/events/upcoming/?limit={limit}&offset={offset}";
        _logger.LogDebug("Fetching upcoming events with URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Upcoming events response length: {Length}", content.Length);

            var result = JsonSerializer.Deserialize<SpaceEventResponse>(content, _jsonOptions);
            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching upcoming events");
            return [];
        }
    }

    public async Task<List<SpaceEvent>> GetPreviousEventsAsync(int limit = 10, int offset = 0)
    {
        var url = $"{BASE_URL}/events/previous/?limit={limit}&offset={offset}";
        _logger.LogDebug("Fetching previous events with URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Previous events response length: {Length}", content.Length);

            var result = JsonSerializer.Deserialize<SpaceEventResponse>(content, _jsonOptions);
            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching previous events");
            return [];
        }
    }
    #endregion

    #region Astronauts
    public async Task<List<Astronaut>> GetAstronautsAsync(int limit = 10, int offset = 0, string? search = null)
    {
        var url = $"{BASE_URL}/astronauts/?limit={limit}&offset={offset}";
        if (!string.IsNullOrEmpty(search))
            url += $"&search={Uri.EscapeDataString(search)}";
        _logger.LogDebug("Fetching astronauts with URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Astronauts response length: {Length}", content.Length);

            var result = JsonSerializer.Deserialize<AstronautResponse>(content, _jsonOptions);
            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching astronauts");
            return [];
        }
    }

    public async Task<Astronaut?> GetAstronautByIdAsync(int id)
    {
        var url = $"{BASE_URL}/astronauts/{id}/";
        _logger.LogDebug("Fetching astronaut by ID {Id} with URL: {Url}", id, url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Astronaut>(content, _jsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching astronaut {Id}", id);
            return null;
        }
    }
    #endregion

    #region Spacewalks
    public async Task<List<Spacewalk>> GetSpacewalksAsync(int limit = 10, int offset = 0)
    {
        var url = $"{BASE_URL}/spacewalks/?limit={limit}&offset={offset}";
        _logger.LogDebug("Fetching spacewalks with URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Spacewalks response length: {Length}", content.Length);

            var result = JsonSerializer.Deserialize<SpacewalkResponse>(content, _jsonOptions);
            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching spacewalks");
            return [];
        }
    }
    #endregion

    #region Space Stations
    public async Task<List<SpaceStation>> GetSpaceStationsAsync(int limit = 10, int offset = 0)
    {
        var url = $"{BASE_URL}/space_stations/?limit={limit}&offset={offset}";
        _logger.LogDebug("Fetching space stations with URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Space stations response length: {Length}", content.Length);

            var result = JsonSerializer.Deserialize<SpaceStationResponse>(content, _jsonOptions);
            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching space stations");
            return [];
        }
    }

    public async Task<SpaceStation?> GetSpaceStationByIdAsync(int id)
    {
        var url = $"{BASE_URL}/space_stations/{id}/";
        _logger.LogDebug("Fetching space station by ID {Id} with URL: {Url}", id, url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SpaceStation>(content, _jsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching space station {Id}", id);
            return null;
        }
    }
    #endregion

    #region Docking Events
    public async Task<List<DockingEvent>> GetDockingEventsAsync(int limit = 10, int offset = 0)
    {
        var url = $"{BASE_URL}/docking_events/?limit={limit}&offset={offset}";
        _logger.LogDebug("Fetching docking events with URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Docking events response length: {Length}", content.Length);

            var result = JsonSerializer.Deserialize<DockingEventResponse>(content, _jsonOptions);
            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching docking events");
            return [];
        }
    }
    #endregion

    #region Expeditions
    public async Task<List<Expedition>> GetExpeditionsAsync(int limit = 10, int offset = 0)
    {
        var url = $"{BASE_URL}/expeditions/?limit={limit}&offset={offset}";
        _logger.LogDebug("Fetching expeditions with URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Expeditions response length: {Length}", content.Length);

            var result = JsonSerializer.Deserialize<ExpeditionResponse>(content, _jsonOptions);
            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching expeditions");
            return [];
        }
    }
    #endregion

        #region Agencies
    public async Task<List<Agency>> GetAgenciesAsync(int limit = 10, int offset = 0, string? search = null)
    {
        var url = $"{BASE_URL}/agencies/?limit={limit}&offset={offset}";
        if (!string.IsNullOrEmpty(search))
            url += $"&search={Uri.EscapeDataString(search)}";
        _logger.LogDebug("Fetching agencies with URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Agencies response length: {Length}", content.Length);

            var result = JsonSerializer.Deserialize<AgencyResponse>(content, _jsonOptions);
            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching agencies");
            return [];
        }
    }

    public async Task<Agency?> GetAgencyByIdAsync(int id)
    {
        var url = $"{BASE_URL}/agencies/{id}/";
        _logger.LogDebug("Fetching agency by ID {Id} with URL: {Url}", id, url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Agency>(content, _jsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching agency {Id}", id);
            return null;
        }
    }
    #endregion

    #region Programs
    public async Task<List<spaceTracker.Models.SpaceProgram>> SpaceProg(int limit = 10, int offset = 0)
{
    try
    {
        // Check last update from existing programs
        var lastUpdated = await _context.SpacePrograms
            .OrderByDescending(p => p.LastUpdated)
            .Select(p => p.LastUpdated)
            .FirstOrDefaultAsync();
        
        var now = DateTime.UtcNow;
        bool shouldFetchApi = lastUpdated == default || (now - lastUpdated).TotalDays >= 7; // 1 week

        if (shouldFetchApi)
        {
            // Call the remote API
            var url = $"{BASE_URL}/programs/?limit={limit}&offset={offset}";
            _logger.LogDebug("Fetching programs with URL: {Url}", url);
            
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Programs response length: {Length}", content.Length);

            var result = JsonSerializer.Deserialize<ProgramResponse>(content, _jsonOptions);
            
            if (result?.Results != null)
            {
                // Get existing program IDs to skip duplicates
                var existingIds = await _context.SpacePrograms
                    .Select(sp => sp.Id)
                    .ToHashSetAsync();

                // Only add new programs (skip existing ones)
                foreach (var p in result.Results)
                {
                    if (!existingIds.Contains(p.Id))
                    {
                        var entity = new SpaceProgramEntity
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Description = p.Description,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate,
                            Image = p.Image,
                            Agencies = p.Agencies,
                            CreatedAt = now,
                            LastUpdated = now
                        };
                        
                        _context.SpacePrograms.Add(entity);
                    }
                }

                await _context.SaveChangesAsync();
            }
        }

        // Always return from database
        var programs = await _context.SpacePrograms
            .OrderBy(sp => sp.Id)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        // Map entities to models
        return programs.Select(e => new spaceTracker.Models.SpaceProgram
        {
            Id = e.Id,
            Name = e.Name,
            Description = e.Description,
            StartDate = e.StartDate,
            EndDate = e.EndDate,
            Image = e.Image,
            Agencies = e.Agencies
        }).ToList();
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error fetching programs");
        return [];
    }
}

    public async Task<ProgramResponse?> GetProgramsAsync()
    {
        var response = await _httpClient.GetAsync("https://lldev.thespacedevs.com/2.3.0/programs/?limit=100");
        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<ProgramResponse>(json);
    }
    #endregion

    #region Spacecraft
    public async Task<List<Spacecraft>> GetSpacecraftAsync(int limit = 10, int offset = 0)
    {
        
        var dbData = await _context.Spacecraft
            .OrderBy(s => s.Id)
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

        if (dbData.Any())
        {
            _logger.LogDebug("Returning {Count} spacecraft from DB", dbData.Count);
            return dbData.Select(s => new Spacecraft
            {
                Id = s.Id,
                Name = s.Name,
                SerialNumber = s.SerialNumber,
                Description = s.Description,
                Image = s.Image,
                SpacecraftConfig = s.SpacecraftConfig
            }).ToList();
        }

        var url = $"{BASE_URL}/spacecraft/?limit={limit}&offset={offset}&mode=detailed";
        _logger.LogDebug("Fetching spacecraft from API: {Url}", url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Spacecraft response length: {Length}", content.Length);

            var result = JsonSerializer.Deserialize<SpacecraftResponse>(content, _jsonOptions);
            var spacecraftList = result?.Results ?? new List<Spacecraft>();

            if (spacecraftList.Any())
            {
                var entities = spacecraftList.Select(sc => new SpacecraftEntity
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    SerialNumber = sc.SerialNumber,
                    Description = sc.Description,
                    Image = sc.Image,
                    SpacecraftConfig = sc.SpacecraftConfig,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow
                }).ToList();

                await _context.Spacecraft.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                _logger.LogDebug("Saved {Count} spacecraft to DB", entities.Count);
            }

            return spacecraftList;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching spacecraft from API");
            return new List<Spacecraft>();
        }
    }

    public async Task<SpacecraftResponse?> GetSpacecraftSeederAsync(int limit = 610, int offset = 0)
    {
        var url = $"https://lldev.thespacedevs.com/2.3.0/spacecraft/?limit={limit}&offset={offset}&mode=detailed";
        _logger.LogDebug("Fetching spacecraft with URL: {Url}", url);

        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<SpacecraftResponse>(json, _jsonOptions);
    }
    #endregion

    #region Starship Dashboard
    public async Task<StarshipDashboard?> GetStarshipDashboardAsync()
    {
        var url = $"{BASE_URL}/dashboard/starship/";
        _logger.LogDebug("Fetching Starship dashboard with URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Starship dashboard response length: {Length}", content.Length);

            return JsonSerializer.Deserialize<StarshipDashboard>(content, _jsonOptions);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching Starship dashboard");
            return null;
        }
    }
    #endregion

    #region Launcher Configurations
    public async Task<List<LauncherConfiguration>> GetLauncherConfigurationsAsync(int limit = 10, int offset = 0)
    {
        var url = $"{BASE_URL}/launcher_configurations/?limit={limit}&offset={offset}";
        _logger.LogDebug("Fetching launcher configurations with URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Launcher configurations response length: {Length}", content.Length);

            var result = JsonSerializer.Deserialize<LauncherConfigurationResponse>(content, _jsonOptions);
            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching launcher configurations");
            return [];
        }
    }
    #endregion

    #region Locations & Pads
    public async Task<List<Location>> GetLocationsAsync(int limit = 10, int offset = 0)
    {
        var url = $"{BASE_URL}/locations/?limit={limit}&offset={offset}";
        _logger.LogDebug("Fetching locations with URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Locations response length: {Length}", content.Length);

            var result = JsonSerializer.Deserialize<LocationResponse>(content, _jsonOptions);
            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching locations");
            return [];
        }
    }

    public async Task<List<Pad>> GetPadsAsync(int limit = 10, int offset = 0)
    {
        var url = $"{BASE_URL}/pads/?limit={limit}&offset={offset}";
        _logger.LogDebug("Fetching pads with URL: {Url}", url);

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("Pads response length: {Length}", content.Length);

            var result = JsonSerializer.Deserialize<PadResponse>(content, _jsonOptions);
            return result?.Results ?? [];
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching pads");
            return [];
        }
    }
    #endregion
}

