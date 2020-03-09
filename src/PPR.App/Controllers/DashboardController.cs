using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PPR.Business.Interfaces;

namespace PPR.App.Controllers {
    [Route ("api/[controller]")]
    public class DashboardController : Controller {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController (IDashboardRepository dashboardRepository) {
            _dashboardRepository = dashboardRepository;
        }

        [HttpGet ("[action]")]
        public IActionResult Books () {
            //System.Threading.Thread.Sleep(500); // Fake latency

            var allForecasts = _dashboardRepository.Books ();
            var result = new {
                Total = allForecasts.Count,
                Forecasts = allForecasts
            };

            return Ok (result);
        }
    }
}
