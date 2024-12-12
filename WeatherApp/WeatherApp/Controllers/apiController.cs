using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Controller{
    
    [ApiController]
    [Route("Weather")]
    public class apiController() : ControllerBase{

        [HttpGet]
        public IActionResult GetWeather()
        {
            return Ok("41");
        }
    }
}
