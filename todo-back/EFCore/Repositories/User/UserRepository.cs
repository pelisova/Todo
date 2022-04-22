using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs.user;
using Core.Entities;
using EFCore.Context;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<List<UserDto>> GetUsers()
        {
            return await _mapper.ProjectTo<UserDto>(_context.Users.Where(user => user.FirstName != "admin")).ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.Include(t => t.Tasks).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User is not found!");
            }

        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}