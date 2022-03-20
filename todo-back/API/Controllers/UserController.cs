using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            // return Ok(users);
            return (!users.Any()) ? NotFound("Users are not found!") : Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Member, Admin")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            return (user == null) ? NotFound("User is not found!") : Ok(user);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok("User is successfully deleted");
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



    }
}