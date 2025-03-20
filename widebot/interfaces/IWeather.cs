using widebot.DTOs;
using widebot.DTOs.WeatherResponseDtos;


namespace widebot.interfaces
{
    public interface IWeather
    {
        Task<WeatherGetResponseDto> GetWeatherDataAsync(string city);
    }
}
