using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.DTOs;
using Core.Entities;

namespace BusinessLayer.Mappings
{
    public class MappingUsers : Profile
    {
        public MappingUsers()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}