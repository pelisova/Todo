using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Services.Token;
using Core.DTOs.user;
using Core.Entities;
using EFCore.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(IMapper mapper, IUserRepository userRepository, ITokenService tokenService, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
            this._tokenService = tokenService;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<RegisteredUserDto> CreateUser(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded) throw new Exception(result.ToString());

            var resultRole = await _userManager.AddToRoleAsync(user, "Member");

            if (!resultRole.Succeeded) throw new Exception(result.ToString());

            return new RegisteredUserDto
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public async Task<LoggedInUserDto> LoginUser(LoginUserDto loginUserDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == loginUserDto.Email);
            if (user == null) throw new Exception("Invalid email!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginUserDto.Password, false);
            if (!result.Succeeded) throw new Exception("Invalid email!", new Exception(result.ToString()));

            var roles = await _userManager.GetRolesAsync(user);

            return new LoggedInUserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Roles = roles,
                Token = await _tokenService.CreateToken(user),
            };
        }

        public async Task<List<UserDto>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }

        public async Task<UserDto> GetUserById(int id)
        {
            return _mapper.Map<UserDto>(await _userRepository.GetUserById(id));
        }

        public async Task<UserDto> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetUserById(id);
            var userToUpdate = _mapper.Map<UpdateUserDto, User>(updateUserDto, user);
            return _mapper.Map<UserDto>(await _userRepository.UpdateUser(userToUpdate));
        }

        public async Task<List<UserDto>> DeleteUser(int id)
        {

            try
            {
               return await _userRepository.DeleteUser(id);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        public async Task<LoggedInUserDto> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null) throw new Exception("Authentication failed");

            var roles = await _userManager.GetRolesAsync(user);

            return new LoggedInUserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Roles = roles,
                Token = await _tokenService.CreateToken(user),
            };
        }
    }
}