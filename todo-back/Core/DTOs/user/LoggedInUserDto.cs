using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.DTOs.user
{
    public class LoggedInUserDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public IList<string>? Roles { get; set; }
        public string? Token { get; set; }
    }
}