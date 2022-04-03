using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class User : IdentityUser<int>
    {
        // doesn't need to be declared because IdentityUser table implement Id
        // public int UserId { get; set; } 

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        // similar to Id, email is also provided by IdentityUSer
        // [Required]
        // [EmailAddress]
        // public string? Email { get; set; }
        public List<TodoTask>? Tasks { get; set; }
        public ICollection<UserRole>? UserRoles { get; set; }


    }
}