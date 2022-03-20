using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs;
using Core.DTOs.user;

namespace BusinessLayer.Services
{
    public interface IUserService
    {
        Task<UserDto> CreateUser(CreateUserDto createUserDto);

        Task<RegisteredUserDto> LoginUser(LoginUserDto loginUserDto);
        Task<List<UserDto>> GetUsers();
        Task<UserDto> GetUserById(int id);
        Task<UserDto> UpdateUser(int id, UpdateUserDto updateUserDto);
        Task DeleteUser(int id);
    }
}