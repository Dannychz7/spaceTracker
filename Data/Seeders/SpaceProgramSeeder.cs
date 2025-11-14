using Microsoft.EntityFrameworkCore;
using spaceTracker.Data.Entities;
using spaceTracker.Models;
using spaceTracker.Services;

namespace spaceTracker.Data.Seeders;

public class SpaceProgramSeeder
{
    private readonly SpaceTrackerDbContext _dbContext;
    private readonly SpaceDevsService _spaceDevsService;

    // Protected manual URLs
    private const string LunaImageUrl =
        "https://upload.wikimedia.org/wikipedia/commons/thumb/c/cc/RIAN_archive_510848_Interplanetary_station_Luna_1_-_blacked.jpg/600px-RIAN_archive_510848_Interplanetary_station_Luna_1_-_blacked.jpg";

    private const string PolarisImageUrl =
        "https://polarisprogram.com/wp-content/uploads/2022/01/Starship-1.png";

    public SpaceProgramSeeder(SpaceTrackerDbContext dbContext, SpaceDevsService spaceDevsService)
    {
        _dbContext = dbContext;
        _spaceDevsService = spaceDevsService;
    }

    public async Task PopulateAsync()
    {
        // TTL: 1 week
        var lastUpdated = await _dbContext.SpacePrograms
            .OrderByDescending(p => p.LastUpdated)
            .Select(p => p.LastUpdated)
            .FirstOrDefaultAsync();

        if (lastUpdated != default && (DateTime.UtcNow - lastUpdated).TotalDays < 7)
        {
            return; 
        }

        var programsResponse = await _spaceDevsService.GetProgramsAsync();
        if (programsResponse?.Results == null || programsResponse.Results.Count == 0)
            return;

        foreach (var program in programsResponse.Results)
        {
            var existing = await _dbContext.SpacePrograms
                .FirstOrDefaultAsync(p => p.Id == program.Id);

            if (existing != null)
            {
                existing.Name = program.Name;
                existing.Description = program.Description;
                existing.StartDate = program.StartDate;
                existing.EndDate = program.EndDate;
                existing.Agencies = program.Agencies;
                existing.LastUpdated = DateTime.UtcNow;

                // Image override rules
                if (existing.Id == 38) // Luna
                {
                    existing.Image = new ProgramImage { ImageUrl = LunaImageUrl };
                }
                else if (existing.Id == 40) // Polaris
                {
                    existing.Image = new ProgramImage { ImageUrl = PolarisImageUrl };
                }
                else
                {
                    existing.Image = program.Image;
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

                // First-time insert override rules
                if (program.Id == 38) // Luna
                {
                    entity.Image = new ProgramImage { ImageUrl = LunaImageUrl };
                }
                else if (program.Id == 40) // Polaris
                {
                    entity.Image = new ProgramImage { ImageUrl = PolarisImageUrl };
                }
                else
                {
                    entity.Image = program.Image;
                }

                await _dbContext.SpacePrograms.AddAsync(entity);
            }
        }

        await _dbContext.SaveChangesAsync();
    }
}