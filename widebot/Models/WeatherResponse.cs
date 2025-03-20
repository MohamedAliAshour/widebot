namespace widebot.Models
{
    public class WeatherResponse
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string CurrentConditions { get; set; }
        public string Temperature { get; set; }
        public string WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
        public string MoonPhase { get; set; }
    }
}
