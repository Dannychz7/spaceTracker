using System.Text.Json.Serialization;

namespace spaceTracker.Models
{
    public class SpaceNewsResponse
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("results")]
        public List<SpaceNewsArticle>? Results { get; set; }
    }

    public class SpaceNewsArticle
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("news_site")]
        public string? NewsSite { get; set; }

        [JsonPropertyName("summary")]
        public string? Summary { get; set; }

        [JsonPropertyName("published_at")]
        public DateTime? PublishedAt { get; set; }
    }

    public class SpaceNewsInfo
    {
        [JsonPropertyName("news_sites")]
        public List<string>? NewsSites { get; set; }
    }
}