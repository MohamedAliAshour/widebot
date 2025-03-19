using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace widebot.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IDistributedCache _cache;
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, IDistributedCache cache, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _cache = cache;
            _configuration = configuration;
            _apiKey = _configuration["WeatherAPI:ApiKey"]; // Read API key from configuration
        }

        public async Task<string> GetWeatherDataAsync(string city)
        {
            string cacheKey = $"weather-{city.ToLower()}";
            var cachedData = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return cachedData; // ✅ Return cached data if available
            }

            // ✅ Use Visual Crossing API
            string apiUrl = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{city}?key={_apiKey}&unitGroup=metric&include=current";

            var response = await _httpClient.GetStringAsync(apiUrl);

            if (!string.IsNullOrEmpty(response))
            {
                var cacheExpiration = TimeSpan.FromMinutes(_configuration.GetValue<int>("Redis:CacheExpirationInMinutes"));
                await _cache.SetStringAsync(cacheKey, response, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = cacheExpiration
                });
            }

            return response;
        }
    }
}
