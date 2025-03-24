using Newtonsoft.Json;

namespace widebot.Models
{
    public class WeatherResponse
    {
        [JsonProperty("address")]
        public string City { get; set; }

        [JsonProperty("resolvedAddress")]
        public string Country { get; set; }

        [JsonProperty("currentConditions")]
        public CurrentConditions CurrentConditions { get; set; }
    }

    public class CurrentConditions
    {
        [JsonProperty("conditions")]
        public string Conditions { get; set; }

        [JsonProperty("temp")]
        public string Temperature { get; set; }

        [JsonProperty("windspeed")]
        public string WindSpeed { get; set; }

        [JsonProperty("winddir")]
        public string WindDirection { get; set; }

        [JsonProperty("sunrise")]
        public string Sunrise { get; set; }

        [JsonProperty("sunset")]
        public string Sunset { get; set; }

        [JsonProperty("moonphase")]
        public string MoonPhase { get; set; }
    }
}
