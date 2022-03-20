using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult<UserDto>> RegisterUser(CreateUserDto createUserDto)
        {
            try
            {
                var user = await _userService.CreateUser(createUserDto);
                return (user == null) ? NotFound() : Created("User is successfully created", user);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<RegisteredUserDto>> LoginUser(LoginUserDto loginUserDto)
        {
            try
            {
                var user = await _userService.LoginUser(loginUserDto);
                return (user == null) ? NotFound() : Ok(user);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}