using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using widebot.Configurations;
using widebot.interfaces;
using widebot.Models;


namespace widebot.Services
{
    public class WeatherService : IWeather
    {
        private readonly HttpClient _httpClient;
        private readonly IDistributedCache _cache;
        private readonly WeatherApiOptions _options;

        public WeatherService(HttpClient httpClient, IDistributedCache cache, IOptions<WeatherApiOptions> options)
        {
            _httpClient = httpClient;
            _cache = cache;
            _options = options.Value;

        }

        public async Task<WeatherResponse> GetWeatherDataAsync(string city)
        {
            string cacheKey = $"weather-{city.ToLower()}";
            var cachedData = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return ParseWeatherResponse(cachedData);
            }

            // Construct the API URL using the values from appsettings.json
            string apiUrl = $"{_options.BaseUrl}{city}?key={_options.ApiKey}&unitGroup={_options.UnitGroup}&include={_options.Include}";

            var response = await _httpClient.GetStringAsync(apiUrl);

            if (!string.IsNullOrEmpty(response))
            {
                var cacheExpiration = TimeSpan.FromMinutes(_options.CacheExpirationInMinutes);
                await _cache.SetStringAsync(cacheKey, response, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = cacheExpiration
                });
            }

            return ParseWeatherResponse(response);
        }


        private WeatherResponse ParseWeatherResponse(string jsonData)
        {
            return JsonConvert.DeserializeObject<WeatherResponse>(jsonData);
        }
    }
}
