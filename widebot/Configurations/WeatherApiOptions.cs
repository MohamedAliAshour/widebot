namespace widebot.Configurations
{
    public class WeatherApiOptions
    {
        public string ApiKey { get; set; } = string.Empty;
        public int CacheExpirationInMinutes { get; set; } = 720;
    }
}
