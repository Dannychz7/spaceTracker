using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace spaceTracker.Models
{
    public class SatellitePosition
    {
        public Info? Info { get; set; }
        public List<Position>? Positions { get; set; }
    }

    public class Info
    {
        public int Satid { get; set; }
        public string? Satname { get; set; }
        public int Transactionscount { get; set; }
    }

    public class Position
    {
        public double Satlatitude { get; set; }
        public double Satlongitude { get; set; }
        public double Sataltitude { get; set; }
        public double Azimuth { get; set; }
        public double Elevation { get; set; }
        public double Ra { get; set; }
        public double Dec { get; set; }
        public long Timestamp { get; set; }
    }

    public class VisualPassesResponse
    {
        public Info? Info { get; set; }
        public List<VisualPass>? Passes { get; set; }
    }

    public class VisualPass
    {
        public long StartUTC { get; set; }
        public long MaxUTC { get; set; }
        public long EndUTC { get; set; }
        public double StartAz { get; set; }
        public string? StartAzCompass { get; set; }
        public double MaxAz { get; set; }
        public string? MaxAzCompass { get; set; }
        public double EndAz { get; set; }
        public string? EndAzCompass { get; set; }
        public double MaxEl { get; set; }
        public double Mag { get; set; }
        public int Duration { get; set; }
    }

    public class RadioPassesResponse
    {
        public Info? Info { get; set; }
        public List<RadioPass>? Passes { get; set; }
    }

    public class RadioPass
    {
        public long StartUTC { get; set; }
        public long MaxUTC { get; set; }
        public long EndUTC { get; set; }
        public double StartAz { get; set; }
        public string? StartAzCompass { get; set; }
        public double MaxAz { get; set; }
        public string? MaxAzCompass { get; set; }
        public double EndAz { get; set; }
        public string? EndAzCompass { get; set; }
        public double MaxEl { get; set; }
    }

    public class SatellitesAboveResponse
    {
        public Info? Info { get; set; }

        [JsonPropertyName("above")]
        public List<SatelliteAbove>? Above { get; set; }
    }

    public class SatelliteAbove
    {
        public int Satid { get; set; }
        public string? Satname { get; set; }

        [JsonPropertyName("intDesignator")]
        public string? Intdesignator { get; set; }

        [JsonPropertyName("launchDate")]
        public string? Launchdate { get; set; }

        public double Satlat { get; set; }
        public double Satlng { get; set; }
        public double Satalt { get; set; }
    }

    public class TleResponse
    {
        public Info? Info { get; set; }
        public string? Tle { get; set; }
    }
}
