using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Services;
using Core.DTOs;
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

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto createUserDto)
        {
            var user = await _userService.CreateUser(createUserDto);

            return (user == null) ? NotFound() : Created("User is successfully created", user);
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            // return Ok(users);
            return (!users.Any()) ? NotFound("Users are not found!") : Ok(users);
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id) 
        {
            var user = await _userService.GetUserById(id);

            return (user == null) ? NotFound("User is not found!") : Ok(user);
        }

        // TODO
        [HttpPatch("{id}")]
        public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UpdateUserDto updateUserDto)
        {
            if(id != updateUserDto.Id) return BadRequest("Invalid user to update!");

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