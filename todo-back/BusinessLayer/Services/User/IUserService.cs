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
        Task<RegisteredUserDto> CreateUser(CreateUserDto createUserDto);

        Task<LoggedInUserDto> LoginUser(LoginUserDto loginUserDto);
        Task<List<UserDto>> GetUsers();
        Task<UserDto> GetUserById(int id);
        Task<LoggedInUserDto> GetUserByEmail(string email);
        Task<UserDto> UpdateUser(int id, UpdateUserDto updateUserDto);
        Task DeleteUser(int id);
    }
}