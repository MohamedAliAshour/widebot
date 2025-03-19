using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using widebot.Services;

namespace widebot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            var weatherData = await _weatherService.GetWeatherDataAsync(city);
            return Ok(weatherData);
        }
    }
}
