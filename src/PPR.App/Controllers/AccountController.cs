using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPR.App.DTOs;
using PPR.Business.Interfaces;

namespace PPR.App.Controllers {

    [Authorize]
    [Route ("api/[controller]")]
    public class AccountController : ControllerBase {
        private readonly IAccountRepository _accountRepository;

        public AccountController (IAccountRepository accountRepository) {
            _accountRepository = accountRepository;
        }

        [AllowAnonymous]
        [HttpPost ("authenticate")]
        public IActionResult Authenticate ([FromBody] UserDTO userParam) {
            // var user = _accountRepository.Authenticate (userParam.Username, userParam.Password);

            var user = _accountRepository.GetUser (userParam.UserName, userParam.Password);

            if (user == null)
                return BadRequest (new { message = "Username or password is incorrect" });

            return Ok (user);
        }

        // [Authorize (Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll () {
            // var users = _accountRepository.GetAll ();
            return Ok (new { });
        }

        [HttpGet ("{id}")]
        public IActionResult GetById (int id) {
            var user = "hi";
            //var user = _accountRepository.GetById (id);

            if (user == null) {
                return NotFound ();
            }

            // only allow admins to access other user records
            // var currentUserId = int.Parse (User.Identity.Name);
            // if (id != currentUserId && !User.IsInRole (Role.Admin)) {
            //     return Forbid ();
            // }

            return Ok (user);
        }
    }
}
