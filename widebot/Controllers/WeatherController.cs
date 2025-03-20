using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using widebot.interfaces;
using widebot.Services;

namespace widebot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeather _weatherService;

        public WeatherController(IWeather weatherService)
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
