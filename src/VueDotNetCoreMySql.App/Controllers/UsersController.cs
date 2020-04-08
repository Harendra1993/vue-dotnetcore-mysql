using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VueDotNetCoreMySql.App.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[api/controller]")]
    public class UsersController : ControllerBase
    {

    }
}
