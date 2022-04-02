using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs.user;
using Core.Entities;

namespace EFCore.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);

        // Task<List<User>> GetUsers();
        Task<List<UserDto>> GetUsers();

        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<User> UpdateUser(User user);
        Task DeleteUser(int id);
    }
}