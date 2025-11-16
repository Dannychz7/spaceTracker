// AstronautSeeder
// ----------------
// Periodically populates and refreshes astronaut data in the local database using the
// SpaceDevs API, enforcing a 7-day TTL to minimize unnecessary network calls.
// Supports incremental updates by detecting existing records, refreshing timestamps,
// and inserting new astronaut entries in batches until all pages are retrieved.
// Author: Daniel Chavez 
// Last Updated: November 2025

using Microsoft.EntityFrameworkCore;
using spaceTracker.Data.Entities;
using spaceTracker.Models;
using spaceTracker.Services;

namespace spaceTracker.Data.Seeders;

public class AstronautSeeder
{
    private readonly SpaceTrackerDbContext _dbContext;
    private readonly SpaceDevsService _spaceDevsService;

    public AstronautSeeder(SpaceTrackerDbContext dbContext, SpaceDevsService spaceDevsService)
    {
        _dbContext = dbContext;
        _spaceDevsService = spaceDevsService;
    }

    public async Task PopulateAsync()
    {
        var lastUpdated = await _dbContext.Astronauts
            .OrderByDescending(a => a.LastUpdated)
            .Select(a => a.LastUpdated)
            .FirstOrDefaultAsync();

        // Refresh every 7 days
        if (lastUpdated != default && (DateTime.UtcNow - lastUpdated).TotalDays < 7)
            return;

        int limit = 50;
        int offset = 0;
        int addedTotal = 0;
        bool hasMore = true;

        while (hasMore)
        {
            var response = await _spaceDevsService.GetAstronautSeederAsync(limit, offset);
            if (response?.Results == null || response.Results.Count == 0)
                break;

            var existingIds = await _dbContext.Astronauts.Select(a => a.Id).ToHashSetAsync();

            foreach (var astro in response.Results)
            {
                if (existingIds.Contains(astro.Id))
                {
                    // Update LastUpdated so TTL works
                    var existing = await _dbContext.Astronauts.FirstAsync(a => a.Id == astro.Id);
                    existing.LastUpdated = DateTime.UtcNow;
                    continue;
                }

                var entity = new AstronautEntity
                {
                    Id = astro.Id,
                    Name = astro.Name,
                    Status = astro.Status,
                    Type = astro.Type,
                    Nationality = astro.Nationality,
                    Agency = astro.Agency,
                    Image = astro.Image,
                    DateOfBirth = astro.DateOfBirth,
                    DateOfDeath = astro.DateOfDeath,
                    ProfileImageThumbnail = astro.ProfileImageThumbnail,
                    ProfileImage = astro.ProfileImage,
                    Bio = astro.Bio,
                    Twitter = astro.Twitter,
                    Instagram = astro.Instagram,
                    Wiki = astro.Wiki,
                    FirstFlight = astro.FirstFlight,
                    LastFlight = astro.LastFlight,
                    FlightsCount = astro.FlightsCount,
                    LandingsCount = astro.LandingsCount,
                    SpacewalksCount = astro.SpacewalksCount,
                    TimeInSpace = astro.TimeInSpace,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow
                };

                await _dbContext.Astronauts.AddAsync(entity);
                addedTotal++;
            }

            await _dbContext.SaveChangesAsync();
            offset += limit;
            hasMore = response.Results.Count == limit;
        }

        Console.WriteLine($"AstronautSeeder: Added {addedTotal} new astronauts.");
    }
}
