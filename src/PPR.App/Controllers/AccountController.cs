using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PPR.App.DTOs.User;
using PPR.App.JwtHelpers;
using PPR.Business.Interfaces;
using PPR.Common.Entities;
using PPR.Common.Utils;

namespace PPR.App.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        #region Properties and constructor
        public IConfiguration _configuration { get; }
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;

        public AccountController(IConfiguration configuration, IMapper mapper, IAccountRepository accountRepository)
        {
            _configuration = configuration;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        #endregion

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = Task.Run(() => _accountRepository.GetUser(model.UserName, model.Password));

                await Task.WhenAny(user);

                if (user != null)
                {
                    var userDTO = _mapper.Map<User, UserDTO>(await user);
                    var userRoles = Task.Run(() => _accountRepository.GetRolesForUser(model.UserName));

                    userDTO.Roles = await userRoles;
                    userDTO.Password = null;

                    userDTO.GenerateToken(_configuration);

                    return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
                }
                else
                {
                    return Ok(new CustomResponse<Error>
                    {
                        Message = Global.ResponseMessages.Forbidden,
                        StatusCode = StatusCodes.Status403Forbidden,
                        Result = new Error { ErrorMessage = Global.ResponseMessages.GenerateInvalid("username or password") }
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        // [Route ("GetUser")]
        // [Authorize (Roles = "User, Admin")]
        // [HttpGet]
        // public async Task<IActionResult> GetUser () {
        //     var name = User.GetClaimValue ("Name");

        //     return Ok (new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "You are an authorized user" });
        // }

        // [Route ("GetAdmin")]
        // [Authorize (Roles = "Admin")]
        // [HttpGet]
        // public async Task<IActionResult> GetAdmin () {
        //     var name = User.GetClaimValue ("Name");

        //     return Ok (new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "You are an authorized user" });
        // }
        [Authorize(Roles = "Admin")]
        [HttpGet("AllUsers")]
        public async Task<IActionResult> AllUsers()
        {
            try
            {
                var users = Task.Run(() => _accountRepository.GetAllUsers());

                await Task.WhenAny(users);

                if (users != null)
                {
                    var usersDTO = _mapper.Map<IEnumerable<User>, List<UserDTO>>(await users);

                    return Ok(new CustomResponse<List<UserDTO>> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = usersDTO });
                }
                else
                {
                    return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = "You are an authorized user" });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromBody] UserCreateDTO userCreateDTO)
        {
            try
            {

                if (userCreateDTO != null)
                {

                    User toAdd = _mapper.Map<User>(userCreateDTO);

                    _accountRepository.CreateUser(toAdd);

                    UserDTO userDTO = _mapper.Map<UserDTO>(toAdd);

                    return Ok(new CustomResponse<UserDTO> { Message = Global.ResponseMessages.Success, StatusCode = StatusCodes.Status200OK, Result = userDTO });
                }
                else
                {
                    return Ok(new CustomResponse<string> { Message = Global.ResponseMessages.BadRequest, StatusCode = StatusCodes.Status400BadRequest, Result = "Some thing wrong with your Request." });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(Error.LogError(ex));
            }
        }
    }
}