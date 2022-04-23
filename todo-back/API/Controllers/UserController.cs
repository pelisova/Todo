using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Extensions;
using BusinessLayer.Services;
using Core.DTOs;
using Core.DTOs.user;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return (!users.Any()) ? NotFound("Users are not found!") : Ok(users);
        }

        [Authorize(Roles = "Member, Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            return (user == null) ? NotFound("User is not found!") : Ok(user);
        }

        [Authorize]
        [HttpGet("email")]
        public async Task<ActionResult<LoggedInUserDto>> GetUserByEmail()
        {
            try
            {
                var email = User.GetUserEmail();
                var user = await _userService.GetUserByEmail(email);
                return Ok(user);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // TODO
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            if (id != updateUserDto.Id) return BadRequest("Invalid user to update!");

            var newUser = await _userService.UpdateUser(id, updateUserDto);

            return (newUser == null) ? NotFound("Oops! User is not found!") : Created("User is successfully updated", newUser);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<UserDto>>> DeleteUser(int id)
        {
            try
            {
                var users = await _userService.DeleteUser(id);
                return (!users.Any()) ? NotFound("Users are not found!") : Ok(users);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



    }
}