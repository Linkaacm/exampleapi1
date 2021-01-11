using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CCB.Log.Standard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;
		private readonly ILogging _logging;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, ILogging logging)
		{
			_logger = logger;
			_logging = logging;

		}

		[HttpGet]
		public IEnumerable<WeatherForecast> Get()
		{
			_logging.LogInfo($"[{typeof(WeatherForecastController).FullName} - {MethodBase.GetCurrentMethod().Name}] - (Se realiza loggeo " +
				$"satisfactoriamente)");
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
}
