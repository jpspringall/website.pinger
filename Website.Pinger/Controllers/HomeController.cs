using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Website.Pinger.Helpers;

namespace ConsumerTest.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Get()
        {
            if (System.IO.File.Exists(EnvironmentReader.StorageFile))
            {
                var results = await System.IO.File.ReadAllTextAsync(EnvironmentReader.StorageFile);

                return new JsonResult(
                    new
                    {
                        currentTime = DateTime.Now,
                        intervalInMinutes = EnvironmentReader.PingerIntervalInMinutes,
                        urls = EnvironmentReader.PingerUrls,
                        results = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PingResult>>(results)
                    }, new JsonSerializerOptions { WriteIndented = true });
            }
            else
            {
                return NotFound();
            }
        }
    }
}