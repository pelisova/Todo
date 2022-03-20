using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.DTOs.task;
using Core.Entities;

namespace Core.DTOs.user
{
    public class UserDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<TaskDto>? Tasks { get; set; }
    }
}