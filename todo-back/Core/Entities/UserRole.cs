using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class UserRole : IdentityUserRole<int>
    {
        public User? user { get; set; }
        public Role? role { get; set; }
    }
}