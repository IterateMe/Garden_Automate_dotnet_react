using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;



namespace GA_FA.Controllers
{
    [ApiController]
    [Route("/WeatherForecast")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }

    [ApiController]
    [Route("/valve")]
    public class ValveController : ControllerBase
    {
        private readonly IConfiguration _config;
        private string connectionString;
        private string targetDevice;
        private AzureIotComms iotHub;
        public ValveController(IConfiguration config)
        {
            _config = config;
            this.connectionString = _config["cs"];
            this.targetDevice = _config["dev"];
            iotHub = new AzureIotComms(this.connectionString, this.targetDevice);
        }
        [HttpPost]
        public String post(Models.ValveModel data)
        {
            
            iotHub.SendCloudToDeviceMessageAsync(data.command);

            return "Turn On";
        }
    }
}
