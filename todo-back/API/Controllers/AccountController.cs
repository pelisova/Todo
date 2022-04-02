using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Messages;
using BusinessLayer.Services;
using Core.DTOs.user;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisteredUserDto>> RegisterUser(CreateUserDto createUserDto)
        {
            try
            {
                var user = await _userService.CreateUser(createUserDto);
                return (user == null) ? NotFound() : Ok(new ResponseMessageModel<RegisteredUserDto>("Account is successfully created!", user));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoggedInUserDto>> LoginUser(LoginUserDto loginUserDto)
        {
            try
            {
                var user = await _userService.LoginUser(loginUserDto);
                return (user == null) ? NotFound() : Ok(new ResponseMessageModel<LoggedInUserDto>("You are successfully logged in.", user));
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.InnerException?.Message + ". " + ex.Message);
            }

        }
    }
}