using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
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

            // Validate and set defaults if missing
            if (string.IsNullOrWhiteSpace(_options.ApiKey))
            {
                throw new ArgumentNullException(nameof(_options.ApiKey), "API Key is missing in configuration.");
            }

            if (_options.CacheExpirationInMinutes <= 0)
            {
                _options.CacheExpirationInMinutes = 720; // Default to 12 hours
            }
        }

        public async Task<WeatherResponse> GetWeatherDataAsync(string city)
        {
            string cacheKey = $"weather-{city.ToLower()}";
            var cachedData = await _cache.GetStringAsync(cacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return ParseWeatherResponse(cachedData);
            }

            string apiUrl = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{city}?key={_options.ApiKey}&unitGroup=metric&include=current";
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
            var json = JObject.Parse(jsonData);
            return new WeatherResponse
            {
                City = json["address"]?.ToString(),
                Country = json["resolvedAddress"]?.ToString(),
                CurrentConditions = json["currentConditions"]?["conditions"]?.ToString(),
                Temperature = json["currentConditions"]?["temp"]?.ToString(),
                WindSpeed = json["currentConditions"]?["windspeed"]?.ToString(),
                WindDirection = json["currentConditions"]?["winddir"]?.ToString(),
                Sunrise = json["currentConditions"]?["sunrise"]?.ToString(),
                Sunset = json["currentConditions"]?["sunset"]?.ToString(),
                MoonPhase = json["currentConditions"]?["moonphase"]?.ToString(),
            };
        }
    }
}
