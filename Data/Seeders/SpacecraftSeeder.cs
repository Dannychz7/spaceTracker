// SpacecraftSeeder
// -----------------
// Populates and refreshes spacecraft data in the local database using the SpaceDevs API,
// enforcing a 7-day update interval to avoid unnecessary API calls. It adds new spacecraft
// in batches and skips existing records to maintain database consistency.
// Author: Daniel Chavez 
// Last Updated: November 2025

using Microsoft.EntityFrameworkCore;
using spaceTracker.Data.Entities;
using spaceTracker.Models;
using spaceTracker.Services;

namespace spaceTracker.Data.Seeders;

public class SpacecraftSeeder
{
    private readonly SpaceTrackerDbContext _dbContext;
    private readonly SpaceDevsService _spaceDevsService;

    public SpacecraftSeeder(SpaceTrackerDbContext dbContext, SpaceDevsService spaceDevsService)
    {
        _dbContext = dbContext;
        _spaceDevsService = spaceDevsService;
    }
    public async Task PopulateAsync()
    {
        var lastUpdated = await _dbContext.Spacecraft
            .OrderByDescending(s => s.LastUpdated)
            .Select(s => s.LastUpdated)
            .FirstOrDefaultAsync();

        if (lastUpdated != default && (DateTime.UtcNow - lastUpdated).TotalDays < 7)
            return;

        int limit = 10;
        int offset = 0;
        int addedTotal = 0;
        bool hasMore = true;

        while (hasMore)
        {
            var response = await _spaceDevsService.GetSpacecraftSeederAsync(limit, offset);
            if (response?.Results == null || response.Results.Count == 0)
                break;

            var existingIds = await _dbContext.Spacecraft
                .Select(s => s.Id)
                .ToHashSetAsync();

            foreach (var sc in response.Results)
            {
                if (existingIds.Contains(sc.Id))
                    continue;

                var entity = new SpacecraftEntity
                {
                    Id = sc.Id,
                    Name = sc.Name,
                    SerialNumber = sc.SerialNumber,
                    Description = sc.Description,
                    Image = sc.Image,
                    SpacecraftConfig = sc.SpacecraftConfig,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow
                };

                await _dbContext.Spacecraft.AddAsync(entity);
                addedTotal++;
            }

            await _dbContext.SaveChangesAsync();

            // Increment offset
            offset += limit;

            // Stop if fewer results than limit (last page)
            hasMore = response.Results.Count == limit;
        }

        Console.WriteLine($"SpacecraftSeeder: Added {addedTotal} new spacecraft.");
    }

}
