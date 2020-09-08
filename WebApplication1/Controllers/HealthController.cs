using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            var random = new Random();
            var value = random.Next(0, 3);
            if (value > 0)
            {
                return StatusCode(500, $"Internal Server Error ({value})");
            }

            return Ok($"Ok request! ({value})");

        }
    }
}
