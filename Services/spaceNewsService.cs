// -------------------------------------------------------------
// spaceNewsService: Implements interacting with the SNAPI 
// (Space News API) to retrieve recent articles from tracked news
// sources. This class provides helper methods for fetching
// articles and available news site information.
// Author: Anthony Petrosino 
// Last Updated: November 2025
// -------------------------------------------------------------

using spaceTracker.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace spaceTracker.Services
{
    public class SpaceNewsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SpaceNewsService> _logger;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        public SpaceNewsService(HttpClient httpClient, ILogger<SpaceNewsService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<List<SpaceNewsArticle>?> GetArticlesAsync(int limit = 10, int offset = 0)
        {
            _logger.LogInformation("[SpaceNewsService] Fetching space news articles");
            try
            {
                var url = $"https://api.spaceflightnewsapi.net/v4/articles/?limit={limit}&offset={offset}";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var newsData = JsonSerializer.Deserialize<SpaceNewsResponse>(content, _jsonOptions);
                return newsData?.Results;
            }
            catch (Exception ex)
            {
                _logger.LogError("[SpaceNewsService] Error fetching articles: {Message}", ex.Message);
                return null;
            }
        }

        public async Task<List<string>?> GetNewsSitesAsync()
        {
            _logger.LogInformation("[SpaceNewsService] Fetching news sites info");
            try
            {
                var url = "https://api.spaceflightnewsapi.net/v4/info/";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var info = JsonSerializer.Deserialize<SpaceNewsInfo>(content, _jsonOptions);
                return info?.NewsSites;
            }
            catch (Exception ex)
            {
                _logger.LogError("[SpaceNewsService] Error fetching news sites: {Message}", ex.Message);
                return null;
            }
        }
    }
}
