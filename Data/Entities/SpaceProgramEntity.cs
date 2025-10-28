using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using spaceTracker.Models;

namespace spaceTracker.Data.Entities;

public class SpaceProgramEntity
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    [Column(TypeName = "TEXT")]
    public string? ImageJson { get; set; }

    [Column(TypeName = "TEXT")]
    public string? AgenciesJson { get; set; }

    public DateTime LastUpdated { get; set; }
    public DateTime CreatedAt { get; set; }

    [NotMapped]
    public ProgramImage? Image
    {
        get => string.IsNullOrEmpty(ImageJson) ? null : JsonSerializer.Deserialize<ProgramImage>(ImageJson);
        set => ImageJson = value == null ? null : JsonSerializer.Serialize(value);
    }

    [NotMapped]
    public List<Agency>? Agencies
    {
        get => string.IsNullOrEmpty(AgenciesJson) ? null : JsonSerializer.Deserialize<List<Agency>>(AgenciesJson);
        set => AgenciesJson = value == null ? null : JsonSerializer.Serialize(value);
    }
}
