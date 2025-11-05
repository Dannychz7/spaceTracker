using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using spaceTracker.Models;

namespace spaceTracker.Data.Entities;

public class AstronautEntity
{
    [Key]
    public int Id { get; set; }

    public string? Name { get; set; }

    [Column(TypeName = "TEXT")]
    public string? StatusJson { get; set; }

    [Column(TypeName = "TEXT")]
    public string? TypeJson { get; set; }

    [Column(TypeName = "TEXT")]
    public string? NationalityJson { get; set; }

    [Column(TypeName = "TEXT")]
    public string? AgencyJson { get; set; }

    [Column(TypeName = "TEXT")]
    public string? ImageJson { get; set; }

    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateOfDeath { get; set; }

    public string? ProfileImageThumbnail { get; set; }
    public string? ProfileImage { get; set; }

    [Column(TypeName = "TEXT")]
    public string? Bio { get; set; }

    public string? Twitter { get; set; }
    public string? Instagram { get; set; }
    public string? Wiki { get; set; }

    public DateTime? FirstFlight { get; set; }
    public DateTime? LastFlight { get; set; }

    public int FlightsCount { get; set; }
    public int LandingsCount { get; set; }
    public int SpacewalksCount { get; set; }

    public string? TimeInSpace { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdated { get; set; }

    // --- NotMapped JSON Deserialization ---

    [NotMapped]
    public SimpleTypeObject? Status
    {
        get => string.IsNullOrEmpty(StatusJson) ? null : JsonSerializer.Deserialize<SimpleTypeObject>(StatusJson);
        set => StatusJson = value == null ? null : JsonSerializer.Serialize(value);
    }

    [NotMapped]
    public SimpleTypeObject? Type
    {
        get => string.IsNullOrEmpty(TypeJson) ? null : JsonSerializer.Deserialize<SimpleTypeObject>(TypeJson);
        set => TypeJson = value == null ? null : JsonSerializer.Serialize(value);
    }

    [NotMapped]
    public List<NationalityInfo>? Nationality
    {
        get => string.IsNullOrEmpty(NationalityJson) ? null : JsonSerializer.Deserialize<List<NationalityInfo>>(NationalityJson);
        set => NationalityJson = value == null ? null : JsonSerializer.Serialize(value);
    }

    [NotMapped]
    public Agency? Agency
    {
        get => string.IsNullOrEmpty(AgencyJson) ? null : JsonSerializer.Deserialize<Agency>(AgencyJson);
        set => AgencyJson = value == null ? null : JsonSerializer.Serialize(value);
    }

    [NotMapped]
    public ProgramImage? Image
    {
        get => string.IsNullOrEmpty(ImageJson) ? null : JsonSerializer.Deserialize<ProgramImage>(ImageJson);
        set => ImageJson = value == null ? null : JsonSerializer.Serialize(value);
    }
}
