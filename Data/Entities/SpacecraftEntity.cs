using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using spaceTracker.Models;

namespace spaceTracker.Data.Entities;

public class SpacecraftEntity
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }
    public string? SerialNumber { get; set; }
    public string? Description { get; set; }

    [Column(TypeName = "TEXT")]
    public string? ImageJson { get; set; }

    [Column(TypeName = "TEXT")]
    public string? ConfigJson { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdated { get; set; }

    [NotMapped]
    public ProgramImage? Image
    {
        get => string.IsNullOrEmpty(ImageJson) ? null : JsonSerializer.Deserialize<ProgramImage>(ImageJson);
        set => ImageJson = value == null ? null : JsonSerializer.Serialize(value);
    }

    [NotMapped]
    public SpacecraftConfiguration? SpacecraftConfig
    {
        get => string.IsNullOrEmpty(ConfigJson) ? null : JsonSerializer.Deserialize<SpacecraftConfiguration>(ConfigJson);
        set => ConfigJson = value == null ? null : JsonSerializer.Serialize(value);
    }
}
