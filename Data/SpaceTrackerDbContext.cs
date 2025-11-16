// SpaceTrackerDbContext
// ----------------------
// Represents the Entity Framework Core database context for the SpaceTracker application,
// managing tables for space programs, spacecraft, and astronauts. Configures table names,
// column mappings, and JSON storage for efficient retrieval and persistence of complex data.
// Author: Daniel Chavez 
// Last Updated: November 2025

using Microsoft.EntityFrameworkCore;
using spaceTracker.Data.Entities;

namespace spaceTracker.Data;

public class SpaceTrackerDbContext : DbContext
{
    public SpaceTrackerDbContext(DbContextOptions<SpaceTrackerDbContext> options) : base(options) { }

    public DbSet<SpaceProgramEntity> SpacePrograms { get; set; }
    public DbSet<SpacecraftEntity> Spacecraft { get; set; }
    public DbSet<AstronautEntity> Astronauts { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SpaceProgramEntity>(entity =>
        {
            entity.ToTable("space_programs");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.ImageJson).HasColumnName("image_json").HasColumnType("TEXT");
            entity.Property(e => e.AgenciesJson).HasColumnName("agencies_json").HasColumnType("TEXT");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.LastUpdated).HasColumnName("last_updated");
        });

        modelBuilder.Entity<SpacecraftEntity>(entity =>
        {
            entity.ToTable("spacecraft");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.SerialNumber).HasColumnName("serial_number");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ImageJson).HasColumnName("image_json").HasColumnType("TEXT");
            entity.Property(e => e.ConfigJson).HasColumnName("config_json").HasColumnType("TEXT");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.LastUpdated).HasColumnName("last_updated");
        });

        modelBuilder.Entity<AstronautEntity>(entity =>
        {
            entity.ToTable("astronauts");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.StatusJson).HasColumnName("status_json").HasColumnType("TEXT");
            entity.Property(e => e.TypeJson).HasColumnName("type_json").HasColumnType("TEXT");
            entity.Property(e => e.NationalityJson).HasColumnName("nationality_json").HasColumnType("TEXT");
            entity.Property(e => e.AgencyJson).HasColumnName("agency_json").HasColumnType("TEXT");
            entity.Property(e => e.ImageJson).HasColumnName("image_json").HasColumnType("TEXT");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DateOfDeath).HasColumnName("date_of_death");
            entity.Property(e => e.ProfileImageThumbnail).HasColumnName("profile_image_thumbnail");
            entity.Property(e => e.ProfileImage).HasColumnName("profile_image");
            entity.Property(e => e.Bio).HasColumnName("bio").HasColumnType("TEXT");
            entity.Property(e => e.Twitter).HasColumnName("twitter");
            entity.Property(e => e.Instagram).HasColumnName("instagram");
            entity.Property(e => e.Wiki).HasColumnName("wiki");
            entity.Property(e => e.FirstFlight).HasColumnName("first_flight");
            entity.Property(e => e.LastFlight).HasColumnName("last_flight");
            entity.Property(e => e.FlightsCount).HasColumnName("flights_count");
            entity.Property(e => e.LandingsCount).HasColumnName("landings_count");
            entity.Property(e => e.SpacewalksCount).HasColumnName("spacewalks_count");
            entity.Property(e => e.TimeInSpace).HasColumnName("time_in_space");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.LastUpdated).HasColumnName("last_updated");
        });
    }
}
