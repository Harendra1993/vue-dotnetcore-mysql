using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VueDotNetCoreMySql.Business.Interfaces;

namespace VueDotNetCoreMySql.App.Controllers
{
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

     
    }
}
