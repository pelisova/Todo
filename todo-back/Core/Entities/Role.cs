using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}