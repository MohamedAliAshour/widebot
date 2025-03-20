using widebot.Models;


namespace widebot.interfaces
{
    public interface IWeather
    {
        Task<WeatherResponse> GetWeatherDataAsync(string city);
    }
}
