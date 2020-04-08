using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PPR.App.Controllers {

    [Authorize]
    [ApiController]
    [Route ("[api/controller]")]
    public class UsersController : ControllerBase {

    }
}
