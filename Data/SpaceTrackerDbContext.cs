using Microsoft.EntityFrameworkCore;
using spaceTracker.Data.Entities;

namespace spaceTracker.Data;

public class SpaceTrackerDbContext : DbContext
{
    public SpaceTrackerDbContext(DbContextOptions<SpaceTrackerDbContext> options) : base(options) { }

    public DbSet<SpaceProgramEntity> SpacePrograms { get; set; }
    public DbSet<SpacecraftEntity> Spacecraft { get; set; }

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
    }
}
