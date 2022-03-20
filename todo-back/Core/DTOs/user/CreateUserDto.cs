using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.DTOs.user
{
    public class CreateUserDto
    {
        [Required]
        public string? FirstName { get; set; }
        
        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}