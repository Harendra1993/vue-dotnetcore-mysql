using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PPR.App.DTOs;
using PPR.App.JwtHelpers;
using PPR.Business.Interfaces;
using PPR.Common.Entities;

namespace PPR.App.Controllers {

    [Authorize]
    [Route ("api/[controller]")]
    public class AccountController : ControllerBase {
        #region Properties and constructor
        public IConfiguration _configuration { get; }
        private readonly IAccountRepository _accountRepository;

        public AccountController (IAccountRepository accountRepository, IConfiguration configuration) {
            _accountRepository = accountRepository;
            _configuration = configuration;
        }

        #endregion

        [AllowAnonymous]
        [HttpPost ("authenticate")]
        public IActionResult Authenticate ([FromBody] UserDTO userParam) {
            // var user = _accountRepository.Authenticate (userParam.Username, userParam.Password);

            var user = _accountRepository.GetUser (userParam.UserName, userParam.Password);

            if (user == null)
                return BadRequest (new { message = "Username or password is incorrect" });

            return Ok (user);
        }

        [HttpPost]
        [Route ("Login")]
        public async Task<IActionResult> Login (LoginBindingModel model) {
            try {
                if (!ModelState.IsValid)
                    return BadRequest (ModelState);

                var user = _dbContext.Users.FirstOrDefault (x => x.Email == model.username && x.Password == model.password);

                if (user != null) {
                    var userDTO = Mapper.Map<User, UserDTO> (user);

                    userDTO.GenerateToken (_configuration);

                    return Ok (new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
                } else {
                    return Ok (new CustomResponse<Error> {
                        Message = Global.ResponseMessages.Forbidden,
                        StatusCode = StatusCodes.Status403Forbidden,
                        Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid ("username or password") }
                    });
                }
            } catch (Exception ex) {
                return StatusCode (Error.LogError (ex));
            }
        }

        [Route ("LoginAsAdmin")]
        [HttpGet]
        public async Task<IActionResult> LoginAsAdmin (string username, string password) {
            if (username == "Admin" && password == "Pass") {
                var token = new JwtTokenBuilder ()
                    .AddSecurityKey (JwtSecurityKey.Create (_configuration.GetValue<string> ("JwtSecretKey")))
                    .AddIssuer (_configuration.GetValue<string> ("JwtIssuer"))
                    .AddAudience (_configuration.GetValue<string> ("JwtAudience"))
                    .AddExpiry (1)
                    .AddClaim ("Name", "Admin")
                    .AddRole ("Admin")
                    .Build ();

                return Ok (new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = token.Value });
            } else
                return Ok (new CustomResponse<Error> { Message = Global.ResponseMessages.Forbidden, StatusCode = StatusCodes.Status403Forbidden, Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid ("username or password") } });

        }

        [Route ("GetUser")]
        [Authorize (Roles = "User, Admin")]
        [HttpGet]
        public async Task<IActionResult> GetUser () {
            var name = User.GetClaimValue ("Name");

            return Ok (new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "You are an authorized user" });
        }

        [Route ("GetAdmin")]
        [Authorize (Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAdmin () {
            var name = User.GetClaimValue ("Name");

            return Ok (new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "You are an authorized user" });
        }

    }
}
