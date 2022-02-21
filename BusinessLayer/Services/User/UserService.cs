using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using EFCore.Repositories;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
        }

        public async Task<UserDto> CreateUser(CreateUserDto createUserDto)
        {
            return _mapper.Map<UserDto>(await _userRepository.CreateUser(_mapper.Map<User>(createUserDto)));
        }

        public async Task<List<UserDto>> GetUsers()
        {
            //  return _mapper.Map<List<UserDto>>(await _userRepository.GetUsers());
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

        public async Task DeleteUser(int id)
        {
           
            try
            {
                await _userRepository.DeleteUser(id);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
        }

    }
}