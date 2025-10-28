using Microsoft.EntityFrameworkCore;
using spaceTracker.Data.Entities;
using spaceTracker.Models;
using spaceTracker.Services;
namespace spaceTracker.Data.Seeders;

public class SpaceProgramSeeder
{
    private readonly SpaceTrackerDbContext _dbContext;
    private readonly SpaceDevsService _spaceDevsService;
    
    // Define the correct URL here to keep it clean
    private const string LunaImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cc/RIAN_archive_510848_Interplanetary_station_Luna_1_-_blacked.jpg/600px-RIAN_archive_510848_Interplanetary_station_Luna_1_-_blacked.jpg";

    public SpaceProgramSeeder(SpaceTrackerDbContext dbContext, SpaceDevsService spaceDevsService)
    {
        _dbContext = dbContext;
        _spaceDevsService = spaceDevsService;
    }

    public async Task PopulateAsync()
    {
        // Check TTL: 1 week
        var lastUpdated = await _dbContext.SpacePrograms
            .OrderByDescending(p => p.LastUpdated)
            .Select(p => p.LastUpdated)
            .FirstOrDefaultAsync();

        if (lastUpdated != default && (DateTime.UtcNow - lastUpdated).TotalDays < 7)
        {
            // DB is fresh, skip API
            return;
        }

        // Fetch data from SpaceDevs API
        var programsResponse = await _spaceDevsService.GetProgramsAsync();

        if (programsResponse?.Results == null || programsResponse.Results.Count == 0)
            return;

        foreach (var program in programsResponse.Results)
        {
            // Check if program exists
            var existing = await _dbContext.SpacePrograms
                .FirstOrDefaultAsync(p => p.Id == program.Id);

            if (existing != null)
            {
                // Always update these fields from the API
                existing.Name = program.Name;
                existing.Description = program.Description;
                existing.StartDate = program.StartDate;
                existing.EndDate = program.EndDate;
                existing.Agencies = program.Agencies;
                existing.LastUpdated = DateTime.UtcNow;
                
                // BUT, only update the image if it's NOT the Luna program
                if (existing.Id != 38)
                {
                    existing.Image = program.Image;
                }
                else
                {
                    // Ensure our manual fix stays
                    existing.Image = new ProgramImage { ImageUrl = LunaImageUrl };
                }
            }
            else
            {
                var entity = new SpaceProgramEntity
                {
                    Id = program.Id,
                    Name = program.Name,
                    Description = program.Description,
                    StartDate = program.StartDate,
                    EndDate = program.EndDate,
                    Agencies = program.Agencies,
                    LastUpdated = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                };

                // Check if this is the Luna program being added for the first time
                if (program.Id == 38)
                {
                    // Manually override with the correct image
                    entity.Image = new ProgramImage { ImageUrl = LunaImageUrl };
                }
                else
                {
                    // Use the image from the API
                    entity.Image = program.Image;
                }

                await _dbContext.SpacePrograms.AddAsync(entity);
            }
        }

        await _dbContext.SaveChangesAsync();
    }
}