namespace widebot.Configurations
{
    public class WeatherApiOptions
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string UnitGroup { get; set; }
        public string Include { get; set; }
        public int CacheExpirationInMinutes { get; set; }
    }
}
